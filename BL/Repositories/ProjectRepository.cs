using eFolio.EF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

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
                .Include(project => project.Context)
                .ThenInclude(context => context.ScreenLinks)
                .ToList(); 

            var developers = db.Set<ProjectDeveloperEntity>()
                .Include(pde => pde.DeveloperEntity)
                .GroupBy(pde => pde.ProjectId)
                .ToDictionary(pde => pde.Key);
             
            foreach (var item in list)
            {
                if (developers.ContainsKey(item.Id))
                {
                    item.Developers = developers[item.Id].ToList();
                }
            }

            return list;
        }

        public void Update(ProjectEntity item)
        {
            db.Update(item);
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
