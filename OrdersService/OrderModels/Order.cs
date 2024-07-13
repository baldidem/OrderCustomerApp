namespace OrdersService.OrderModels
{
    public class Order
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public bool Status { get; set; }
        public Guid AddressId { get; set; }
        public Guid ProductId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Address Address { get; set; }
        public Product Product { get; set; }
        public Customer Customer { get; set; }
    }
}
