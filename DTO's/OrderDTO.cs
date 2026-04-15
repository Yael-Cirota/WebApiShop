using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_s
{
    public record OrderDTO(
        int OrderId,

        DateOnly? OrderDate,

        [Range(minimum: 0, maximum:10000)]
        int OrderSum,

        ICollection<OrderItemDTO> OrderItems,

        int UserId
    );
}
