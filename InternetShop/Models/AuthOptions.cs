using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace InternetShop.WEB.Models
{
    public class AuthOptions
    {
        public const string Issuer = "IgorTrofymov";
        public const string Audience = "https://localhost:44344";
        private const string Key = "super_secretmysupersecret_secretkey!123";
        public const int Lifetime = 5;

        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
        }
    }
}
