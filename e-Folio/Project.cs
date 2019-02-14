using System.Collections.Generic;

namespace eFolio
{
    public class Project
    {
        public string NameProject { get; set; }
        public string NameOrder { get; set; }
        public Description description { get; set; }
        public List<Developer> Developers { get; set; }
    }
}