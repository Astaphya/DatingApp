using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using DatingApp.Application.Common.Interfaces.Repositories;
using DatingApp.Application.Common.Interfaces.Services;
using DatingApp.Shared.Interfaces.Requests;
using DatingApp.Shared.Interfaces.Responses;
using DatingApp.Shared.Models.Responses;
using DatingApp.Shared.Models.Users;
using DatingApp.Shared.Models.Users.Auth;

namespace DatingApp.Application.Handlers.Users.Auth.Commands.Login;

public class LoginUserCommand: AuthDto ,IAppRequest<UserDto>
{
}

public class LoginUserCommandHandler : IAppRequestHandler<LoginUserCommand, UserDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly ITokenService _tokenService;

    public LoginUserCommandHandler(IUserRepository userRepository, IMapper mapper, ITokenService tokenService)
    {
        this._userRepository = userRepository;
        this._mapper = mapper;
        this._tokenService = tokenService;
    }

    public async Task<IAppResponse<UserDto>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserByUsernameAsync(request.Username);

        if (user is null)
            return new AppResponse<UserDto>().AddError("invalidUsername");


        using var hmac = new HMACSHA512(user.PasswordSalt);
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(request.Password));

        if (computedHash.Where((t, i) => t != user.PasswordHash[i]).Any())
        {
            return new AppResponse<UserDto>().AddError("invalidPassword");
        }

        var userDto = _mapper.Map<UserDto>(user);
        userDto.Token = _tokenService.CreateToken(user);

        return new AppResponse<UserDto>().AddResult(userDto);

    }
}