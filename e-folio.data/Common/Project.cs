using System.Collections.Generic;
using e_folio.core.Entities;

namespace e_folio.data
{
    public class Project : ProjectEntity
    {
        public string InternalDescription { get; set; }
        public string ExternalDescription { get; set; }
    }
}