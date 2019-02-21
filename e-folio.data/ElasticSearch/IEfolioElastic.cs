using System;
using System.Collections.Generic;
using System.Text;

namespace eFolio.Elastic
{
    public interface IEfolioElastic
    {
        void AddItem(ElasticProjectData item);
        void AddItem(string path);
        List<ElasticProjectData> SearchItems(string searchString);
        void DeleteItem(int _Id);
        ElasticProjectData GetItemById(int _Id);
        void Update(ElasticProjectData InsertData);
    }
}
