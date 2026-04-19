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

    string FirstName,

    string LastName,

    [EmailAddress(ErrorMessage = "the input must be in email format ... @ . "), Required]
    string Email,

    string Password
    );
}
