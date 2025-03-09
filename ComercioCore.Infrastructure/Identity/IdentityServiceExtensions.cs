using ComercioCore.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ComercioCore.Infrastructure.Identity
{
    public static class IdentityServiceExtensions
    {
        public static IServiceCollection AddIdentityInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<ComercioCoreDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ComercioCoreDbContext, IdentityRole>()
                .AddEntityFrameworkStores<ComercioCoreDbContext>()
                .AddDefaultTokenProviders();

            return services;
        }
    }
}
