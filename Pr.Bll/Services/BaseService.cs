using AutoMapper;
using Pr.Bll.Interfaces;
using Pr.Dal.Interfaces;
using Pr.Models.Db;
using Pr.Models.Dto;
using System.Linq.Expressions;

namespace Pr.Bll.Services
{
	public abstract class BaseService<TEntity, TKey, TModel> : IBaseService<TEntity, TKey, TModel>
			where TEntity : BaseEntity<TKey>
			where TModel : BaseModel<TKey>
	{
		private readonly IMapper _mapper;
		protected readonly IRepository<TEntity, TKey> _repository;

		protected BaseService(IRepository<TEntity, TKey> repository, IMapper mapper)
		{
			_repository = repository;
			_mapper = mapper;
		}

		public virtual async Task<IEnumerable<TModel>> GetAllAsync()
		{
			var entities = await _repository.GetAllAsync();
			return _mapper.Map<IEnumerable<TModel>>(entities);
		}

		public virtual async Task<IEnumerable<TModel>> GetAllAsync(int skip, int take)
		{
			var entities = await _repository.GetAllAsync(skip, take);
			return _mapper.Map<IEnumerable<TModel>>(entities);
		}

		public virtual async Task<TModel> GetByIdAsync(TKey id)
		{
			var entity = await _repository.GetByIdAsync(id);
			return _mapper.Map<TModel>(entity);
		}

		public virtual async Task<IEnumerable<TModel>> FindAsync(Expression<Func<TEntity, bool>> predicate)
		{
			var entities = await _repository.FindAsync(predicate);
			return _mapper.Map<IEnumerable<TModel>>(entities);
		}

		public virtual async Task<IEnumerable<TModel>> FindAsync(Expression<Func<TEntity, bool>> predicate, int skip, int take)
		{
			var entities = await _repository.FindAsync(predicate, skip, take);
			return _mapper.Map<IEnumerable<TModel>>(entities);
		}

		public virtual async Task<TModel> AddAsync(TModel dto)
		{
			var entity = _mapper.Map<TEntity>(dto);
			await _repository.AddAsync(entity);
			return _mapper.Map<TModel>(entity);
		}

		public virtual async Task<TModel> UpdateAsync(TKey id, TModel dto)
		{
			var entity = await _repository.GetByIdAsync(id) ?? throw new Exception($"Entity with id {id} was not found.");
			_mapper.Map(dto, entity);

			await _repository.UpdateAsync(entity);
			return _mapper.Map<TModel>(entity);
		}

		public virtual async Task RemoveAsync(TKey id)
		{
			var entity = await _repository.GetByIdAsync(id) ?? throw new Exception($"Entity with id {id} was not found.");
			await _repository.RemoveAsync(id);
		}

		public virtual async Task<IEnumerable<TModel>> SelectAsync<TResult>(Expression<Func<TEntity, TResult>> selector)
		{
			var entities = await _repository.SelectAsync(selector);
			return _mapper.Map<IEnumerable<TModel>>(entities);
		}

		public virtual async Task<IEnumerable<TModel>> FindAndSelectAsync<TResult>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TResult>> selector)
		{
			var entities = await _repository.FindAndSelectAsync(predicate, selector);
			return _mapper.Map<IEnumerable<TModel>>(entities);
		}

		public virtual async Task<IEnumerable<TModel>> GetNestedEntitiesAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
		{
			var entities = await _repository.GetNestedEntitiesAsync(predicate, includeProperties);
			return _mapper.Map<IEnumerable<TModel>>(entities);
		}

		public virtual async Task<int> CountAsync()
		{
			return await _repository.CountAsync();
		}
	}
}
