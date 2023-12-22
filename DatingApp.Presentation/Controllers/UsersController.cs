using DatingApp.Presentation.Extensions;
using DatingApp.Application.Common.Helpers;
using DatingApp.Application.Handlers.Photos.Commands;
using DatingApp.Application.Handlers.Users.Commands;
using DatingApp.Application.Handlers.Users.Queries;
using DatingApp.Shared.Interfaces.Responses;
using DatingApp.Shared.Models.Members;
using DatingApp.Shared.Models.Photos;
using DatingApp.Shared.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.Presentation.Controllers;

[Authorize]
public class UsersController : BaseApiController
{
    [HttpGet("users")]
    public async Task<IAppResponse<PagedList<ProfileDto>>> GetUsers(
        [FromQuery] GetPaginatedUsersQuery paginatedUsersQuery)
    {
        var query = await Mediator.Send(paginatedUsersQuery);

        if (!query.IsSuccess)
            return new AppResponse<PagedList<ProfileDto>>().AddErrors(query.Errors);

        Response.AddPaginationHeader(new PaginationHeader(query.Result.CurrentPage, query.Result.PageSize,
            query.Result.TotalCount, query.Result.TotalPages));
        return query;
    }

    [HttpGet("{username}")]
    public async Task<IAppResponse<ProfileDto>> GetUser(string username)
    {
        return await Mediator.Send(new GetUserByUsernameQuery { Username = username });
    }


    [HttpPut("update")]
    public async Task<IAppResponse<string>> UpdateUser(UpdateProfileCommand updateProfileCommand)
    {
        return await Mediator.Send(updateProfileCommand);
    }

    [HttpPost("add-photo")]
    public async Task<IAppResponse<PhotoDto>> AddPhoto(AddPhotoCommand addPhotoCommand)
    {
        return await Mediator.Send(addPhotoCommand);
    }


    [HttpPut("set-main-photo/{photoId:int}")]
    public async Task<IAppResponse<string>> SetMainPhoto(int photoId)
    {
        return await Mediator.Send(new SetMainPhotoCommand() { PhotoId = photoId });
    }

    [HttpDelete("delete-photo/{photoId:int}")]
    public async Task<IAppResponse<string>> DeletePhoto(int photoId)
    {
        return await Mediator.Send(new DeletePhotoCommand() { PhotoId = photoId });
    }
}