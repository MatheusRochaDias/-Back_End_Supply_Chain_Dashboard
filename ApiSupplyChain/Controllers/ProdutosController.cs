using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiSupplyChain.Data;
using Newtonsoft.Json;

namespace ApiSupplyChain.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProdutosController(AppDbContext context)
        {
            _context = context;
        }


        // GET: api/Produtos/name
        [HttpGet( Name = "GetProdutosByName")]
        public async Task<ActionResult<IEnumerable<Produto>>> GetProdutosByName([FromQuery]  string? name = null, int page = 1, int? pageSize = null)
        {
            var produtos = _context.Produtos.AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                produtos = produtos.Where(p => p.Manufacturer.Contains(name));
            }

            var totalProdutos = produtos.Count();

            var manufacturers = await produtos.Select(p => p.Manufacturer).Distinct().ToListAsync();


            var response = from produto in produtos
                           orderby produto.Register_Number descending
                           select new
                           {
                               Id = produto.Id,
                               Name = produto.Name,
                               RegisterNumber = produto.Register_Number,
                               Manufacturer = produto.Manufacturer,
                               Type = produto.Type,
                               Description = produto.Description
                           };


            var pageSizeOrDefault = pageSize ?? int.MaxValue;

            var paginatedResponse = response.Skip((page - 1) * pageSizeOrDefault).Take(pageSizeOrDefault).ToList();


            var totalPages = (int)Math.Ceiling((double)totalProdutos / pageSizeOrDefault);

            var metadata = new
            {
                totalCount = totalProdutos,
                pageSize = pageSizeOrDefault,
                currentPage = page,
                totalPages = totalPages,
                manufacturers = manufacturers,
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            var data = new
            {
                paginatedData = paginatedResponse,
                metaData = metadata,
            };


            return Ok(data);
        }



        // PUT: api/Produtos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduto(int id, Produto produto)
        {
            if (id != produto.Id)
            {
                return BadRequest();
            }

            _context.Entry(produto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProdutoExists(id))
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

        // POST: api/Produtos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("produtos")]
        public async Task<ActionResult<Produto>> PostProduto(Produto produto)
        {
             if (await _context.Produtos.AnyAsync(p => p.Register_Number == produto.Register_Number))
           {
              return Conflict("Já existe um produto com esse número de registro.");
            }
            if (_context.Produtos == null)
          {
              return Problem("Entity set 'AppDbContext.Produtos'  is null.");
          }
            _context.Produtos.Add(produto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProdutosByName", new { id = produto.Id }, produto);
        }

        // DELETE: api/Produtos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduto(int id)
        {
            if (_context.Produtos == null)
            {
                return NotFound();
            }
            var produto = await _context.Produtos.FindAsync(id);
            if (produto == null)
            {
                return NotFound();
            }

            _context.Produtos.Remove(produto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProdutoExists(int id)
        {
            return (_context.Produtos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
