using e_folio.core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;

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
            throw new NotImplementedException();
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
