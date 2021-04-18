using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using StoreApp.BLL.DTO;

namespace StoreApp.BLL.Interfaces
{
    public interface IShopsService
    {
        void CreateShop(ShopDTO user);
        Task<ShopDTO> GetShopById(int id);
    }
}
