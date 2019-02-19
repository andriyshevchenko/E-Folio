using System;
using System.Collections.Generic;
using System.Text;

namespace e_folio.core.Entities
{
    public class DeveloperEntity
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string CVLink { get; set; }
        public IList<ProjectEntity> Projects { get; set; }
    }
}
