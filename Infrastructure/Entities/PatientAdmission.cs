using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entities
{
	public class PatientAdmission:IEntity<Guid>
	{
		public Guid Id { get; set; }	
		public string Name { get; set; }
		public int Age { get; set; }
		public DateTime AdmissionDate { get; set; }
	}
}
