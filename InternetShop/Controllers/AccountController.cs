using BotDetect.Web.Mvc;
using InternetShop.BLL.DTO;
using InternetShop.BLL.Infrastructure;
using InternetShop.BLL.Services;
using InternetShop.DAL.Models;
using InternetShop.ViewModels;
using InternetShop.WEB.Models;
using InternetShop.WEB.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;


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
                        var claims = await userService.Authenticate(userDto);
                        string token = await GenerateJwtToken(claims);
                        Response.Cookies.Append("Authorization", "Bearer " + token, new CookieOptions() { IsEssential = true });
                        return RedirectToAction("Index", "Home");
                    }
                    else ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
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
                ModelState.AddModelError("CaptchaCode", "Incorrect!");
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
                    Password = model.Password,
                    RememberMe = model.RememberMe
                };
                List<Claim> claims =await userService.Authenticate(userDto);
                   string token = await GenerateJwtToken(claims);

                   Response.Cookies.Append("Authorization", "Bearer " + token, new CookieOptions() { IsEssential = true });

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Email or pass is incorrect");
                throw new ApplicationException("INVALID_LOGIN_ATTEMPT");
            }
        }


        private async Task<string> GenerateJwtToken(List<Claim> claims)
        {
            
            //signInManager.PasswordSignInAsync(userDto.UserName, userDto.Password, false, false);
           
            var key = AuthOptions.GetSymmetricSecurityKey();
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(AuthOptions.Lifetime));

            var token = new JwtSecurityToken(
                AuthOptions.Issuer,
                AuthOptions.Audience,
                claims,
                expires: expires,
                signingCredentials: creds
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public IActionResult Logout()
        {
            Response.Cookies.Delete("Authorization");
            return RedirectToAction("Login");
        }

    }
}