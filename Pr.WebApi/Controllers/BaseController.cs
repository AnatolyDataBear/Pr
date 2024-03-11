using Microsoft.AspNetCore.Mvc;
using Pr.Bll.Interfaces;
using Pr.Models.Db;
using Pr.Models.Dto;

namespace Pr.WebApi.Controllers
{
	public abstract class BaseController<TEntity, TKey, TDto> : ControllerBase
		where TEntity : BaseEntity<TKey>
		where TDto : BaseModel<TKey>
	{

		protected readonly IBaseService<TEntity, TKey, TDto> _service;

		public BaseController(IBaseService<TEntity, TKey, TDto> service)
		{
			_service = service;
		}

		/// <summary>
		/// Выборка всех записей.
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public virtual async Task<IEnumerable<TDto>> GetAll()
		{
			return await _service.GetAllAsync();
		}

		/// <summary>
		/// Получение элемента по Id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpGet("{id}")]
		public virtual async Task<ActionResult<TDto>> GetById(TKey id)
		{
			var entity = await _service.GetByIdAsync(id);

			if (entity == null)
			{
				return NotFound();
			}

			return entity;
		}

		/// <summary>
		/// Создание нового элемента
		/// </summary>
		/// <param name="dto"></param>
		/// <returns></returns>
		[HttpPost]
		public virtual async Task<ActionResult<TDto>> Create([FromBody] TDto dto)
		{
			if (dto == null)
			{
				return BadRequest();
			}
			
			dto.CreationTime = DateTime.UtcNow;

			var entity = await _service.AddAsync(dto);

			return CreatedAtAction(nameof(GetById), new { id = entity.Id }, entity);
		}

		/// <summary>
		/// Изменение элемента
		/// </summary>
		/// <param name="id"></param>
		/// <param name="dto"></param>
		/// <returns></returns>
		[HttpPut("{id}")]
		public virtual async Task<IActionResult> Update(TKey id, [FromBody] TDto dto)
		{
			if (dto == null || !id.Equals(dto.Id))
			{
				return BadRequest();
			}

			await _service.UpdateAsync(id, dto);

			return NoContent();
		}

		/// <summary>
		/// Удаление элемента
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpDelete("{id}")]
		public virtual async Task<IActionResult> Delete(TKey id)
		{
			var entity = await _service.GetByIdAsync(id);

			if (entity == null)
			{
				return NotFound();
			}

			await _service.RemoveAsync(id);

			return NoContent();
		}

		/// <summary>
		/// Количество элементов
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		[Route("Count")]
		public async Task<int> Count()
		{
			return await _service.CountAsync();
		}
	}
}
