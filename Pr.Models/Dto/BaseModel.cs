namespace Pr.Models.Dto
{
	public abstract class BaseModel<Tkey>
	{
		/// <summary>
		/// Уникальный идентификатор
		/// </summary>
		public Tkey Id { get; set; }

		/// <summary>
		/// Время создания
		/// </summary>
		public DateTime CreationTime { get; set; }
	}
}
