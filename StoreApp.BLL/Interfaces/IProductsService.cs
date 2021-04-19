using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using StoreApp.BLL.DTO.Warehouse;

namespace StoreApp.BLL.Interfaces
{
    public interface IProductsService
    {
        Task CreateProduct(ProductDTO product);
        Task<ProductDTO> GetProductById(int id);
    }
}
