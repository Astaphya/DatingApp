using System.ComponentModel.DataAnnotations;
using DatingApp.Shared.Interfaces.Users.Auth;

namespace DatingApp.Shared.Models.Users.Auth
{
    public class RegisterDto : IUserRegister
    {
        [Required] public string Username { get; set; } = null!;
        [Required] public string KnownAs { get; set; } = null!;
        [Required] public string Gender { get; set; }  = null!;
        [Required] public DateOnly? DateOfBirth { get; set; } // optional to make required work!
        [Required] public string City { get; set; }  = null!;
        [Required] public string Country { get; set; }  = null!;

        [Required]
        [StringLength(8, MinimumLength = 4)]
        public string Password { get; set; } = null!;
    }
}