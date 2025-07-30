using Baiomy.FCI2.DAL.Entities.Departments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.SqlServer.Storage.Internal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baiomy.FCI2.DAL.Persistence.Data.Configurations
{
    internal class DepartmentConfigurations : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.ToTable("Departments", "dbo").HasKey(k => k.ID);
            builder.Property(d => d.Name).HasColumnType("VarChar").HasMaxLength(50).IsRequired();
            builder.Property(d => d.CreatedOn).HasComputedColumnSql("GETUTCDATE()");

            builder.HasMany(e => e.Employees)                
                   .WithOne(d => d.Department)                   
                   .HasForeignKey(e => e.DepartmentID)
                   .OnDelete(DeleteBehavior.SetNull);

        }
    }
}
