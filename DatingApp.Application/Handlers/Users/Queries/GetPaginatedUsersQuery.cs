using System.Security.Claims;
using AutoMapper;
using DatingApp.Application.Common.Helpers;
using DatingApp.Application.Common.Interfaces.Repositories;
using DatingApp.Application.Common.Interfaces.Services;
using DatingApp.Shared.Interfaces.Requests;
using DatingApp.Shared.Interfaces.Responses;
using DatingApp.Shared.Models.Members;
using DatingApp.Shared.Models.Responses;

namespace DatingApp.Application.Handlers.Users.Queries;

public class GetPaginatedUsersQuery : UserParams ,IAppRequest<PagedList<ProfileDto>>
{
    
}

public class GetPaginatedUsersQueryHandler : IAppRequestHandler<GetPaginatedUsersQuery, PagedList<ProfileDto>>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly ITokenService _tokenService;
    private readonly ICurrentUserService _currentUserService;

    public GetPaginatedUsersQueryHandler(
        IUserRepository userRepository,
        IMapper mapper,
        ITokenService tokenService,
        ICurrentUserService currentUserService)
    {
        this._userRepository = userRepository;
        this._mapper = mapper;
        this._tokenService = tokenService;
        _currentUserService = currentUserService;
    }

    public async Task<IAppResponse<PagedList<ProfileDto>>> Handle(GetPaginatedUsersQuery request,
        CancellationToken cancellationToken)
    {
        var currentUser =
            await _userRepository.GetUserByUsernameAsync(_currentUserService.GetUsername(ClaimsPrincipal.Current));
        if (currentUser is null)
            return new AppResponse<PagedList<ProfileDto>>().AddError("userDoesNotAuthorize");

        request.CurrentUsername = currentUser.Username;

        if (string.IsNullOrEmpty(request.Gender))
        {
            request.Gender = currentUser.Gender == "male" ? "female" : "male";
        }

        var users = await _userRepository.GetMembersAsync(request);
        return new AppResponse<PagedList<ProfileDto>>().AddResult(users);
    }
}

