using System.ComponentModel.DataAnnotations;

namespace CustomersService.DTOs
{
    public class CustomerCreateDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public Guid AddressId { get; set; }
    }
}
