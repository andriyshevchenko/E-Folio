using System;
using System.Collections.Generic;
using System.Text;

namespace e_folio.data.ElasticSearch
{
    public interface IEfolioElastic
    {
        void AddItem(ElasticProjectData item);
        void AddItem(string path);
        List<ElasticProjectData> SearchItems(string searchString);
    }
}
