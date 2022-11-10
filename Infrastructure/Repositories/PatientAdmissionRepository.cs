using Infrastructure.DbContexts;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
	internal class PatientAdmissionRepository:Repository<PatientAdmission,Guid>,IPatientAdmissionRepository
	{
		public PatientAdmissionRepository( IApplicationDbContext context ) : base((DbContext)context)
		{
		}
	}
}
