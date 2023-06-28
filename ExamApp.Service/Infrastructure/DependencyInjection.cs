using ExamApp.Service.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetCore.AutoRegisterDi;
using System.Reflection;

namespace ExamApp.Service.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddService(this IServiceCollection services)
        {
            var repositoryAssembly = Assembly.GetAssembly(typeof(StudentService));
            services.RegisterAssemblyPublicNonGenericClasses(repositoryAssembly)
               .Where(c => c.Name.EndsWith("Service"))
               .AsPublicImplementedInterfaces(ServiceLifetime.Scoped);
            return services;
        }
    }
}
