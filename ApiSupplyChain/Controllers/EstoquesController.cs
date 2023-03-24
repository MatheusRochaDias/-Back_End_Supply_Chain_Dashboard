using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiSupplyChain.Data;
using ApiSupplyChain.Migrations;
using Newtonsoft.Json;

namespace ApiSupplyChain.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstoquesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EstoquesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Estoques
        [HttpGet(Name = "GetEstoque")]
        public async Task<ActionResult<IEnumerable<Estoque>>> GetEstoque([FromQuery] string? name = null, int page = 1, int pageSize = 10)
        {

            var estoques = _context.Estoque.AsQueryable();


            if (!string.IsNullOrEmpty(name))
            {
                estoques = estoques.Where(p => p.Produto.Name.Contains(name));
            }

            var totalEstoque = estoques.Count();

            var response = from estoque in estoques
                           orderby estoque.Produto.Register_Number descending
                           select new
                           {
                               Id = estoque.Id,
                               Produto = _context.Produtos.SingleOrDefault(p => p.Id == estoque.ProdutoId),
                               Quantidade = estoque.Quantidade,
                               Local = estoque.Local,
                           };



            var paginatedResponse = response.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            var totalPages = (int)Math.Ceiling((double)totalEstoque / pageSize);

            var metadata = new
            {
                totalCount = totalEstoque,
                pageSize = pageSize,
                currentPage = page,
                totalPages = totalPages,
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            var data = new
            {
                paginatedData = paginatedResponse,
                metaData = metadata,
            };


            return Ok(data);

        }

        // GET: api/Estoques/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Estoque>> GetEstoque(int id)
        {
          if (_context.Estoque == null)
          {
              return NotFound();
          }
            var estoque = await _context.Estoque.FindAsync(id);

            if (estoque == null)
            {
                return NotFound();
            }

            return estoque;
        }

        // PUT: api/Estoques/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEstoque(int id, Estoque estoque)
        {
            if (id != estoque.Id)
            {
                return BadRequest();
            }

            _context.Entry(estoque).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EstoqueExists(id))
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

        // POST: api/Estoques
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Estoque>> PostEstoque(Estoque estoque)
        {
            var auxEstoque= new Estoque();
            auxEstoque.Local = estoque.Local;
            auxEstoque.Quantidade = estoque.Quantidade;

            if (await _context.Estoque.AnyAsync(p => p.ProdutoId == estoque.ProdutoId))
            {
                return Conflict("Já existe um produto com esse número de registro.");
            }

            if (_context.Estoque == null)
          {
              return Problem("Entity set 'AppDbContext.Estoque'  is null.");
          }

            _context.Estoque.Add(auxEstoque);
            _context.SaveChanges();

            var estoqueTotal = _context.Estoque.SingleOrDefault(e => e.Produto == null);
            estoqueTotal.Produto = estoque.Produto;

            _context.SaveChanges();


            return Ok("Concluido");
        }

        // DELETE: api/Estoques/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEstoque(int id)
        {
            if (_context.Estoque == null)
            {
                return NotFound();
            }
            var estoque = await _context.Estoque.FindAsync(id);
            if (estoque == null)
            {
                return NotFound();
            }

            _context.Estoque.Remove(estoque);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EstoqueExists(int id)
        {
            return (_context.Estoque?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
