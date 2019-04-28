namespace Fakes
{
	public interface IDbContext
	{
		IDbSet DbSet { get; set; }
	}

	public class DbContext : IDbContext
	{
		public IDbSet DbSet { get; set; }
	}
}