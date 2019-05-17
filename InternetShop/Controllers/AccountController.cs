using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using InternetShop.BLL.DTO;
using InternetShop.BLL.Infrastructure;
using InternetShop.BLL.Interfaces;
using InternetShop.BLL.Services;
using InternetShop.DAL.Models;
using InternetShop.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InternetShop.WEB.Controllers
{
    public class AccountController : Controller
    {
        private UserService userService;
        private SignInManager<User> signInManager;
        private UserManager<User> userManager;
        

        public AccountController(UserService userService, SignInManager<User> signInManager, UserManager<User> user)
        {
            this.userService = userService;
            this.signInManager = signInManager;
            userManager = user;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                UserDTO userDto = new UserDTO
                {
                    Email = model.Email,
                    Year = model.Year,
                    Password = model.Password,
                    UserName = model.Email
                };
                
               
               OperationDetails operationDetails = await userService.Create(userDto);
               if (operationDetails.Succeeded)
               {
                   var claimPrincipals = HttpContext.User;
                   ClaimsIdentity claim =await userService.Authenticate(userDto);
                   await HttpContext.SignInAsync(IdentityConstants.ApplicationScheme, new ClaimsPrincipal(claim));
                   return RedirectToAction("Index", "Home");
                }
            }

            return View(model);
        }
        async Task Authenticate(User user, IdentityRole role)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, role.Name),
                new Claim(ClaimTypes.DateOfBirth, user.Year.ToString())
            };
            await signInManager.PasswordSignInAsync("asd", "asd", false, false);
        }
    }

}