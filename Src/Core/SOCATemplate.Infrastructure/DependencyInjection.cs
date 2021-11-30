using System;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using SOCATemplate.Infrastructure.Common;

namespace SOCATemplate.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration,
            Action<InfrastructureOption> option)
        {
            option.Invoke(new InfrastructureOption(services, configuration));

            return services;
        }
    }
}
