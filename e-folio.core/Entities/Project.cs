using System;
using System.Collections.Generic;
using System.Text;

namespace e_folio.core.Entities
{
    public class Project
    {
        public Project()
        {
        }

        public Project(string nameProject, string nameOrder)
        {
            NameProject = nameProject;
            NameOrder = nameOrder;
        }

        public string Id { get; set; }
        public string NameProject { get; set; }
        public string NameOrder { get; set; }
    }
}
