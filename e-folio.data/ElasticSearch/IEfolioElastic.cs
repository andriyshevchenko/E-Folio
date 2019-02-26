using System;
using System.Collections.Generic;
using System.Text;
using eFolio.DTO;
namespace eFolio.Elastic
{
    public interface IEfolioElastic
    {

        void AddItem(ElasticProjectData item);
        void AddItem(ElasticDeveloperData item);


        void AddItemProject(string path);
        void AddItemDeveloper(string path);

        List<ElasticProjectData> SearchItemsProject(string searchString, Paging paging);
        List<ElasticDeveloperData> SearchItemsDeveloper(string tstring, Paging paging);


        void DeleteProjectItem(int _Id);
        void DeleteDeveloperItem(int _Id);

        ElasticProjectData GetProjectById(int _Id);
        ElasticDeveloperData GetDeveloperById(int _Id);

        void UpdateProjectData(ElasticProjectData InsertData);
        void UpdateDeveloperData(ElasticDeveloperData InsertData);

    }
}
