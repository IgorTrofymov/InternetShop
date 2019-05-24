using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using InternetShop.BLL.DTO;
using InternetShop.BLL.Infrastructure;

namespace InternetShop.BLL.Interfaces
{
    public interface IService<T>
    {
        Task<OperationDetails> Create(T item);
    }
}
