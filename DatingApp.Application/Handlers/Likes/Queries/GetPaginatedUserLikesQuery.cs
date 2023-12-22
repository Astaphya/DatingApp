using System.Security.Claims;
using AutoMapper;
using DatingApp.Application.Common.Helpers;
using DatingApp.Application.Common.Interfaces.Repositories;
using DatingApp.Application.Common.Interfaces.Services;
using DatingApp.Shared.Interfaces.Requests;
using DatingApp.Shared.Interfaces.Responses;
using DatingApp.Shared.Models.Likes;
using DatingApp.Shared.Models.Responses;

namespace DatingApp.Application.Handlers.Likes.Queries;

public class GetPaginatedUserLikesQuery : LikesParams ,IAppRequest<PagedList<LikeDto>>
{
    
}

public class GetPaginatedUserLikesQueryHandler : IAppRequestHandler<GetPaginatedUserLikesQuery, PagedList<LikeDto>>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly ITokenService _tokenService;
    private readonly ICurrentUserService _currentUserService;
    private readonly ILikesRepository _likesRepository;

    public GetPaginatedUserLikesQueryHandler(
        IUserRepository userRepository,
        IMapper mapper,
        ICurrentUserService currentUserService,
        ILikesRepository likesRepository)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _currentUserService = currentUserService;
        _likesRepository = likesRepository;
    }

    public async Task<IAppResponse<PagedList<LikeDto>>> Handle(GetPaginatedUserLikesQuery request,
        CancellationToken cancellationToken)
    {
        request.UserId = _currentUserService.GetUserId(ClaimsPrincipal.Current);
        var users = await _likesRepository.GetUserLikes(request);
        return new AppResponse<PagedList<LikeDto>>().AddResult(users);
    }
}