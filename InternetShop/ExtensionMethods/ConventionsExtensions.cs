using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetShop.BLL.DTO;
using InternetShop.WEB.ViewModels;

namespace InternetShop.WEB.ExtensionMethods
{
    public static class ConventionsExtensions
    {
        public static IEnumerable<CategoryViewModel> GetViewCategoryViewModels(this IEnumerable<CategoryDTO> category)
        {
            List<CategoryViewModel> cats = new List<CategoryViewModel>();
            foreach (var categoryDto in category)
            {
                cats.Add(new CategoryViewModel
                {
                    Id = categoryDto.Id, Name = categoryDto.Name, Description = categoryDto.Description,
                    ParentId = categoryDto.ParentId
                });
            }

            return cats;
        }
    }
}
