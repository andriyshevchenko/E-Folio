using System.Collections.Generic;
using eFolio.DTO;

namespace eFolio.BL
{
    public interface IRepositoryService
    {
        Project GetItem(int id);
        IEnumerable<Project> GetItemsList();
        IEnumerable<Project> Search(string request);
    }
}