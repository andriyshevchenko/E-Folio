using AutoMapper;
using eFolio.DTO;
using eFolio.DTO.Common;
using eFolio.EF;
using eFolio.Elastic;
using System;
using System.Collections.Generic;
using System.IO;

namespace eFolio.BL
{
    public class ProjectService : IProjectService
    {
        private readonly IMapper mapper;
        private ProjectRepository projectRepository;
        private ElasticSearch elastic;

        public ProjectService(eFolioDBContext DBContext, IMapper mapper)
        {
            projectRepository = new ProjectRepository(DBContext);
            elastic = new ElasticSearch();
            this.mapper = mapper;
        }

        public void Add(Project item)
        {
            ProjectEntity pe = mapper.Map<ProjectEntity>(item);
            projectRepository.Add(pe);

            item.UpdateId(pe.Id);
            ElasticProjectData epd = mapper.Map<ElasticProjectData>(item);
            elastic.AddItem(epd);
        }

        public void Delete(int id)
        {
            projectRepository.Delete(id);

            elastic.DeleteProjectItem(id);
        }

        public Project GetItem(int id, params string[] extended)
        {
            var projectEntity = projectRepository.GetItem(id, extended);
            var elasticProject = elastic.GetProjectById(id);

            return GetMergeProject(projectEntity, elasticProject);
        }

        public IEnumerable<Project> GetItemsList()
        {
            var projectEntities = projectRepository.GetItemsList();
            var elasticProjects = GetElasticProjects(projectEntities);

            var e1 = projectEntities.GetEnumerator();
            var e2 = elasticProjects.GetEnumerator();
            while (e1.MoveNext() && e2.MoveNext())
            {
                yield return GetMergeProject(e1.Current, e2.Current);
            }
        }

        public IEnumerable<Project> Search(string request, Paging paging)
        {
            var elasticProjects = elastic.SearchItemsProject(request, paging);
            var projectEntities = GetEntityProjects(elasticProjects);

            var e1 = projectEntities.GetEnumerator();
            var e2 = elasticProjects.GetEnumerator();
            while (e1.MoveNext() && e2.MoveNext())
            {
                yield return GetMergeProject(e1.Current, e2.Current);
            }
        }

        public void Update(Project item)
        {
            ProjectEntity oldProjectEntity = projectRepository.GetItem(item.Id);

            if (oldProjectEntity == null)
            {
                return;
            }

            ProjectEntity projectEntity = mapper.Map<Project, ProjectEntity>(
                item, oldProjectEntity, options => options
                .ConfigureMap()
                .ForMember(proj => proj.Context, mo => mo.Ignore())
            );

            projectRepository.Update(projectEntity);

            elastic.UpdateProjectData(mapper.Map<ElasticProjectData>(item));
        }

        public void UpdateDetails(int project, Context context)
        {
            ProjectEntity oldProjectEntity = projectRepository.GetItem(project);
            if (oldProjectEntity == null)
            {
                return;
            }

            oldProjectEntity.Context.Update(context);
            
            projectRepository.Update(oldProjectEntity);
        }

        /// <summary>
        /// Updates existing screenshots, or adds new.
        /// </summary>
        /// <param name="project"></param>
        /// <param name="files"></param>
        public void UpdateScreenshots(int project, Dictionary<int, FolioFile> files)
        {
            ProjectEntity oldProjectEntity = projectRepository.GetItem(project);
            if (oldProjectEntity == null)
            {
                return;
            }

            List<int> ids = oldProjectEntity.Context.ScreenLinkIds();
            foreach (var item in files)
            {
                var where = ids.BinarySearch(item.Key);

                if (where > -1)
                {
                    oldProjectEntity.Context.ScreenLinks[where].Update(item.Value);
                }
                else
                {
                    oldProjectEntity.Context.ScreenLinks.Add(
                        new FolioFileEntity(item.Value, oldProjectEntity.ContextId)
                    );
                }
            }

            projectRepository.Update(oldProjectEntity);
        }

        public void DeleteScreeenshots(int project, int[] deleted)
        {
            ProjectEntity oldProjectEntity = projectRepository.GetItem(project);
            if (oldProjectEntity == null)
            {
                return;
            }

            List<int> ids = oldProjectEntity.Context.ScreenLinkIds();
            foreach (var id in deleted)
            {
                var where = ids.BinarySearch(id);
                if (where > -1)
                {
                    oldProjectEntity.Context.ScreenLinks.RemoveAt(where);
                }
            }
            projectRepository.Update(oldProjectEntity);
        }

        private IEnumerable<ElasticProjectData> GetElasticProjects(IEnumerable<ProjectEntity> projects)
        {
            foreach (var item in projects)
            {
                yield return elastic.GetProjectById(item.Id);
            }
        }

        private IEnumerable<ProjectEntity> GetEntityProjects(IEnumerable<ElasticProjectData> projects)
        {
            foreach (var item in projects)
            {
                yield return projectRepository.GetItem(item.Id);
            }
        }

        private Project GetMergeProject(ProjectEntity projectEntity, ElasticProjectData elasticProjectData)
        {
            var project = mapper.Map<Project>(Tuple.Create(elasticProjectData, projectEntity));
            project.HasPhoto(
                File.ReadAllBytes(projectEntity.PhotoLink), 
                Path.GetExtension(projectEntity.PhotoLink)
            );
            return project;
        }
    }
}
