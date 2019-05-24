using System;
using System.Collections.Generic;
using System.Text;

namespace InternetShop.BLL.DTO
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int? CategoryId { get; set; }
    }
}
