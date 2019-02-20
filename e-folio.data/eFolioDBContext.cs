using Microsoft.EntityFrameworkCore;
using e_folio.core.Entities;

namespace e_folio.data
{
    public class eFolioDBContext : DbContext
    {
        public eFolioDBContext(DbContextOptions<eFolioDBContext> options): base(options) 
        {
            Database.EnsureCreated();
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<Developer> Developers { get; set; }
        public DbSet<Description> Descriptions { get; set; }
    }
   
}
