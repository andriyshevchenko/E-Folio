using System;
using System.Collections.Generic;
using System.Text;

namespace e_folio.core.Entities
{
    public class Project
    {
        public int ProjectID { get; set; }
        public string NameProject { get; set; }
        public string NameClient { get; set; }

        public Project()
        {

        }

        public Project(string nameProject, string nameClient)
        {
            NameProject = nameProject;
            NameClient = nameClient;
        }
    }
}
