using e_folio.core.Entities;
using System;
using System.Collections.Generic;

namespace e_folio.data
{
    interface IRepository : IDisposable
    {
        IEnumerable<Project> GetProjectList();
        IEnumerable<Project> Search(string request);
        Project GetProject(int id);
        void Create(Project project);
        void Update(Project project);
        void Delete(int id);
        void Save();
    }
}