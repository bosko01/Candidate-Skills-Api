using Domain.Interfaces.ICandidateRepository;
using Domain.Interfaces.ISkillRepository;
using Domain.Interfaces.UnitOfWork;
using Infrastructure.Repositories.CandidateRepository;
using Infrastructure.Repositories.SkillRepository;
using Infrastructure.Repositories.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        
        public static IServiceCollection AddInfrastructureLayer(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services
                .AddDatabase(configuration)
                .AddRepositories();

            return services;
        }

        private static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("Default"),
                    sql => sql.MigrationsAssembly("Infrastructure")
                ));

            return services;
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<ISkillRepository, SkillRepository>();   
            services.AddScoped<ICandidateRepository, CandidateRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }

    }
}
