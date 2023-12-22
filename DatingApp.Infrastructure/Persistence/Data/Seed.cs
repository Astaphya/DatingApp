using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using DatingApp.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.Infrastructure.Persistence.Data
{
    public class Seed
    {
        public static async Task SeedUsers(ApplicationDbContext context)
        {
            if(await context.Users.AnyAsync()) return; // if users exist, return. 
            
            // if not, add users. 
            // read json file and convert to list of users. 
            var userData = await File.ReadAllTextAsync("Data/UserSeedData.json");
            var options = new JsonSerializerOptions {PropertyNameCaseInsensitive = true};
            var users = JsonSerializer.Deserialize<List<AppUser>>(userData);

            foreach(var user in users) 
            {
                using var hmac = new HMACSHA512();

                user.Username = user.Username.ToLower();
                user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("Pa$$w0rd"));
                user.PasswordSalt = hmac.Key;

                context.Users.Add(user);
            }

            await context.SaveChangesAsync();
        }
        
    }
}