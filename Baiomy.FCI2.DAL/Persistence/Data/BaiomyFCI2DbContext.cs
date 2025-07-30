using Baiomy.FCI2.DAL.Entities.Departments;
using Baiomy.FCI2.DAL.Entities.Employees;
using Baiomy.FCI2.DAL.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Baiomy.FCI2.DAL.Persistence.Data
{
    public class BaiomyFCI2DbContext : IdentityDbContext<ApplicationUser>
	{    
        public BaiomyFCI2DbContext(DbContextOptions options):base(options)
        {
            
        }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

		}

		

		#region OldWay_ConnectionString
		//protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		//=> optionsBuilder.UseSqlServer("Server=.;Database= BaiomyFCI2; Trusted_Connection=True ; TrustServerCertificate=True;"); 
		#endregion

		public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }

    }
}
