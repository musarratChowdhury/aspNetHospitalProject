using Infrastructure.BusinessObject;

namespace Infrastructure.Services
{
	public interface IInventoryService
	{
		void CreateInventory( Inventory inventory );
	}
}