using System.Collections.Generic;
using eFolio.DTO;

namespace eFolio.BL
{
    public interface IProjectService
    {
        void Add(Project item);
        void Update(Project item);
        void Delete(int id);
        Project GetItem(int id);
        IEnumerable<Project> GetItemsList();
        IEnumerable<Project> Search(string request);
    }
}