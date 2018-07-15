#region using Microsoft
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
#endregion
using PhotoHub.DAL.Entities;

namespace PhotoHub.DAL.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<User> AppUsers { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<BlackList> BlackLists { get; set; }
        public DbSet<Following> Followings { get; set; }
        public DbSet<Bookmark> Bookmarks { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Filter> Filters { get; set; }
        public DbSet<Confirmed> Confirmed { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Taging> Tagings { get; set; }
        public DbSet<PhotoReport> PhotoReports { get; set; }
        public DbSet<UserReport> UserReports { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
