using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using StoreApp.BLL.DTO;
using StoreApp.UI.WPF.Helpers;
using StoreApp.UI.WPF.Models;

namespace StoreApp.UI.WPF.Services
{
    public class ShopsMapService
    {
        private Automapper.ObjectMapper objectMapper = Automapper.ObjectMapper.Instance;
        BLLServicesStorage services = BLLServicesStorage.Instance;

        public async Task CreateShop(ShopUI shopUI)
        {
            ShopDTO shopDTO = objectMapper.Mapper.Map<ShopDTO>(shopUI);
            await services.ShopsService.CreateShop(shopDTO);
        }

        public async Task<ShopUI> GetShopById(int id)
        {
            var result = await services.ShopsService.GetShopById(id);
            return objectMapper.Mapper.Map<ShopUI>(result);
        }

        public async Task<List<ShopUI>> GetAllShops()
        {
            var result = await services.ShopsService.GetAllShops();
            return objectMapper.Mapper.Map<List<ShopUI>>(result);
        }

        internal async Task<ShopUI> GetLastAddedShop()
        {
            var result = await services.ShopsService.GetLastAddedShop();
            return objectMapper.Mapper.Map<ShopUI>(result);
        }
    }
}
