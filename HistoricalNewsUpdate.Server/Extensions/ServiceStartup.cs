using Euroland.NetCore.ToolsFramework.Authentication;
using HistoricalNewsUpdate.Services;

namespace HistoricalNewsUpdate.Extensions
{
    public static class ServiceStartup
    {
        public static IServiceCollection AddServiceModule(this IServiceCollection services)
        {

            services.AddSingleton<ILdapAuthentication, LdapAuthentication>();
            services.AddScoped<IAuthService, AuthService>();

            return services;
        }
    }
}
