using Pr.Dal.Interfaces;
using Pr.Models.Db;

namespace Pr.Dal.Repositories
{
	public class CompanyRepository : Repository<Company, Guid>, ICompanyRepository
	{
		public CompanyRepository(PrDbContext dbContext) : base(dbContext)
		{
		}
	}
}
