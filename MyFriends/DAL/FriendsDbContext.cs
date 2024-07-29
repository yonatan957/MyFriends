using Microsoft.EntityFrameworkCore;
using MyFriends.Models;

namespace MyFriends.DAL
{
    public class FriendsDbContext : DbContext
    {
        public FriendsDbContext(DbContextOptions<FriendsDbContext> options) : base(options) 
        {
            //Database.EnsureCreated();
        }
        public DbSet<MyFriends.Models.Friend> Friend { get; set; } = default!;
        public DbSet<MyFriends.Models.Image> Image { get; set; } = default!;
    }
}
