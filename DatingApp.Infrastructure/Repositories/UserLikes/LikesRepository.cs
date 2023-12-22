using DatingApp.Application.Common.Helpers;
using DatingApp.Application.Common.Interfaces.Repositories;
using DatingApp.Presentation.Data;
using DatingApp.Domain.Entities.UserLikes;
using DatingApp.Domain.Entities.Users;
using DatingApp.Infrastructure.Persistence;
using DatingApp.Infrastructure.Persistence.Data;
using DatingApp.Shared.Extensions;
using DatingApp.Shared.Models.Likes;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.Infrastructure.Repositories.UserLikes
{
    public class LikesRepository : ILikesRepository
    {
        private readonly ApplicationDbContext _context;
        public  LikesRepository(ApplicationDbContext context)
        {
          _context = context;
            
        }
        public async Task<UserLike> GetUserLike(int sourceUserId, int targetUserId)
        {
             return await _context.Likes.FindAsync(sourceUserId, targetUserId);
        }

        public async  Task<PagedList<LikeDto>> GetUserLikes(LikesParams likesParams)
        {
            var users = _context.Users.OrderBy(u=> u.Username).AsQueryable();
            var likes = _context.Likes.AsQueryable();

            switch (likesParams.Predicate)
            {
                case "liked":
                    likes = likes.Where(like => like.SourceUserId == likesParams.UserId);
                    users = likes.Select(like => like.TargetUser);
                    break;
                case "likedBy":
                    likes = likes.Where(like => like.TargetUserId == likesParams.UserId);
                    users = likes.Select(like => like.SourceUser);
                    break;
            }

            var likedUsers = users.Select(user => new LikeDto
            {
                Username = user.Username,
                KnownAs = user.KnownAs,
                Age = user.DateOfBirth.CalculateAge(),
                PhotoUrl = user.Photos.FirstOrDefault(x => x.IsMain).PhotoUrl,
                City = user.City,
                Id = user.Id
            });

            return await PagedList<LikeDto>.CreateAsync(likedUsers, likesParams.PageNumber, likesParams.PageSize);
        }

        public async Task<AppUser> GetUserWithLikes(int userId)
        {
           return await _context.Users
            .Include(x=> x.LikedUsers)
            .FirstOrDefaultAsync(x=> x.Id == userId);
        }
    }
}