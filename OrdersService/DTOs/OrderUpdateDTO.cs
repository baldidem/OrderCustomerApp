namespace OrdersService.DTOs
{
    public class OrderUpdateDTO
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public bool Status { get; set; }
        public Guid AddressId { get; set; }
        public Guid ProductId { get; set; }
    }
}
