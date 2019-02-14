using System;
using System.Data.SqlClient;
using System.IO.Compression;
using Microsoft.EntityFrameworkCore;
using e_folio.core.Entities;
using e_folio.core;

namespace e_folio.data
{
    public class eFolioDBContext : DbContext
    {
        public eFolioDBContext() : base(new DbContextOptionsBuilder().UseSqlServer("Data Source=.;Initial Catalog=eFolio;").Options)
        {
                
        }

        public DbSet<Project> Projects { get; set; }   
    }
}
