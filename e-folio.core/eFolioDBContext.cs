using Microsoft.EntityFrameworkCore;

namespace eFolio.EF
{
    public class eFolioDBContext : DbContext
    {
        public eFolioDBContext(DbContextOptions<eFolioDBContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
         
        public DbSet<ProjectEntity> Projects { get; set; }
        public DbSet<DeveloperEntity> Developers { get; set; }
        public DbSet<ContextEntity> Contexts { get; set; }
        public  DbSet<FolioFileEntity> FolioFiles { get; set; }
        public DbSet<ClientEntity> Clients { get; set; }
        public DbSet<ContactPersonEntity> ContactPersons { get; set; } 

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FolioFileEntity>().HasOne(ff => ff.Context).WithMany(ct => ct.ScreenLinks).HasForeignKey(ff => ff.ContextEntityId);
            modelBuilder.Entity<ContextEntity>().HasOne(ct => ct.Project).WithOne(ct => ct.Context).HasForeignKey(typeof(ProjectEntity), nameof(ProjectEntity.ContextId));
            modelBuilder.Entity<ProjectDeveloperEntity>().HasKey(pd => new { pd.ProjectId, pd.DeveloperId });
            modelBuilder.Entity<ProjectDeveloperEntity>().HasOne(pd => pd.ProjectEntity).WithMany(p => p.Developers).HasForeignKey(pd => pd.ProjectId);
            modelBuilder.Entity<ProjectDeveloperEntity>().HasOne(pd => pd.DeveloperEntity).WithMany(d => d.Projects).HasForeignKey(pd => pd.DeveloperId);
        }
    }

}
