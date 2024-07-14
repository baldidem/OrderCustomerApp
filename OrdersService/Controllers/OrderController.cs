using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrdersService.Data;
using OrdersService.DTOs;
using OrdersService.OrderModels;

namespace OrdersService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderDbContext _context;

        public OrderController(OrderDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<ResponseModel<List<Order>>> GetOrders()
        {
            var orders = _context.Orders.Include(x => x.Address)
                                        .Include(x => x.Product)
                                        .ToList();

            return Ok(new ResponseModel<List<Order>>
            {
                Success = true,
                Message = "",
                Data = orders
            });
        }

        [HttpGet("{id}")]
        public ActionResult<ResponseModel<Order>> GetOrder(Guid id)
        {
            var order = _context.Orders.Where(x => x.Id == id)
                .Include(x => x.Address)
                .Include(x => x.Product)
                .FirstOrDefault();

            if (order == null)
            {
                return NotFound(new ResponseModel<Order> { Success = false, Message = "Order not found", Data = new() });
            }

            return Ok(new ResponseModel<Order> { Success = true, Message = "Order retrieved successfully", Data = order });
        }

        [HttpPost]
        public ActionResult<ResponseModel<Order>> PostOrder([FromBody] OrderCreateDTO orderDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ResponseModel<Order> { Success = false, Message = "Model is not valid", Data = new() });
            }


            var addressCheck = _context.Addresses.Where(x => x.Id == orderDto.AddressId).FirstOrDefault();

            if (addressCheck == null)
            {
                return BadRequest(new ResponseModel<Order> { Success = false, Message = "AddressId is not found in the database", Data = new() });
            }

            var productCheck = _context.Products.Where(x => x.Id == orderDto.ProductId).FirstOrDefault();

            if (productCheck == null)
            {
                return BadRequest(new ResponseModel<Order> { Success = false, Message = "ProductId is not found in the database", Data = new() });
            }

            var customerCheck = _context.Customers.Where(x => x.Id == orderDto.CustomerId).FirstOrDefault();

            if (customerCheck == null)
            {
                return BadRequest(new ResponseModel<Order> { Success = false, Message = "CustomerId is not found in the database", Data = new() });
            }

            var order = new Order
            {
                CustomerId = orderDto.CustomerId,
                Quantity = orderDto.Quantity,
                Price = orderDto.Price,
                Status = orderDto.Status,
                AddressId = orderDto.AddressId,
                ProductId = orderDto.ProductId,
                CreatedAt = DateTime.Now
            };

            _context.Orders.Add(order);
            _context.SaveChanges();
            return Ok(new ResponseModel<Order> { Success = true, Message = "Order created successfully", Data = new() });
        }

        [HttpPut()]
        public ActionResult<ResponseModel<Order>> PutOrder([FromBody] OrderUpdateDTO orderDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ResponseModel<Order> { Success = false, Message = "Model is not valid", Data = new() });
            }

            var existingOrder = _context.Orders.Where(x => x.Id == orderDto.Id).FirstOrDefault(); ;

            if (existingOrder == null)
            {
                return NotFound(new ResponseModel<Order> { Success = false, Message = "Order not found", Data = new() });
            }

            var addressCheck = _context.Addresses.Where(x => x.Id == orderDto.AddressId).FirstOrDefault();

            if (addressCheck == null)
            {
                return BadRequest(new ResponseModel<Order> { Success = false, Message = "AddressId is not found in the database", Data = new() });
            }

            var productCheck = _context.Products.Where(x => x.Id == orderDto.ProductId).FirstOrDefault();

            if (productCheck == null)
            {
                return BadRequest(new ResponseModel<Order> { Success = false, Message = "ProductId is not found in the database", Data = new() });
            }

            var customerCheck = _context.Customers.Where(x => x.Id == orderDto.CustomerId).FirstOrDefault();

            if (customerCheck == null)
            {
                return BadRequest(new ResponseModel<Order> { Success = false, Message = "CustomerId is not found in the database", Data = new() });
            }

            existingOrder.CustomerId = orderDto.CustomerId;
            existingOrder.Quantity = orderDto.Quantity;
            existingOrder.Price = orderDto.Price;
            existingOrder.Status = orderDto.Status;
            existingOrder.AddressId = orderDto.AddressId;
            existingOrder.ProductId = orderDto.ProductId;
            existingOrder.UpdatedAt = DateTime.Now;

            _context.Orders.Update(existingOrder);
            _context.SaveChanges();

            return Ok(new ResponseModel<Order>
            {
                Success = true,
                Message = "Order updated successfully",
                Data = new()
            });
        }

        [HttpDelete("{id}")]
        public ActionResult<ResponseModel<Order>> DeleteOrder(Guid id)
        {
            var order = _context.Orders.Where(x => x.Id == id).FirstOrDefault();

            if (order == null)
            {
                return NotFound(new ResponseModel<Order> { Success = false, Message = "Order not found", Data = new() });
            }

            _context.Orders.Remove(order);
            _context.SaveChanges();

            return Ok(new ResponseModel<Order>
            {
                Success = true,
                Message = "Order deleted successfully",
                Data = new()
            });
        }
    }
}

