using DatingApp.Application.Handlers.Users.Auth.Commands.Login;
using DatingApp.Application.Handlers.Users.Auth.Commands.Register;
using DatingApp.Shared.Interfaces.Responses;
using DatingApp.Shared.Models.Users;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.Presentation.Controllers
{
    public class AccountController : BaseApiController
    {
        [HttpPost("register")] // POST: api/account/register?username=dave&password=pwd
        public async Task<IAppResponse<UserDto>> Register(RegisterUserCommand registerCommand)
        {
            return await Mediator.Send(registerCommand);
        }

       [HttpPost("login")]
        public async Task<IAppResponse<UserDto>> Login(LoginUserCommand loginCommand)
        {
            return await Mediator.Send(loginCommand);
        }
        
    }
}