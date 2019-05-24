using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using InternetShop.BLL.DTO;
using InternetShop.BLL.Infrastructure;

namespace InternetShop.BLL.Interfaces
{
    public interface IProductService : IService<ProductDTO>
    {
        IEnumerable<ProductDTO> GetAll();
        IEnumerable<ProductDTO> GetSome(Func<ProductDTO, bool> predicate);
    }
}
