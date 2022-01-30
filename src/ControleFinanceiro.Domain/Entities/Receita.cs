using System.ComponentModel.DataAnnotations;
using ControleFinanceiro.Domain.Core;

namespace ControleFinanceiro.Domain.Entities
{
    public class Receita : Entity
    {
        [MaxLength(100)]
        [MinLength(3)]
        public string Descricao { get; set; }

        [Required]
        public double Valor { get; set; }

        [Required]
        public DateTime Data { get; set; }        
    }
}