using Core.Abstractions.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace DataLayer
{
    /// <summary>
    /// Data Layer Startup
    /// </summary>
    public static class DataLayerStartup
    {
        /// <summary>
        /// Adds the datalayer.
        /// </summary>
        /// <param name="services">The services.</param>
        public static void AddDatalayer(this IServiceCollection services)
        {
            services.AddTransient<IMemberRepository, MemberRepository>();
        }
    }
}
