using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Pan.Web
{
    public static class PanWebServiceCollectionExtensions
    {
        /// <summary>
        ///     Adds the <see cref="IRestClient" /> and related services to the <see cref="IServiceCollection" />.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection" />.</param>
        /// <returns>The <see cref="IServiceCollection" />.</returns>
        public static IServiceCollection UserPanWeb(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddLogging();
            services.AddOptions();

            services.AddHttpClient();
            services.TryAddTransient<IRestClient, RestClient>();

            return services;
        }
    }
}