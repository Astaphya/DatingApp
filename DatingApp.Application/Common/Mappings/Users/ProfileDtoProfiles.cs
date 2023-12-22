using AutoMapper;
using DatingApp.Domain.Entities.Users;
using DatingApp.Shared.Models.Members;

namespace DatingApp.Application.Common.Mappings.Users;

public class ProfileDtoProfiles : Profile
{
    public ProfileDtoProfiles()
    {
        CreateMap<AppUser, ProfileDto>();
    }
    
}