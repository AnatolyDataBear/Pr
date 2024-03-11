using System.ComponentModel.DataAnnotations;

namespace Pr.Models.Dto
{
	/// <summary>
	/// Компания
	/// </summary>
	public class CompanyDto : BaseModel<Guid>
	{
		/// <summary>
		/// Наименование компании
		/// </summary>
		[MaxLength(250)]
		public required string Name { get; set; }
	}
}
