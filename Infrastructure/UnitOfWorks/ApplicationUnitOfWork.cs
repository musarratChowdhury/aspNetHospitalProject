using Infrastructure.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using Infrastructure.DbContexts;
using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.UnitOfWorks
{
	public class ApplicationUnitOfWork : UnitOfWork, IApplicationUnitOfWork
	{
		public IPatientAdmissionRepository Patients { get; private set; }
		

		public ApplicationUnitOfWork( IApplicationDbContext dbContext, IPatientAdmissionRepository patientAdmissionRepository ) : base((DbContext)dbContext)
		{
			Patients = patientAdmissionRepository;
		}
		
	}
}
