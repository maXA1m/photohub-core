using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PhotoHub.DAL.Entities;

namespace PhotoHub.DAL.Data
{
    /// <summary>
    /// Main DB context in the application.
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        #region Properties

        /// <summary>
        /// Gets and sets <see cref="DbSet"/> of <see cref="User"/>(not identity) entities.
        /// </summary>
        public DbSet<User> AppUsers { get; set; }

        /// <summary>
        /// Gets and sets <see cref="DbSet"/> of <see cref="Comment"/> entities.
        /// </summary>
        public DbSet<Comment> Comments { get; set; }

        /// <summary>
        /// Gets and sets <see cref="DbSet"/> of <see cref="Like"/> entities.
        /// </summary>
        public DbSet<Like> Likes { get; set; }

        /// <summary>
        /// Gets and sets <see cref="DbSet"/> of <see cref="BlackList"/> entities.
        /// </summary>
        public DbSet<BlackList> BlackLists { get; set; }

        /// <summary>
        /// Gets and sets <see cref="DbSet"/> of <see cref="Following"/> entities.
        /// </summary>
        public DbSet<Following> Followings { get; set; }

        /// <summary>
        /// Gets and sets <see cref="DbSet"/> of <see cref="Bookmark"/> entities.
        /// </summary>
        public DbSet<Bookmark> Bookmarks { get; set; }

        /// <summary>
        /// Gets and sets <see cref="DbSet"/> of <see cref="Photo"/> entities.
        /// </summary>
        public DbSet<Photo> Photos { get; set; }

        /// <summary>
        /// Gets and sets <see cref="DbSet"/> of <see cref="Filter"/> entities.
        /// </summary>
        public DbSet<Filter> Filters { get; set; }

        /// <summary>
        /// Gets and sets <see cref="DbSet"/> of <see cref="Confirmed"/> entities.
        /// </summary>
        public DbSet<Confirmed> Confirmed { get; set; }

        /// <summary>
        /// Gets and sets <see cref="DbSet"/> of <see cref="Tag"/> entities.
        /// </summary>
        public DbSet<Tag> Tags { get; set; }

        /// <summary>
        /// Gets and sets <see cref="DbSet"/> of <see cref="Taging"/> entities.
        /// </summary>
        public DbSet<Taging> Tagings { get; set; }

        /// <summary>
        /// Gets and sets <see cref="DbSet"/> of <see cref="PhotoReport"/> entities.
        /// </summary>
        public DbSet<PhotoReport> PhotoReports { get; set; }

        /// <summary>
        /// Gets and sets <see cref="DbSet"/> of <see cref="UserReport"/> entities.
        /// </summary>
        public DbSet<UserReport> UserReports { get; set; }

        #endregion

        #region .ctors

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationDbContext"/>.
        /// </summary>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        #endregion

        #region Logic
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        #endregion
    }
}
