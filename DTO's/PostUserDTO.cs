using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_s
{
    public record PostUserDTO(

    int Id,

    [StringLength(16,ErrorMessage ="the input length must be less than 16 digits")]
    string FirstName,

    string LastName,

    [EmailAddress(ErrorMessage = "the input must be in email format ... @ . "), Required]
    string Email,

    string Password
    );
}
