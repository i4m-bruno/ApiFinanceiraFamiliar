using ChallengeAlura.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public async Task<ActionResult<Despesas>> GetDespesa(int id)
        {
            var categoria = await _context.Despesas.FindAsync(id);

            if (categoria == null)
                return NotFound();

            return Ok(categoria);
        }
     }
}
