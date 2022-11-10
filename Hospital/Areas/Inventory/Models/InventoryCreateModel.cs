using Autofac;
using Infrastructure.BusinessObject;
using Infrastructure.Services;
using InventoryBO = Infrastructure.BusinessObject.Inventory;

using Microsoft.AspNetCore.Cors.Infrastructure;

namespace Hospital.Areas.Inventory.Models
{
	public class InventoryCreateModel
	{
		
		public string Name { get; set; }
		public int Amount { get; set; }
		public DateTime EntryDate { get; set; }


		private  IInventoryService _inventoryService;
		private ILifetimeScope _scope;

		public InventoryCreateModel()
		{

		}

		internal void ResolveDependency( ILifetimeScope scope )
		{
			_scope = scope;
			_inventoryService = _scope.Resolve<IInventoryService>();
		}

		public async Task CreateInventory()
		{
			InventoryBO inventory = new InventoryBO();
			inventory.Name = Name;
			inventory.Amount = Amount;
			inventory.EntryDate = EntryDate;

			_inventoryService.CreateInventory(inventory);

			//_inventoryService.CreatePatient(Inventory);
		}
	}
}
