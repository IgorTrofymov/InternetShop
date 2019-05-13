using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InternetShop.DAL.Interfaces;
using InternetShop.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace InternetShop.DAL.Repositories
{
    class UserRepository : IRepositiry<User>
    {
        private ApplicationContext db;

        public UserRepository(ApplicationContext context)
        {
            db = context;
        }
        public IEnumerable<User> GetAll()
        {
            return db.Users.ToList();
        }

        public User Get(string id)
        {
            return db.Users.Find(id);
        }

        public IEnumerable<User> Find(Func<User, bool> predicate)
        {
            return db.Users.Where(predicate).ToList();
        }

        public void Create(User item)
        {
            db.Users.Add(item);
            db.SaveChanges();
        }

        public void Update(User item)
        {
            db.Entry(item).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Delete(string id)
        {
            User user = db.Users.Find(id);
            if (user != null)
            {
                db.Users.Remove(user);
                db.SaveChanges();
            }
        }
    }
}
