using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using StoreApp.BLL.DTO;

namespace StoreApp.BLL.Interfaces
{
    public interface IUsersService
    {
        void CreateUser(UserDTO user);
        Task<UserDTO> GetUserById(int id);
    }
}
