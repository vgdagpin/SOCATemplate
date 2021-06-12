using System;
using System.IO;
using System.Reflection;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using SOCATemplate.Infrastructure.Persistence;
using SOCATemplate.Interfaces;

namespace SOCATemplate.DbMigration.SQLite
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureUseSQLite(this IServiceCollection services, IConfiguration configuration)
        {
            string appDataDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            if (configuration["AppDataDirectory"] != null)
            {
                appDataDirectory = configuration["AppDataDirectory"];
            }

            if (!Directory.Exists(appDataDirectory))
            {
                Directory.CreateDirectory(appDataDirectory);
            }

            string conString = $"Filename={Path.Combine(appDataDirectory, $"SOCATemplateDb_SQLite.db3")}";
            string conStringFromSettings = configuration.GetConnectionString($"{nameof(SOCATemplateDbContext)}_SQLiteConStr");

            if (!string.IsNullOrEmpty(conStringFromSettings))
            {
                conString = conStringFromSettings;
            }

            services.AddDbContext<SOCATemplateDbContext>((svc, options) =>
            {
                options.UseSqlite
                (
                    connectionString: conString,
                    sqliteOptionsAction: opt =>
                    {
                        opt.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
                    }
                );
            });

            services.AddScoped<ISOCATemplateDbContext>(provider => provider.GetService<SOCATemplateDbContext>());
            services.AddScoped<DbContext>(provider => provider.GetService<SOCATemplateDbContext>());

            return services;
        }
    }
}
