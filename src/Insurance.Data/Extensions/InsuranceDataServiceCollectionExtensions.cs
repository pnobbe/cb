using System;
using System.Net.Http.Headers;
using Insurance.Core.Interfaces.Repositories;
using Insurance.Data.Repositories;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    ///     Provides extension methods for services from this assembly.
    /// </summary>
    public static class InsuranceDataServiceCollectionExtensions
    {
        /// <summary>
        ///     Configuration.
        /// </summary>
        public static IConfiguration Configuration { get; set; }

        /// <summary>
        ///     Adds the data services and database provider to the container.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
        /// <param name="apiEndpointName">Api endpoint string.</param>
        /// <returns>An instance of <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddInsuranceDataServices(this IServiceCollection services, string apiEndpointName)
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (string.IsNullOrEmpty(apiEndpointName))
            {
                throw new ArgumentNullException(nameof(apiEndpointName));
            }

            // The startup essential properties can be used to setup components.
            (Configuration) = services.BuildServiceProvider().GetRequiredService<IConfiguration>();

            // Register repositories with the DI container.
            // NOTE: It's not clear from the documentation, but AddHttpClient also registers the client repositories in the DI container,
            // replacing what would otherwise be an AddScoped call.
            // FUTURE: Reuse the client configuration to avoid repetition.
            services.AddHttpClient<IProductRepository, ProductRepository>(client =>
            {
                client.BaseAddress = new Uri(Configuration.GetConnectionString(apiEndpointName));
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            });
            services.AddHttpClient<IProductTypeRepository, ProductTypeRepository>(client =>
            {
                client.BaseAddress = new Uri(Configuration.GetConnectionString(apiEndpointName));
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            });
            services.AddHttpClient<ITestRepository, TestRepository>(client =>
            {
                client.BaseAddress = new Uri(Configuration.GetConnectionString(apiEndpointName));
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            });

            return services;
        }
    }
}
