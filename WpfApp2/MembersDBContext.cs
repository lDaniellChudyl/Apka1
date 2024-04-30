using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2
{
    internal class MembersDBContext:DbContext
    {
        public DbSet<Member> Dane { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Member>()
                .Property(f => f.Number)
                .ValueGeneratedOnAdd();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.UseSqlServer($"Data Source=localhost\\SQLEXPRESS;Initial Catalog=Dane;Integrated Security=True; TrustServerCertificate=True;");
        }
    }
}
