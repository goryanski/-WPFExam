using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using StoreApp.BLL.DTO;
using StoreApp.BLL.DTO.ExtraTables;
using StoreApp.BLL.DTO.Warehouse;
using StoreApp.UI.WPF.Helpers;
using StoreApp.UI.WPF.Helpers.ExtraModels;
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

        public async Task<List<SoldProductExtraModel>> GetLastProducts()
        {
            var soldDbProducts = await services.SoldProductsService.GetLastProducts();
            return await MyMap(soldDbProducts);
        }

        internal async Task<List<SoldProductExtraModel>> GetProductsByRange(DateTime dateFrom, DateTime dateTo)
        {
            var soldDbProducts = await services.SoldProductsService.GetProductsByRange(dateFrom, dateTo);
            return await MyMap(soldDbProducts);
        }

        private async Task<List<SoldProductExtraModel>> MyMap(List<SoldProductDTO> soldDbProducts)
        {
            List<SoldProductExtraModel> viewSoldProducts = new List<SoldProductExtraModel>();
            foreach (var product in soldDbProducts)
            {
                ProductDTO productDTO = await services.ProductsService.GetProductById(product.Id);
                ShopDTO shopDTO = await services.ShopsService.GetShopById(product.ShopId);

                SoldProductExtraModel viewProduct = new SoldProductExtraModel
                {
                    ProductName = productDTO.Name,
                    SoldDate = product.SoldDate,
                    Id = product.Id,
                    Amount = product.Amount,
                    ShopName = shopDTO.Name
                };

                viewSoldProducts.Add(viewProduct);
            }
            return viewSoldProducts;
        }
    }
}
