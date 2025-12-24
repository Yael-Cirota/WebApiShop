using DTO_s;
using Entities;

namespace Service
{
    public interface IOrderService
    {
        Task<OrderDTO> AddOrder(OrderDTO order);
        Task<OrderDTO> GetById(int id);
    }
}