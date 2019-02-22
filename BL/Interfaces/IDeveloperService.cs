using System.Collections.Generic;
using eFolio.DTO;

namespace eFolio.BL
{
    interface IDeveloperService
    {
        Developer GetItem(int id);
        IEnumerable<Developer> GetItemsList();
        IEnumerable<Developer> Search(string request);
    }
}