using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using StoreApp.BLL.DTO.ExtraTables;
using StoreApp.BLL.DTO.Warehouse;
using StoreApp.UI.WPF.Helpers;
using StoreApp.UI.WPF.Helpers.ExtraModels;
using StoreApp.UI.WPF.Models.ExtraModels;
using StoreApp.UI.WPF.Models.Warehouse;

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
            var result = await services.WroteOffProductsService.GetProductById(id);
            return objectMapper.Mapper.Map<WroteOffProductUI>(result);
        }

        public async Task<List<WroteOffProductExtraModel>> GetLastProducts()
        {
            var wroteOffDbProducts = await services.WroteOffProductsService.GetLastProducts();
            return await MyMap(wroteOffDbProducts);
        }

        internal async Task<List<WroteOffProductExtraModel>> GetProductsByRange(DateTime dateFrom, DateTime dateTo)
        {
            var wroteOffDbProducts = await services.WroteOffProductsService.GetProductsByRange(dateFrom, dateTo);
            return await MyMap(wroteOffDbProducts);
        }

        private async Task<List<WroteOffProductExtraModel>> MyMap(List<WroteOffProductDTO> wroteOffDbProducts)
        {
            List<WroteOffProductExtraModel> viewWroteOffProducts = new List<WroteOffProductExtraModel>();
            foreach (var product in wroteOffDbProducts)
            {
                ProductDTO productDTO = await services.ProductsService.GetProductById(product.ProductId);
                WroteOffProductExtraModel viewProduct = new WroteOffProductExtraModel
                {
                    Name = productDTO.Name,
                    Date = product.Date,
                    Id = product.Id,
                    Amount = product.Amount
                };

                viewWroteOffProducts.Add(viewProduct);
            }
            return viewWroteOffProducts;
        }
    }
}
