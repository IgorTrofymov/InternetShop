using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;
using InternetShop.BLL.DTO;
using InternetShop.BLL.Infrastructure;

namespace InternetShop.BLL.Interfaces
{
    public interface IProductService : IService<ProductDTO>
    {
        IEnumerable<ProductDTO> GetSome(ProdFilter filter);
    }
}
