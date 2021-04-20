using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using StoreApp.BLL.DTO;

namespace StoreApp.BLL.Interfaces
{
    public interface IOrdersService
    {
        Task CreateOrder(OrderDTO order);
        Task<OrderDTO> GetOrderById(int id);
    }
}
