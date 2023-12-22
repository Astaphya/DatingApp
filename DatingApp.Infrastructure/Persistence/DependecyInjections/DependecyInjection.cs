using DatingApp.Application.Common.Interfaces.Data;
using DatingApp.Application.Common.Interfaces.Repositories;
using DatingApp.Application.Common.Interfaces.Services;
using DatingApp.Infrastructure.Persistence.Data;
using DatingApp.Infrastructure.Repositories.UserLikes;
using DatingApp.Infrastructure.Repositories.Users;
using DatingApp.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DatingApp.Infrastructure.Persistence.DependecyInjections
{
    public static class DependecyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
            IConfiguration config)
        {
            // services.AddDbContext<DataContext>(opt =>
            // {
            //     opt.UseSqlite(config.GetConnectionString("DefaultConnection"));
            // });

            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPhotoService,PhotoService>();
             services.AddScoped<ILikesRepository,LikesRepository>();
             services.AddScoped<ICurrentUserService, CurrentUserService>();
             services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
             return services;
        }
    }
}