using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Customer
    {
        public Guid Id { get; init; }
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }

    }
}