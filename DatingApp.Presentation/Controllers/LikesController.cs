using DatingApp.Application.Common.Helpers;
using DatingApp.Application.Handlers.Likes.Commands;
using DatingApp.Application.Handlers.Likes.Queries;
using DatingApp.Presentation.Extensions;
using DatingApp.Shared.Interfaces.Responses;
using DatingApp.Shared.Models.Likes;
using DatingApp.Shared.Models.Responses;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.Presentation.Controllers
{
    public class LikesController : BaseApiController
    {
        [HttpPost("{username}")]
        public async Task<IAppResponse<string>> AddLike(string username)
        {
            return await Mediator.Send(new AddUserLikeCommand() { Username = username });
        }

        [HttpGet]
        public async Task<IAppResponse<PagedList<LikeDto>>> GetUserLikes([FromQuery]GetPaginatedUserLikesQuery userLikesQuery)
        {
           var query = await Mediator.Send(userLikesQuery);
           if (!query.IsSuccess)
               return new AppResponse<PagedList<LikeDto>>().AddErrors(query.Errors);
            
           Response.AddPaginationHeader(new PaginationHeader(query.Result.CurrentPage,query.Result.PageSize
               ,query.Result.TotalCount,query.Result.TotalPages));

           return query;
        }
        
    }
}