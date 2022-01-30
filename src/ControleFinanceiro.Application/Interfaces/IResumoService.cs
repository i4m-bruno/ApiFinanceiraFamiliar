using ControleFinanceiro.Application.DTOs;
using ControleFinanceiro.Application.DTOs.Resumo;

namespace ControleFinanceiro.Application.Interfaces
{
    public interface IResumoService
    {
         Task<ResponseDto<ResumoDto>> GetResumoAsync(int ano, int mes);
    }
}