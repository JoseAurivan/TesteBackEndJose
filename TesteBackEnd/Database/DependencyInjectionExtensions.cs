using Application;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Database
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddDatabaseContext(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddDbContext<Context>(opt => opt.UseInMemoryDatabase("teste"));
            serviceCollection.AddScoped<IContext>(sp => sp.GetRequiredService<Context>());
            return serviceCollection;
        }
        
    }
}