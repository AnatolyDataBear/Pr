using Pr.Models.Db;
using Pr.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Pr.Bll.Interfaces
{
	public interface IBaseService<TEntity, TKey, TModel>
		where TEntity : BaseEntity<TKey>
		where TModel : BaseModel<TKey>
	{
		Task<IEnumerable<TModel>> GetAllAsync();
		Task<IEnumerable<TModel>> GetAllAsync(int skip, int take);
		Task<TModel> GetByIdAsync(TKey id);
		Task<IEnumerable<TModel>> FindAsync(Expression<Func<TEntity, bool>> predicate);
		Task<IEnumerable<TModel>> FindAsync(Expression<Func<TEntity, bool>> predicate, int skip, int take);
		Task<TModel> AddAsync(TModel dto);
		Task<TModel> UpdateAsync(TKey id, TModel dto);
		Task RemoveAsync(TKey id);
		Task<IEnumerable<TModel>> SelectAsync<TResult>(Expression<Func<TEntity, TResult>> selector);
		Task<IEnumerable<TModel>> FindAndSelectAsync<TResult>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TResult>> selector);
		Task<IEnumerable<TModel>> GetNestedEntitiesAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties);
		Task<int> CountAsync();
	}
}
