using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace elastic_test
{
    class ElasticClient
    {
        private static readonly HttpClient client = new HttpClient();
        private const string BaseAdress = "http://localhost:9200/";
        public static async Task ShowIndexes()
        {
            var stringTask = client.GetStringAsync($"{BaseAdress}_cat/indices?v");
            var msg = await stringTask;
            Console.Write(msg);
        }
    }
    
}
