using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class OrderRepository : IOrderRepository
    {
        ShopContext _dbContext;

        public OrderRepository(ShopContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Order> GetOrderById(int id)
        {
            return await _dbContext.Orders.Include(o => o.OrderItems).FirstOrDefaultAsync(oo => oo.OrderId == id);
        }
        public async Task<Order> AddOrder(Order order)
        {
            await _dbContext.Orders.AddAsync(order);
            await _dbContext.SaveChangesAsync();
            //return await _dbContext.FindAsync<Order>(order.OrderId);
            return order;
        }
    }
}
