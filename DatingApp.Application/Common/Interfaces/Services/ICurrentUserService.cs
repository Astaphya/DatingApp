using System.Security.Claims;

namespace DatingApp.Application.Common.Interfaces.Services;

public interface ICurrentUserService
{
    string? GetUsername(ClaimsPrincipal user);
    int GetUserId(ClaimsPrincipal user);
}
