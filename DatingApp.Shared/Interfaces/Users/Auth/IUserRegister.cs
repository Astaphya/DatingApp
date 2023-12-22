using DatingApp.Shared.Interfaces.Locations;

namespace DatingApp.Shared.Interfaces.Users.Auth;

public interface IUserRegister : IUserbase,IAuthInformation,ILocation
{
    public DateOnly? DateOfBirth { get; set; }
}