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
            Console.WriteLine("Paste working directory");
            string directory = Console.ReadLine();
            if (string.IsNullOrEmpty(directory))
            {
                directory = Environment.CurrentDirectory;
            }
            var projects = ConvertFromXml<XmlProject>(
                 Path.Combine(directory, "seed items", "seed_projects.xml")
             );

           
        }
    }
}
