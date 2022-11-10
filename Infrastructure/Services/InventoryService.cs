using Infrastructure.UnitOfWorks;

using InventoryBO = Infrastructure.BusinessObject.Inventory;
using InventoryEO = Infrastructure.Entities.InventoryItem;

namespace Infrastructure.Services
{
	public class InventoryService : IInventoryService
	{
		private readonly IApplicationUnitOfWork2 _applicationUnitOfWork;

		public InventoryService( IApplicationUnitOfWork2 applicationUnitofWork )
		{
			_applicationUnitOfWork = applicationUnitofWork;
		}

		public void CreateInventory( InventoryBO inventory )
		{

			InventoryEO newInventory = new InventoryEO();
			newInventory.Name = inventory.Name;
			newInventory.Amount = inventory.Amount;
			newInventory.EntryDate = inventory.EntryDate;

			_applicationUnitOfWork.Inventories.Add(newInventory);
			_applicationUnitOfWork.Save();
		}
	}
}
