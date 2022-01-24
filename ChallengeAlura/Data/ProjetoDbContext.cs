using ChallengeAlura.Data.ModelBuilders;
using Microsoft.EntityFrameworkCore;

namespace ChallengeAlura.Data
{
    public class ProjetoDbContext : DbContext
    {
        public ProjetoDbContext(DbContextOptions<ProjetoDbContext> options) : base(options)   { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new DespesasBuilder());

        }

        public DbSet<Receitas> Receitas { get; set; }
        public DbSet<Despesas> Despesas { get; set; }
        public String DbPath { get; }

    }
}
