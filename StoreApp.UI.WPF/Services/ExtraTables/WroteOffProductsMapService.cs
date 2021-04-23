using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using StoreApp.BLL.DTO.ExtraTables;
using StoreApp.UI.WPF.Helpers;
using StoreApp.UI.WPF.Models.ExtraModels;

namespace StoreApp.UI.WPF.Services.ExtraTables
{
    public class WroteOffProductsMapService
    {
        private Automapper.ObjectMapper objectMapper = Automapper.ObjectMapper.Instance;
        BLLServicesStorage services = BLLServicesStorage.Instance;

        public async Task CreateProduct(WroteOffProductUI productUI)
        {
            WroteOffProductDTO productDTO = objectMapper.Mapper.Map<WroteOffProductDTO>(productUI);
            await services.WroteOffProductsService.CreateProduct(productDTO);
        }

        public async Task<WroteOffProductUI> GetProductById(int id)
        {
            var result = await services.SoldProductsService.GetProductById(id);
            return objectMapper.Mapper.Map<WroteOffProductUI>(result);
        }

        public async Task<List<WroteOffProductUI>> GetAllProducts()
        {
            var result = await services.SoldProductsService.GetAllProducts();
            return objectMapper.Mapper.Map<List<WroteOffProductUI>>(result);
        }
    }
}
