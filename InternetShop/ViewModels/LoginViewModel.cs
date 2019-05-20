using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InternetShop.WEB.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "you must enter Email")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "You can't sign in with empty password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
