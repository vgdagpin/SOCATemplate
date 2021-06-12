using System;
using System.Linq;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

using SOCATemplate.Application.Common.Entities;

/*
Do not modify this file! This is auto generated!
Any changes to this file will be gone when template gets generated again.
*/

namespace SOCATemplate.Interfaces
{
	public interface ISOCATemplateDbContext
	{
		Guid UID { get; }
		bool HasSeedData { get; set; }

		#region Entities
		IQueryable<User> Users { get; }
        #endregion

		int SaveChanges();
		Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
	}
}

