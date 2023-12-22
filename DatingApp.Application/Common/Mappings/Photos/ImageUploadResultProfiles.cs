using AutoMapper;
using CloudinaryDotNet.Actions;
using DatingApp.Domain.Entities.Photos;

namespace DatingApp.Application.Common.Mappings.Photos;

public class ImageUploadResultProfiles : Profile
{
    public ImageUploadResultProfiles()
    {
        CreateMap<ImageUploadResult, Photo>()
            .ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(src => src.SecureUrl.AbsoluteUri))
            .ForMember(dest => dest.PublicId, opt => opt.MapFrom(src => src.PublicId));
    }
    
}