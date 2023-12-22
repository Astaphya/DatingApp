using DatingApp.Domain.Entities.Photos;
using DatingApp.Domain.Entities.UserLikes;
using DatingApp.Shared.Interfaces.Locations;
using DatingApp.Shared.Interfaces.Users;
using DatingApp.Shared.Interfaces.Users.Auth;
using DatingApp.Shared.Interfaces.Users.Shared;

namespace DatingApp.Domain.Entities.Users
{
    public class AppUser : IUserbase,ILocation,IPersonalDetails,IProfileDetails,IUsername
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public byte[] PasswordHash { get; set; } = null!;
        public byte[] PasswordSalt { get; set; } = null!;
        public DateOnly DateOfBirth  { get; set; }
        public string? KnownAs { get; set;}
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime LastActive { get; set;} = DateTime.UtcNow;
        public string? Gender { get; set;}
        public string? Introduction { get; set;}
        public string? LookingFor { get; set;}
        public string? Interests { get; set;}
        public string? City { get; set;}
        public string? Country { get; set;}
        public List<Photo> Photos { get; set; } = new ();
        public List<UserLike> LikedByUsers { get; set; } = new();
        public List<UserLike> LikedUsers { get; set; } = new();

    }
}