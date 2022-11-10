using Infrastructure.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PatientBO = Infrastructure.BusinessObject.PatientAdmission;
using PatientEO = Infrastructure.Entities.PatientAdmission;

namespace Infrastructure.Services
{
	public class PatientAdmissionService : IPatientAdmissionService
	{

		private readonly IApplicationUnitOfWork _applicationUnitOfWork;

		public PatientAdmissionService( IApplicationUnitOfWork applicationUnitofWork )
		{
			_applicationUnitOfWork = applicationUnitofWork;
		}

		public void CreatePatient( PatientBO patient )
		{

			PatientEO newPatient = new PatientEO();
			newPatient.Name = patient.Name;
			newPatient.Age = patient.Age;
			newPatient.AdmissionDate = patient.AdmissionDate;

			_applicationUnitOfWork.Patients.Add(newPatient);
			_applicationUnitOfWork.Save();
		}
	}
}
