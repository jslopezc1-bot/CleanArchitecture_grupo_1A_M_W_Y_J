using CleanArchitecture.Application.Interface.Services;
using CleanArchitecture.Application.Services.Categoria;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Application
{
    public static class ConfigurationServices
    {
        public static void AddConfigurationServicesApp(this IServiceCollection services)
        {
            services.AddScoped<ICategoriaServices, CategoriaServices>();
        }
    }
}
