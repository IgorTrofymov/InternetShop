using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using InternetShop.DAL.Models;
using Microsoft.AspNetCore.Authorization;

namespace InternetShop.WEB.Requirements
{
    public class AgeRequirementHandler : AuthorizationHandler<AgeRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AgeRequirement requirement)
        {
            if (context.User.HasClaim(c => c.Type == ClaimTypes.DateOfBirth))
            {
                DateTime year;
                if (DateTime.TryParse(context.User.FindFirst(c => c.Type == ClaimTypes.DateOfBirth).Value, out year))
                {
                    int calculatedAge = DateTime.Today.Year - year.Year;
                    if (year > DateTime.Today.AddYears(-calculatedAge))
                    {
                        calculatedAge--;
                    }

                    if (calculatedAge >= requirement.Age)
                    {
                        context.Succeed(requirement);
                    }
                }
            }

            return Task.CompletedTask;
        }
    }
}
