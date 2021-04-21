using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using StoreApp.BLL.DTO;
using StoreApp.UI.WPF.Helpers;
using StoreApp.UI.WPF.Models;

namespace StoreApp.UI.WPF.Services
{
    public class OrdersMapService
    {
        private Automapper.ObjectMapper objectMapper = Automapper.ObjectMapper.Instance;
        BLLServicesStorage services = BLLServicesStorage.Instance;

        public async Task CreateOrder(OrderUI orderUI)
        {
            OrderDTO orderDTO = objectMapper.Mapper.Map<OrderDTO>(orderUI);
            await services.OrdersService.CreateOrder(orderDTO);
        }

        public async Task<OrderUI> GetOrderById(int id)
        {
            var result = await services.OrdersService.GetOrderById(id);
            return objectMapper.Mapper.Map<OrderUI>(result);
        }

        public async Task<List<OrderUI>> GetAllOrders()
        {
            var result = await services.OrdersService.GetAllOrders();
            return objectMapper.Mapper.Map<List<OrderUI>>(result);
        }

        internal async Task DeleteOrder(int orderId)
        {
            await services.OrdersService.DeleteOrder(orderId);
        }
    }
}
