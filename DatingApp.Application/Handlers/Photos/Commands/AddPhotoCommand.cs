using System.Security.Claims;
using AutoMapper;
using DatingApp.Application.Common.Interfaces.Repositories;
using DatingApp.Application.Common.Interfaces.Services;
using DatingApp.Domain.Entities.Photos;
using DatingApp.Shared.Interfaces.Requests;
using DatingApp.Shared.Interfaces.Responses;
using DatingApp.Shared.Models.Photos;
using DatingApp.Shared.Models.Responses;
using Microsoft.AspNetCore.Http;

namespace DatingApp.Application.Handlers.Photos.Commands;

public class AddPhotoCommand :  IAppRequest<PhotoDto>
{
    public IFormFile? File { get; set; }
}

public class AddPhotoCommandHandler : IAppRequestHandler<AddPhotoCommand, PhotoDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;
    private readonly IPhotoService _photoService;

    public AddPhotoCommandHandler(IUserRepository userRepository,
        IMapper mapper,
        ICurrentUserService currentUserService,
        IPhotoService photoService)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _currentUserService = currentUserService;
        _photoService = photoService;
    }

    public async Task<IAppResponse<PhotoDto>> Handle(AddPhotoCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserByUsernameAsync(_currentUserService.GetUsername(ClaimsPrincipal.Current));

        if (user is null)
            return new AppResponse<PhotoDto>().AddError("userNotFound");

        if (request.File is null)
            return new AppResponse<PhotoDto>().AddError("shouldUploadPhoto");

        var imageUploadResult = await _photoService.AddPhotoAsync(request.File);

        if (imageUploadResult.Error != null)
            return new AppResponse<PhotoDto>().AddError(imageUploadResult.Error.Message);

        var photo = _mapper.Map<Photo>(imageUploadResult);
        
        if(user.Photos.Count == 0) photo.IsMain = true;
        user.Photos.Add(photo);

        if(await _userRepository.SaveAllAsync())
        {
            return new AppResponse<PhotoDto>().AddResult(_mapper.Map<PhotoDto>(photo));
        }
            
        return new AppResponse<PhotoDto>().AddError("somethingWentWrong");
    }



}