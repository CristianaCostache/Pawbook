using Microsoft.EntityFrameworkCore;

namespace Pawbook.Models
{
    public class PawbookContext : DbContext
    {
        public PawbookContext(DbContextOptions<PawbookContext> options) : base(options)
        { }

        public DbSet<User>? Users { get; set; }
        public DbSet<Post>? Posts { get; set; }
        public DbSet<Comment>? Comments { get; set; }
        public DbSet<Paw>? Paws { get; set; }
        public DbSet<Friendship>? Friendships { get; set; }
        public DbSet<Hatership>? Haterships { get; set; }
    }
}
