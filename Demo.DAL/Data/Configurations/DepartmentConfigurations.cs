using Demo.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Data.Configurations
{
    public class DepartmentConfigurations : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            //Fluent APIs for Department Entity
            builder.Property(D => D.Id).UseIdentityColumn(10,10);

            //the First 3 Lines(by default بيتنفذوا كده كده)
            builder.HasMany(D => D.Employees)
                .WithOne(D => D.Department)
                .HasForeignKey(E => E.DepartmentId)
                .OnDelete(DeleteBehavior.Cascade);
                ;

            builder.Property(D => D.Code)
                .IsRequired(true);

            builder.Property(E => E.Name)
                .IsRequired(true)
                .HasMaxLength(50);
        }
    }
}
