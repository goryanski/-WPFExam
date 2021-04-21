using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using StoreApp.BLL.DTO.Warehouse;
using StoreApp.UI.WPF.Helpers;
using StoreApp.UI.WPF.Models.Warehouse;

namespace StoreApp.UI.WPF.Services.Warehouse
{
    public class ProductsMapService
    {
        private Automapper.ObjectMapper objectMapper = Automapper.ObjectMapper.Instance;
        BLLServicesStorage services = BLLServicesStorage.Instance;

        private ProductDTO GetProductDTO(ProductUI product) => objectMapper.Mapper.Map<ProductDTO>(product);


        public async Task CreateProduct(ProductUI product)
        {
            await services.ProductsService.CreateProduct(GetProductDTO(product));
        }

        public async Task<ProductUI> GetProductById(int id)
        {
            var result = await services.ProductsService.GetProductById(id);
            return objectMapper.Mapper.Map<ProductUI>(result);
        }

        internal async Task<ProductUI> GetFullProductById(int id)
        {
            var result = await services.ProductsService.GetFullProductById(id);
            return objectMapper.Mapper.Map<ProductUI>(result);
        }

        public async Task<List<ProductUI>> GetProductsByCategory(int categoryId)
        {
            var result = await services.ProductsService.GetProductsByCategory(categoryId);
            return objectMapper.Mapper.Map<List<ProductUI>>(result);
        }

        internal async Task<List<ProductUI>> GetNewProducts()
        {
            var result = await services.ProductsService.GetNewProducts();
            return objectMapper.Mapper.Map<List<ProductUI>>(result);
        }

        internal async Task<List<ProductUI>> GetMostPopularProducts()
        {
            var result = await services.ProductsService.GetMostPopularProducts();
            return objectMapper.Mapper.Map<List<ProductUI>>(result);
        }

        internal async Task<List<ProductUI>> GetProductsToOrder()
        {
            var result = await services.ProductsService.GetProductsToOrder();
            return objectMapper.Mapper.Map<List<ProductUI>>(result);
        }

        internal async Task<List<ProductUI>> GetProductsWriteOffSoon()
        {
            var result = await services.ProductsService.GetProductsWriteOffSoon();
            return objectMapper.Mapper.Map<List<ProductUI>>(result);
        }

        internal async Task<List<ProductUI>> GetProductsBySearchText(string srchText)
        {
            var result = await services.ProductsService.GetProductsBySearchText(srchText);
            return objectMapper.Mapper.Map<List<ProductUI>>(result);
        }

        internal async Task DeleteProductByCount(ProductUI product, int newValue)
        {
            await services.ProductsService.DeleteProductByCount(GetProductDTO(product), newValue);
        }

        internal async Task DeleteWholeProduct(ProductUI product)
        {
            await services.ProductsService.DeleteWholeProduct(GetProductDTO(product));
        }

        internal async Task UpdateProduct(ProductUI product)
        {
            await services.ProductsService.UpdateProduct(GetProductDTO(product));
        }

        internal async Task ReturnProductToWarehouse(int productIdToRestore, int countTorestore)
        {
            await services.ProductsService.RestoreProduct(productIdToRestore, countTorestore);
        }
    }
}
