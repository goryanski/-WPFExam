using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreApp.BLL.DTO;
using StoreApp.BLL.Interfaces;
using StoreApp.DAL.Entities;
using StoreApp.DAL.Interfaces;

namespace StoreApp.BLL.Services
{
    public class UsersService //: IUsersService
    {
        private IUnitOfWork uow;
        private Automapper.ObjectMapper objectMapper = Automapper.ObjectMapper.Instance;

        public UsersService(IUnitOfWork uow)
        {
            this.uow = uow;
        }      

        public Task CreateUser(UserDTO user)
        {
            return Task.Run(async () =>
            {
                var result = objectMapper.Mapper.Map<User>(user);
                await uow.UsersRepository.Create(result);
            });
        }

        public async Task<UserDTO> GetUserById(int id)
        {
            var result = await uow.UsersRepository.Get(id);
            return objectMapper.Mapper.Map<UserDTO>(result);
        }

        public async Task<List<UserDTO>> GetAllUsers()
        {
            var result = await uow.UsersRepository.GetAll();
            return objectMapper.Mapper.Map<List<UserDTO>>(result);
        }

        public async Task<UserDTO> GetUserByLogin(string login)
        {
            var result = await uow.UsersRepository.GetAll(u => u.Login == login);
            return objectMapper.Mapper.Map<List<UserDTO>>(result).FirstOrDefault();
        }

        public async Task<bool> IsUserAdmin(UserDTO user)
        {
            List<User> srchUser = await uow.UsersRepository.GetAll(u => u.IsAdmin == true);
            User admin = srchUser.First();

            return admin.Login == user.Login && admin.Password == user.Password;
        }

        public async Task<int> GetUserId(UserDTO user)
        {
            List<User> srchUser = await uow.UsersRepository
                .GetAll(u => u.Login == user.Login && u.Password == user.Password);
            return srchUser.First().Id;
        }

        public async void RemoveUser(UserDTO userDTO)
        {
            await uow.UsersRepository.Delete(userDTO.Id);
        }

        public async void EditUser(UserDTO userDTO)
        {
            User user = objectMapper.Mapper.Map<User>(userDTO);
            await uow.UsersRepository.Update(user);
        }
    }
}
