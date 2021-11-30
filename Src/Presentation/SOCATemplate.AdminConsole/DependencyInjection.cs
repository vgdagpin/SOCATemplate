using System;

using SOCATemplate.Application;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using TasqR;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Design;
using SOCATemplate.Infrastructure.Persistence;
using System.IO;
using SOCATemplate.Infrastructure;
using SOCATemplate.DbMigration.SQLite;

namespace SOCATemplate.AdminConsole
{
    public static class DependencyInjection
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddInfrastructure(configuration, opt =>
            {
                var dir = Environment.ExpandEnvironmentVariables(@"%USERPROFILE%\AppData\Local\Temp\SOCATemplate\");

                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                opt.UseSQLite(Path.Combine(dir, "SOCATemplate.db"));
            });

            services.AddApplication();
            services.AddTasqR(Assembly.GetExecutingAssembly());

            services.AddMemoryCache();

            return services;
        }
    }
}
