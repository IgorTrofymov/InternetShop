using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BotDetect.Web.Mvc;
using InternetShop.BLL.DTO;
using InternetShop.BLL.Infrastructure;
using InternetShop.BLL.Interfaces;
using InternetShop.BLL.Services;
using InternetShop.DAL.Models;
using InternetShop.ViewModels;
using InternetShop.WEB.ViewModels;
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
        [ValidateAntiForgeryToken]
        //[CaptchaValidation("CaptchaCode", "UserRegisterCaptcha","Incorrect CAPTCHA code!")]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            MvcCaptcha mvcCaptcha = new MvcCaptcha("UserRegisterCaptcha");
            string validatingInstanceId = mvcCaptcha.WebCaptcha.ValidatingInstanceId;
            if (mvcCaptcha.Validate(model.CaptchaCode, validatingInstanceId))
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
                    MvcCaptcha.ResetCaptcha("UserRegisterCaptcha");


                    OperationDetails operationDetails = await userService.Create(userDto);
                    if (operationDetails.Succeeded)
                    {
                        var claimPrincipals = HttpContext.User;
                        ClaimsIdentity claim = await userService.Authenticate(userDto);
                        await HttpContext.SignInAsync(IdentityConstants.ApplicationScheme, new ClaimsPrincipal(claim));
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    MvcCaptcha.ResetCaptcha("UserRegisterCaptcha");
                    ModelState.AddModelError("", "Wrong email or password");
                }
            }
            else
            {
                MvcCaptcha.ResetCaptcha("UserRegisterCaptcha");
                ModelState.AddModelError("CaptchaCode","Incorrect!");
            }

            return View(model);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                UserDTO userDto = new UserDTO
                {
                    Email = model.Email,
                    UserName = model.Email,
                    Password = model.Password
                };
                ClaimsIdentity claim = await userService.Authenticate(userDto);
                string name = claim.Name;
                if (name != null)
                {
                    await HttpContext.SignInAsync(IdentityConstants.ApplicationScheme, new ClaimsPrincipal(claim));
                    return RedirectToAction("Index", "Home");
                }
                else { ModelState.AddModelError("", "Email or pass is incorrect");}

            }
            else
            {
                ModelState.AddModelError("","Email isn't entered");
            }

            return View(model);
        }

        public IActionResult Logout()
        {
            ClaimsIdentity id = new ClaimsIdentity();
            HttpContext.SignInAsync(IdentityConstants.ApplicationScheme, new ClaimsPrincipal(id));
            return RedirectToAction("Login");
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