using DatingApp.Shared.Interfaces.Likes;

namespace DatingApp.Shared.Models.Likes
{
    public class LikeDto : IUserLike
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public int Age { get; set; }
        public string? KnownAs { get; set; }
        public string? PhotoUrl { get; set; }
        public string? City { get; set; }
    }
}