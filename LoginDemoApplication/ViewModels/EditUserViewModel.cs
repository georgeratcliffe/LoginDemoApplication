using System.ComponentModel.DataAnnotations;

namespace LoginDemoApplication.ViewModels
{
    public class EditUserViewModel
    {
        [Required]
        [MaxLength(50, ErrorMessage = "Name cannot exceed 50 chars")]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm New Password")]
        [Compare("Password",
                ErrorMessage = "Password and confirmation password do not match")]
        public string ConfirmPassword { get; set; }
    }
}
