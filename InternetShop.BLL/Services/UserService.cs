using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using InternetShop.BLL.DTO;
using InternetShop.BLL.Infrastructure;
using InternetShop.BLL.Interfaces;
using InternetShop.DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace InternetShop.BLL.Services
{
    public class UserService : IUserService
    {
        private ApplicationContext db;
        private UserManager<User> userManager;

        public UserService(ApplicationContext db, UserManager<User> userManager)
        {
            this.db = db;
            this.userManager = userManager;

        }


        public async Task<OperationDetails> Create(UserDTO userDto)
        {
            var user = await this.userManager.FindByEmailAsync(userDto.Email);
            if (user == null)
            {
                user = new User {Email = userDto.Email, UserName = userDto.Email, Year = userDto.Year};

                var result = await userManager.CreateAsync(user, userDto.Password);
                if (result.Succeeded)
                {
                    //Authenticate(userDto);
                    await  userManager.AddToRoleAsync(user, "baseUser");
                    return new OperationDetails(true, "it's ok", "");
                }
            }
                return new OperationDetails(false, "This email is already busy","Email");
        }

        public async  Task<List<Claim>> Authenticate(UserDTO userDto )
        {
            User user = await  userManager.FindByEmailAsync(userDto.Email);
            //signInManager.PasswordSignInAsync(userDto.UserName, userDto.Password, false, false);
            var claims = new List<Claim>();
            if (await userManager.CheckPasswordAsync(user, userDto.Password))
            {
                claims.Add(new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email));
                    //new Claim(ClaimsIdentity.DefaultRoleClaimType, userDto.Role?.Name),
                    claims.Add(new Claim(ClaimTypes.Email, user.Email));
                    //new Claim("age",age.ToString()),
                    //new Claim("trueornot",age>=18?"true":"false"),
                   claims.Add( new Claim(ClaimTypes.DateOfBirth, user.Year.ToString()));
                
                var userRoles = await userManager.GetRolesAsync(await userManager.FindByEmailAsync(user.Email));
                foreach (var role in userRoles)
                {
                    claims.Add(new Claim(ClaimsIdentity.DefaultRoleClaimType, role));
                }

            }

          await  signInManager.PasswordSignInAsync(user, userDto.Password, userDto.RememberMe, false);

            return claims;

        }

        public Task SetInitialData(UserDTO adminDto, List<string> roles)
        {
            throw new NotImplementedException();
        }
    }
}
