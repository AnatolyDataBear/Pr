using Microsoft.AspNetCore.Mvc;
using Pr.Bll.Interfaces;
using Pr.Models.Db;
using Pr.Models.Dto;

namespace Pr.WebApi.Controllers
{
	/// <summary>
	/// Компании
	/// </summary>
	[Route("api/[controller]")]
	[ApiController]
	public class CompaniesController : BaseController<Company, Guid, CompanyDto>
	{
		private new readonly ICompanyService _service;

		/// <summary>
		/// Company initializer
		/// </summary>
		/// <param name="service"></param>

		public CompaniesController(IBaseService<Company, Guid, CompanyDto> service) : base(service)
		{
			_service = (ICompanyService)service;
		}

		/// <summary>
		/// Генерация ошибки
		/// </summary>
		/// <returns></returns>
		/// <exception cref="Exception"></exception>
		[HttpGet]
		[Route("CustomError")]
		public async Task<IActionResult> Bad()
		{
			throw new Exception("any error");
			return NoContent();
		}
	}
}
