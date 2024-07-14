using CustomersService.Data;
using CustomersService.DTOs;
using CustomersService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CustomersService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {

        private readonly CustomerDbContext _context;

        public CustomerController(CustomerDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<ResponseModel<List<Customer>>> GetCustomers()
        {
            var customers = _context.Customers.Include(x => x.Address).ToList();
            return Ok(new ResponseModel<List<Customer>> { Success = true, Message = "", Data = customers });
        }

        [HttpGet("{id}")]
        public ActionResult<ResponseModel<Customer>> GetCustomer(Guid id)
        {
            var customer = _context.Customers.Include(c => c.Address).FirstOrDefault(c => c.Id == id);
            if (customer == null)
            {
                return NotFound(new ResponseModel<Customer> { Success = false, Message = "Customer not found", Data = new() });
            }
            return Ok(new ResponseModel<Customer> { Success = true, Message = "", Data = customer });
        }

        [HttpPost]
        public ActionResult<ResponseModel<Customer>> PostCustomer([FromBody] CustomerCreateDTO customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ResponseModel<Customer> { Success = false, Message = "Model is not valid", Data = new() });
            }

            var addressCheck = _context.Addresses.Where(x => x.Id == customer.AddressId).FirstOrDefault();

            if (addressCheck == null)
            {
                return BadRequest(new ResponseModel<Customer> { Success = false, Message = "AddressId is not found in the database", Data = new() });
            }

            Customer addingCustomer = new Customer()
            {
                Name = customer.Name,
                Email = customer.Email,
                AddressId = customer.AddressId,
                CreatedAt = DateTime.Now
            };

            _context.Customers.Add(addingCustomer);
            _context.SaveChanges();

            return Ok(new ResponseModel<Customer> { Success = true, Message = "Customer created successfully", Data = new() });

        }

        [HttpPut]
        public ActionResult<ResponseModel<Customer>> PutCustomer([FromBody] CustomerUpdateDTO customerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ResponseModel<Customer>
                {
                    Success = false,
                    Message = "Model is not valid",
                    Data = new()
                });
            }

            var existingCustomer = _context.Customers.Where(x => x.Id == customerDto.Id).FirstOrDefault();

            if (existingCustomer == null)
            {
                return NotFound(new ResponseModel<Customer>
                {
                    Success = false,
                    Message = "Customer not found",
                    Data = new()
                });
            }

            var addressCheck = _context.Addresses.Where(x => x.Id == customerDto.AddressId).FirstOrDefault();

            if (addressCheck == null)
            {
                return BadRequest(new ResponseModel<Customer> { Success = false, Message = "AddressId is not found in the database", Data = new() });
            }

            existingCustomer.Name = customerDto.Name;
            existingCustomer.Email = customerDto.Email;
            existingCustomer.AddressId = customerDto.AddressId;
            existingCustomer.UpdatedAt = DateTime.Now;

            _context.Customers.Update(existingCustomer);
            _context.SaveChanges();

            return Ok(new ResponseModel<Customer>
            {
                Success = true,
                Message = "Customer updated successfully",
                Data = new()
            });
        }

        [HttpDelete("{id}")]
        public ActionResult<ResponseModel<Customer>> DeleteCustomer(Guid id)
        {
            var customer = _context.Customers.Where(x => x.Id == id).FirstOrDefault();
            if (customer == null)
            {
                return NotFound(new ResponseModel<Customer>
                {
                    Success = false,
                    Message = "Customer not found",
                    Data = new()
                });
            }

            _context.Customers.Remove(customer);
            _context.SaveChanges();

            return Ok(new ResponseModel<Customer>
            {
                Success = true,
                Message = "Customer deleted successfully",
                Data = new()
            });
        }
    }
}
