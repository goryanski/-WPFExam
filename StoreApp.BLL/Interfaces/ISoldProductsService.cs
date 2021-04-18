using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using StoreApp.BLL.DTO.ExtraTables;

namespace StoreApp.BLL.Interfaces
{
    public interface ISoldProductsService
    {
        void CreateProduct(SoldProductDTO product);
        Task<SoldProductDTO> GetProductById(int id);
    }
}
