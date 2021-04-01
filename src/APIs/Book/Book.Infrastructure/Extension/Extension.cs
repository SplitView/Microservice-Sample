using Book.Application.Common.Interface;
using Book.Infrastructure.Data;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Book.Infrastructure.Extension
{
    public static class Extension
    {
        public static IServiceCollection RegisterInfrastructureDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BookDbContext>(options =>
                    options.UseSqlServer(
                        configuration.GetConnectionString("DefaultConnection"),
                        b => b.MigrationsAssembly(typeof(BookDbContext).Assembly.FullName)));


            services.AddScoped<IBookDbContext>(provider => provider.GetService<BookDbContext>());
            return services;
        }
    }
}
