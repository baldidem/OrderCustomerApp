using System.ComponentModel.DataAnnotations;

namespace OrdersService.OrderModels
{
    public class Customer
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
