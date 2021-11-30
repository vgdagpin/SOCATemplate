using System;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using SOCATemplate.Application.Common.Interfaces;
using SOCATemplate.Infrastructure.Common;
using SOCATemplate.Infrastructure.Persistence;

namespace SOCATemplate.DbMigration.InMemoryDatabase
{
    public static class DependencyInjection
    {
        public static void UseSqlServer
            (
                this InfrastructureOption option,
                string conString = null
            )
        {
            var conStr = conString ?? option.Configuration.GetConnectionString($"{nameof(SOCATemplateDbContext)}_MSSQLConStr");

            option.Services.AddDbContext<SOCATemplateDbContext>((svc, options) =>
            {
                options.UseInMemoryDatabase($"Test:{Guid.NewGuid().ToString().Substring(0, 8)}");
                options.ConfigureWarnings(a => a.Ignore(InMemoryEventId.TransactionIgnoredWarning));
            });

            option.Services.AddScoped<ISOCATemplateDbContext>(provider => provider.GetService<SOCATemplateDbContext>());
            option.Services.AddScoped<DbContext>(provider => provider.GetService<SOCATemplateDbContext>());
        }

        private static SOCATemplateDbContext LoadSeeds(this SOCATemplateDbContext dbContext)
        {
            if (dbContext.HasSeedData)
            {
                return dbContext;
            }

            //new User_Configuration().LoadSeedDataTo(dbContext.Users);

            //dbContext.SaveChanges();

           // dbContext.HasSeedData = true;

            return dbContext;
        }
    }


}
