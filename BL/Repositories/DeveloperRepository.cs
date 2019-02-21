
using eFolio.EF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace eFolio.BL
{
    public class DeveloperRepository : IRepository<DeveloperEntity>
    {
        private eFolioDBContext db;

        public DeveloperRepository(string connectionString)
        {
            //var optionsBuilder = new DbContextOptionsBuilder<eFolioDBContext>();

            //var options = optionsBuilder.UseSqlServer(connectionString).Options;

            //this.db = new eFolioDBContext(options);
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

        public DeveloperEntity GetItem(int id)
        {
            DeveloperEntity developer = db.Developers.Find(id);

            return developer;
        }

        public IEnumerable<DeveloperEntity> GetItemsList()
        {
            return db.Developers.ToListAsync().Result;
        }

        public IEnumerable<DeveloperEntity> Search(string request)
        {
            throw new NotImplementedException();
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
