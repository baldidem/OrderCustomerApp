using System.ComponentModel.DataAnnotations;

namespace OrdersService.OrderModels
{
    public class Product
    {
        public Guid Id { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
