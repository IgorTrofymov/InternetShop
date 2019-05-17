using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace InternetShop.WEB.Requirements
{
    public class EmailRequirementHandler : AuthorizationHandler<EmailRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            EmailRequirement requirement)
        {
            if (context.User.HasClaim(c => c.Type == ClaimTypes.Email))
            {
                string email = context.User.FindFirst(c => c.Type == ClaimTypes.Email).Value;
                if (email.EndsWith(requirement.Email))
                {
                    context.Succeed(requirement);
                }
            }
            return Task.CompletedTask;
        }
    }
}