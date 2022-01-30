using ControleFinanceiro.Application.DTOs;
using ControleFinanceiro.Application.DTOs.Despesa;
using ControleFinanceiro.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ControleFinanceiro.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DespesasController : ControllerBase
    {
        private readonly IDespesaService _despesaService;

        public DespesasController(IDespesaService despesaService)
        {
            _despesaService = despesaService;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DespesaDto>>> GetAllDespesaAsync([FromQuery] string? descricao)
        {
            return Ok(await _despesaService.GetAllDespesasAsync(descricao));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DespesaDto>> GetDespesaByIdAsync([FromRoute]int id)
        {
            var despesa = await _despesaService.GetDespesaByIdAsync(id);
            if (despesa is null) return NotFound();
            return Ok(despesa);
        }

        [HttpGet("{ano}/{mes}")]
        public async Task<ActionResult<IEnumerable<DespesaDto>>> GetAllDespesasByDataAsync([FromRoute] string ano, [FromRoute] string mes)
        {
            return Ok(await _despesaService.GetAllDespesasByDataAsync(ano, mes));
        }

        [HttpPost]
        public async Task<ActionResult<ResponseDto<DespesaDto>>> CreateDespesaAsync([FromBody] CreateDespesaDto despesaDto)
        {
            var despesa = await _despesaService.CreateDespesaAsync(despesaDto);
            
            if (!despesa.Success) return BadRequest(despesa);
            
            return Ok(despesa);           
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<DespesaDto>> UpdateDespesaAsync([FromRoute]int id, [FromBody] CreateDespesaDto despesaDto)
        {
            var despesa = await _despesaService.UpdateDespesaAsync(id, despesaDto);
            if (despesa is null) return NotFound();
            return Ok(despesa);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDespesaAsync([FromRoute]int id)
        {
            var deleted = await _despesaService.DeleteDespesaAsync(id);
            if (deleted) return NoContent();
            return NotFound();
        }
    }
}