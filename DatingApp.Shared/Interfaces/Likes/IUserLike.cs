using DatingApp.Shared.Interfaces.Locations;
using DatingApp.Shared.Interfaces.Users.Auth;
using DatingApp.Shared.Interfaces.Users.Shared;

namespace DatingApp.Shared.Interfaces.Likes;

public interface IUserLike : IUsername,IKnownAs,IPhotoUrl,ICity,IAge
{
    public int Id { get; set; }
}