namespace Fakes
{
	public class Entity
	{
		public int? Id { get; set; }

		public Entity(int? id = null)
		{
			Id = id;
		}
	}
}