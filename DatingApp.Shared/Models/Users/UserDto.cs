using DatingApp.Shared.Interfaces.Users;

namespace DatingApp.Shared.Models.Users
{
    public class UserDto : IUserbase
    {
        public string Username { get; set; } = null!;
        public string Token { get; set; } = null!;
        public string? PhotoUrl { get; set; }
        public string KnownAs { get; set; } = null!;
        public string Gender { get; set; } = null!;
    }
}