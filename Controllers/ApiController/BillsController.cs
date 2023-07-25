using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WB.WebApp.Data;
using WB.WebApp.Models;
using WB.WebApp.Utils;

namespace WB.WebApp.Controllers.ApiController
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillsController : ControllerBase
    {
        private readonly InterviewContext _context;

        public BillsController(InterviewContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("CreateDomy")]
        public async Task<ActionResult<Boolean>> CreateDomy()
        {
            List<Bill> data = new List<Bill>();
            var productIds = await _context.Products.Select(p=>p.Id).ToListAsync();
            var shopIds = await _context.Shops.Select(p => p.Id).ToListAsync();
            var customerIds = await _context.Customers.Select(p => p.Id).ToListAsync();
            var numberProduct = productIds.Count();
            var numberShop = shopIds.Count();
            var numberCustomer = customerIds.Count();
            for (int i = 0; i < numberProduct; i++)
            {
                var numberS = i.ToString("000#");
                data.Add(new Bill()
                {
                    Id = 0,
                    ProductId = productIds[i],
                    CustomerId = customerIds[(int)i%numberCustomer],
                    ShopId = shopIds[(int)i%numberShop],
                });
            }
            await _context.Bills.AddRangeAsync(data);
            await _context.SaveChangesAsync();
            return Ok(true);
        }

        // GET: api/Bills
        [HttpGet]
        public async Task<ActionResult<BillResponseData>> GetBills()
        {
            if (_context.Bills == null)
            {
                return NotFound();
            }
            var bills = await _context.Bills.Include(b=>b.Customer)
                                            .Include(b=>b.Product)
                                            .Include(b=>b.Shop)
                                            .OrderBy(b=>b.Customer.Email)
                                            .ThenByDescending(b=>b.Shop.Location)
                                            .ToListAsync();
            var customerIds = bills.Select(b=>b.CustomerId).Distinct();
            var shopIds = bills.Select(b => b.ShopId).Distinct();
            var productIds = bills.Select(b => b.ProductId).Distinct();
            if (customerIds.Count()<30 || shopIds.Count() < 3 || productIds.Count() < 3000)
            {
                return Ok(new BillResponseData()
                {
                    IsError = true,
                });
            }
            return Ok(new BillResponseData()
            {
                IsError = false,
                Bills = bills.ToListViewModel().ToList(),
            });
        }
    }
}
