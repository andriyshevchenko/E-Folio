using System.Collections.Generic;

namespace eFolio.EF
{
    public class ProjectEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ContextEntity Context { get; set; }

        public ICollection<ProjectDeveloperEntity> Developers { get; set; }
    }
}
