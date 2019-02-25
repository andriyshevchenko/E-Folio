using AutoMapper;
using eFolio.DTO;
using eFolio.EF;
using eFolio.Elastic;
using System;
using System.Collections.Generic;

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
            projectRepository.Add(mapper.Map<ProjectEntity>(item));
        }

        public void Delete(int id)
        {
            projectRepository.Delete(id);
        }

        public Project GetItem(int id)
        {
            var projectEntity = projectRepository.GetItem(id);
            var elasticProject = elastic.GetItemById(id);

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

        public IEnumerable<Project> Search(string request)
        {
            var elasticProjects = elastic.SearchItems(request);
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
            projectRepository.Update(mapper.Map<ProjectEntity>(item));
        }

        private IEnumerable<ElasticProjectData> GetElasticProjects(IEnumerable<ProjectEntity> projects)
        {
            foreach (var item in projects)
            {
                yield return elastic.GetItemById(item.Id);
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

            return project;
        }
    }
}
