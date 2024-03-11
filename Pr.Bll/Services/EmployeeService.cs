using AutoMapper;
using Pr.Bll.Interfaces;
using Pr.Dal.Interfaces;
using Pr.Models.Db;
using Pr.Models.Dto;

namespace Pr.Bll.Services
{
	public class EmployeeService : BaseService<Employee, Guid, EmployeeDto>, IEmployeeService
	{
		private readonly IMapper _mapper;

		public EmployeeService(IEmployeeRepository repository,
			IMapper mapper) : base(repository, mapper)
		{
			//_repository = repository;
			_mapper = mapper;
		}
	}
}
