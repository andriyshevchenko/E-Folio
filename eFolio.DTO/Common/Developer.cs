using System.Collections.Generic;

namespace eFolio.DTO.Common
{
    public class Developer
    {
        public Developer()
        {

        }
        public Developer(int id, string fullName, string cVLink)
        {
            Id = id;
            FullName = fullName;
            CVLink = cVLink;
            Projects = new List<Project>();
        }

        public void UpdateId(int id)
        {
            if (id > 0)
            {
                Id = id; 
            }
        }

        public int Id { get; set; }
        public string FullName { get; set; }
        public string CVLink { get; set; }
        public string InternalCV { get; set; }
        public string ExternalCV { get; set; }

        public ICollection<Project> Projects { get; set; }
    }
}