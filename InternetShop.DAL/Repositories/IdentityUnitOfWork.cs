using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using InternetShop.DAL.Interfaces;
using InternetShop.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace DAL.Repositories
{
    class IdentityUnitOfWork : IUnitOfWork
    {
        private ApplicationContext db;
        //private UserManager<User> userManager;
        private RoleManager<IdentityRole> roleManager;

        public IdentityUnitOfWork()
        {
        }
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        //public UserManager<User> UserManager { get; }
        public RoleManager<IdentityRole> RoleManager { get; }
        public Task SaveAsync()
        {
            throw new NotImplementedException();
        }
    }
}
