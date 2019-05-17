using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace InternetShop.DAL.Models
{
    public class User : IdentityUser
    {
        public DateTime Year { get; set; }
    }
}
