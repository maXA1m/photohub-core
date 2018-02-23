using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PhotoHub.DAL.Entities;

namespace PhotoHub.DAL.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<BlackList> BlackLists { get; set; }
        public DbSet<Following> Followings { get; set; }
        public DbSet<PhotoView> PhotoViews { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Filter> Filters { get; set; }
        public DbSet<Confirmed> Confirmed { get; set; }
        public DbSet<Giveaway> Giveaways { get; set; }
        public DbSet<GiveawayOwner> GiveawayOwners { get; set; }
        public DbSet<Winner> Winners { get; set; }
        public DbSet<Participant> Participants { get; set; }
        public DbSet<BlackListGiveaway> BlackListsGiveaway { get; set; }

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
