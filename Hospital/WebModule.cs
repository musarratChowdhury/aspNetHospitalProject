using Autofac;
using Hospital.Areas.Inventory.Models;
using Hospital.Areas.Patient.Models;
using Hospital.Models;

namespace Hospital
{
	public class WebModule:Module
	{
		protected override void Load( ContainerBuilder builder )
		{
			//builder.RegisterType<CourseModel>().As<ICourseModel>()
			//	.SingleInstance()
			//	.InstancePerLifetimeScope();
			//builder.RegisterType<CourseModel>().AsSelf();

			builder.RegisterType<PatientCreateModel>().AsSelf();
			builder.RegisterType<InventoryCreateModel>().AsSelf();
			builder.RegisterType<RegisterVM>().AsSelf();

			base.Load(builder);
		}
	}
}
