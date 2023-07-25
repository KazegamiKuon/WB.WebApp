using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WB.WebApp.Data;
using WB.WebApp.Models;
using WB.WebApp.Utils;

namespace WB.WebApp.Controllers.ApiController
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly InterviewContext _context;

        public CustomersController(InterviewContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("CreateDomy")]
        public async Task<ActionResult<Boolean>> CreateDomy()
        {
            List<Customer> data = new List<Customer>();
            for(int i=0;i<30;i++)
            {
                var numberS = i.ToString("000#");
                data.Add(new Customer()
                {
                    Id = 0,
                    Name = $"Customer {numberS}",
                    Email = $"customer{numberS}@gmail.com",
                    Dob = DateTime.Now
                });
            }
            await _context.Customers.AddRangeAsync(data);
            await _context.SaveChangesAsync();
            return Ok(true);
        }

        // GET: api/Customers
        [HttpGet]        
        public async Task<ActionResult<IEnumerable<CustomerViewModel>>> GetCustomers()
        {
            if (_context.Customers == null)
            {
                return NotFound();
            }
            var customers = await _context.Customers.ToListAsync();
            return Ok(customers.ToListViewModel());
        }

        // POST: api/Customers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Boolean>> PostCustomer(CustomerViewModel[] data)
        {
            if (_context.Customers == null)
            {
                return Problem("Entity set 'InterviewContext.Customers'  is null.");
            }
            var customers = data.Where(data=>data.IsNew && !data.IsDeleted).ToList().ToListModel();
            _context.Customers.AddRange(customers);
            await _context.SaveChangesAsync();

            return Ok(true);
        }

        private bool CustomerExists(int id)
        {
            return (_context.Customers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
