using Autofac;
using Infrastructure.BusinessObject;
using Infrastructure.Services;

using Microsoft.AspNetCore.Cors.Infrastructure;

namespace Hospital.Areas.Patient.Models
{
	public class PatientCreateModel
	{
		public string Name { get; set; }
		public int Age { get; set; }
		public DateTime AdmissionDate { get; set; }


		private  IPatientAdmissionService _patientAdmissionService;
		private ILifetimeScope _scope;

		public PatientCreateModel()
		{

		}

		internal void ResolveDependency( ILifetimeScope scope )
		{
			_scope = scope;
			_patientAdmissionService = _scope.Resolve<IPatientAdmissionService>();	
		}

		public async Task CreatePatient()
		{
			PatientAdmission patient = new PatientAdmission();
			patient.Name = Name;
			patient.Age = Age;
			patient.AdmissionDate = AdmissionDate;

			_patientAdmissionService.CreatePatient(patient);
		}
	}
}
