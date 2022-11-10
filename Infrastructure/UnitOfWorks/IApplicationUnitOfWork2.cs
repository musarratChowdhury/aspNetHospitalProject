using Infrastructure.Repositories;

namespace Infrastructure.UnitOfWorks
{
	public interface IApplicationUnitOfWork2:IUnitOfWork
	{
		IInventoryRepository Inventories { get; }
	}
}