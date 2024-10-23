using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductsAPI.DTO;
using ProductsAPI.Models;

namespace ProductsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ContextDataBase _context;

        public ProductsController(ContextDataBase context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {    
            var products =  await _context.Products.Select(p => ProductToDTO(p)).ToListAsync();
            return Ok(products);
        }
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetProduct(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var p = await _context.Products.Where(i => i.ProductID == id).Select(p => ProductToDTO(p)).FirstOrDefaultAsync();

            if (p == null)
            {
                return NotFound();
            }
            return Ok(p);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(Product entity)
        {
            _context.Products.Add(entity);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(CreateProduct), new {id = entity.ProductID}, entity);
        } 
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, Product entity)
        {
            if(id != entity.ProductID)
            {
                return BadRequest();
            }

            var product = await _context.Products.FirstOrDefaultAsync(i => i.ProductID == id);

            if(product == null)
            {
                return NotFound();
            }

            product.ProductName = entity.ProductName;
            product.CategoryID = entity.CategoryID;
            product.QuantityPerUnit = entity.QuantityPerUnit;
            product.UnitPrice = entity.UnitPrice;

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

        public async Task<IActionResult> DeleteProduct(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FirstOrDefaultAsync(i => i.ProductID == id);

            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);

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
        private static ProductDTO ProductToDTO(Product p)
        {
            var entity = new ProductDTO();
            if (p != null)
            {
                entity.ProductID = p.ProductID;
                entity.ProductName = p.ProductName;
                entity.QuantityPerUnit = p.QuantityPerUnit;
                entity.UnitPrice = p.UnitPrice;
            }
            return entity;
        }
    }
}
