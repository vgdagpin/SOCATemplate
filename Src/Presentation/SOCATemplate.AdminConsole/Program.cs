using System;

using SOCATemplate.Application;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using TasqR;
using System.Reflection;
using SOCATemplate.DbMigration.SqlServer;
using System.Threading.Tasks;

namespace SOCATemplate.AdminConsole
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var mainService = CreateHostBuilder(args).Build().Services;
            var mainProcessor = mainService.GetService<ITasqR>();



            Console.WriteLine("Hello World!");



        }


        static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddInfrastructureUseSqlServer(configuration);
            services.AddApplication();
            services.AddTasqR(Assembly.GetExecutingAssembly());

            services.AddMemoryCache();
        }


        #region HostBuilder
        static AppServiceBuilder CreateHostBuilder(string[] args)
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var builder = new AppServiceBuilder();

            ConfigureServices(builder.Services, config);

            return builder;
        }

        class AppServiceBuilder
        {
            public ServiceCollection Services { get; } = new ServiceCollection();
            public AppServiceProvider Build() => new AppServiceProvider(Services.BuildServiceProvider());
        }

        class AppServiceProvider
        {
            public AppServiceProvider(IServiceProvider services) { Services = services; }
            public IServiceProvider Services { get; }
        }
        #endregion
    }
}
