using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL.Interfaces;
using DAL.Models;
using InternetShop.DAL.Interfaces;
using InternetShop.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private ApplicationContext db;

        public ProductRepository(ApplicationContext db)
        {
            this.db = db;
        }
        public IEnumerable<Product> GetAll()
        {
            return db.Products.ToList();
        }

        public Product Get(string id)
        {
                return db.Products.Find(id);
        }

        public IEnumerable<Product> Find(Func<Product, bool> predicate)
        {
            return db.Products.Where(predicate).ToList();
        }

        public void Create(Product item)
        {
            db.Products.Add(item);
            db.SaveChanges();
        }

        public void Update(Product item)
        {
            db.Entry(item).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Delete(string id)
        {
            Product product = db.Products.Find(id);
            if (product != null)
            {
                db.Products.Remove(product);
                db.SaveChanges();
            }
        }

        public IQueryable<Product> GetSome(ProdFilter filter)
        {
            var products = db.Products.AsQueryable();
            if (filter.PriceFrom != null)
            {
                products = products.Where(c => c.Price >= filter.PriceFrom);
            }

            if (filter.PriceTo != null)
            {
                products = products.Where(p => p.Price <= filter.PriceTo);
            }

            if (filter.CatIds.Count() > 0)
            {
                string categories = string.Empty;
                for (int i = 0; i < filter.CatIds.Count(); i++)
                {
                    categories += filter.CatIds.Count() - i > 1 ? filter.CatIds[i].ToString()+"," : filter.CatIds[i].ToString();
                }
                products = products.FromSql("select * from dbo.products where categoryId in (" + categories+")");//Where(c=>c.CategoryId )
            }
            return products;
        }
    }
}
