using Infrastructure.BusinessObject;

namespace Infrastructure.Services
{
	public interface IPatientAdmissionService
	{
		void CreatePatient( PatientAdmission patient );
	}
}