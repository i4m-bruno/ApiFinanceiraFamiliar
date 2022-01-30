using System.ComponentModel.DataAnnotations;
using ControleFinanceiro.Domain.Entities.Enums;

namespace ControleFinanceiro.Application.DTOs.Despesa
{
    public class CreateDespesaDto
    {
        [StringLength(maximumLength: 100, MinimumLength = 3,
             ErrorMessage ="Descrição deve conter no máximo de {1}, valor mínimo {2}")]
        public string Descricao { get; set; }

        [Required(ErrorMessage ="Valor é Obrigatório")]
        public double Valor { get; set; }

        [Required(ErrorMessage ="Data é Obrigatório")]
        public DateTime Data { get; set; }

        public CategoriaType? Categoria { get; set;}  
    }
}