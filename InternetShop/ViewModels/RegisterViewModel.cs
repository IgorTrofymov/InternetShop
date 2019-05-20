using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;

namespace InternetShop.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        //[Required(ErrorMessage = "Please enter your Name")]
        [Display(Name="User name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Password can't be empty")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "This field must be the same as Password")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Date of birth")]
        public DateTime Year { get; set; }

        public string CaptchaCode { get; set; }


    }
}
