using AutoMapper;
using DTO_s;
using Entities;
using Repository;

namespace Service
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<OrderDTO> GetById(int id)
        {
            Order order = await _orderRepository.GetOrderById(id);
            OrderDTO orderDTO = _mapper.Map<Order, OrderDTO>(order);
            return orderDTO;
        }

        public async Task<OrderDTO> AddOrder(OrderDTO order)
        {
            Order ord = _mapper.Map<OrderDTO, Order>(order);
            Order res = await _orderRepository.AddOrder(ord);
            OrderDTO orderDTO = _mapper.Map<Order, OrderDTO>(res);
            return orderDTO;
        }
    }
}
