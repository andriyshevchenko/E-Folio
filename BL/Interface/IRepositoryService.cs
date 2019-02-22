using System.Collections.Generic;
using e_folio.data;

namespace eFolio.BL
{
    interface IRepositoryService
    {
        Project GetItem(int id);
        IEnumerable<Project> GetItemsList();
        IEnumerable<Project> Search(string request);
    }
}