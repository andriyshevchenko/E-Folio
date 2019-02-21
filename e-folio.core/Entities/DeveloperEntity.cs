using System.Collections.Generic;

namespace eFolio.EF
{
    public class DeveloperEntity
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string CVLink { get; set; }

        public ICollection<ProjectDeveloperEntity> Projects { get; set; }
    }
}