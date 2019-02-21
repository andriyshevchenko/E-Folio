using System.Collections.Generic;

namespace eFolio.Elastic
{
    public interface IEfolioElastic
    {
        void AddItem(ElasticProjectData item);
        void AddItem(string path);
        List<ElasticProjectData> SearchItems(string searchString);
    }
}