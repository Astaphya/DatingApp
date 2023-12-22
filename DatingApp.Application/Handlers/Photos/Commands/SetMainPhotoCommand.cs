using System.Security.Claims;
using DatingApp.Application.Common.Interfaces.Repositories;
using DatingApp.Application.Common.Interfaces.Services;
using DatingApp.Shared.Interfaces.Requests;
using DatingApp.Shared.Interfaces.Responses;
using DatingApp.Shared.Models.Responses;

namespace DatingApp.Application.Handlers.Photos.Commands;

public class SetMainPhotoCommand :  IAppRequest<string>
{
    public int PhotoId { get; set; }
}

public class SetMainPhotoCommandHandler : IAppRequestHandler<SetMainPhotoCommand, string>
{
    private readonly IUserRepository _userRepository;
    private readonly ICurrentUserService _currentUserService;

    public SetMainPhotoCommandHandler(IUserRepository userRepository,
        ICurrentUserService currentUserService)
    {
        this._userRepository = userRepository;
        _currentUserService = currentUserService;
    }

    public async Task<IAppResponse<string>> Handle(SetMainPhotoCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserByUsernameAsync(
            _currentUserService.GetUsername(ClaimsPrincipal.Current));

        if (user is null)
            return new AppResponse<string>().AddError("userIsNotFound");

        var photo = user.Photos.FirstOrDefault(x => x.Id == request.PhotoId);

        if (photo is null)
            return new AppResponse<string>().AddError("photoIsNotFound");

        if (photo.IsMain)
            return new AppResponse<string>().AddError("thisIsAlreadyYourMainPhoto");

        var currentMain = user.Photos.FirstOrDefault(x => x.IsMain);

        if (currentMain != null)
            currentMain.IsMain = false;

        photo.IsMain = true;

        if (await _userRepository.SaveAllAsync())
            return new AppResponse<string>().AddResult("mainPhotoUpdatedSuccesfully");

        return new AppResponse<string>().AddError("failedToSetPhotoAsMain");

    }
}
