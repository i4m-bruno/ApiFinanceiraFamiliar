using ControleFinanceiro.Application.DTOs;
using ControleFinanceiro.Application.DTOs.Despesa;

namespace ControleFinanceiro.Application.Interfaces
{
    public interface IDespesaService
    {
        Task<ResponseDto<IEnumerable<DespesaDto>>> GetAllDespesasAsync(string? descricao);
        Task<ResponseDto<IEnumerable<DespesaDto>>> GetAllDespesasByDataAsync(string ano, string mes);
        Task<DespesaDto> GetDespesaByIdAsync(int id);
        Task<ResponseDto<DespesaDto>> CreateDespesaAsync(CreateDespesaDto despesaDto);
        Task<DespesaDto> UpdateDespesaAsync(int id, CreateDespesaDto despesaDto);
        Task<bool> DeleteDespesaAsync(int id);
        
    }
}