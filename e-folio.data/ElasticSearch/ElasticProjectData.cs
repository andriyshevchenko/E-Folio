using System;
using System.Collections.Generic;
using System.Text;

namespace e_folio.data.ElasticSearch
{
    public class ElasticProjectData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string InternalDescr { get; set; }
        public string ExternalDescr { get; set; }
    }
}
