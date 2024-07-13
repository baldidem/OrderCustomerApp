using System.ComponentModel.DataAnnotations;

namespace CustomersService.Models
{
    public class Address
    {
        public Guid Id { get; set; }
        public string AddressLine { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public int CityCode { get; set; }
    }
}
