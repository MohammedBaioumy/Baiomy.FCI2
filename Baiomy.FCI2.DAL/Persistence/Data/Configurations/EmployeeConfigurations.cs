using Baiomy.FCI2.DAL.Common.Enums;
using Baiomy.FCI2.DAL.Entities.Employees;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baiomy.FCI2.DAL.Persistence.Data.Configurations
{
    internal class EmployeeConfigurations : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(e => e.Name).HasColumnType("varchar(30)").IsRequired();
            builder.Property(e => e.Address).HasColumnType("varchar(100)");
            builder.Property(e => e.Image).HasColumnType("varchar(100)");
            builder.Property(e => e.Salary).HasColumnType("decimal(8,2)").IsRequired();
            builder.Property(e => e.PhoneNumber).HasColumnType("varchar(15)").IsRequired();
            builder.Property(e => e.Email).HasColumnType("varchar(50)");
            builder.Property(e => e.CreatedOn).HasComputedColumnSql("GETUTCDATE()");

            builder.Property(e => e.Gender).HasConversion(

                (gender) => gender.ToString(), //Convert from (1,2) values of enum to string (Male,Female) to store it in DB
                (gender) => (Gender) Enum.Parse(typeof(Gender),gender)//Convert from string (Male,Female) stored in DB to values of enum (1,2) 

             );
            builder.Property(e => e.EmployeeType).HasConversion(

              (Type) => Type.ToString(), //Convert from (1,2) values of enum to string (PartTime,FullTime) to store it in DB
              (Type) => (EmployeeType) Enum.Parse(typeof(EmployeeType), Type)//Convert from string (PartTime,FullTime) stored in DB to values of enum (1,2) 

             );

        }
    }
}
