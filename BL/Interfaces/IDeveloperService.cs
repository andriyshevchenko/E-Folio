using System.Collections.Generic;
using eFolio.DTO;
using eFolio.DTO.Common;

namespace eFolio.BL
{
    public interface IDeveloperService
    {
        void Add(Developer item);
        void Update(Developer item);
        void Delete(int id);
        Developer GetItem(int id);
        IEnumerable<Developer> GetItemsList();
        IEnumerable<Developer> Search(string request, Paging paging);
    }
}