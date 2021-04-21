using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using StoreApp.BLL.DTO;
using StoreApp.BLL.Interfaces;
using StoreApp.DAL.Entities;
using StoreApp.DAL.Interfaces;

namespace StoreApp.BLL.Services
{
    public class OrdersService : IOrdersService
    {
        private IUnitOfWork uow;
        private Automapper.ObjectMapper objectMapper = Automapper.ObjectMapper.Instance;

        public OrdersService(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public async Task CreateOrder(OrderDTO order)
        {
            await Task.Run(async () =>
            {
                var result = objectMapper.Mapper.Map<Order>(order);
                await uow.OrderRepository.Create(result);
            });
        }

        public async Task<OrderDTO> GetOrderById(int id)
        {
            var result = await uow.OrderRepository.Get(id);
            return objectMapper.Mapper.Map<OrderDTO>(result);
        }

        public async Task<List<OrderDTO>> GetAllOrders()
        {
            var result = await uow.OrderRepository.GetAll();
            return objectMapper.Mapper.Map<List<OrderDTO>>(result);
        }

        public async Task DeleteOrder(int orderId)
        { 
            await uow.OrderRepository.Delete(orderId);
        }
    }
}
