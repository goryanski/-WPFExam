using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreApp.BLL.DTO.ExtraTables;
using StoreApp.BLL.Interfaces;
using StoreApp.DAL.Entities.ExtraTables;
using StoreApp.DAL.Interfaces;

namespace StoreApp.BLL.Services.ExtraTables
{
    public class WroteOffProductsService : IWroteOffProductsService
    {
        private IUnitOfWork uow;
        private Automapper.ObjectMapper objectMapper = Automapper.ObjectMapper.Instance;

        public WroteOffProductsService(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public async Task CreateProduct(WroteOffProductDTO product)
        {
            await Task.Run(async () =>
            {
                var result = objectMapper.Mapper.Map<WroteOffProduct>(product);
                await uow.WroteOffProductsRepository.Create(result);
            });
        }

        public async Task<WroteOffProductDTO> GetProductById(int id)
        {
            var result = await uow.WroteOffProductsRepository.Get(id);
            return objectMapper.Mapper.Map<WroteOffProductDTO>(result);
        }

        public async Task<List<WroteOffProductDTO>> GetLastProducts()
        {
            var products = await uow.WroteOffProductsRepository.GetAll();
            var result = products.OrderByDescending(p => p.Date)
                .Take(15)
                .ToList();

            return objectMapper.Mapper.Map<List<WroteOffProductDTO>>(result);
        }

        public async Task<List<WroteOffProductDTO>> GetProductsByRange(DateTime dateFrom, DateTime dateTo)
        {
            var result = await uow.WroteOffProductsRepository.GetAll(p => p.Date >= dateFrom && p.Date <= dateTo);
            return objectMapper.Mapper.Map<List<WroteOffProductDTO>>(result);
        }
    }
}
