using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using StoreApp.BLL.DTO.ExtraTables;
using StoreApp.UI.WPF.Helpers;
using StoreApp.UI.WPF.Models.ExtraModels;

namespace StoreApp.UI.WPF.Services.ExtraTables
{
    public class SoldProductsMapService
    {
        private Automapper.ObjectMapper objectMapper = Automapper.ObjectMapper.Instance;
        BLLServicesStorage services = BLLServicesStorage.Instance;

        public async Task CreateProduct(SoldProductUI productUI)
        {
            SoldProductDTO productDTO = objectMapper.Mapper.Map<SoldProductDTO>(productUI);
            await services.SoldProductsService.CreateProduct(productDTO);
        }

        public async Task<SoldProductUI> GetProductById(int id)
        {
            var result = await services.SoldProductsService.GetProductById(id);
            return objectMapper.Mapper.Map<SoldProductUI>(result);
        }

        public async Task<List<SoldProductUI>> GetAllProducts()
        {
            var result = await services.SoldProductsService.GetAllProducts();
            return objectMapper.Mapper.Map<List<SoldProductUI>>(result);
        }

        //public async Task<int> GetGeneralAmountSoldProductsById(int id)
        //{
        //    return await services.SoldProductsService.GetGeneralAmountSoldProductsById(id);
        //}
    }
}
