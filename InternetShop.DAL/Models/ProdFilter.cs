using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class ProdFilter
    {
        public int? PriceFrom { get; set; }
        public int? PriceTo { get; set; }
        public string Name { get; set; }
        public List<int> CatIds { get; set; }

        public ProdFilter()
        {
            CatIds = new List<int>();
        }
    }
}
