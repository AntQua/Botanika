using System.ComponentModel.DataAnnotations;

namespace CLOUD462022.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "User name is required")]
        [Display(Name = "UserName")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required, minimum 11 characters")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        public string ReturnUrl { get; set; }
    }
}
