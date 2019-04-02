using System.Collections.Generic;
using System.Threading.Tasks;
using eFolio.DTO;
using eFolio.DTO.Common;

namespace eFolio.BL
{
    public interface IDeveloperService
    {
        void Add(Developer item);
        void Update(Developer item);
        void Delete(int id);
        Task<Developer> GetItemAsync(int id);
        Task<IEnumerable<Developer>> GetItemsListAsync();
        Task<IEnumerable<Developer>> SearchAsync(string request, Paging paging);
    }
}