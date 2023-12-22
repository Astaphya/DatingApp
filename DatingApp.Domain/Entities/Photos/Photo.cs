using System.ComponentModel.DataAnnotations.Schema;
using DatingApp.Domain.Entities.Users;
using DatingApp.Shared.Interfaces.Photos;

namespace DatingApp.Domain.Entities.Photos
{
    [Table("Photos")]
    public class Photo : IPhoto
    {
        public int Id { get; set; }
        public string? PhotoUrl { get; set; }
        public bool IsMain { get; set; }
        public string? PublicId { get; set; }
        public AppUser AppUser { get; set; } = null!;
        public int AppUserId { get; set;}
    }
}