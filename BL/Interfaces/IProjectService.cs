using System.Collections.Generic;
using eFolio.DTO;

namespace eFolio.BL
{
    public interface IProjectService
    {
        void Add(Project item);
        void Update(Project item);
        void UpdateDetails(int project, Context context);
        void DeleteScreeenshots(int project, int[] deleted); 
        void UpdateScreenshots(int project, Dictionary<int, FolioFile> files);
        void Delete(int id);
        Project GetItem(int id, params string[] extended);
        IEnumerable<Project> GetItemsList();
        IEnumerable<Project> Search(string request, Paging paging);
    }
}