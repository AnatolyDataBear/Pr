using Microsoft.EntityFrameworkCore;
using Pr.Models.Db;

namespace Pr.Dal
{
	public class PrDbContext : DbContext
	{
		public DbSet<Company> Companies { get; set; }
		public DbSet<Employee> Employees { get; set; }

		public PrDbContext(DbContextOptions<PrDbContext> options) : base(options)
		{
			Database.EnsureCreated();
		}
	}
}
