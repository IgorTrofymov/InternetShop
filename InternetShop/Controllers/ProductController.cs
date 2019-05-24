using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetShop.BLL.Interfaces;
using InternetShop.BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace InternetShop.WEB.Controllers
{
    public class ProductController : Controller
    {
        private IProductService service;

        public ProductController(ProductService service)
        {
            this.service = service;
        }
        public IActionResult ShowAll()
        {
            return View();
        }
    }
}