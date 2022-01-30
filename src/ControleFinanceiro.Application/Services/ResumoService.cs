using System.Linq.Expressions;
using ControleFinanceiro.Application.DTOs;
using ControleFinanceiro.Application.DTOs.Resumo;
using ControleFinanceiro.Application.Interfaces;
using ControleFinanceiro.Domain.Entities;
using ControleFinanceiro.Domain.Repositories;

namespace ControleFinanceiro.Application.Services
{
    public class ResumoService : IResumoService
    {
        private readonly IReceitaRepository _receitaRepository;
        private readonly IDespesaRepository _despesaRepository;
        public ResumoService(IReceitaRepository receitaRepository, IDespesaRepository despesaRepository)
        {
            _receitaRepository = receitaRepository;
            _despesaRepository = despesaRepository;
        }
        public async Task<ResponseDto<ResumoDto>> GetResumoAsync(int ano, int mes)
        {

            Expression<Func<Receita,bool>> receitaExpression = x => x.Data.Year == ano && x.Data.Month == mes;
            Expression<Func<Despesa,bool>> despesaExpression = x => x.Data.Year == ano && x.Data.Month == mes;
            ResponseDto<ResumoDto> response = new();

            var receitas = await _receitaRepository.GetAllAsync(receitaExpression);
            var despesas = await _despesaRepository.GetAllAsync(despesaExpression);

            var receitasValorTotal = receitas.Sum(x => x.Valor);
            var despesasValorTotal = despesas.Sum(x => x.Valor);
            var categoriasValor = despesas.GroupBy(x => x.Categoria).Select(x => new ValorCategoriaDto {Categoria = x.Key, Valor = Math.Round(x.Sum(s => s.Valor))} );

            response.Data = new ResumoDto
            {
                ReceitasValorTotal = Math.Round(receitasValorTotal, 2),
                DespesasValorTotal = Math.Round(despesasValorTotal, 2),
                Saldo = Math.Round(receitasValorTotal - despesasValorTotal, 2),
                CategoriasValor = categoriasValor
            };

            return response;
        }
    }
}