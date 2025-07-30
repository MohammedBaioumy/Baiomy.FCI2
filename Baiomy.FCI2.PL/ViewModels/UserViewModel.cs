using System.ComponentModel.DataAnnotations;

namespace Baiomy.FCI2.PL.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; } = null!;

        [Display(Name="User Name")]
        public string UserName { get; set; } = null!;
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Display(Name="Full Name")]
        public string Name { get; set; } = null!;

    }
}
