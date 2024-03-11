using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pr.Models.Db
{
	public class Employee : BaseEntity<Guid>
	{
		[NotMapped]
		public string FullName => $"{Surname} {Name} {MiddleName}".TrimEnd();

		[MaxLength(250)]
		public required string Surname { get; set; }
		[MaxLength(250)]
		public required string Name { get; set; }
		[MaxLength(250)]
		public required string MiddleName { get; set; }

		public Guid CompanyId { get; set; }
		public Company? Company { get; set; }

		public DateTime CreationTime { get; set; }

	}
}
