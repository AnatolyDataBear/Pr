using AutoMapper;
using Pr.Models.Db;
using Pr.Models.Dto;

namespace Pr.Models.Mapping
{
	public class AutoMapperProfile : Profile
	{
		public AutoMapperProfile()
		{
			CreateMap<Company, CompanyDto>();
			CreateMap<CompanyDto, Company>();

			CreateMap<Employee, EmployeeDto>();
			CreateMap<EmployeeDto, Employee>();
		}
	}
}
