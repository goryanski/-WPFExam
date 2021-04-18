using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using StoreApp.BLL.DTO;
using StoreApp.UI.WPF.Helpers;
using StoreApp.UI.WPF.Models;

namespace StoreApp.UI.WPF.Services
{
    public class UsersMapService
    {
        private Automapper.ObjectMapper objectMapper = Automapper.ObjectMapper.Instance;
        BLLServicesStorage services = BLLServicesStorage.Instance;

        public async Task CreateUser(UserUI user)
        {
            await services.UsersService.CreateUser(GetUserDTO(user));
        }

        public async Task<UserUI> GetUserById(int id)
        {
            var result = await services.UsersService.GetUserById(id);
            return objectMapper.Mapper.Map<UserUI>(result);
        }

        public async Task<List<UserUI>> GetAllUsers()
        {
            var result = await services.UsersService.GetAllUsers();
            return objectMapper.Mapper.Map<List<UserUI>>(result);
        }

        public async Task<UserUI> GetUserByLogin(string login)
        {
            var result = await services.UsersService.GetUserByLogin(login);
            return objectMapper.Mapper.Map<UserUI>(result);
        }

        internal async Task<bool> IsUserAdmin(UserUI user)
        {
            return await services.UsersService.IsUserAdmin(GetUserDTO(user));
        }

        internal void EditUser(UserUI user)
        {
            services.UsersService.EditUser(GetUserDTO(user));
        }

        internal void RemoveUser(UserUI user)
        {
            services.UsersService.RemoveUser(GetUserDTO(user));
        }

        private UserDTO GetUserDTO(UserUI user) => objectMapper.Mapper.Map<UserDTO>(user);

        internal Task<int> GetUserId(UserUI user)
        {
            return services.UsersService.GetUserId(GetUserDTO(user));
        }
    }
}
