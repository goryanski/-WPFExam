using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using StoreApp.BLL.DTO.Warehouse;

namespace StoreApp.BLL.Interfaces
{
    public interface IWarehouseSectionsService
    {
        void CreateSection(WarehouseSectionDTO section);
        Task<WarehouseSectionDTO> GetSectionById(int id);
    }
}
