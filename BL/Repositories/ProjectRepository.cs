using e_folio.data;
using eFolio.EF;
using eFolio.Elastic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;


namespace eFolio.BL
{
    public class ProjectRepository : IRepository<Project>
    {
        private eFolioDBContext db;
        private ElasticSearch es;

        public ProjectRepository(eFolioDBContext eFolioDBContext)
        {
            this.db = eFolioDBContext;
            this.es = new ElasticSearch();
        }

        public void Add(Project item)
        {
            ElasticProjectData elasticProjectData = new ElasticProjectData();
            elasticProjectData.Id = item.Id;
            elasticProjectData.Name = item.Name;
            elasticProjectData.InternalDescr = item.InternalDescription;
            elasticProjectData.ExternalDescr = item.ExternalDescription;


            //db.Projects.Add((ProjectEntity)item);
            es.AddItem(elasticProjectData);

            db.SaveChanges();
        }

        public void Delete(int id)  
        {
            ProjectEntity project = db.Projects.Find(id);
            db.Projects.Remove(project);
            es.DeleteItem(id);

            db.SaveChanges();
        }

        public Project GetItem(int id)
        {
            ProjectEntity projectEntity = db.Projects.Find(id);

            Project project = new Project();
            project.ExternalDescription = es.GetItemById(id).ExternalDescr;
            project.InternalDescription = es.GetItemById(id).InternalDescr;
            project.Id = projectEntity.Id;
            project.Name = projectEntity.Name;
            //project.Developers = projectEntity.Developers;
            //project.Context = projectEntity.Context;

            return project;
        }

        public IEnumerable<Project> GetItemsList()
        {
            List<ProjectEntity> list = db.Projects.ToListAsync().Result;
            foreach (var item in list)
            {
                yield return new Project()
                {
                    Id = item.Id,
                    Name = item.Name,
                    //Context = item.Context,
                    //Developers = item.Developers,
                    ExternalDescription = GetItem(item.Id).ExternalDescription,
                    InternalDescription = GetItem(item.Id).InternalDescription
                };
            }
        }

        public IEnumerable<ProjectEntity> Search(string request)
        {
            ElasticSearch elasticSearch = new ElasticSearch();
            var response = elasticSearch.SearchItems(request);

            List<Project> projects = new List<Project>();
            for (int i = 0; i < response.Count; i++)
            {
                Project project = new Project();
                var projectEntity = GetItem(response[i].Id);
                project.Name = projectEntity.Name;
                project.Developers = projectEntity.Developers;
                project.Context = projectEntity.Context;
                project.ExternalDescription = response[i].ExternalDescr;
                project.InternalDescription = response[i].InternalDescr;

                projects.Add(project);
            }

            return null;//
        }

        public void Update(Project item)
        {
            //db.Projects.Update((ProjectEntity)item);

            //update in es

            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        IEnumerable<Project> IRepository<Project>.Search(string request)
        {
            throw new NotImplementedException();
        }
    }
}
