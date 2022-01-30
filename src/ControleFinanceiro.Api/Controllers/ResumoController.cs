using ControleFinanceiro.Application.DTOs;
using ControleFinanceiro.Application.DTOs.Resumo;
using ControleFinanceiro.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ControleFinanceiro.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ResumoController : ControllerBase
    {
        private readonly IResumoService _resumoService;
        
        public ResumoController(IResumoService resumoService)
        {
            _resumoService = resumoService;
        }

        [HttpGet("{ano}/{mes}")]
        public async Task<ActionResult<ResponseDto<ResumoDto>>> GetResumoasync([FromRoute] int ano, [FromRoute]int mes)
        {
            return Ok(await _resumoService.GetResumoAsync(ano, mes));
        }
    }
}