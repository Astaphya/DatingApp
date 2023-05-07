using System.Security.Claims;

namespace API.Extensions
{
    public static class ClaimsPrinciplesExtensions
    {
        // extension method to get the username of the user
        public static string GetUsername(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }        
        
    }
}