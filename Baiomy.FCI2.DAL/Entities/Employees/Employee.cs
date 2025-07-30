using Baiomy.FCI2.DAL.Common.Enums;
using Baiomy.FCI2.DAL.Entities.Departments;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baiomy.FCI2.DAL.Entities.Employees
{
    public class Employee : EntityBase
    {
        public string Name { get; set; } = null!;
        public int? Age { get; set; }
        public string? Address { get; set; }
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }
        public string PhoneNumber { get; set; } = null!;
        public string Email { get; set; } = null!;
        public DateOnly HirringDate { get; set; }
        public Gender Gender { get; set; }
        public EmployeeType EmployeeType { get; set; }

        public string? Image { get; set; }

        public int? DepartmentID { get; set; }
        
        public virtual Department? Department { get; set; }

    }
}
