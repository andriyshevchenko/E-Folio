using System.Collections.Generic;
using eFolio.DTO;

namespace eFolio.BL
{
    interface IRepositoryService
    {
        Project GetItem(int id);
        IEnumerable<Project> GetItemsList();
        IEnumerable<Project> Search(string request);
    }
}