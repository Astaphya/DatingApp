using DatingApp.Application.Common.Interfaces.Repositories;
using DatingApp.Application.Common.Interfaces.Services;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DatingApp.Presentation.Extensions
{
    public class LogUserActivity : IAsyncActionFilter
    {
        private readonly ICurrentUserService _currentUserService;

        public LogUserActivity(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
           var resultContext = await next();

           if(!resultContext.HttpContext.User.Identity.IsAuthenticated) return;

           var userId =_currentUserService.GetUserId(resultContext.HttpContext.User);


           var repo = resultContext.HttpContext.RequestServices.GetRequiredService<IUserRepository>();
           var user = await repo.GetUserByIdAsync(userId);
           user.LastActive = DateTime.UtcNow;
           await repo.SaveAllAsync();
        }
    }
}