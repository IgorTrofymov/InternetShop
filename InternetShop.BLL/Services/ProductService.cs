using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Convertor.ModelToDTO;
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

        public IEnumerable<ProductDTO> GetSome(ProdFilter filter)
        {
            List<Product> prods = repository.GetSome(filter).ToList();

            //this code uses Reflection to convert types
            var a = ProductConvertor<ProductDTO, Product>.ConvertFromModelToDTO(prods); 

            //this code uses extension method to convert types
            return prods.GetProdDtos();
        }



        IEnumerable<ProductDTO> IService<ProductDTO>.GetAll()
        {
            List<ProductDTO> productDtos = new List<ProductDTO>();
            repository.GetAll().ToList().ForEach(c=> productDtos.Add(new ProductDTO
            {
                CategoryId = c.CategoryId, Name = c.Name, Description = c.Description, Id = c.ProductId
                , Price = c.Price}
            ));
            return productDtos;
        }
    }
}
