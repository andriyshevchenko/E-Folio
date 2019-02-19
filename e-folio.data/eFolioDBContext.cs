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

        public DbSet<ProjectEntity> Projects { get; set; } 
        public DbSet<DeveloperEntity> Developers { get; set; }
        public DbSet<ContextEntity> Contexsts { get; set; }
        public DbSet<FolioFileEntity> FolioFiles { get; set; }
        public DbSet<ClientEntity> Clients { get; set; }
        public DbSet<ContactPersonEntity> ContactPersons { get; set; }
    }
}
