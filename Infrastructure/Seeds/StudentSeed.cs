using Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Seeds
{
	public class StudentSeed
	{
			internal Student[] Students
		{
			get
			{
				return new Student[]
				{

					new Student{Id = new Guid("5CF7AA6A-294F-4E7F-8227-0BCF06058495"),Name="Amin",Department="EEE"},
					new Student{Id = new Guid("3D21EE91-15F8-4532-AFA8-4A011D001680"),Name="Sadit",Department ="CSE"},
					new Student{Id = new Guid("1C02F3A8-D05B-4390-B33C-603AAF114F30"),Name="Rahim",Department ="ESE"}

				};
			}
		}
	}
}
