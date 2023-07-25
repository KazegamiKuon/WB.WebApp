using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WB.WebApp.Data;
using WB.WebApp.Models;
using WB.WebApp.Utils;

namespace WB.WebApp.Controllers.ApiController
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly InterviewContext _context;

        public ProductsController(InterviewContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("CreateDomy")]
        public async Task<ActionResult<Boolean>> CreateDomy()
        {
            List<Product> data = new List<Product>();
            for (int i = 0; i < 3000; i++)
            {
                var numberS = i.ToString("000#");
                data.Add(new Product()
                {
                    Id = 0,
                    Name = $"Product {numberS}",
                    Price = (20000+i).ToString(),
                });
            }
            await _context.Products.AddRangeAsync(data);
            await _context.SaveChangesAsync();
            return Ok(true);
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductViewModel>>> GetProducts()
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            var data = await _context.Products.ToListAsync();
            return data.ToListViewModel();
        }

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Boolean>> PostProduct(ProductViewModel[] data)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'InterviewContext.Products'  is null.");
            }
            var products = data.Where(d=>d.IsNew && !d.IsDeleted).ToList().ToListModel();
            await _context.Products.AddRangeAsync(products);
            await _context.SaveChangesAsync();

            return Ok(true);
        }

        private bool ProductExists(int id)
        {
            return (_context.Products?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
