using ExamApp.Repository.Infrastructure;
using ExamApp.Service.Infrastructure;

namespace ExamApp.Api.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddProjectDependencies(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Program));
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddRepository().AddService();
            return services;
        }
    }
}
