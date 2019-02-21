using System.Collections.Generic;

namespace e_folio.data
{
    public class Project 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Context Context { get; set; }

        public ICollection<Developer> Developers { get; set; }

        public string InternalDescription { get; set; }
        public string ExternalDescription { get; set; }
    }
}