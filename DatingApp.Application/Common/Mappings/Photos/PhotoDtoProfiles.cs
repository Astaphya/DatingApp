using AutoMapper;
using DatingApp.Domain.Entities.Photos;
using DatingApp.Shared.Models.Photos;

namespace DatingApp.Application.Common.Mappings.Photos;

public class PhotoDtoProfiles : Profile
{
    public PhotoDtoProfiles()
    {
        CreateMap<Photo, PhotoDto>().ReverseMap();
    }
    
}