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
using System.Drawing;

namespace ApiSupplyChain.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovimentacaoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MovimentacaoController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Movimentacao/5
        [HttpGet(Name = "GetMovimentações")]
        public async Task<ActionResult<IEnumerable<Movimentacao>>> GetMovimentacao([FromQuery] string? name = null, int? filter = null, int page = 1, int? pageSize = null, int? month = null, int? year = null)
        {

            var movimentacoes = _context.Movimentacao.AsQueryable();


            if (!string.IsNullOrEmpty(name))
            {
                movimentacoes = movimentacoes.Where(p => p.Produto.Name.Contains(name));
            }

            if (filter.HasValue)
            {
                string filterStr = filter.Value.ToString();
                movimentacoes = movimentacoes.Where(p => p.Produto.Register_Number.ToString().Contains(filterStr));
            }

            if (month.HasValue && year.HasValue)
            {
                var startDate = new DateTime(year.Value, month.Value, 1);
                var endDate = startDate.AddMonths(1).AddDays(-1);
                movimentacoes = movimentacoes.Where(p => p.DataEvento >= startDate && p.DataEvento <= endDate);
            }

            var totalMovimentacoes = movimentacoes.Count();

            var response = from movimentacao in movimentacoes
                           join estoque in _context.Estoque on movimentacao.Produto.Id equals estoque.ProdutoId
                           orderby movimentacao.Produto.Register_Number descending
                           select new
                           {
                               Id = movimentacao.Id,
                               Produto = _context.Produtos.SingleOrDefault(p => p.Id == movimentacao.Produto.Id),                      
                               DataEvento = movimentacao.DataEvento,
                               TipoMovimentacao = movimentacao.TipoMovimentacao,
                               Local=movimentacao.Local,
                               QuantidadeMovimentada= movimentacao.QuantidadeMovimentada,
                               Quantidade = estoque.Quantidade
                           };


            var pageSizeOrDefault = pageSize ?? int.MaxValue;


            var paginatedResponse = response.Skip((page - 1) * pageSizeOrDefault).Take(pageSizeOrDefault).ToList();

            var totalPages = (int)Math.Ceiling((double)totalMovimentacoes / pageSizeOrDefault);

           
            var metadata = new
            {
                totalCount = totalMovimentacoes,
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

       




        // PUT: api/Movimentacao/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovimentacao(int id, Movimentacao movimentacao)
        {
            if (id != movimentacao.Id)
            {
                return BadRequest();
            }

            _context.Entry(movimentacao).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovimentacaoExists(id))
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

        // POST: api/Movimentacao
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Movimentacao>> PostMovimentacao(Movimentacao movimentacao)
        {
            var auxMovimentacao = new Movimentacao();
            auxMovimentacao.QuantidadeMovimentada = movimentacao.QuantidadeMovimentada;
            auxMovimentacao.TipoMovimentacao = movimentacao.TipoMovimentacao;
            auxMovimentacao.Local = movimentacao.Local ;
            auxMovimentacao.DataEvento = movimentacao.DataEvento;
          if (_context.Movimentacao == null)
          {
              return Problem("Entity set 'AppDbContext.Movimentacao'  is null.");
          }
            _context.Movimentacao.Add(auxMovimentacao);
            _context.SaveChanges();

            var movimentacaoFeita = _context.Movimentacao.SingleOrDefault(m => m.DataEvento == auxMovimentacao.DataEvento && auxMovimentacao.Local == m.Local);
            movimentacaoFeita.Produto = movimentacao.Produto;

            _context.SaveChanges();

            var i = 1;

            if(auxMovimentacao.TipoMovimentacao == "Entrada")
            {
                i = 1;
            }
            else { i = -1; }
            var auxEstoque = new Estoque();
            var insercao = _context.Estoque.SingleOrDefault(m => m.Produto == auxMovimentacao.Produto);
            insercao.Quantidade = insercao.Quantidade + i * auxMovimentacao.QuantidadeMovimentada;

            _context.SaveChanges();

            return Ok(movimentacaoFeita);         
            
        }

        // DELETE: api/Movimentacao/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovimentacao(int id)
        {
            if (_context.Movimentacao == null)
            {
                return NotFound();
            }
            var movimentacao = await _context.Movimentacao.FindAsync(id);
            if (movimentacao == null)
            {
                return NotFound();
            }

            _context.Movimentacao.Remove(movimentacao);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MovimentacaoExists(int id)
        {
            return (_context.Movimentacao?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
