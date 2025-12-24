using AutoMapper;
using DTO_s;
using Entities;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _iOrderRepository;
        private readonly IMapper _iMapper;

        public OrderService(IOrderRepository orderRepository, IMapper mapper)
        {
            _iOrderRepository = orderRepository;
            _iMapper = mapper;
        }
        public async Task<OrderDTO> GetById(int id)
        {
            Order order = await _iOrderRepository.GetOrderById(id);
            OrderDTO orderDTO = _iMapper.Map<Order, OrderDTO>(order);
            return orderDTO;
        }
        public async Task<OrderDTO> AddOrder(OrderDTO order)
        {
            Order ord = _iMapper.Map<OrderDTO, Order>(order);
            Order res = await _iOrderRepository.AddOrder(ord);
            OrderDTO orderDTO = _iMapper.Map<Order, OrderDTO>(res);
            return orderDTO;
        }
    }
}
