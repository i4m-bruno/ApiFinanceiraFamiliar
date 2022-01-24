using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChallengeAlura.Data.ModelBuilders
{
    public class DespesasBuilder : IEntityTypeConfiguration<Despesas>
    {

        public void Configure(EntityTypeBuilder<Despesas> builder)
        {
            builder
                .Property(p => p.Descricao)
                .HasMaxLength(30);

        }
    }
}
