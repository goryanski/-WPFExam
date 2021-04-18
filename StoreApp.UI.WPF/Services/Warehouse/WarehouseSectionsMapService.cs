using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using StoreApp.BLL.DTO.Warehouse;
using StoreApp.UI.WPF.Helpers;
using StoreApp.UI.WPF.Models.Warehouse;

namespace StoreApp.UI.WPF.Services.Warehouse
{
    public class WarehouseSectionsMapService
    {
        private Automapper.ObjectMapper objectMapper = Automapper.ObjectMapper.Instance;
        BLLServicesStorage services = BLLServicesStorage.Instance;

        public void CreateSection(WarehouseSectionUI sectionUI)
        {
            WarehouseSectionDTO sectionDTO = objectMapper.Mapper.Map<WarehouseSectionDTO>(sectionUI);
            services.WarehouseSectionsService.CreateSection(sectionDTO);
        }

        public async Task<WarehouseSectionUI> GetSectionById(int id)
        {
            var result = await services.WarehouseSectionsService.GetSectionById(id);
            return objectMapper.Mapper.Map<WarehouseSectionUI>(result);
        }

        public async Task<List<WarehouseSectionUI>> GetAllSections()
        {
            var result = await services.WarehouseSectionsService.GetAllSections();
            return objectMapper.Mapper.Map<List<WarehouseSectionUI>>(result);
        }
    }
}
