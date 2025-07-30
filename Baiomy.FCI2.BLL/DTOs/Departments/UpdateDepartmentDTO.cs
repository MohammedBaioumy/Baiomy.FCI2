using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baiomy.FCI2.BLL.DTOs.Departments
{
    public class UpdateDepartmentDTO
    {      
        public int ID { get; set; }

        [Required(ErrorMessage = "Code is required")]
        public string Code { get; set; } = null!;

        [Required(ErrorMessage = "Name is required")]
        [MaxLength(50, ErrorMessage = "Maximum length is 50")]
        public string Name { get; set; } = null!;
        [MaxLength(250, ErrorMessage = "Maximum length is 250")]
        public string? Description { get; set; } = null!;

        [Display(Name = "Creation date")]
        [Required(ErrorMessage = "Creation date is required")]
        public DateOnly CreationDate { get; set; }

    }
}
