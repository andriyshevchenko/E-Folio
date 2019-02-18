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

        public int Id { get; set; }
        public string NameProject { get; set; }
        public string NameOrder { get; set; }
    }
}
