using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Baiomy.FCI2.PL.ViewModels
{
    public class DetailsUpdateUserViewModel
    {
        public string Id { get; set; } = null!;

        [Display(Name="User Name")]
        public string UserName { get; set; } = null!;
       
        [EmailAddress]
        public string Email { get; set; } = null!;


        [Display(Name= "First Name")]
        public string FirstName { get; set; } = null!;

        [Display(Name = "Last Name")]
        public string LastName { get; set; } = null!;


    }
}
