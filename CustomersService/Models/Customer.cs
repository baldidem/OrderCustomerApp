using System.ComponentModel.DataAnnotations;

namespace CustomersService.Models
{
    public class Customer
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public Guid AddressId { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        [Required]
        public DateTime UpdatedAt { get; set; }
        public Address Address { get; set; }
    }
}
