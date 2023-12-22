
using DatingApp.Domain.Entities.Users;

namespace DatingApp.Application.Common.Interfaces.Services
{
    public interface ITokenService
    {
        string CreateToken(AppUser appUser);
        
    }
}