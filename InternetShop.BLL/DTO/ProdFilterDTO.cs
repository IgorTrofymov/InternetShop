using System;
using System.Collections.Generic;
using System.Text;

namespace InternetShop.BLL.DTO
{
    public class ProdFilterDTO
    {
        public int? PriceFrom { get; set; }
        public int? PriceTo { get; set; }
        public string Name { get; set; }
        public IEnumerable<int> CatIds { get; set; }

        public ProdFilterDTO()
        {
            CatIds = new List<int>();
        }
    }
}
