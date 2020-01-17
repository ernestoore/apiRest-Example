using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiciosRestApp.Models;

namespace ServiciosRestApp.Controllers
{
    [Produces("application/json")]
    [Route("api/Productos")]
    public class ProductosController : Controller
    {
        private readonly EmpresaContext _context;

        public ProductosController(EmpresaContext context)
        {
            _context = context;
        }

        // GET: api/Productos
        [HttpGet]
        public IEnumerable<Productos> GetProductos()
        {
            return _context.Productos;
        }

        // GET: api/Productos/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductos([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var productos = await _context.Productos.SingleOrDefaultAsync(m => m.Id == id);

            if (productos == null)
            {
                return NotFound();
            }

            return Ok(productos);
        }

        // PUT: api/Productos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductos([FromRoute] int id, [FromBody] Productos productos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != productos.Id)
            {
                return BadRequest();
            }

            _context.Entry(productos).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductosExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Productos
        [HttpPost]
        public async Task<IActionResult> PostProductos([FromBody] Productos productos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Productos.Add(productos);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProductos", new { id = productos.Id }, productos);
        }

        // DELETE: api/Productos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductos([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var productos = await _context.Productos.SingleOrDefaultAsync(m => m.Id == id);
            if (productos == null)
            {
                return NotFound();
            }

            _context.Productos.Remove(productos);
            await _context.SaveChangesAsync();

            return Ok(productos);
        }

        private bool ProductosExists(int id)
        {
            return _context.Productos.Any(e => e.Id == id);
        }
    }
}