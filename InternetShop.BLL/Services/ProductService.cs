using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;
using DAL.Repositories;
using InternetShop.BLL.DTO;
using InternetShop.BLL.Infrastructure;
using InternetShop.BLL.Interfaces;
using InternetShop.DAL.Models;

namespace InternetShop.BLL.Services
{
    public class ProductService : IProductService
    {
        private ProductRepository repository;

        public ProductService(ProductRepository repository)
        {
            this.repository = repository;
        }
        public Task<OperationDetails> Create(ProductDTO item)
        {
            Product product = repository.Find(c => c.Name == item.Name).FirstOrDefault();
            if (product == null)
            {
                product = new Product
                {
                    Name = item.Name, CategoryId = item.CategoryId, Created = DateTime.Now, Price = item.Price, Sold = 0
                };
                repository.Create(product);
                return Task.FromResult(new OperationDetails(true, "it's ok", ""));
            }
            return Task.FromResult(new OperationDetails(true, "there is already such product", "Name"));
        }

        public IEnumerable<ProductDTO> GetAll()
        {
            List<ProductDTO> list = new List<ProductDTO>();
            repository.GetAll().ToList().ForEach(c =>
           {
               list.Add(new ProductDTO
               {
                   Name = c.Name, Id = c.ProductId, Description = c.Description, Price = c.Price,
                   CategoryId = c.CategoryId
               });
           });
            return list;
        }

        public IEnumerable<ProductDTO> GetSome(Func<ProductDTO, bool> predicate)
        {
            List<ProductDTO> list = new List<ProductDTO>();
            repository.Find(predicate).ToList().ForEach(c =>
            {
                list.Add(new ProductDTO
                {
                    Name = c.Name,
                    Id = c.ProductId,
                    Description = c.Description,
                    Price = c.Price,
                    CategoryId = c.CategoryId
                });
            });
            return list;
        }
    }
}
