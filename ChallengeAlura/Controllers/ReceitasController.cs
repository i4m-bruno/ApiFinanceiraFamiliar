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
        public async Task<ActionResult<Receitas>> GetReceita(int id)
        {
            var receita = await _context.Receitas.FindAsync(id);

            if (receita == null)
                return NotFound();

            return Ok(receita);
        }

    }
}
