using System;
using System.Reflection;

using Microsoft.EntityFrameworkCore;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using SOCATemplate.Infrastructure.Constants;
using SOCATemplate.Infrastructure.Persistence;
using SOCATemplate.Interfaces;

namespace SOCATemplate.DbMigration.SqlServer
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureUseSqlServer
            (
                this IServiceCollection services,
                IConfiguration configuration,
                ILoggerFactory loggerFactory = null
            )
        {
            services.AddDbContext<SOCATemplateDbContext>((svc, options) =>
            {
                options.UseSqlServer
                (
                    connectionString: configuration.GetConnectionString($"{nameof(SOCATemplateDbContext)}_MSSQLConStr"),
                    sqlServerOptionsAction: opt =>
                    {
                        opt.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
                        opt.MigrationsHistoryTable("MigrationHistory", SchemaConstant.Admin);
                    }
                );
            });

            services.AddScoped<ISOCATemplateDbContext>(provider => provider.GetService<SOCATemplateDbContext>());
            services.AddScoped<DbContext>(provider => provider.GetService<SOCATemplateDbContext>());

            return services;
        }
    }
}

