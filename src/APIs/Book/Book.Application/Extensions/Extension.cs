using MediatR;

using Microsoft.Extensions.DependencyInjection;

using System.Reflection;

namespace Book.Application.Extensions
{
    public static class Extension
    {
        public static IServiceCollection RegisterApplicationDependencies(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
