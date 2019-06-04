using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL.Models;
using InternetShop.BLL.DTO;

namespace InternetShop.BLL
{
    public static class Extensions
    {
        public static IEnumerable<ProductDTO> GetProdDtos(this IEnumerable<Product> products)
        {
           return products.Select(c=>new ProductDTO {
                CategoryId = c.CategoryId,
                Name = c.Name,
                Id =
                    c.ProductId,
                Description = c.Description,
                Price = c.Price});
        }
    }
}
