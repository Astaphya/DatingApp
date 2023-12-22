using DatingApp.Application.Common.Interfaces.Data;
using DatingApp.Domain.Entities.UserLikes;
using DatingApp.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.Infrastructure.Persistence.Data
{
    public class ApplicationDbContext : DbContext,IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<AppUser> Users { get; set; } 
        public DbSet<UserLike> Likes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserLike>()
                .HasKey(x => new { x.SourceUserId, x.TargetUserId });

            modelBuilder.Entity<UserLike>()
                .HasOne(s=> s.SourceUser)
                .WithMany(l=> l.LikedUsers)
                .HasForeignKey(s => s.SourceUserId)
                .OnDelete(DeleteBehavior.Cascade);


              modelBuilder.Entity<UserLike>()
                .HasOne(s=> s.TargetUser)
                .WithMany(l=> l.LikedByUsers)
                .HasForeignKey(s => s.TargetUserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}