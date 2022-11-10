using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DbContexts
{
	public interface IApplicationDbContext
	{
		DbSet<InventoryItem> inventoryItems { get; set; }
		DbSet<PatientAdmission> patientAdmissions { get; set; }
	}
}