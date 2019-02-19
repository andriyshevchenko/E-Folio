using System.Collections.Generic;

namespace eFolio
{
    public class Project
    {
        public string Name { get; set; }
        public Context Context { get; set; }
        public List<Developer> Developers { get; set; }

        public string InternalDescription { get; set; }
        public string ExternalDescription { get; set; }
    }
}