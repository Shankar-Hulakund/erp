using erp_sql.Data;
using erp_sql.DTO;
using erp_sql.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using erp_sql.DTO;


namespace erp_sql.Controllers.Customer
{
    [ApiController]
    [Route("api/customer")]
    public class CustomerController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CustomerController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("getAllcustomers")]
        public async Task<IActionResult> GetCustomer()
        {
            var customers = await _context.Customers.ToListAsync();
            return Ok(customers);
        }

        [HttpGet("getcustomers/{Custid}")]
        public async Task<IActionResult> GetCustomerId(int Custid)
        {
            var customer = await _context.Customers.FindAsync(Custid);
            if (customer == null)
                return NotFound();
            return Ok(customer);
        }

        [HttpPost("createCustomer")]
        public async Task<IActionResult> createCustomer([FromBody] CustomerDto customerDto)
        {
            var newCustomer = new Customers
            {
                CustName = customerDto.CustName
            };
            _context.Customers.Add(newCustomer);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Customer created successfully." });
        }

        //[HttpPut("updateCustomerId/{Custid}")]
        //public async Task<IActionResult> updateCustomerId(int Custid, updateCustomerDto updateCustomerDto)
        //{

        //    if (Custid != updateCustomerDto.Custid)
        //        return BadRequest("ID in URL and body do not match.");

        //}

    }
}
