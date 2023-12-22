using System.Security.Claims;
using AutoMapper;
using DatingApp.Application.Common.Interfaces.Repositories;
using DatingApp.Application.Common.Interfaces.Services;
using DatingApp.Shared.Interfaces.Requests;
using DatingApp.Shared.Interfaces.Responses;
using DatingApp.Shared.Models.Members;
using DatingApp.Shared.Models.Responses;

namespace DatingApp.Application.Handlers.Users.Commands;


public class UpdateProfileCommand : ProfileDto ,IAppRequest<string>
{
}

public class UpdateProfileCommandHandler : IAppRequestHandler<UpdateProfileCommand, string>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;

    public UpdateProfileCommandHandler(IUserRepository userRepository,IMapper mapper,ICurrentUserService currentUserService)
    {
        this._userRepository = userRepository;
        this._mapper = mapper;
        _currentUserService = currentUserService;
    }

    public async Task<IAppResponse<string>> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserByUsernameAsync(_currentUserService.GetUsername(ClaimsPrincipal.Current));

        if (user is null)
            return new AppResponse<string>().AddError("userNotFound");
        
        _mapper.Map(request, user);
        var result = await _userRepository.SaveAllAsync();
        return result ? 
            new AppResponse<string>().AddResult("profileUpdatedSuccesfully") 
            : new AppResponse<string>().AddError("profileUpdateFailed");
    }



}