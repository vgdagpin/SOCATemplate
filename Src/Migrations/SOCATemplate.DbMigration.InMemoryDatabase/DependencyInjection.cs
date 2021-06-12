using System;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;

using SOCATemplate.Infrastructure.Persistence;
using SOCATemplate.Interfaces;

namespace SOCATemplate.DbMigration.InMemoryDatabase
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureUseInMemory(this IServiceCollection services)
        {
            services.AddDbContext<SOCATemplateDbContext>((svc, options) =>
            {
                options.UseInMemoryDatabase($"Test:{Guid.NewGuid().ToString().Substring(0, 8)}");
                options.ConfigureWarnings(a => a.Ignore(InMemoryEventId.TransactionIgnoredWarning));
            });

            services.AddScoped<ISOCATemplateDbContext>(provider => provider.GetService<SOCATemplateDbContext>().LoadSeeds());
            services.AddScoped<DbContext>(provider => provider.GetService<SOCATemplateDbContext>().LoadSeeds());

            return services;
        }

        private static SOCATemplateDbContext LoadSeeds(this SOCATemplateDbContext dbContext)
        {
            if (dbContext.HasSeedData)
            {
                return dbContext;
            }

            //new Job_Configuration().LoadSeedDataTo(dbContext.Jobs);
            //new JobParameter_Configuration().LoadSeedDataTo(dbContext.JobParameters);

            //dbContext.SaveChanges();

           // dbContext.HasSeedData = true;

            return dbContext;
        }
    }
}
