using AutoMapper;
using DTO_s;
using Entities;
using Microsoft.Azure.Documents;
using Repositories;

namespace Service
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly IProductService _productService;

        public OrderService(IOrderRepository orderRepository, IMapper mapper, IProductService productService)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _productService = productService;
        }

        public async Task<OrderDTO> GetById(int id)
        {
            Order order = await _orderRepository.GetOrderById(id);
            OrderDTO orderDTO = _mapper.Map<Order, OrderDTO>(order);
            return orderDTO;
        }

        public async Task<OrderDTO> AddOrder(OrderDTO order)
        {
            try
            {
                Order ord = _mapper.Map<OrderDTO, Order>(order);
                ord.OrderSum = await checkOrderSum(ord.OrderItems);
                if (ord.OrderSum != order.OrderSum)
                {
                    throw new Exception("Order sum must is incorrect.");
                }
                Order res = await _orderRepository.AddOrder(ord);
                OrderDTO orderDTO = _mapper.Map<Order, OrderDTO>(res);
                return orderDTO;
            }
            catch (Exception ex)
            {
                throw new Exception("Error while adding order: " + ex.Message);
            } 
        }

        private async Task<int> checkOrderSum(ICollection<OrderItem> orderItems)
        {
            int sum = 0;
            foreach (var item in orderItems)
            {
                ProductDTO product = await _productService.GetProductById(item.ProductId);
                sum += (int)(item.Quantity * product.Price);
            }
            return sum;
        }
    }
}
