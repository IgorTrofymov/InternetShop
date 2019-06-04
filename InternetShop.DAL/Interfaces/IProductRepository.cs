using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL.Models;
using InternetShop.DAL.Interfaces;

namespace DAL.Interfaces
{
    public interface IProductRepository : IRepositiry<Product>
    {
        IQueryable<Product> GetSome(ProdFilter filter);
    }
}
