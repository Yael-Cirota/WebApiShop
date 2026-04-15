using System.ComponentModel.DataAnnotations;

namespace DTO_s
{
    public record UserDTO(

        int Id,

        string FirstName,

        string LastName,
             
        [EmailAddress, Required]
        string Email
    );
}
