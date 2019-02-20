using System;
using System.Collections.Generic;
using System.Text;

namespace e_folio.core.Entities
{
    public class ProjectDeveloperEntity
    {
        public int ProjectId { get; set; }
        public ProjectEntity ProjectEntity { get; set; }

        public int DeveloperId { get; set; }
        public DeveloperEntity DeveloperEntity { get; set; }
    }
}
