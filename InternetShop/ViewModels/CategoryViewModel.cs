using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetShop.WEB.ViewModels
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        public string   Name { get; set; }
        public int? ParentId { get; set; }
        public string Description { get; set; }
    }
    public class SeededCategories
    {
        public int? Seed { get; set; }
        public IEnumerable<CategoryViewModel> Categories { get; set; }
    }
}
