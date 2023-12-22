using System.Security.Claims;
using DatingApp.Application.Common.Interfaces.Repositories;
using DatingApp.Application.Common.Interfaces.Services;
using DatingApp.Shared.Interfaces.Requests;
using DatingApp.Shared.Interfaces.Responses;
using DatingApp.Shared.Models.Responses;

namespace DatingApp.Application.Handlers.Photos.Commands;

public class DeletePhotoCommand :  IAppRequest<string>
{
    public int PhotoId { get; set; }
}

public class DeletePhotoCommandHandler : IAppRequestHandler<DeletePhotoCommand, string>
{
    private readonly IUserRepository _userRepository;
    private readonly ICurrentUserService _currentUserService;
    private readonly IPhotoService _photoService;

    public DeletePhotoCommandHandler(IUserRepository userRepository,
        ICurrentUserService currentUserService,
        IPhotoService photoService)
    {
        this._userRepository = userRepository;
        _currentUserService = currentUserService;
        _photoService = photoService;
    }

    public async Task<IAppResponse<string>> Handle(DeletePhotoCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserByUsernameAsync(_currentUserService.GetUsername(ClaimsPrincipal.Current));

        if (user is null)
            return new AppResponse<string>().AddError("userIsNotFound");

        var photo = user.Photos.FirstOrDefault(x => x.Id == request.PhotoId);

        if (photo is null) 
            return new AppResponse<string>().AddError("photoIsNotFound");


        if (photo.IsMain) 
            return new AppResponse<string>().AddError("cantDeleteMainPhoto");
        
        if (photo.PublicId != null)
        {
            var result = await _photoService.DeletePhotoAsync(photo.PublicId);
            
            if (result.Error != null)
                return new AppResponse<string>().AddError(result.Error.Message);
        }

        user.Photos.Remove(photo);

        if (await _userRepository.SaveAllAsync())
            return new AppResponse<string>().AddResult("photoDeletedSuccessfully");

        return new AppResponse<string>().AddError("problemOccuredDeletingPhoto");
        
    }
}