using ControleFinanceiro.Domain.Entities;
using ControleFinanceiro.Domain.Entities.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControleFinanceiro.Infrastructure.Configurations
{
    public class DespesaConfiguration : IEntityTypeConfiguration<Despesa>
    {
        public void Configure(EntityTypeBuilder<Despesa> builder)
        {
            builder.ToTable("Despesas");

            builder.HasKey(p => p.Id);
            builder.Property(p => p.Data).IsRequired();
            builder.Property(p => p.Descricao).HasMaxLength(100);
            builder.Property(p => p.Valor).HasPrecision(12,2);
            builder.Property(p => p.Categoria)
                .HasConversion<string>()
                .HasMaxLength(20)
                .HasDefaultValue(CategoriaType.Outras);
        }
    }
}