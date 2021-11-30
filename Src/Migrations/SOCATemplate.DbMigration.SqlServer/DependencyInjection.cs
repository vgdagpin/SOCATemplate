using System;
using System.Reflection;

using Microsoft.EntityFrameworkCore;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using SOCATemplate.Application.Common.Interfaces;
using SOCATemplate.Infrastructure.Common;
using SOCATemplate.Infrastructure.Constants;
using SOCATemplate.Infrastructure.Persistence;

namespace SOCATemplate.DbMigration.SqlServer
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
                options.UseSqlServer
                (
                    connectionString: conStr,
                    sqlServerOptionsAction: opt =>
                    {
                        opt.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
                        opt.MigrationsHistoryTable("tbl_MigrationHistory", Constants.SchemaConstant.Admin);
                    }
                );
            });

            option.Services.AddScoped<ISOCATemplateDbContext>(provider => provider.GetService<SOCATemplateDbContext>());
            option.Services.AddScoped<DbContext>(provider => provider.GetService<SOCATemplateDbContext>());
        }
    }
}

