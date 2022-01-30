using System.ComponentModel.DataAnnotations;
using ControleFinanceiro.Domain.Core;
using ControleFinanceiro.Domain.Entities.Enums;

namespace ControleFinanceiro.Domain.Entities
{
    public class Despesa : Entity
    {
        public string Descricao { get; set; }
        public double Valor { get; set; }
        public DateTime Data { get; set; }
        public CategoriaType Categoria { get; set; }
    }
}