using System;
using System.Collections.Generic;
using System.Text;

namespace e_folio.core.Entities
{
    public class ProjectEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ContextEntity Context { get; set; }

        public ICollection<ProjectDeveloperEntity> Developers { get; set; }
    }
}
