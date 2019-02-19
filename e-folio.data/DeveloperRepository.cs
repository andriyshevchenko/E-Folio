using eFolio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace e_folio.data
{
    public class DeveloperRepository : IRepository<Developer>
    {
        private eFolioDBContext db;

        public DeveloperRepository()
        {
            var optionsBuilder = new DbContextOptionsBuilder<eFolioDBContext>();

            var options = optionsBuilder.UseSqlServer(@"Data source=.\SQLEXPRESS;Initial Catalog=eFolio;User Id = sa; Password = intel123").Options;

            this.db = new eFolioDBContext(options);
        }

        public void Create(Developer item)
        {
            db.Developers.Add(item);
        }

        public void Delete(int id)
        {
            Developer developer = db.Developers.Find(id);

            db.Developers.Remove(developer);
        }

        public Developer GetItem(int id)
        {
            Developer developer = db.Developers.Find(id);

            return developer;
        }

        public IEnumerable<Developer> GetItemsList()
        {
            return db.Developers.ToListAsync().Result;
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public IEnumerable<Developer> Search(string request)
        {
            throw new NotImplementedException();
        }

        public void Update(Developer item)
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
