using DatingApp.Application.Common.Helpers;
using DatingApp.Application.Common.Mappings;
using DatingApp.Application.Common.Mappings.Photos;
using DatingApp.Application.Common.Mappings.Users;
using DatingApp.Infrastructure.Persistence.Data;
using DatingApp.Presentation.Extensions;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.Presentation;

public static class PresentationServiceInjection
{
    public static IServiceCollection AddServices(this IServiceCollection services,
        IConfiguration config)
    {
        services.AddCors();
        services.Configure<CloudinarySettings>(config.GetSection("CloudinarySettings"));
        services.AddScoped<LogUserActivity>();
        services.AddAutoMapper(typeof(AutoMapperProfiles),typeof(UserDtoProfiles),typeof(ProfileDtoProfiles),typeof(ImageUploadResultProfiles),
        typeof(PhotoDtoProfiles)); // This registers all profiles in the assembly containing Startup
        services.AddDbContext<ApplicationDbContext>(opt =>
        {
            opt.UseSqlite(config.GetConnectionString("DefaultConnection"));
        });
        return services;
    }

}