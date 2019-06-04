using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using InternetShop.BLL.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.Cookies;
using InternetShop.Models;
using InternetShop.WEB.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using InternetShop.BLL.Services;
using InternetShop.WEB.ExtensionMethods;
using DAL.Models;

namespace InternetShop.Controllers
{
    public class HomeController : Controller
    {
        private CategoryService categoryService;
        private ProductService productService;

        public HomeController(CategoryService catService, ProductService productService)
        {
            this.categoryService = catService;
            this.productService = productService;
        }

        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Index()
        {
            IEnumerable<CategoryViewModel> categories =new List<CategoryViewModel>();
            if (HttpContext.Session.Keys.Contains("categories"))
            {
                categories = HttpContext.Session.Get<IEnumerable<CategoryViewModel>>("categories").ToList();
            }
            else
            {
                categories = categoryService.GetAll().GetViewCategoryViewModels();//.ForEach(cat => categories.Add(new CategoryViewModel
                //{
                //    Id = cat.Id,
                //    Name = cat.Name,
                //    ParentId = cat.ParentId,
                //    Description = cat.Description
                //}));
                HttpContext.Session.Set<IEnumerable<CategoryViewModel>>("categories", categories);
            }
            List<int> ints = new List<int>{1,2,};
            ProdFilter prodFilter = new ProdFilter {CatIds = ints, PriceFrom = 200, PriceTo = 1000};
            var prodDtos = productService.GetSome(prodFilter).ToList();

            List<ProductViewModel> products = new List<ProductViewModel>();
            //var prodDtos = productService.GetSome()
            SeededCategories model = new SeededCategories {Seed = null, Categories = categories};
            return View(model);
        }
        [Authorize(Policy = "AgeLimit")]
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }
        //[Authorize(Policy = "GmailOnly")]
        [Authorize(Roles = "baseUser")]
        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
