using System.ComponentModel.DataAnnotations;

namespace Identity.Models.ViewModels
{
    public class LoginViewModel
    {
        [Display(Name = "Email is required")]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
