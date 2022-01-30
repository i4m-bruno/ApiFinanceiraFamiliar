using ControleFinanceiro.Domain.Entities.Enums;
using Microsoft.EntityFrameworkCore;

namespace ControleFinanceiro.Application.DTOs.Resumo
{
    public class ResumoDto
    {
        [Precision(14, 2)]
        public double ReceitasValorTotal { get; set; }
        
        [Precision(14, 2)]
        public double DespesasValorTotal { get; set; }

        [Precision(14, 2)]
        public double Saldo { get; set; }

        public IEnumerable<ValorCategoriaDto> CategoriasValor { get; set; }

    }

    public class ValorCategoriaDto
    {
        public CategoriaType Categoria { get; set;}
        
        [Precision(14, 2)]
        public double Valor { get; set; }
    }
}