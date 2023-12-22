using AutoMapper;
using DatingApp.Application.Common.Interfaces.Repositories;
using DatingApp.Shared.Interfaces.Requests;
using DatingApp.Shared.Interfaces.Responses;
using DatingApp.Shared.Models.Members;
using DatingApp.Shared.Models.Responses;

namespace DatingApp.Application.Handlers.Users.Queries;

public class GetUserByUsernameQuery: IAppRequest<ProfileDto>
{
    public string? Username { get; set; } 
}

public class GetUserByUsernameQueryHandler : IAppRequestHandler<GetUserByUsernameQuery, ProfileDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetUserByUsernameQueryHandler(IUserRepository userRepository, IMapper mapper)
    {
        this._userRepository = userRepository;
        this._mapper = mapper;
    }

    public async Task<IAppResponse<ProfileDto>> Handle(GetUserByUsernameQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserByUsernameAsync(request.Username);
        
        return user is null ? 
            new AppResponse<ProfileDto>().AddError("userNotFound") 
            : new AppResponse<ProfileDto>().AddResult(_mapper.Map<ProfileDto>(user));
    }
}