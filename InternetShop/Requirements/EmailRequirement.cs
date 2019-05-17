using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace InternetShop.WEB.Requirements
{
    public class EmailRequirement : IAuthorizationRequirement
    {
        protected internal string Email { get; set; }

        public EmailRequirement(string email)
        {
            Email = email;
        }
    }
}
