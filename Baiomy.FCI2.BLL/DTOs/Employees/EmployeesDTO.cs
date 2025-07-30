using Baiomy.FCI2.DAL.Common.Enums;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baiomy.FCI2.BLL.DTOs.Employees
{
    public class EmployeesDTO
    {

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }
        public int? Age { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = null!;
        public string Gender { get; set; } = null!;
        
        [Display(Name = "Employee Type")]
        public string EmployeeType { get; set; } = null!;
        public string? Department { get; set; }

        public string? Image { get; set; }


    }
}
