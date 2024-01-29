using Demo.DAL.Data.Configurations;
using Demo.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Contexts
{
    public class MVCAppDbContext:IdentityDbContext
    {

        public MVCAppDbContext(DbContextOptions<MVCAppDbContext>options):base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Fluent API 

            modelBuilder.ApplyConfiguration(new DepartmentConfigurations());

            modelBuilder.ApplyConfiguration(new EmployeeConfigurations());

            base.OnModelCreating(modelBuilder);
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //   =>optionsBuilder.UseSqlServer("Server = .; Database = MVCAppDb;Trusted_Connection = true;");

        public DbSet<Department> Departments { get; set; }

        public DbSet<Employee> Employees { get; set; }

        ///Identity Dbsets(4 Dbsets to Users and 3 Dbsets = 7 Dbsets) inherited from IdentityDbContext 
        ///=> Generate 7 Tables and including tables(Users + Roles + Relationship between them(UserRoles) )
        ///public DbSet<IdentityUser> Users { get; set; }
        ///public DbSet<IdentityRole> Roles { get; set; }


    }
}
