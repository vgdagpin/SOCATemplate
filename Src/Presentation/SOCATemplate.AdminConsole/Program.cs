using System;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using System.Reflection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Design;
using SOCATemplate.Infrastructure.Persistence;
using System.IO;
using SOCATemplate.Application.Common.Interfaces;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SOCATemplate.AdminConsole
{
    public class Program : IDesignTimeDbContextFactory<SOCATemplateDbContext>
    {
        private static IConfiguration configuration = null;
        public static IConfiguration Configuration
        {
            get
            {
                if (configuration == null)
                {
                    var configBuilder = new ConfigurationBuilder();

                    configBuilder.SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                            .AddUserSecrets(Assembly.GetExecutingAssembly());

                    configuration = configBuilder.Build();
                }

                return configuration;
            }
        }

        private static IServiceProvider serviceProvider;
        public static IServiceProvider ServiceProvider
        {
            get
            {
                if (serviceProvider == null)
                {
                    serviceProvider = new ServiceCollection()
                        .ConfigureServices(Configuration)
                        .BuildServiceProvider();
                }

                return serviceProvider;
            }
        }

        static async Task Main(string[] args)
        {
            ServiceProvider.GetService<SOCATemplateDbContext>().Database.Migrate();

            Console.WriteLine("Hello World!");
        }


        


        public SOCATemplateDbContext CreateDbContext(string[] args) => ServiceProvider.GetService<SOCATemplateDbContext>();
    }
}
