using DatingApp.Application.Common.Helpers;
using DatingApp.Domain.Entities.UserLikes;
using DatingApp.Domain.Entities.Users;
using DatingApp.Shared.Models.Likes;

namespace DatingApp.Application.Common.Interfaces.Repositories
{
    public interface ILikesRepository
    {
        Task<UserLike> GetUserLike(int sourceUserId, int targetUserId);
        Task<AppUser> GetUserWithLikes(int userId);
        Task<PagedList<LikeDto>> GetUserLikes(LikesParams likesParams);
        
    }
}