using e_folio.core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace e_folio.data
{
    public class DeveloperRepository : IRepository<DeveloperEntity>
    {
        private eFolioDBContext db;

        public DeveloperRepository()
        {
            var optionsBuilder = new DbContextOptionsBuilder<eFolioDBContext>();

            var options = optionsBuilder.UseSqlServer(@"Data source=.\SQLEXPRESS;Initial Catalog=eFolio;User Id = sa; Password = intel123").Options;

            this.db = new eFolioDBContext(options);
        }

        public void Create(DeveloperEntity item)
        {
            db.Developers.Add(item);
        }

        public void Delete(int id)
        {
            DeveloperEntity developer = db.Developers.Find(id);

            db.Developers.Remove(developer);
        }

        public DeveloperEntity GetItem(int id)
        {
            DeveloperEntity developer = db.Developers.Find(id);

            return developer;
        }

        public IEnumerable<DeveloperEntity> GetItemsList()
        {
            return db.Developers.ToListAsync().Result;
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public IEnumerable<DeveloperEntity> Search(string request)
        {
            throw new NotImplementedException();
        }

        public void Update(DeveloperEntity item)
        {
            db.Developers.Update(item);
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
