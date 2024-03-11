using Pr.Models.Db;
using System.ComponentModel.DataAnnotations;

namespace Pr.Models.Dto
{
	/// <summary>
	/// Сотрудник
	/// </summary>
	public class EmployeeDto : BaseModel<Guid>
	{
		/// <summary>
		/// ФИО
		/// </summary>
		public required string FullName { get; set; }


		/// <summary>
		/// Фамилия
		/// </summary>
		public required string Surname { get; set; }
		

		/// <summary>
		/// Имя
		/// </summary>
		public required string Name { get; set; }
		
		/// <summary>
		/// Отчество
		/// </summary>
		public required string MiddleName { get; set; }

		/// <summary>
		/// ИД компании
		/// </summary>
		public Guid CompanyId { get; set; }
	}
}
