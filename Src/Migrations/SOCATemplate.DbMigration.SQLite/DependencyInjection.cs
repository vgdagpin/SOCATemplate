using System;
using System.IO;
using System.Reflection;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using SOCATemplate.Application.Common.Interfaces;
using SOCATemplate.Infrastructure.Common;
using SOCATemplate.Infrastructure.Persistence;

namespace SOCATemplate.DbMigration.SQLite
{
    public static class DependencyInjection
    {
        public static void UseSQLite
            (
                this InfrastructureOption option,
                string appDataFile
            )
        {
            option.Services.AddDbContext<SOCATemplateDbContext>((svc, options) =>
            {
                options.UseSqlite
                (
                    connectionString: $"Filename={appDataFile}",
                    sqliteOptionsAction: opt =>
                    {
                        opt.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
                        opt.MigrationsHistoryTable("tbl_MigrationHistory", "adm");
                    }
                );
            });

            option.Services.AddScoped<ISOCATemplateDbContext>(provider => provider.GetService<SOCATemplateDbContext>());
            option.Services.AddScoped<DbContext>(provider => provider.GetService<SOCATemplateDbContext>());
        }
    }
}
