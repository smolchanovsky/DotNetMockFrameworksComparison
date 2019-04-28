namespace Fakes
{
	public interface IRepository
	{
		Entity[] GetAll();
		bool Fill(ref Entity entity);
		bool Fill(int id, ref Entity entity);
		Entity GetById(int id);
		bool TryGetById(int id, out Entity entity);
	}
}