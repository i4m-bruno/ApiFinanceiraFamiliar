using System.ComponentModel.DataAnnotations;

namespace ControleFinanceiro.Domain.Core
{
    public abstract class Entity
    {
        [Key]
        public int Id { get; set; }
    }
}