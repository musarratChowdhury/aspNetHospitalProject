using Infrastructure.DbContexts;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.UnitOfWorks
{

	public class ApplicationUnitOfWork2 : UnitOfWork
, IApplicationUnitOfWork2
	{
		public IInventoryRepository Inventories { get; private set; }


		public ApplicationUnitOfWork2( IApplicationDbContext dbContext, IInventoryRepository inventoryRepository ) : base((DbContext)dbContext)
		{
			Inventories = inventoryRepository;
		}

	}

}
