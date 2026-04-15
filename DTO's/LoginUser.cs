using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_s
{
    public class LoginUser
    {
        [EmailAddress (ErrorMessage = "Email must be a valid email adress"),Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}


