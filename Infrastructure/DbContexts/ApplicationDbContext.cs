
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Infrastructure.Entities;

namespace Infrastructure.DbContexts
{
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser,ApplicationRole,Guid>, IApplicationDbContext
	{
		private readonly string _connectionString;
		private readonly string _migrationAssemblyName;

		public ApplicationDbContext( string connectionString, string migrationAssemblyName )
		{
			_connectionString = connectionString;
			_migrationAssemblyName = migrationAssemblyName;
		}
		protected override void OnConfiguring( DbContextOptionsBuilder optionsBuilder )
		{
			if (!optionsBuilder.IsConfigured)
			{
				optionsBuilder.UseSqlServer(_connectionString, b => b.MigrationsAssembly(_migrationAssemblyName));
			}
			base.OnConfiguring(optionsBuilder);
		}

		public DbSet<PatientAdmission> patientAdmissions { get; set; }
		public DbSet<InventoryItem> inventoryItems { get; set; }
	}
}
