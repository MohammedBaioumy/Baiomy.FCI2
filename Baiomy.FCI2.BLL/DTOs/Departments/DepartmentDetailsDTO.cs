using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baiomy.FCI2.BLL.DTOs.Departments
{
    public class DepartmentDetailsDTO
    {
        public int ID { get; set; }
        [Display(Name = "Created on")]
        public DateTime CreatedOn { get; set; }
        [Display(Name = "Created by")]
        public int CreatedBy { get; set; }
        [Display(Name = "Last modification on")]
        public DateTime LastModificationOn { get; set; }
        [Display(Name = "Last modification by")]
        public int LastModificationBy { get; set; }
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Description { get; set; } = null!;
        [Display(Name= "Creation date")]
        public DateOnly CreationDate { get; set; }
    }
}
