using System.Linq.Expressions;

namespace Pr.Dal.Interfaces
{
	public interface IRepository<TEntity, TKey> where TEntity : class
	{
		Task<IEnumerable<TEntity>> GetAllAsync();
		Task<IEnumerable<TEntity>> GetAllAsync(int skip, int take);

		Task<TEntity?> GetByIdAsync(TKey id);

		Task<TEntity> AddAsync(TEntity entity);
		Task AddRangeAsync(IEnumerable<TEntity> entities);

		Task UpdateAsync(TEntity entity);
		Task UpdateRangeAsync(IEnumerable<TEntity> entities);

		Task RemoveAsync(TKey id);
		Task RemoveRangeAsync(IEnumerable<TEntity> entities);

		Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
		Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, int skip, int take);
		Task<IEnumerable<TResult>> SelectAsync<TResult>(Expression<Func<TEntity, TResult>> selector);
		Task<IEnumerable<TResult>> FindAndSelectAsync<TResult>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TResult>> selector);
		Task<IEnumerable<TEntity>> GetNestedEntitiesAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties);
		Task<int> CountAsync();
		Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate);
		Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);
	}
}
