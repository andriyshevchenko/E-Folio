using e_folio.core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace e_folio.data
{
    public class ProjectRepository : IRepository<ProjectEntity>
    {
        private eFolioDBContext db;

        public ProjectRepository()
        {
            var optionsBuilder = new DbContextOptionsBuilder<eFolioDBContext>();

            var options = optionsBuilder.UseSqlServer(@"Data source=.\SQLEXPRESS;Initial Catalog=eFolio;User Id = sa; Password = intel123").Options;

            this.db = new eFolioDBContext(options);
        }

        public void Create(ProjectEntity item)
        {
            db.Projects.Add(item);
        }

        public void Delete(int id)
        {
            ProjectEntity project = db.Projects.Find(id);

            db.Projects.Remove(project);
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

        public void Save()
        {
            db.SaveChanges();
        }

        public IEnumerable<ProjectEntity> Search(string request)
        {
            throw new NotImplementedException();
        }

        public void Update(ProjectEntity item)
        {
            db.Projects.Update(item);
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
