using eFolio.DTO;
using eFolio.EF;
using eFolio.Elastic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;


namespace eFolio.BL
{
    public class ProjectRepository : IRepository<ProjectEntity>
    {
        private eFolioDBContext db;

        public ProjectRepository(eFolioDBContext eFolioDBContext)
        {
            this.db = eFolioDBContext;
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
            var projectEntity = db.Projects
                .Include(item => item.Developers)
                .Include(item => item.Context)
                .ThenInclude(context => context.ScreenLinks)
                .SingleOrDefault(item => item.Id == id);

            return projectEntity;
        }

        public IEnumerable<ProjectEntity> GetItemsList()
        {
            List<ProjectEntity> list = db.Projects
                .Include(project => project.Developers)
                .Include(project => project.Context)
                .ThenInclude(context => context.ScreenLinks)
                .ToListAsync().Result;

            return list;
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
