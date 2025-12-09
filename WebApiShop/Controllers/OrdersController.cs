using Entities;
using Microsoft.AspNetCore.Mvc;
using Service;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _iOrderService;

        public OrdersController(IOrderService orderService)
        {
            _iOrderService = orderService;
        }

        // GET api/<OrdersController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetById(int id)
        {
            Order orderResult = await _iOrderService.GetById(id);
            if (orderResult == null)
                return NoContent();
            return Ok(orderResult);
        }

        // POST api/<OrdersController>
        [HttpPost]
        public async Task<ActionResult<Order>> Post([FromBody] Order order)
        {
            Order orderResult = await _iOrderService.AddOrder(order);
            return CreatedAtAction(nameof(GetById), new { id = orderResult.OrderId }, orderResult);
        }
    }
}
