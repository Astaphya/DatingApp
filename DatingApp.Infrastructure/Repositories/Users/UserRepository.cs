using AutoMapper;
using AutoMapper.QueryableExtensions;
using DatingApp.Application.Common.Helpers;
using DatingApp.Application.Common.Interfaces.Data;
using DatingApp.Application.Common.Interfaces.Repositories;
using DatingApp.Domain.Entities.Users;
using DatingApp.Shared.Models.Members;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.Infrastructure.Repositories.Users;

public class UserRepository : IUserRepository
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public UserRepository(IApplicationDbContext context, IMapper mapper)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<AppUser?> GetMemberAsync(string username)
    {
        return  _context.Users
            .FirstOrDefault(x => x.Username == username);
    }

    public async Task<PagedList<ProfileDto>> GetMembersAsync(UserParams userParams)
    {
        var query = _context.Users.AsQueryable();

        query = query.Where(x => x.Username != userParams.CurrentUsername);
        query = query.Where(x => x.Gender == userParams.Gender);

        var minDob = DateOnly.FromDateTime(DateTime.Now.AddYears(-userParams.MaxAge - 1));
        var maxDob = DateOnly.FromDateTime(DateTime.Now.AddYears(-userParams.MinAge));

        query = query.Where(x => x.DateOfBirth >= minDob && x.DateOfBirth <= maxDob);

        query = userParams.OrderBy switch
        {
            "created" => query.OrderByDescending(u => u.Created),
            _ => query.OrderByDescending(u => u.LastActive)
        };

        return await PagedList<ProfileDto>.CreateAsync(
            query.AsNoTracking().ProjectTo<ProfileDto>(_mapper.ConfigurationProvider)
            , userParams.PageNumber, userParams.PageSize);
    }

    public async Task<AppUser?> GetUserByIdAsync(int id)
    {
        return await _context.Users.FindAsync(id);
    }

    public async Task<AppUser?> GetUserByUsernameAsync(string username)
    {
        return await _context.Users
            .Include(p => p.Photos)
            .SingleOrDefaultAsync(x => x.Username == username);
    }

    public async Task<IEnumerable<AppUser>> GetUsersAsync()
    {
        return await _context.Users.Include(p => p.Photos).ToListAsync();
    }

    public async Task<bool> SaveAllAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> UserExists(string username)
    {
        return await _context.Users.AnyAsync(x => x.Username == username.ToLower());
    }

    public async Task Add(AppUser user)
    {
        await _context.Users.AddAsync(user);
    }

    public async Task<bool> UpdateUser(AppUser user)
    {
        _context.Users.Update(user);
        return await SaveAllAsync();
    }

    // public void Update(AppUser user)
    // {
    //     _context.Entry(user).State = EntityState.Modified;
    // }
}