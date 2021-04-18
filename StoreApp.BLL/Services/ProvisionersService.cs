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
    public class ProvisionersService : IProvisionersService
    {
        private IUnitOfWork uow;
        private Automapper.ObjectMapper objectMapper = Automapper.ObjectMapper.Instance;

        public ProvisionersService(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public void CreateProvisioner(ProvisionerDTO provisioner)
        {
            Task.Run(async () =>
            {
                var result = objectMapper.Mapper.Map<Provisioner>(provisioner);
                await uow.ProvisionersRepository.Create(result);
            });
        }

        public async Task<ProvisionerDTO> GetProvisionerById(int id)
        {
            var result = await uow.ProvisionersRepository.Get(id);
            return objectMapper.Mapper.Map<ProvisionerDTO>(result);
        }

        public async Task<List<ProvisionerDTO>> GetAllProducts()
        {
            var result = await uow.ProvisionersRepository.GetAll();
            return objectMapper.Mapper.Map<List<ProvisionerDTO>>(result);
        }
    }
}
