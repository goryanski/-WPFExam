using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public void CreateProduct(WroteOffProductDTO product)
        {
            Task.Run(async () =>
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

        public async Task<List<WroteOffProductDTO>> GetAllProducts()
        {
            var result = await uow.WroteOffProductsRepository.GetAll();
            return objectMapper.Mapper.Map<List<WroteOffProductDTO>>(result);
        }
    }
}
