using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using InternetShop.BLL.DTO;
using InternetShop.BLL.Infrastructure;

namespace InternetShop.BLL.Interfaces
{
    public interface IUserService 
    {
        Task<OperationDetails> Create(UserDTO userDto);
        Task<List<Claim>> Authenticate(UserDTO userDto);

        Task SetInitialData(UserDTO adminDto, List<string> roles);
    }
}
