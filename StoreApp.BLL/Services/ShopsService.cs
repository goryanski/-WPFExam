using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using StoreApp.BLL.DTO;
using StoreApp.BLL.Interfaces;
using StoreApp.DAL.Entities;
using StoreApp.DAL.Interfaces;

namespace StoreApp.BLL.Services
{
    public class ShopsService : IShopsService
    {
        private IUnitOfWork uow;
        private Automapper.ObjectMapper objectMapper = Automapper.ObjectMapper.Instance;

        public ShopsService(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public void CreateShop(ShopDTO shop)
        {         
            Task.Run(async () =>
            {
                var result = objectMapper.Mapper.Map<Shop>(shop);
                await uow.ShopsRepository.Create(result);
            });
        }

        public async Task<ShopDTO> GetShopById(int id)
        {
            var result = await uow.ShopsRepository.Get(id);
            return objectMapper.Mapper.Map<ShopDTO>(result);
        }
        public async Task<List<ShopDTO>> GetAllShops()
        {
            var result = await uow.ShopsRepository.GetAll();
            return objectMapper.Mapper.Map<List<ShopDTO>>(result);
        }
    }
}
