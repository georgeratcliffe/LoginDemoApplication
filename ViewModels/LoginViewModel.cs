using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LoginDemoApplication.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [MaxLength(50, ErrorMessage = "Name cannot exceed 50 chars")]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me")]
        public bool RememberMe { get; set; }
    }
}
