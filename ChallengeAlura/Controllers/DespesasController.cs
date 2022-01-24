using ChallengeAlura.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ChallengeAlura.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DespesasController : ControllerBase
    {
        private readonly ProjetoDbContext _context;

        public DespesasController(ProjetoDbContext context)
        {
            _context = context;
        }

        // GET api/despesas
        [HttpGet(Name = "GetDespesas")]
        public async Task<ActionResult<IEnumerable<Despesas>>> GetDespesas()
        {
            return await _context.Despesas.ToListAsync();
        }

        // GET api/despesas/id
        [HttpGet("{id}", Name = "GetDespesasId")]
        public async Task<ActionResult> GetDespesa([FromBody]int id)
        {
            var categoria = await _context.Despesas.FindAsync(id);

            if (categoria == null)
                return NotFound();

            return Ok(categoria);
        }

        // POST api/despesas
        [HttpPost]
        public async Task<ActionResult> PostDespesa([FromBody] Despesas despesa)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (despesa == null)
            {
                return NotFound();
            }

            if (despesa.Id != 0)
            {
                if (despesa.Valor <= 0) { throw new ArgumentException("Campo valor deve ser maior que zero!"); }
                if (despesa.Data == new DateTime()) { throw new ArgumentException("Data não deve ser nula!"); } 
                if (despesa.Descricao == "") { throw new ArgumentException("Adicione uma descrição!"); }


                var novaDespesa = new Despesas
                {
                    Data = despesa.Data,
                    Descricao = despesa.Descricao,
                    Valor = despesa.Valor,
                };

                List<Despesas>? todasDespesas = _context.Despesas.ToList();
#pragma warning disable CS8629 // O tipo de valor de nulidade pode ser nulo.
                var query = todasDespesas.Where(r => r.Data.Value.Month == despesa.Data.Value.Month);
#pragma warning restore CS8629 // O tipo de valor de nulidade pode ser nulo.
                var descricoesdoMes = query.Select(d => d.Descricao).ToList();

                if (descricoesdoMes.Contains(despesa.Descricao))
                {
                    throw new ArgumentException("Já existe uma despesa com essa descrição neste mês!");
                }

                try
                {
                    await _context.Despesas.AddAsync(novaDespesa);
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }

                return await GetDespesa(despesa.Id);
            }
            throw new ArgumentException("ID não deve ser nulo");
        }

        // DELETE api/despesas
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDespesas(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var idDespesa = await _context.Despesas.FindAsync(id);
            if (idDespesa == null)
            {
                return NotFound();
            }

            try
            {
                _context.Despesas.Remove(idDespesa);
                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }

            return NoContent();
        }

        // PUT api/receitas/id
        [HttpPut("{id}")]
        public async Task<ActionResult> PutDespesa(int id, Despesas despesa)
        {
            if (id != despesa.Id)
            {
                return BadRequest();
            }

            var despesaItem = await _context.Despesas.FindAsync(id);
            if (despesaItem == null)
                return NotFound();

            despesaItem.Descricao = despesa.Descricao;
            despesaItem.Valor = despesa.Valor;
            despesaItem.Data = despesa.Data;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex) when (!itemExiste(id))
            {
                return NotFound(ex);
            }
            return Ok();
        }
        private bool itemExiste(int id)
        {
            return _context.Despesas.Any(i => i.Id == id);
        }
    }
}
