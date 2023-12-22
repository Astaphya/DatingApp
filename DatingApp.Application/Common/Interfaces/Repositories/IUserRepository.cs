using DatingApp.Application.Common.Helpers;
using DatingApp.Domain.Entities.Users;
using DatingApp.Shared.Models.Members;

namespace DatingApp.Application.Common.Interfaces.Repositories
{
    public interface IUserRepository
    {
        // void Update(AppUser user);
        Task<bool> SaveAllAsync();
        Task<IEnumerable<AppUser>> GetUsersAsync();
        Task<AppUser?> GetUserByIdAsync(int id);
        Task<AppUser?> GetUserByUsernameAsync(string username);
        Task<PagedList<ProfileDto>> GetMembersAsync(UserParams userParams);
        Task<AppUser?> GetMemberAsync(string username);
        Task<bool> UserExists(string username);
        Task Add(AppUser user);
        Task<bool> UpdateUser(AppUser user);





    }
}