using Infrastructure.UnitOfWorks;
using Infrastructure.Repositories;

namespace Infrastructure.UnitOfWorks
{
	public interface IApplicationUnitOfWork:IUnitOfWork
	{
		IPatientAdmissionRepository Patients { get; }
	}
}