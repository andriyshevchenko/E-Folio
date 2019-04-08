using eFolio.DTO;
using eFolio.DTO.Common;
using System.Collections.Generic;
namespace eFolio.Elastic
{
    public interface IEfolioElastic
    {

        void AddItem(ElasticProjectData item);
        void AddItem(ElasticDeveloperData item);


        void AddItemProject(string path);
        void AddItemDeveloper(string path);

        List<ElasticProjectData> SearchItemsProject(string searchString, Paging paging, DescriptionKind isExtended);
        List<ElasticDeveloperData> SearchItemsDeveloper(string tstring, Paging paging, CVKind isExtended);


        void DeleteProjectItem(int _Id);
        void DeleteDeveloperItem(int _Id);

        ElasticProjectData GetProjectById(int _Id, DescriptionKind isExtended);
        ElasticDeveloperData GetDeveloperById(int _Id, CVKind isExtended);

        void UpdateProjectData(ElasticProjectData InsertData);
        void UpdateDeveloperData(ElasticDeveloperData InsertData);

    }
}
