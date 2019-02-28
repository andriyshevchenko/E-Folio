using System.Collections.Generic;

namespace eFolio.DTO
{
    public class Project 
    {
        public int Id { get; set; }

        public void UpdateId(int id)
        {
            if (id > 0)
            {
                Id = id;
            }
        }

        public string Name { get; set; }
        public Context Context { get; set; }

        public ICollection<Developer> Developers { get; set; }

        public string InternalDescription { get; set; }
        public string ExternalDescription { get; set; }
    }
}