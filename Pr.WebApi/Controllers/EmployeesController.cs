using Microsoft.AspNetCore.Mvc;
using Pr.Bll.Interfaces;
using Pr.Models.Db;
using Pr.Models.Dto;

namespace Pr.WebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class EmployeesController : BaseController<Employee, Guid, EmployeeDto>
	{
		private new readonly IEmployeeService _service;

		/// <summary>
		/// Employee initializer
		/// </summary>
		/// <param name="service"></param>

		public EmployeesController(IBaseService<Employee, Guid, EmployeeDto> service) : base(service)
		{
			_service = (IEmployeeService)service;
		}
	}
}
