using System.Collections.Generic;

namespace e_folio.data
{
    public class Developer
    {
        public string FullName { get; set; }
        public string CVLink { get; set; }
        public IList<Project> Projects { get; set; }
    }
}