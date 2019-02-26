namespace eFolio.EF
{
    public class ProjectDeveloperEntity
    {
        public ProjectDeveloperEntity(int projectId, int developerId)
        {
            ProjectId = projectId;
            DeveloperId = developerId;
        } 

        public int ProjectId { get; set; }
        public ProjectEntity ProjectEntity { get; set; }

        public int DeveloperId { get; set; }
        public DeveloperEntity DeveloperEntity { get; set; }
    }
}
