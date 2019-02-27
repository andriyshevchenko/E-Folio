using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Nest;
using eFolio.DTO;
namespace eFolio.Elastic
{
    public class ElasticSearch : IEfolioElastic
    {
        public ElasticClient clientProject;
        public ElasticClient clientDeveloper;

        private ConnectionSettings settingsProject = new ConnectionSettings(new Uri("http://localhost:9200")).DefaultIndex("elasticprojectdata");
        private ConnectionSettings settingsDeveloper = new ConnectionSettings(new Uri("http://localhost:9200")).DefaultIndex("elasticdeveloperdata");

        public ElasticSearch()
        {
            clientProject = new ElasticClient(settingsProject);
            clientDeveloper = new ElasticClient(settingsDeveloper);
        }



        public void AddItem(ElasticProjectData item) //works
        {
             clientProject.IndexDocument(item);
        }
        public void AddItem(ElasticDeveloperData item) //works
        {
            clientDeveloper.IndexDocument(item);
        }



        public  void AddItemProject(string path) //works
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
                clientProject.IndexDocument(items[i]);
            }
        }
        public void AddItemDeveloper(string path) //works
        {
            List<ElasticDeveloperData> items;
            if (File.Exists(path))
            {
                XmlSerializer formatter = new XmlSerializer(typeof(ElasticProjectData[]));
                using (FileStream fs = new FileStream(path, FileMode.Open))
                {
                    items = new List<ElasticDeveloperData>((ElasticDeveloperData[])formatter.Deserialize(fs));
                }
            }
            else
            {
                items = new List<ElasticDeveloperData>();
            }

            for (int i = 0; i < items.Count; i++)
            {
                clientProject.IndexDocument(items[i]);
            }
        }



        public List<ElasticProjectData> SearchItemsProject(string tstring, Paging paging)
        {
            int from = paging.From;
            int size = paging.Size;
            var sr = clientProject.Search<ElasticProjectData>(s => s.
                From(from)
                .Size(size).
                Query(q => q
                    .Bool(b => b.
                       Should( m => m.
                         Match(mp => mp
                            .Field(fg => fg.Name)
                            .Query(tstring)
                            .Fuzziness(Fuzziness.Auto)
                      ),

                        m => m.Match(mp => mp
                            .Field(fg => fg.InternalDescr)
                            .Query(tstring)
                            .Fuzziness(Fuzziness.Auto)
                      )

                      ))));
            var returnlist = new List<ElasticProjectData>();

            List<ElasticProjectData> resultList = new List<ElasticProjectData>();
            var temp = new ElasticProjectData();
            returnlist = HitToDataConvertProject(sr);
            return returnlist;
        }
        public  List<ElasticDeveloperData> SearchItemsDeveloper(string tstring, Paging paging)
        {
            int from = paging.From;
            int size = paging.Size;
            var sr = clientDeveloper.Search<ElasticDeveloperData>(s => s.
            From(from)
            .Size(size).
            Query(q => q.
                Bool(b => b.
                    Should(
                      m => m.Match(mp => mp
                    .Field(fg => fg.Name)
                    .Query(tstring)
                      .Fuzziness(Fuzziness.Auto)
                      ),

                      m => m.Match(mp => mp
                      .Field(fg => fg.InternalCV)
                      .Query(tstring)
                      .Fuzziness(Fuzziness.Auto)
                      )

                      ))));
            var returnlist = new List<ElasticProjectData>();

            List<ElasticDeveloperData> resultList = new List<ElasticDeveloperData>();
            resultList = HitToDataConvertDeveloper(sr);
            return resultList;
        }






        public void DeleteProjectItem(int _Id) //works!
        {
            clientProject.Delete<ElasticProjectData>(_Id);
        }

        public void DeleteDeveloperItem(int _Id) //works!
        {
            clientDeveloper.Delete<ElasticDeveloperData>(_Id);
        }




        public ElasticProjectData GetProjectById(int _Id) //works!
        {
            var searchResponse1 = clientProject.Search<ElasticProjectData>(s => s
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

        public ElasticDeveloperData GetDeveloperById(int _Id) //works!
        {
            var searchResponse1 = clientProject.Search<ElasticDeveloperData>(s => s
                      .Query(q => q
                             .Match(m => m
                                 .Field(f => f.Id).Query(_Id.ToString())
                         )
                                )
                                        );
            var temp = new ElasticDeveloperData();
            foreach (var hit in searchResponse1.Hits)
            {
                temp.Name = hit.Source.Name;
                temp.Id = hit.Source.Id;
                temp.InternalCV = hit.Source.InternalCV;
                temp.ExternalCV = hit.Source.ExternalCV;
            }
            return temp;
        }





        private List<ElasticProjectData> HitToDataConvertProject(ISearchResponse<ElasticProjectData> searchResponse)
        {
            List<ElasticProjectData> resultList = new List<ElasticProjectData>();
            
            foreach (var hit in searchResponse.Hits)
            {
                var temp = new ElasticProjectData();
                temp.Name = hit.Source.Name;
                temp.Id = hit.Source.Id;
                temp.InternalDescr = hit.Source.InternalDescr;
                temp.ExternalDescr = hit.Source.ExternalDescr;
                resultList.Add(temp);
            }
            return resultList;
        }
        private List<ElasticDeveloperData> HitToDataConvertDeveloper(ISearchResponse<ElasticDeveloperData> searchResponse)
        {
            List<ElasticDeveloperData> resultList = new List<ElasticDeveloperData>();
            foreach (var hit in searchResponse.Hits)
            {
                var temp = new ElasticDeveloperData();
                temp.Name = hit.Source.Name;
                temp.Id = hit.Source.Id;
                temp.InternalCV = hit.Source.InternalCV;
                temp.ExternalCV = hit.Source.ExternalCV;
                resultList.Add(temp);
            }
            return resultList;
        }





        public void UpdateProjectData(ElasticProjectData InsertData) //wokrs!
        {
            int InsertId = InsertData.Id;
            var updateResponse = clientProject.Update<ElasticProjectData, object>(InsertId, u => u.Doc(InsertData).RetryOnConflict(1));
        }
        public void UpdateDeveloperData(ElasticDeveloperData InsertData) //wokrs!
        {
            int InsertId = InsertData.Id;
            var updateResponse = clientDeveloper.Update<ElasticDeveloperData, object>(InsertId, u => u.Doc(InsertData).RetryOnConflict(1));
        }
        


    }
}
