using e_folio.core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace e_folio.data
{
    public class ProjectRepository : IRepository<ProjectEntity>
    {
        private eFolioDBContext db;

        public ProjectRepository(string connectionString)
        {
            var optionsBuilder = new DbContextOptionsBuilder<eFolioDBContext>();
            
            var options = optionsBuilder.UseSqlServer(connectionString).Options;

            this.db = new eFolioDBContext(options);
        }

        public void Add(ProjectEntity item)
        {
            db.Projects.Add(item);

            db.SaveChanges();
        }

        public void Delete(int id)  
        {
            ProjectEntity project = db.Projects.Find(id);
            db.Projects.Remove(project);

            db.SaveChanges();
        }

        public ProjectEntity GetItem(int id)
        {
            ProjectEntity project = db.Projects.Find(id);

            return project;
        }

        public IEnumerable<ProjectEntity> GetItemsList()
        {
            return db.Projects.ToListAsync().Result;
        }

        public IEnumerable<ProjectEntity> Search(string request)
        {
            ElasticSearch.ElasticSearch elasticSearch = new ElasticSearch.ElasticSearch();
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

            return projects;
        }

        public void Update(ProjectEntity item)
        {
            db.Projects.Update(item);

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
    }
}
