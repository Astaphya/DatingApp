using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using DatingApp.Application.Common.Interfaces.Repositories;
using DatingApp.Application.Common.Interfaces.Services;
using DatingApp.Domain.Entities.Users;
using DatingApp.Shared.Interfaces.Requests;
using DatingApp.Shared.Interfaces.Responses;
using DatingApp.Shared.Interfaces.Users.Auth;
using DatingApp.Shared.Models.Responses;
using DatingApp.Shared.Models.Users;
using DatingApp.Shared.Models.Users.Auth;

namespace DatingApp.Application.Handlers.Users.Auth.Commands.Register
{
    public class RegisterUserCommand : RegisterDto ,IAppRequest<UserDto>
    {
    }

    public class RegisterUserCommandHandler : IAppRequestHandler<RegisterUserCommand, UserDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;

        public RegisterUserCommandHandler(IUserRepository userRepository,IMapper mapper,ITokenService tokenService)
        {
            this._userRepository = userRepository;
            this._mapper = mapper;
            this._tokenService = tokenService;
        }
        public async Task<IAppResponse<UserDto>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {

            if (await _userRepository.UserExists(request.Username))
                return new AppResponse<UserDto>().AddError("userAlreadyExists");
            
            var user = _mapper.Map<AppUser>(request);
            SetHashCredentials(request, user);

            await _userRepository.Add(user);
            var result =  await _userRepository.SaveAllAsync();
            
            var userDto = _mapper.Map<UserDto>(user);
            userDto.Token = _tokenService.CreateToken(user);

            return result ? 
                new AppResponse<UserDto>().AddResult(userDto) 
                : new AppResponse<UserDto>().AddError("userCreationFailed");
        }

        private static void SetHashCredentials(IAuthInformation request, AppUser user)
        {
            using var hmac = new HMACSHA512();
            user.Username = request.Username.ToLower();
            user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(request.Password));
            user.PasswordSalt = hmac.Key;
        }
    }
    }

