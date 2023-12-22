using DatingApp.Domain.Entities.UserLikes;
using DatingApp.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.Application.Common.Interfaces.Data;

public interface IApplicationDbContext
{
    public DbSet<AppUser> Users { get; set; } 
    public DbSet<UserLike> Likes { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
    
    DbSet<TDbSet> Set<TDbSet>() where TDbSet : class;

    
}