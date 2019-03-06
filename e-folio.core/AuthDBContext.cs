using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace eFolio.EF
{
    public class AuthDBContext : IdentityDbContext<UserEntity, IdentityRole<int>, int>
    {
        public AuthDBContext(DbContextOptions<AuthDBContext> options)
            : base(options)
        {

        }

        // public DbSet<UserEntity> User { get; set; }

        public AuthDBContext()
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Data Source=.,1434; Initial Catalog=eFolio; User ID=sa; Password=Sasha020297Burko");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
