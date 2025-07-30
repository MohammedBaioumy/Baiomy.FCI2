using System.ComponentModel.DataAnnotations;

namespace Baiomy.FCI2.PL.ViewModels
{
    public class ResetPasswordViewModel
    {
        [DataType(DataType.Password)]
        public string NewPassword { get; set; } = null!;


        [Compare("NewPassword")]
        public string ConfirmPassword { get; set; } = null!;
    }
}
