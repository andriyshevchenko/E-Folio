namespace eFolio.Elastic
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Xml.Serialization;
    using Nest;
    using eFolio.DTO;
    using System.Linq;
    using eFolio.DTO.Common;

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
            var resp = clientProject.IndexDocument(item);
        }

        public void AddItem(ElasticDeveloperData item) //works
        {
            clientDeveloper.IndexDocument(item);
        }

        public void AddItemProject(string path) //works
        {
            List<ElasticProjectData> items;
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

            for (int i = 0; i < items.Count; i++)
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
                clientDeveloper.IndexDocument(items[i]);
            }
        }

        public List<ElasticProjectData> SearchItemsProject(string tstring, Paging paging, DescriptionKind isExtended)
        {
            int from = paging.From;
            int size = paging.Size;
            var sr = clientProject.Search<ElasticProjectData>(s => s.
                From(from)
                .Size(size).
                Query(q => q
                    .Bool(b => b.
                       Should(m => m.
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
            returnlist = HitToDataConvertProject(sr, isExtended);
            return returnlist;
        }

        public List<ElasticDeveloperData> SearchItemsDeveloper(string query, Paging paging, CVKind isExtended)
        {
            int from = paging.From;
            int size = paging.Size;
            var sr = clientDeveloper.Search<ElasticDeveloperData>(
                s => s.From(from)
                      .Size(size)
                      .Query(
                          q => q.Bool(
                              b => b.Should(
                                  m => m.Match(
                                      mp => mp.Field(fg => fg.Name)
                                       .Query(query)
                                       .Fuzziness(Fuzziness.Auto)
                                  ),
                                  m => m.Match(
                                      mp => mp.Field(fg => fg.InternalCV)
                                       .Query(query)
                                       .Fuzziness(Fuzziness.Auto)
                                  )
                              )
                          )
                      )
                  );
            var returnlist = new List<ElasticProjectData>();

            List<ElasticDeveloperData> resultList = new List<ElasticDeveloperData>();
            resultList = HitToDataConvertDeveloper(sr, isExtended);
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

        public ElasticProjectData GetProjectById(int _Id, DescriptionKind isExtended) //works!
        {
            var searchResponse1 = clientProject.Search<ElasticProjectData>(s => s
                      .Query(q => q
                             .Match(m => m
                                 .Field(f => f.Id).Query(_Id.ToString())
                         )
                                )
                                        );
            if (searchResponse1.Hits.Count > 1)
            {
                throw new Exception("ElasticSearch: Many projects with same id found: " + _Id);
            }
            else if (searchResponse1.Hits.Count == 0)
            {
                return ElasticProjectData.NotFound;
            }
            return new ElasticProjectData(searchResponse1.Hits.First().Source, isExtended);
        }

        public ElasticDeveloperData GetDeveloperById(int id, CVKind isExtended) //works!
        {
            var searchResponse1 = clientDeveloper.Search<ElasticDeveloperData>(s => s
                      .Query(q => q
                             .Match(m => m
                                 .Field(f => f.Id).Query(id.ToString())
                         )
                                )
                                        ); 
            if (searchResponse1.Hits.Count > 1)
            {
                throw new Exception("ElasticSearch: Many developers with same id found: " + id);
            }
            else if (searchResponse1.Hits.Count == 0)
            {
                return ElasticDeveloperData.NotFound;
            }
            return new ElasticDeveloperData(searchResponse1.Hits.First().Source, isExtended);
        }

        private List<ElasticProjectData> HitToDataConvertProject(ISearchResponse<ElasticProjectData> searchResponse, DescriptionKind isExtended)
        {
            List<ElasticProjectData> resultList = new List<ElasticProjectData>();

            foreach (var hit in searchResponse.Hits)
            {
                resultList.Add(new ElasticProjectData(hit.Source, isExtended));
            }
            return resultList;
        }

        private List<ElasticDeveloperData> HitToDataConvertDeveloper(ISearchResponse<ElasticDeveloperData> searchResponse, CVKind isExtended)
        {
            List<ElasticDeveloperData> resultList = new List<ElasticDeveloperData>();
            foreach (var hit in searchResponse.Hits)
            {
                resultList.Add(new ElasticDeveloperData(hit.Source, isExtended));
            }
            return resultList;
        }

        public void UpdateProjectData(ElasticProjectData InsertData) //wokrs!
        {
            int InsertId = InsertData.Id;
            var updateResponse = clientProject.Update<ElasticProjectData, object>(InsertId, u => u.Doc(InsertData).RetryOnConflict(1));
            if (!updateResponse.IsValid && updateResponse.ApiCall.HttpStatusCode == 404)
            {
                //insert document
                var insertResponse = clientProject.IndexDocument(InsertData);
            }

        }
        public void UpdateDeveloperData(ElasticDeveloperData InsertData) //wokrs!
        {
            int InsertId = InsertData.Id;
            var updateResponse = clientDeveloper.Update<ElasticDeveloperData, object>(InsertId, u => u.Doc(InsertData).RetryOnConflict(1));
        }
    }
}
