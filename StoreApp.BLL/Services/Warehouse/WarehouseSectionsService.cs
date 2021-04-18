using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using StoreApp.BLL.DTO.Warehouse;
using StoreApp.BLL.Interfaces;
using StoreApp.DAL.Entities.Warehouse;
using StoreApp.DAL.Interfaces;

namespace StoreApp.BLL.Services.Warehouse
{
    public class WarehouseSectionsService : IWarehouseSectionsService
    {
        private IUnitOfWork uow;
        private Automapper.ObjectMapper objectMapper = Automapper.ObjectMapper.Instance;

        public WarehouseSectionsService(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public void CreateSection(WarehouseSectionDTO section)
        {
            Task.Run(async () =>
            {
                var result = objectMapper.Mapper.Map<WarehouseSection>(section);
                await uow.WarehouseSectionsRepository.Create(result);
            });
        }

        public async Task<WarehouseSectionDTO> GetSectionById(int id)
        {
            var result = await uow.WarehouseSectionsRepository.Get(id);
            return objectMapper.Mapper.Map<WarehouseSectionDTO>(result);
        }

        public async Task<List<WarehouseSectionDTO>> GetAllSections()
        {
            var result = await uow.WarehouseSectionsRepository.GetAll();
            return objectMapper.Mapper.Map<List<WarehouseSectionDTO>>(result);
        }
    }
}
