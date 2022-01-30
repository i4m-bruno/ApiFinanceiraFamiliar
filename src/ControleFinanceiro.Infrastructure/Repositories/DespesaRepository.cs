using ControleFinanceiro.Domain.Entities;
using ControleFinanceiro.Domain.Repositories;
using ControleFinanceiro.Infrastructure.Context;

namespace ControleFinanceiro.Infrastructure.Repositories
{
    public class DespesaRepository : Repository<Despesa>, IDespesaRepository
    {
        public DespesaRepository(ApplicationDbContext dbContext) : base(dbContext)
        {}
    }
}