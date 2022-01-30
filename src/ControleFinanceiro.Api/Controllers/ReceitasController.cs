using ControleFinanceiro.Application.DTOs;
using ControleFinanceiro.Application.DTOs.Receita;
using ControleFinanceiro.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ControleFinanceiro.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReceitasController : ControllerBase
    {

        private readonly IReceitaService _receitaService;

        public ReceitasController(IReceitaService receitaService)
        {
            _receitaService = receitaService;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReceitaDto>>> GetAllReceitaAsync([FromQuery] string? descricao)
        {
            return Ok(await _receitaService.GetAllReceitasAsync(descricao));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReceitaDto>> GetReceitaByIdAsync([FromRoute]int id)
        {
            var receita = await _receitaService.GetReceitaByIdAsync(id);
            if (receita is null) return NotFound();
            return Ok(receita);
        }

        [HttpGet("{ano}/{mes}")]
        public async Task<ActionResult<IEnumerable<ReceitaDto>>> GetAllReceitasByDataAsync([FromRoute] int ano, [FromRoute] int mes)
        {
            return Ok(await _receitaService.GetAllReceitasByDataAsync(ano, mes));
        }

        [HttpPost]
        public async Task<ActionResult<ResponseDto<ReceitaDto>>> CreateReceitaAsync([FromBody] CreateReceitaDto receitaDto)
        {
            var receita = await _receitaService.CreateReceitaAsync(receitaDto);
            
            if (!receita.Success) return BadRequest(receita);
            
            return Ok(receita);           
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ReceitaDto>> UpdateReceitaAsync([FromRoute]int id, [FromBody] CreateReceitaDto receitaDto)
        {
            var receita = await _receitaService.UpdateReceitaAsync(id, receitaDto);
            if (receita is null) return NotFound();
            return Ok(receita);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteReceitaAsync([FromRoute]int id)
        {
            var deleted = await _receitaService.DeleteReceitaAsync(id);
            if (deleted) return NoContent();
            return NotFound();
        }
    }
}