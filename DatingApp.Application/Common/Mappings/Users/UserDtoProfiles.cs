using AutoMapper;
using DatingApp.Domain.Entities.Users;
using DatingApp.Shared.Models.Users;

namespace DatingApp.Application.Common.Mappings.Users;

public class UserDtoProfiles : Profile
{

    public UserDtoProfiles()
    {
        CreateMap<AppUser, UserDto>()
            .ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(src => src.Photos.FirstOrDefault(x=>x.IsMain).PhotoUrl));
    }
    
}