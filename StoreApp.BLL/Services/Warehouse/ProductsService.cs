using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreApp.BLL.DTO.Warehouse;
using StoreApp.BLL.Interfaces;
using StoreApp.DAL.Entities.Warehouse;
using StoreApp.DAL.Interfaces;

namespace StoreApp.BLL.Services.Warehouse
{
    public class ProductsService : IProductsService
    {
        private IUnitOfWork uow;
        private Automapper.ObjectMapper objectMapper = Automapper.ObjectMapper.Instance;

        public ProductsService(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public async Task CreateProduct(ProductDTO product)
        {
            await Task.Run(async () =>
            {
                var result = objectMapper.Mapper.Map<Product>(product);
                await uow.ProductsRepository.Create(result);
            });
        }

        public async Task<ProductDTO> GetProductById(int id)
        {
            var result = await uow.ProductsRepository.Get(id);
            return objectMapper.Mapper.Map<ProductDTO>(result);
        }

        public async Task<ProductDTO> GetFullProductById(int id)
        {
            var result = await uow.ProductsRepository.GetFullData(id);
            return objectMapper.Mapper.Map<ProductDTO>(result);
        }

        public async Task<List<ProductDTO>> GetProductsByCategory(int categoryId)
        {
            var result = await uow.ProductsRepository.GetAll(p => p.CategoryId == categoryId);
            return objectMapper.Mapper.Map<List<ProductDTO>>(result);
        }

        public async Task<List<ProductDTO>> GetNewProducts()
        {
            var result = await uow.ProductsRepository.GetAll(p => p.ArrivalDate >= DateTime.Now.AddDays(-3));
            return objectMapper.Mapper.Map<List<ProductDTO>>(result);
        }

        public async Task<List<ProductDTO>> GetMostPopularProducts()
        {
            var result = await uow.ProductsRepository.GetAll(p => p.Rating == 5);
            return objectMapper.Mapper.Map<List<ProductDTO>>(result);
        }

        public async Task<List<ProductDTO>> GetProductsToOrder()
        {
            var result = await uow.ProductsRepository.GetAll(p => p.AmountInStorage <= 50);
            return objectMapper.Mapper.Map<List<ProductDTO>>(result);
        }

        public async Task<List<ProductDTO>> GetProductsWriteOffSoon()
        {
            var result = await uow.ProductsRepository.GetAll(p => p.SellBy <= DateTime.Now.AddDays(+3));
            return objectMapper.Mapper.Map<List<ProductDTO>>(result);
        }

        public async Task<List<ProductDTO>> GetProductsBySearchText(string srchText)
        {
            var result = await uow.ProductsRepository.GetAll(p => p.Name.ToLower().Contains(srchText.ToLower()));
            return objectMapper.Mapper.Map<List<ProductDTO>>(result);
        }

        public async Task DeleteProductByCount(ProductDTO productDTO, int newValue)
        {
            var result = objectMapper.Mapper.Map<Product>(productDTO);
            await uow.ProductsRepository.UpdateProductCount(result, newValue);
        }

        public async Task UpdateProduct(ProductDTO productDTO)
        {
            var result = objectMapper.Mapper.Map<Product>(productDTO);
            await uow.ProductsRepository.Update(result);
        }

        public async Task DeleteWholeProduct(ProductDTO productDTO)
        {
            var result = objectMapper.Mapper.Map<Product>(productDTO);
            await uow.ProductsRepository.DeleteProduct(result);
        }
    }
}
