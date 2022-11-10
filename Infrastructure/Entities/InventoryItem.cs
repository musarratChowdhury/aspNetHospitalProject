using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entities
{
	public class InventoryItem:IEntity<Guid>
	{
		public Guid Id { get; set; }	
		public string Name { get; set; }
		public int Amount { get; set; }	
		public DateTime EntryDate { get; set; }
	}
}
