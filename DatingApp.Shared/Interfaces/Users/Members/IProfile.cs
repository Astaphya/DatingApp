using DatingApp.Shared.Interfaces.Locations;
using DatingApp.Shared.Interfaces.Users.Auth;
using DatingApp.Shared.Interfaces.Users.Shared;
using DatingApp.Shared.Models.Photos;

namespace DatingApp.Shared.Interfaces.Users.Members;

public interface IProfile : IUserbase,ILocation,IProfileDetails,IPhotoUrl,IAge,IUsername
{
    public int Id { get; set; }
    public DateTime Created { get; set; }
    public DateTime LastActive { get; set;}
    public ICollection<PhotoDto> Photos { get; set; }
}