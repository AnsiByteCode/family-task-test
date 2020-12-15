using Core.Abstractions.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Services
{
    /// <summary>
    /// Service Layer Startup
    /// </summary>
    public static class ServiceLayerStartup
    {
        /// <summary>
        /// Adds the service layer.
        /// </summary>
        /// <param name="services">The services.</param>
        public static void AddServiceLayer(this IServiceCollection services)
        {
            services.AddTransient<IMemberService, MemberService>();
        }
    }
}