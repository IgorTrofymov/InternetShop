using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Repositories;
using InternetShop.BLL.DTO;
using InternetShop.BLL.Infrastructure;
using InternetShop.BLL.Interfaces;
using InternetShop.DAL.Models;

namespace InternetShop.BLL.Services
{
    public class CategoryService : ICategoruService
    {
        private CategoryRepository categoryRepository;
        //private ApplicationContext db;

        public CategoryService(/*ApplicationContext db,*/ CategoryRepository categoryRepository)
        {
            //this.db = db;
            this.categoryRepository = categoryRepository;
        }
        public Task<OperationDetails> Create(CategoryDTO item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CategoryDTO> GetAll()
        {
            List<CategoryDTO> categories = new List<CategoryDTO>();
            categoryRepository.GetAll().ToList().ForEach(c => categories.Add(new CategoryDTO { Id = c.Id, Name = c.Name, ParentId = c.ParentCategoryId, Description = c.Description }));
            //db.Categories.ToList().ForEach(c=>categories.Add(new CategoryDTO{ Id=c.Id, Name = c.Name, ParentId = c.ParentCategoryId, Description = c.Description}));
             return categories;
        }
    }
}
