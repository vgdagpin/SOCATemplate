using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

using SOCATemplate.Interfaces;
using SOCATemplate.Application.Common.Entities;

/*
Do not modify this file! This is auto generated!
Any changes to this file will be gone when template gets generated again.
*/

namespace SOCATemplate.Infrastructure.Persistence
{
	public partial class SOCATemplateDbContext : DbContext, ISOCATemplateDbContext
	{
		public Guid UID { get; } = Guid.NewGuid();
		public bool HasSeedData { get; set; }

		#region Entities
		private DbSet<User> db_Users { get; set; }
		public IQueryable<User> Users 
		{ 
			get { return db_Users; }
			private set { db_Users = (DbSet<User>)value; }
		}
        #endregion

		public SOCATemplateDbContext(DbContextOptions<SOCATemplateDbContext> dbContextOpt) 
			: base(dbContextOpt) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
			=> modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
	}
}

namespace SOCATemplate.Infrastructure.Persistence.Configurations
{
	#region Configurations
	public partial class User_Configuration : BaseConfiguration<User> { }
    #endregion
}
