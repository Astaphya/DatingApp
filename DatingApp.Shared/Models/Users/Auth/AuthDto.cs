using DatingApp.Shared.Interfaces.Users.Auth;

namespace DatingApp.Shared.Models.Users.Auth
{
    public class AuthDto : IAuthInformation
    {
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}