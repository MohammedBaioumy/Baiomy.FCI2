using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baiomy.FCI2.BLL.DTOs.Employees
{
    public class EmployeeDetailsDTO
    {
        public int Id { get; set; }
        
        public string Name { get; set; } = null!;

        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }
        public int? Age { get; set; }
        public string? Address { get; set; }
        [Phone]
        [Display(Name = "Phone Number")]

        public string PhoneNumber { get; set; } = null!;
        [Display(Name = "Hirring Date")]

        public DateOnly HirringDate { get; set; }
        [Display(Name = "Is Active")]

        public bool IsActive { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = null!;
        public string Gender { get; set; } = null!;
        [Display(Name = "Employee Type")]

        public string EmployeeType { get; set; } = null!;
        [Display(Name = "Created on")]
        public DateTime CreatedOn { get; set; }
        [Display(Name = "Created by")]
        public int CreatedBy { get; set; }
        [Display(Name = "Last modification on")]
        public DateTime LastModificationOn { get; set; }
        [Display(Name = "Last modification by")]
        public int LastModificationBy { get; set; }
        public string? Department { get; set; }

        public string? Image { get; set; }

        public IFormFile? ImageFF { get; set; }


        public int? DepartmentID { get; set; }

    }
}
