using System.ComponentModel.DataAnnotations;

namespace Baiomy.FCI2.PL.ViewModels
{
    public class DepartmentViewModel
    {
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
