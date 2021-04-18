using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using StoreApp.BLL.DTO;
using StoreApp.UI.WPF.Helpers;
using StoreApp.UI.WPF.Models;

namespace StoreApp.UI.WPF.Services
{
    public class ProvisionersMapService
    {
        private Automapper.ObjectMapper objectMapper = Automapper.ObjectMapper.Instance;
        BLLServicesStorage services = BLLServicesStorage.Instance;

        public void CreateProvisioner(ProvisionerUI provisionerUI)
        {
            ProvisionerDTO provisionerDTO = objectMapper.Mapper.Map<ProvisionerDTO>(provisionerUI);
            services.ProvisionersService.CreateProvisioner(provisionerDTO);
        }

        public async Task<ProvisionerUI> GetSectionById(int id)
        {
            var result = await services.ProvisionersService.GetProvisionerById(id);
            return objectMapper.Mapper.Map<ProvisionerUI>(result);
        }

        public async Task<List<ProvisionerUI>> GetAllProvisioners()
        {
            var result = await services.ProvisionersService.GetAllProducts();
            return objectMapper.Mapper.Map<List<ProvisionerUI>>(result);
        }
    }
}
