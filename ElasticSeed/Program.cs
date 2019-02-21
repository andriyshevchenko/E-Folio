using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Elasticsearch.Net;
using Nest;

namespace ElasticSeed
{
    /// <summary>
    /// For xml conversion only.
    /// </summary>
    [Serializable]
    public class XmlProject
    {
       
        public XmlProject()
        {

        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string InternalDescription { get; set; }
        public string ExternalDescription { get; set; }
    }
    
    public class Program
    {
        private static List<T> ConvertFromXml<T>(string path)
        {
            List<T> items;
            if (File.Exists(path))
            {
                XmlSerializer formatter = new XmlSerializer(typeof(T[]));
                using (FileStream fs = new FileStream(path, FileMode.Open))
                {
                    items = new List<T>((T[])formatter.Deserialize(fs));
                }
            }
            else
            {
                items = new List<T>();
            }

            return items;
        }

        static void Main(string[] args)
        {
          /*  Console.WriteLine("Paste working directory");
            string directory = Console.ReadLine();
            if (string.IsNullOrEmpty(directory))
            {
                directory = Environment.CurrentDirectory;
            }
            var projects = ConvertFromXml<XmlProject>(
                 Path.Combine(directory, "seed items", "seed_projects.xml")
             ); 
            XmlProject tproj = new XmlProject
            {
                Id = 0,
                Name = "projects",
                InternalDescription = "int descr",
                ExternalDescription = "ext descr"

            }; */
           // ElasticClient client = new ElasticClient();
            var Project1 = new XmlProject { Id = 1, ExternalDescription = "nferj", InternalDescription = "fne", Name = "project2    " };
            var settings = new ConnectionSettings(new Uri("http://localhost:9200")).DefaultIndex("xmlproject");

            var client = new ElasticClient(settings);
            var i1 = client.IndexDocument(Project1);
           // for (int j=0; j<projects.Count; j++)
            //{
           //     i1 = client.IndexDocument(projects[j]);
          //  }

            var SR1 = new SearchRequest<XmlProject> { Query = new MatchAllQuery() };
            var SR2 = client.Search<XmlProject>(SR1);



            var searchResponse = client.Search<XmlProject>(s => s
                      .Query(q => q
                             .Match(m => m
                                 .Field(f => f.InternalDescription).Query("fne")
                         )

                                       ) 

                );
            foreach (var hit in SR2.Hits) {
                Console.WriteLine(hit.Source.ExternalDescription);
            }

            var result = SR2.Documents;
            var highlights = searchResponse.HitsMetadata.Hits.Select(h => h
                 .Highlights);
            Console.WriteLine(result);
            Console.WriteLine(SR2.ToString());
            Console.ReadKey();
        }
    }
}
