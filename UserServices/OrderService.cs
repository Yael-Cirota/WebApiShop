using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repositories;

namespace Service
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _iOrderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _iOrderRepository = orderRepository;
        }
        public async Task<Order> GetById(int id)
        {
            return await _iOrderRepository.GetOrderById(id);
        }
        public async Task<Order> AddOrder(Order order)
        {
            return await _iOrderRepository.AddOrder(order);
        }
    }
}
