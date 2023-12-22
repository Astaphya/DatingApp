using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace DatingApp.Application;

public static class DependecyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
      return services;
    }
}