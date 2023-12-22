using DatingApp.Shared.Interfaces.Users.Members;

namespace DatingApp.Shared.Models.Members
{
    public class ProfileUpdateDto : IProfileUpdate
    {
        public string? Introduction { get; set; }
        public string? LookingFor { get; set; }
        public string? Interests { get; set; }
        public string? City { get; set; }
        public string? Country  { get; set; }
        
    }
}