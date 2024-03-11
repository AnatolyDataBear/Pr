using Microsoft.EntityFrameworkCore;
using Pr.Dal.Interfaces;
using System.Linq.Expressions;

namespace Pr.Dal.Repositories
{
	public abstract class Repository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : class
	{
		protected readonly PrDbContext _dbContext;
		protected readonly DbSet<TEntity> _dbSet;

		protected Repository(PrDbContext dbContext)
		{
			Context = dbContext;
			_dbContext = dbContext;
			_dbSet = dbContext.Set<TEntity>();
		}

		protected PrDbContext Context { get; }

		public async Task<IEnumerable<TEntity>> GetAllAsync()
		{
			return await _dbSet.ToListAsync();
		}

		public async Task<IEnumerable<TEntity>> GetAllAsync(int skip, int take)
		{
			return await _dbSet.Skip(skip).Take(take).ToListAsync();
		}


		public async Task<TEntity?> GetByIdAsync(TKey id)
		{
			return await _dbSet.FindAsync(id) ?? throw new ArgumentException($"Entity with id {id} was not found.");
		}

		public async Task<TEntity> AddAsync(TEntity entity)
		{
			await _dbSet.AddAsync(entity);
			await _dbContext.SaveChangesAsync();
			return entity;
		}

		public async Task AddRangeAsync(IEnumerable<TEntity> entities)
		{
			Repository<TEntity, TKey>.ApplyToEntityRange(entities, e => AddEntity(e));
			await _dbContext.SaveChangesAsync();
		}

		public async Task UpdateAsync(TEntity entity)
		{
			_dbContext.Entry(entity).State = EntityState.Modified;
			await _dbContext.SaveChangesAsync();
		}

		public async Task UpdateRangeAsync(IEnumerable<TEntity> entities)
		{
			ApplyToEntityRange(entities, UpdateEntity);
			await Context.SaveChangesAsync();
		}

		public async Task RemoveAsync(TKey id)
		{
			var entity = await _dbSet.FindAsync(id) ?? throw new ArgumentException($"Entity with ID {id} not found.");
			_dbSet.Remove(entity);
			await _dbContext.SaveChangesAsync();
		}

		public async Task RemoveRangeAsync(IEnumerable<TEntity> entities)
		{
			ApplyToEntityRange(entities, DeleteEntity);
			await Context.SaveChangesAsync();
		}

		public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
		{
			return await _dbSet.Where(predicate).ToListAsync();
		}

		public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, int skip, int take)
		{
			//Guard.AgainstNull(predicate, nameof(predicate));

			return await _dbSet.Where(predicate).Skip(skip).Take(take).ToListAsync();
		}

		public virtual async Task<IEnumerable<TResult>> SelectAsync<TResult>(Expression<Func<TEntity, TResult>> selector)
		{
			return await _dbSet.Select(selector).ToListAsync();
		}

		public virtual async Task<IEnumerable<TResult>> SelectAsync<TResult>(Expression<Func<TEntity, TResult>> selector, int skip, int take)
		{
			//Guard.AgainstNull(selector, nameof(selector));

			return await _dbSet.Select(selector).Skip(skip).Take(take).ToListAsync();
		}

		public virtual async Task<IEnumerable<TResult>> FindAndSelectAsync<TResult>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TResult>> selector)
		{
			return await _dbSet.Where(predicate).Select(selector).ToListAsync();
		}

		public virtual async Task<IEnumerable<TResult>> FindAndSelectAsync<TResult>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TResult>> selector, int skip, int take)
		{
			//Guard.AgainstNull(predicate, nameof(predicate));
			//Guard.AgainstNull(selector, nameof(selector));

			return await _dbSet.Where(predicate).Select(selector).Skip(skip).Take(take).ToListAsync();
		}

		public async Task<IEnumerable<TEntity>> GetNestedEntitiesAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
		{
			var query = _dbSet.Where(predicate);
			foreach (var includeProperty in includeProperties)
			{
				query = query.Include(includeProperty);
			}
			return await query.ToListAsync();
		}

		public async Task<IEnumerable<TEntity>> GetNestedEntitiesAsync(Expression<Func<TEntity, bool>> predicate, int skip, int take, params Expression<Func<TEntity, object>>[] includeProperties)
		{
			//Guard.AgainstNull(predicate, nameof(predicate));
			//Guard.AgainstNullOrEmpty(includeProperties, nameof(includeProperties));

			var query = _dbSet.Where(predicate);
			foreach (var includeProperty in includeProperties)
			{
				query = query.Include(includeProperty);
			}
			return await query.Skip(skip).Take(take).ToListAsync();
		}

		public async Task<int> CountAsync()
		{
			return await _dbSet.CountAsync();
		}

		public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
		{
			return await _dbSet.CountAsync(predicate);
		}

		public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
		{
			return await _dbSet.AnyAsync(predicate);
		}

		private static void ApplyToEntityRange(IEnumerable<TEntity> entities, Action<TEntity> action)
		{
			foreach (var entity in entities)
			{
				action(entity);
			}
		}

		private void EnsureAttached(TEntity entity)
		{
			if (!Context.Set<TEntity>().Local.Contains(entity))
			{
				Context.Set<TEntity>().Attach(entity);
			}
		}

		private TEntity AddEntity(TEntity entity)
		{
			EnsureAttached(entity);
			Context.Entry(entity).State = EntityState.Added;
			return entity;
		}

		private void UpdateEntity(TEntity entity)
		{
			EnsureAttached(entity);
			Context.Entry(entity).State = EntityState.Modified;
		}

		private void DeleteEntity(TEntity entity)
		{
			EnsureAttached(entity);
			Context.Entry(entity).State = EntityState.Deleted;
		}
	}
}
