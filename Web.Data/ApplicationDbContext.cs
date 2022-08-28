using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFinal.Data.Entities;

namespace WebFinal.Data
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext()
            : base()
        {
        }

        public static string ConnectionString { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString);
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }

        public DbSet<Usuarios> Usuarios { get; set; }
    }
}
