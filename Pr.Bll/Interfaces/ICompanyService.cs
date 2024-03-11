using Pr.Models.Db;
using Pr.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pr.Bll.Interfaces
{
	public interface ICompanyService : IBaseService<Company, Guid, CompanyDto>
	{
	}
}
