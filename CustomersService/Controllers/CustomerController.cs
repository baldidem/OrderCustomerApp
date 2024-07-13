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

        //public ActionResult<Customer> GetCustomers()
        //{
        //    var customers = _context.Customers.Include(x=>x.Address).ToList();
        //    return 
        //}

        [HttpGet]
        public ActionResult<ResponseModel<IEnumerable<Customer>>> GetCustomers()
        {
            var customers = _context.Customers.Include(x => x.Address).ToList();
            return Ok(new ResponseModel<IEnumerable<Customer>>
            {
                Success = true,
                Message = "",
                Data = customers
            });
        }

        [HttpGet("{id}")]
        public ActionResult<ResponseModel<Customer>> GetCustomer(Guid id)
        {
            var customer = _context.Customers.Include(c => c.Address).FirstOrDefault(c => c.Id == id);
            if (customer == null)
            {
                return NotFound(new ResponseModel<Customer>
                {
                    Success = false,
                    Message = "Customer not found",
                    Data = null
                });
            }
            return Ok(new ResponseModel<Customer>
            {
                Success = true,
                Message = "",
                Data = customer
            });
        }

        [HttpPost]
        public ActionResult<ResponseModel<Customer>> PostCustomer([FromBody] CustomerDTO customer)
        {
            if (!ModelState.IsValid)
            {
                //var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return BadRequest(new ResponseModel<Customer>
                {
                    Success = false,
                    Message = "Model is not appropriate",
                    Data = null
                });
            }

            //customer.Id = Guid.NewGuid();

            Customer addingCustomer = new Customer()
            {
                Id = customer.Id,
                Name = customer.Name,
                Email = customer.Email,
                AddressId = customer.AddressId,
                CreatedAt = DateTime.Now
            };

            _context.Customers.Add(addingCustomer);
            _context.SaveChanges();

            return Ok(new ResponseModel<Customer> { Success = true, Message = "Customer created successfully", Data = null });

            //return CreatedAtAction(nameof(GetCustomer), new { id = customer.Id }, new ResponseModel<Customer>
            //{
            //    Success = true,
            //    Message = "Customer created successfully",
            //    Data = customer
            //});
        }

        [HttpPut("{id}")]
        public ActionResult<ResponseModel<Customer>> PutCustomer(Guid id)
        {
            var customer = _context.Customers.Where(x => x.Id == id).FirstOrDefault();
            if (customer == null)
            {
                return BadRequest(new ResponseModel<Customer> { Success = false, Message = "Customer Not Found", Data = null });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(new ResponseModel<Customer>
                {
                    Success = false,
                    Message = "Model is not valid",
                    Data = null
                });
            }

            var existingCustomer = _context.Customers.Where(x=>x.Id == id).FirstOrDefault();
            if (existingCustomer == null)
            {
                return NotFound(new ResponseModel<Customer>
                {
                    Success = false,
                    Message = "Customer not found",
                    Data = null
                });
            }

            existingCustomer.Name = customer.Name;
            existingCustomer.Email = customer.Email;
            existingCustomer.AddressId = customer.AddressId;
            existingCustomer.UpdatedAt = DateTime.UtcNow;

            _context.Entry(existingCustomer).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(new ResponseModel<Customer>
            {
                Success = true,
                Message = "Customer updated successfully",
                Data = existingCustomer
            });
        }

        [HttpDelete("{id}")]
        public ActionResult<ResponseModel<Customer>> DeleteCustomer(Guid id)
        {
            var customer = _context.Customers.Find(id);
            if (customer == null)
            {
                return NotFound(new ResponseModel<Customer>
                {
                    Success = false,
                    Message = "Customer not found",
                    Data = null
                });
            }

            _context.Customers.Remove(customer);
            _context.SaveChanges();

            return Ok(new ResponseModel<Customer>
            {
                Success = true,
                Message = "Customer deleted successfully",
                Data = null
            });
        }
    }
}
