using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL.Models;
using InternetShop.DAL.Interfaces;
using InternetShop.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class ProductRepository : IRepositiry<Product>
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
    }
}
