using System.ComponentModel.DataAnnotations;

namespace WebApiShop
{
    public class User
    {
        public int Id { get; set; }

        [EmailAddress, Required]
        public string Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        [StringLength(8, ErrorMessage = "Password must be between 4 and 8 chars", MinimumLength = 4), Required]
        public string Password { get; set; }

        public User() { }

        public User(string Email, string firstName, string lastName, string password)
        {
            this.Email = Email;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Password = password;
        }
    }


}
