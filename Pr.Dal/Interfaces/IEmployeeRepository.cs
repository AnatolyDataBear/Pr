using Pr.Models.Db;

namespace Pr.Dal.Interfaces
{
	public interface IEmployeeRepository : IRepository<Employee, Guid>
	{
	}
}
