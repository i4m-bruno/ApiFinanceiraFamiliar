using ChallengeAlura.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChallengeAlura.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReceitasController : ControllerBase
    {
        private readonly ProjetoDbContext _context;

        public ReceitasController(ProjetoDbContext context)
        {
            _context = context;
        }

        // GET api/receitas
        [HttpGet(Name = "GetReceitas")]
        public async Task<ActionResult<IEnumerable<Receitas>>> GetReceitas()
        {
            return await _context.Receitas.ToListAsync();
        }


        //GET api/receitas/id
        [HttpGet("{id}", Name = "GetReceitasId")]
        public async Task<ActionResult> GetReceita([FromBody] int id)
        {
            var receita = await _context.Receitas.FindAsync(id);

            if (receita == null)
                return NotFound();

            return Ok(receita);
        }

        // POST api/receitas
        [HttpPost]
        public async Task<ActionResult> PostReceita([FromBody] Receitas receita)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (receita == null)
            {
                throw new ArgumentNullException(nameof(receita));
            }

            if (receita.Id != 0)
            {
                if (receita.Data == new DateTime()) { throw new ArgumentException(nameof(receita.Data)); }
                if (receita.Valor <= 0) { throw new ArgumentException(nameof(receita.Valor)); }
                if (receita.Descricao == "") { throw new ArgumentException(nameof(receita.Descricao)); }

                var novaReceita = new Receitas
                {
                    Data = receita.Data,
                    Valor = receita.Valor,
                    Descricao = receita.Descricao,
                };

                List<Receitas>? todasReceitas = _context.Receitas.ToList();
#pragma warning disable CS8629 // O tipo de valor de nulidade pode ser nulo.
                var query = todasReceitas.Where(r => r.Data.Value.Month == receita.Data.Value.Month);
#pragma warning restore CS8629 // O tipo de valor de nulidade pode ser nulo.
                var descricoesdoMes = query.Select(d => d.Descricao).ToList();

                if (descricoesdoMes.Contains(receita.Descricao))
                {
                    throw new ArgumentException("Já existe uma despesa com essa descrição neste mês!");
                }

                try
                {
                    await _context.Receitas.AddAsync(novaReceita);
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }

                return await GetReceita(receita.Id);
            }
            throw new ArgumentException("Id não deve ser nulo");
        }

        // DELETE api/receitas
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteReceita(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var idReceita = await _context.Receitas.FindAsync(id);
            if (idReceita == null)
            {
                return NotFound();
            }

            try
            {
                _context.Receitas.Remove(idReceita);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

            return NoContent();

        }

        // PUT api/receitas/id
        [HttpPut("{id}")]
        public async Task<ActionResult> PutReceita(int id, Receitas receita)
        {
            if (id != receita.Id)
            {
                return BadRequest();
            }

            var receitaitem = await _context.Receitas.FindAsync(id);
            if (receitaitem == null)
                return NotFound();

            receitaitem.Descricao = receita.Descricao;
            receitaitem.Valor = receita.Valor;
            receitaitem.Data = receita.Data;

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
            return _context.Receitas.Any(i => i.Id == id);
        }
    }
}
