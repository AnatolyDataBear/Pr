namespace Pr.Models.Db
{
	public abstract class BaseEntity<TKey>
	{
		public required TKey Id { get; set; }
	}
}
