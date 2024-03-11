using System.ComponentModel.DataAnnotations;

namespace Pr.Models.Db
{
	public class Company : BaseEntity<Guid>
	{
		[MaxLength(250)]
		public required string Name { get; set; }
		
		public DateTime CreationTime { get; set; }
	}
}
