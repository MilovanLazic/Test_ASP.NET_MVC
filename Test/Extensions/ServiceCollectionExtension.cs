using Test.BusinessLogic;
using Test.BusinessLogic.Interfaces;
using Test.DataAccess;

namespace Test.Web.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddServices(this WebApplicationBuilder builder)
        {
            builder
                .AddHttpClients()
                .AddLogics();
        }
        private static IServiceCollection AddHttpClients(this WebApplicationBuilder builder)
        {
            builder.Services
                    .AddHttpClient()
                    .AddHttpClient("EmployeeHttpClient", client =>
                    {
                        client.BaseAddress = new Uri(builder.Configuration.GetSection("ExternalServiceSettings").GetValue<String>("EmployeeEndpointUrl").Replace("{key}", builder.Configuration.GetSection("ExternalServiceSettings").GetValue<String>("EmployeeAPIKey")));
                    });

            return builder.Services;
        }

        private static IServiceCollection AddLogics(this IServiceCollection services)
        {
            return services.
                 AddScoped<IUnitOfWork, UnitOfWork>()
                .AddScoped<IEmployeeLogic, EmployeeLogic>();
        }
    }
}
