using Pr.Dal.Interfaces;
using Pr.Models.Db;

namespace Pr.Dal.Repositories
{
	public class EmployeeRepository : Repository<Employee, Guid>, IEmployeeRepository
	{
		public EmployeeRepository(PrDbContext dbContext) : base(dbContext)
		{
		}
	}
}
