using ControleFinanceiro.Application.DTOs;
using ControleFinanceiro.Application.DTOs.Receita;

namespace ControleFinanceiro.Application.Interfaces
{
    public interface IReceitaService
    {
        Task<ResponseDto<IEnumerable<ReceitaDto>>> GetAllReceitasAsync(string? descricao);
        Task<ResponseDto<IEnumerable<ReceitaDto>>> GetAllReceitasByDataAsync(int ano, int mes);
        Task<ReceitaDto> GetReceitaByIdAsync(int id);
        Task<ResponseDto<ReceitaDto>> CreateReceitaAsync(CreateReceitaDto ReceitaDto);
        Task<ReceitaDto> UpdateReceitaAsync(int id, CreateReceitaDto ReceitaDto);
        Task<bool> DeleteReceitaAsync(int id);
    }
}