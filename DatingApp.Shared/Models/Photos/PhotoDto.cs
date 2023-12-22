using DatingApp.Shared.Interfaces.Photos;

namespace DatingApp.Shared.Models.Photos
{
    public class PhotoDto :IPhoto
    {
        public int Id { get; set; }
        public string? PhotoUrl { get; set; }
        public bool IsMain { get; set; }
    }
}