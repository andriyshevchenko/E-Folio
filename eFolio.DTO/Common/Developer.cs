using System.Collections.Generic;

namespace eFolio.DTO
{
    public class Developer
    {
        public Developer(int id, string fullName, string cVLink)
        {
            Id = id;
            FullName = fullName;
            CVLink = cVLink;
            Projects = new List<Project>();
        }

        public int Id { get; set; }
        public string FullName { get; set; }
        public string CVLink { get; set; }
        public ICollection<Project> Projects { get; set; }
    }
}