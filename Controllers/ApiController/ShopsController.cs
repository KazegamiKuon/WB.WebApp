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
    public class ShopsController : ControllerBase
    {
        private readonly InterviewContext _context;

        public ShopsController(InterviewContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("CreateDomy")]
        public async Task<ActionResult<Boolean>> CreateDomy()
        {
            List<Shop> data = new List<Shop>();
            for (int i = 0; i < 3; i++)
            {
                var numberS = i.ToString("000#");
                data.Add(new Shop()
                {
                    Id = 0,
                    Name = $"Shop {numberS}",
                    Location = "Viet Nam",
                });
            }
            await _context.Shops.AddRangeAsync(data);
            await _context.SaveChangesAsync();
            return Ok(true);
        }

        // GET: api/Shops
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShopViewModel>>> GetShops()
        {
            if (_context.Shops == null)
            {
                return NotFound();
            }
            var shops = await _context.Shops.ToListAsync();
            return shops.ToListViewModel();
        }

        // POST: api/Shops
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Boolean>> PostShop(ShopViewModel[] data)
        {
            if (_context.Shops == null)
            {
                return Problem("Entity set 'InterviewContext.Shops'  is null.");
            }
            var shops = data.Where(d=>d.IsNew && !d.IsDeleted).ToList().ToListModel();
            await _context.Shops.AddRangeAsync(shops);
            await _context.SaveChangesAsync();

            return Ok(true);
        }

        private bool ShopExists(int id)
        {
            return (_context.Shops?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
