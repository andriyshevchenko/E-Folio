using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Nest;
namespace e_folio.data.ElasticSearch
{
    public class ElasticSearch : IEfolioElastic
    {
        public ElasticClient client;
        public  ConnectionSettings settings = new ConnectionSettings(new Uri("http://localhost:9200")).DefaultIndex("ElasticProjectData");
        public ElasticSearch()
        {
            client = new ElasticClient(settings);
        }
        public void AddItem(ElasticProjectData item)
        {
             client.IndexDocument(item);
        }
        public  void AddItem(string path)
        {
            List <ElasticProjectData> items;
            if (File.Exists(path))
            {
                XmlSerializer formatter = new XmlSerializer(typeof(ElasticProjectData[]));
                using (FileStream fs = new FileStream(path, FileMode.Open))
                {
                    items = new List<ElasticProjectData>((ElasticProjectData[])formatter.Deserialize(fs));
                }
            }
            else
            {
                  items = new List<ElasticProjectData>();
            }

            for (int i=0; i<items.Count; i++)
            {
                client.IndexDocument(items[i]);
            }
        }
        public List<ElasticProjectData> SearchItems(string searchString)
        {
            var searchResponse1 = client.Search<ElasticProjectData>(s => s
                      .Query(q => q
                             .Match(m => m
                                 .Field(f => f.Name).Query(searchString)
                         )
                                )
                                        );
            var searchResponse2 = client.Search<ElasticProjectData>(s => s
                      .Query(q => q
                             .Match(m => m
                                 .Field(f => f.InternalDescr).Query(searchString)
                         )
                                )
                                        );
            var resultSearchlist = new List<ElasticProjectData>();
            resultSearchlist = HitToDataConvert(searchResponse1);
            resultSearchlist.AddRange( HitToDataConvert(searchResponse1));


            return resultSearchlist;
        }

        public void DeleteItem(int _Id)
        {
            client.Delete<ElasticProjectData>(_Id);
        }
        public ElasticProjectData GetItemById(int _Id)
        {
            var searchResponse1 = client.Search<ElasticProjectData>(s => s
                      .Query(q => q
                             .Match(m => m
                                 .Field(f => f.Id).Query(_Id.ToString())
                         )
                                )
                                        );
            var temp = new ElasticProjectData();
            foreach (var hit in searchResponse1.Hits)
            {
                temp.Name = hit.Source.Name;
                temp.Id = hit.Source.Id;
                temp.InternalDescr = hit.Source.InternalDescr;
                temp.ExternalDescr = hit.Source.ExternalDescr;
            }
            return temp;
        }
        private List<ElasticProjectData> HitToDataConvert(ISearchResponse<ElasticProjectData> searchResponse)
        {
            List<ElasticProjectData> resultList = new List<ElasticProjectData>();
            var temp = new ElasticProjectData();
            foreach (var hit in searchResponse.Hits)
            {
                temp.Name = hit.Source.Name;
                temp.Id = hit.Source.Id;
                temp.InternalDescr = hit.Source.InternalDescr;
                temp.ExternalDescr = hit.Source.ExternalDescr;
                resultList.Add(temp);
            }
            return resultList;
        }
        
    }
}
