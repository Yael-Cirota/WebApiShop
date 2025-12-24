using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_s
{
    public record OrderDTO(
        int OrderId,
        DateOnly? OrderDate,
        int OrderSum,
        //ICollection<OrderItemDTO> OrderItems,
        int UserId
    );
}
