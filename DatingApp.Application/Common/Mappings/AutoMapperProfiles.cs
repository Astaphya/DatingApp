using AutoMapper;
using DatingApp.Domain.Entities.Photos;
using DatingApp.Domain.Entities.Users;
using DatingApp.Shared.Extensions;
using DatingApp.Shared.Models.Members;
using DatingApp.Shared.Models.Photos;
using DatingApp.Shared.Models.Users.Auth;

namespace DatingApp.Application.Common.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AppUser, ProfileDto>().ForMember(dest => dest.PhotoUrl,opt => opt.MapFrom(src => src.Photos.FirstOrDefault(x => x.IsMain).PhotoUrl))
            .ForMember(dest => dest.Age,opt => opt.MapFrom(src => src.DateOfBirth.CalculateAge()));
            CreateMap<Photo, PhotoDto>();
            CreateMap<ProfileUpdateDto, AppUser>();
            CreateMap<RegisterDto,AppUser>();
            
        }
        
    }
}