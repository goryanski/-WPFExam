using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using StoreApp.BLL.DTO;

namespace StoreApp.BLL.Interfaces
{
    public interface IUsersService
    {
        Task CreateUser(UserDTO user);
        Task<UserDTO> GetUserById(int id);
    }
}
