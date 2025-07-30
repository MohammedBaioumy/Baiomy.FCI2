using Baiomy.FCI2.DAL.Common.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baiomy.FCI2.BLL.DTOs.Employees
{
    public class CreatedEmployeeDTO
    {

        [Required(ErrorMessage = "Name is required")]
        [MaxLength(30, ErrorMessage = "Maximum length is 30 ")]
        public string Name { get; set; } = null!;



        [Required(ErrorMessage = "Salary is required")]
        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }


        public int? Age { get; set; }


        [RegularExpression(@"^[0-9]{1,3}-[a-zA-z]{5,10}-[a-zA-z]{4,10}-[a-zA-Z]{5,10}$" ,
                             ErrorMessage ="Address must be like 123-street-city-country")]
        public string? Address { get; set; }



        [Phone(ErrorMessage ="must be a valid phone number")]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; } = null!;



        [Required(ErrorMessage =" Hirring Date is required")]
        [Display(Name = "Hirring Date")]
        public DateOnly HirringDate { get; set; }



        [Required(ErrorMessage = " Is Active is required")]        
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }



        [EmailAddress(ErrorMessage ="must be a valid email address")]     
        public string Email { get; set; } = null!;


        public Gender Gender { get; set; }

        public IFormFile? Image { get; set; }


        [Display(Name = "Employee Type")] 
        public EmployeeType EmployeeType { get; set; }
        public int? DepartmentID { get; set; }



    }
}
