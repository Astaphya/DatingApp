using DatingApp.Shared.Interfaces.Users.Shared;

namespace DatingApp.Shared.Interfaces.Photos;

public interface IPhoto : IPhotoUrl
{
    public int Id { get; set; }
    public bool IsMain { get; set; }
}