

using KafeinPortal.Data.Model.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafeinPortal.Data.Context
{
    public class EfContext : DbContext
    {
        public EfContext(DbContextOptions<EfContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Project>()
                .HasOne<Customer>(s => s.Customer)
                .WithMany(g => g.Projects)
                .HasForeignKey(s => s.CustomerId);

            modelBuilder.Entity<ProjectDetail>()
                .HasOne<Project>(s => s.Project)
                .WithOne(g => g.ProjectDetails)
                .HasForeignKey<ProjectDetail>(s => s.ProjectId);
            
               

           

        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectDetail> ProjectDetails { get; set; }
        
        




    }
}
