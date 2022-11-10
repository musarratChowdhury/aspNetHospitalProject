using Autofac;
using Infrastructure.DbContexts;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Infrastructure.UnitOfWorks;

namespace Infrastructure
{
	public class InfrastructureModule : Module
	{
		private readonly string _connectionString;
		private readonly string _migrationAssemblyName;

		public InfrastructureModule( string connectionString, string migrationAssemblyName )
		{
			_connectionString = connectionString;
			_migrationAssemblyName = migrationAssemblyName;
		}
		protected override void Load( ContainerBuilder builder )
		{
			builder.RegisterType<ApplicationDbContext>().AsSelf()
				.WithParameter("connectionString", _connectionString)
				.WithParameter("migrationAssemblyName", _migrationAssemblyName)
				.InstancePerLifetimeScope();


			builder.RegisterType<ApplicationDbContext>().As<IApplicationDbContext>()
				.WithParameter("connectionString", _connectionString)
				.WithParameter("migrationAssemblyName", _migrationAssemblyName)
				.InstancePerLifetimeScope();
			builder.RegisterType<PatientAdmissionService>().As<IPatientAdmissionService>()
			.InstancePerLifetimeScope();

			builder.RegisterType<PatientAdmissionRepository>().As<IPatientAdmissionRepository>()
				.InstancePerLifetimeScope();

			builder.RegisterType<ApplicationUnitOfWork>().As<IApplicationUnitOfWork>()
				.InstancePerLifetimeScope();

			builder.RegisterType<InventoryService>().As<IInventoryService>()
			.InstancePerLifetimeScope();

			builder.RegisterType<InventoryRepository>().As<IInventoryRepository>()
				.InstancePerLifetimeScope();

			builder.RegisterType<ApplicationUnitOfWork2>().As<IApplicationUnitOfWork2>()
				.InstancePerLifetimeScope();



			base.Load(builder);
		}

	
	}
}
