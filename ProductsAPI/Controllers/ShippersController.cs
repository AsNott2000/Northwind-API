using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductsAPI.DTO;
using ProductsAPI.Models;

namespace ProductsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShippersController : ControllerBase
    {
        
        private readonly ContextDataBase _context;

        public ShippersController(ContextDataBase context)
        {
            _context = context;
        }

        [HttpGet]

        public async Task<IActionResult> GetShippers()
        {
            var shippers = await _context.Shippers.Select(s => ShipperToDTO(s)).ToListAsync();
            return Ok(shippers);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetShipper(int? id){
            if (id == null)
            {
                return NotFound();
            }

            var s = await _context.Shippers.Where(i => i.ShipperID == id).Select(s => ShipperToDTO(s)).FirstOrDefaultAsync();

            if (s == null)
            {
                return NotFound();
            }

            return Ok(s);
        }

        [HttpPost]
        public async Task<IActionResult> CreateShipper(Shipper entity)
        {
            _context.Shippers.Add(entity);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(CreateShipper), new {id = entity.ShipperID}, entity);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateShipper(int id, Shipper entity)
        {
            if (id != entity.ShipperID)
            {
                return BadRequest();
            }
            var shipper = await _context.Shippers.FirstOrDefaultAsync(i => i.ShipperID == id);
            
            if (shipper == null)
            {
                return NotFound();
            }

            shipper.CompanyName = entity.CompanyName;
            shipper.Phone = entity.Phone;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete]

        public async Task<IActionResult> DeleteShipper(int? id)
        {
            if (id == null)
            {
               return NotFound();
            }

            var shipper = await _context.Shippers.FirstOrDefaultAsync(i=> i.ShipperID == id);
            if (shipper == null)
            {
               return BadRequest(); 
            }
            
            _context.Shippers.Remove(shipper);
            
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                
                return NotFound();
            }
            return NoContent();
        }

        private static ShipperDTO ShipperToDTO(Shipper s)
        {
            var entity = new ShipperDTO();
            if (s != null)
            {
                entity.ShipperID = s.ShipperID;
                entity.CompanyName = s.CompanyName;
                entity.Phone = s.Phone;
            }
            return entity;
        }
    }
}