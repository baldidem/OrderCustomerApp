using System.ComponentModel.DataAnnotations;

namespace CustomersService.DTOs
{
    public class CustomerUpdateDTO
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public Guid AddressId { get; set; }
    }
}
