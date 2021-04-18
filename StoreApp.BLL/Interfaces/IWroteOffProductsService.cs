using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using StoreApp.BLL.DTO.ExtraTables;

namespace StoreApp.BLL.Interfaces
{
    public interface IWroteOffProductsService
    {
        void CreateProduct(WroteOffProductDTO product);
        Task<WroteOffProductDTO> GetProductById(int id);
    }
}
