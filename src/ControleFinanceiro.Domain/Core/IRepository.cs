using System.Linq.Expressions;

namespace ControleFinanceiro.Domain.Core
{
    public interface IRepository<TEntity> where TEntity : class
    {
         Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity,bool>>? predicate = null);
         Task<TEntity> GetByIdAsync(int id);
         Task<TEntity> CreateAsync(TEntity entity);
         Task UpdateAsync(TEntity entity);
         Task DeleteAsync(int id);
         Task<bool> AnyAsync(Expression<Func<TEntity,bool>> predicate);
         Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity,bool>> predicate);
    }
}