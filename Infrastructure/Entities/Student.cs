﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entities
{
	public class Student:IEntity<Guid>
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Department { get; set; }
	}
}
