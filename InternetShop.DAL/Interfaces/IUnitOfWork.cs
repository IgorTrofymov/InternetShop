using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using InternetShop.DAL.Models;
using Microsoft.AspNetCore.Identity;

namespace InternetShop.DAL.Interfaces
{
    interface IUnitOfWork : IDisposable
    {
        //UserManager<User> UserManager { get; }
        RoleManager<IdentityRole> RoleManager { get; }
        Task SaveAsync();
    }
}
