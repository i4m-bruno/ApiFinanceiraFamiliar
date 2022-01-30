using System.ComponentModel;

namespace ControleFinanceiro.Domain.Entities.Enums
{
    public enum CategoriaType
    {
        [Description("Outras")]
        Outras = 1,

        [Description("Alimentação")]
        Alimentacao = 2,

        [Description("Saúde")]
        Saude = 3, 

        [Description("Moradia")]
        Moradia = 4,

        [Description("Transporte")]
        Transporte = 5,

        [Description("Educação")]
        Educacao = 6,

        [Description("Lazer")]
        Lazer = 7,

        [Description("Imprevistos")]
        Imprevistos = 8
    }
}