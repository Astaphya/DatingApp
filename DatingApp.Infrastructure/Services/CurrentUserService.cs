using System.Security.Claims;
using DatingApp.Application.Common.Interfaces.Services;

namespace DatingApp.Infrastructure.Services;

public class CurrentUserService : ICurrentUserService
{
    public string? GetUsername(ClaimsPrincipal user) => user.FindFirst(ClaimTypes.Name)?.Value;
    public int GetUserId(ClaimsPrincipal user) =>  int.Parse(user.FindFirst(ClaimTypes.NameIdentifier)?.Value);
    
}