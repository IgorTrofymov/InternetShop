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
    public class CategoryRepository : IRepositiry<Category>
    {
        private ApplicationContext db;

        public CategoryRepository(ApplicationContext db)
        {
            this.db = db;
        }
        public void Create(Category item)
        {
            db.Categories.Add(item);
            db.SaveChanges();
        }

        public void Delete(string id)
        {
            Category cat = db.Categories.Find(id);
            if (cat != null)
            {
                db.Categories.Remove(cat);
                db.SaveChanges();
            }
        }

        public IEnumerable<Category> Find(Func<Category, bool> predicate)
        {
           return db.Categories.Where(predicate).ToList();
        }

        public Category Get(string id)
        {
            Category cat = db.Categories.Find(id);
            if (cat != null)
                return cat;
            return new Category();
        }

        public IEnumerable<Category> GetAll()
        {
            return db.Categories.ToList();
        }

        public void Update(Category item)
        {
            db.Entry<Category>(item).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}
