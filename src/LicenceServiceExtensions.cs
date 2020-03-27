using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microestc.Authorization
{
    public static class LicenceServiceExtensions
    {
        public static IServiceCollection AddLicenceService(this IServiceCollection services, Action<LicenceOptions> configure)
        {
            if (services == null)
            {
                throw new ArgumentNullException("services");
            }
            if (configure == null)
            {
                throw new ArgumentNullException("configure");
            }

            services.Configure(configure);

            services.TryAddSingleton<ILicenceProvider, DefaultLicenceProvider>();

            return services;
        }

    }
}

