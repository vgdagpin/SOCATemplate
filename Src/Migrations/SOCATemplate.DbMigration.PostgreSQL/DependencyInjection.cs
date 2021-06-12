using System;
using System.Reflection;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using SOCATemplate.Infrastructure.Constants;
using SOCATemplate.Infrastructure.Persistence;
using SOCATemplate.Interfaces;

namespace SOCATemplate.DbMigration.PostgreSQL
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureUsePostgreSQL(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<SOCATemplateDbContext>((svc, options) =>
            {
                options.UseNpgsql
                (
                    connectionString: configuration.GetConnectionString($"{nameof(SOCATemplateDbContext)}_PostgreSQLConStr"),
                    npgsqlOptionsAction: opt =>
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
