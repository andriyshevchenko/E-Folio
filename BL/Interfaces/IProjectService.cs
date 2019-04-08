using System.Collections.Generic;
using eFolio.DTO;
using eFolio.DTO.Common;

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
        Project GetItem(int id, DescriptionKind isExtended, params string[] includedProperties);
        IEnumerable<Project> GetItemsList(DescriptionKind isExtended);
        IEnumerable<Project> Search(string request, Paging paging, DescriptionKind isExtended);
    }
}