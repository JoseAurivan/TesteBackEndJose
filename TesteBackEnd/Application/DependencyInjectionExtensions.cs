using Application.Service;
using Application.Service.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Database
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IClienteService, ClienteService>();
            serviceCollection.AddScoped<IFilmeService, FilmeService>();
            serviceCollection.AddScoped<ILocacaoService, LocacaoService>();
            return serviceCollection;
        }
    }
}