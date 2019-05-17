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
        private RoleManager<IdentityRole> roleManager;
        private UserManager<User> userManager;
        private SignInManager<User> signInManager;

        public UserService(ApplicationContext db, RoleManager<IdentityRole> role, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this.db = db;
            this.roleManager = role;
            this.userManager = userManager;
            this.signInManager = signInManager;

        }
        public void Dispose()
        {
            throw new NotImplementedException();
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

        public async  Task<ClaimsIdentity> Authenticate(UserDTO userDto )
        {
            int age = DateTime.Now.Year - userDto.Year.Year;
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userDto.Email),
                //new Claim(ClaimsIdentity.DefaultRoleClaimType, userDto.Role?.Name),
                new Claim(ClaimTypes.Email, userDto.Email),
                //new Claim("age",age.ToString()),
                //new Claim("trueornot",age>=18?"true":"false"),
                new Claim(ClaimTypes.DateOfBirth, userDto.Year.ToString())
            };
            var userRoles = await  userManager.GetRolesAsync( await  userManager.FindByEmailAsync(userDto.Email));
            foreach (var role in userRoles)
            {
                claims.Add(new Claim(ClaimsIdentity.DefaultRoleClaimType, role));
            }
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie",
                ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            return id;

        }

        public Task SetInitialData(UserDTO adminDto, List<string> roles)
        {
            throw new NotImplementedException();
        }
    }
}
