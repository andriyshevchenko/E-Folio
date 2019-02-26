using eFolio.EF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace eFolio.BL
{
    public class DeveloperRepository : IRepository<DeveloperEntity>
    {
        private eFolioDBContext db;

        public DeveloperRepository(eFolioDBContext eFolioDBContext)
        {
            this.db = eFolioDBContext;
        }

        public void Add(DeveloperEntity item)
        {
            db.Developers.Add(item);

            db.SaveChanges();
        }

        public void Delete(int id)
        {
            DeveloperEntity developer = db.Developers.Find(id);

            db.Developers.Remove(developer);

            db.SaveChanges();
        }

        public IEnumerable<DeveloperEntity> GetItemsList()
        {
            return db.Developers.ToListAsync().Result;
        }

        public DeveloperEntity GetItem(int id)
        {
            var developerEntity = db.Developers.Find(id);

            return developerEntity;
        }

        public void Update(DeveloperEntity item)
        {
            db.Developers.Update(item);

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
