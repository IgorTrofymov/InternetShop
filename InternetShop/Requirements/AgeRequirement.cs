using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection.KeyManagement.Internal;

namespace InternetShop.WEB.Requirements
{
    public class AgeRequirement : IAuthorizationRequirement
    {
        protected internal int Age { get; set; }

        public AgeRequirement(int age)
        {
            Age = age;
        }
    }
}
