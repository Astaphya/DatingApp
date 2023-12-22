using System.Security.Claims;
using AutoMapper;
using DatingApp.Application.Common.Interfaces.Repositories;
using DatingApp.Application.Common.Interfaces.Services;
using DatingApp.Domain.Entities.UserLikes;
using DatingApp.Shared.Interfaces.Requests;
using DatingApp.Shared.Interfaces.Responses;
using DatingApp.Shared.Models.Responses;

namespace DatingApp.Application.Handlers.Likes.Commands;

public class AddUserLikeCommand : IAppRequest<string>
{
    public string? Username { get; set; }
}

public class AddUserLikeCommandHandler : IAppRequestHandler<AddUserLikeCommand, string>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;
    private readonly ILikesRepository _likesRepository;

    public AddUserLikeCommandHandler(IUserRepository userRepository,
        IMapper mapper,
        ICurrentUserService currentUserService,
        ILikesRepository likesRepository)
    {
        this._userRepository = userRepository;
        this._mapper = mapper;
        _currentUserService = currentUserService;
        _likesRepository = likesRepository;
    }

    public async Task<IAppResponse<string>> Handle(AddUserLikeCommand request, CancellationToken cancellationToken)
    {
        var sourceUserId = _currentUserService.GetUserId(ClaimsPrincipal.Current);
        var likedUser = await _userRepository.GetUserByUsernameAsync(request.Username);
        var sourceUser = await _likesRepository.GetUserWithLikes(sourceUserId);

        if (likedUser is null)
            return new AppResponse<string>().AddError("likedUserNotFound");

        if (sourceUser.Username.Equals(request.Username))
            return new AppResponse<string>().AddError("cantLikeYourself!");

        var userLike = await _likesRepository.GetUserLike(sourceUserId, likedUser.Id);

        if (userLike != null)
            return new AppResponse<string>().AddError("alreadyLikedThisUser");

        userLike = new UserLike
        {
            SourceUserId = sourceUserId,
            TargetUserId = likedUser.Id
        };

        sourceUser.LikedUsers.Add(userLike);

        if (await _userRepository.SaveAllAsync())
            return new AppResponse<string>().AddResult(likedUser.Username + " " + "likedSuccesfully");

        return new AppResponse<string>().AddError("failedToLikeUser");
    }
}

