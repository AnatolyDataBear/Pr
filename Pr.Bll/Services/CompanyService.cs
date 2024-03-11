using AutoMapper;
using Pr.Bll.Interfaces;
using Pr.Dal.Interfaces;
using Pr.Models.Db;
using Pr.Models.Dto;

namespace Pr.Bll.Services
{
	public class CompanyService : BaseService<Company, Guid, CompanyDto>, ICompanyService
	{
		private readonly IMapper _mapper;
		//private readonly ICompanyRepository _repository;

		public CompanyService(ICompanyRepository repository,
			IMapper mapper) : base(repository, mapper)
		{
			//_repository = repository;
			_mapper = mapper;
		}
	}
}
