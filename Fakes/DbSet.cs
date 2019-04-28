namespace Fakes
{
	public interface IDbSet
	{
		string Name { get; set; }
	}

	public class DbSet : IDbSet
	{
		public string Name { get; set; }

		public DbSet(string name = null)
		{
			Name = name;
		}
	}
}