using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using StoreApp.BLL.DTO;

namespace StoreApp.BLL.Interfaces
{
    public interface IProvisionersService
    {
        void CreateProvisioner(ProvisionerDTO provisioner);
        Task<ProvisionerDTO> GetProvisionerById(int id);
    }
}
