using System;
using System.Threading.Tasks;
using PhotoHub.DAL.Interfaces;
using PhotoHub.DAL.Data;
using PhotoHub.DAL.Entities;

namespace PhotoHub.DAL.Repositories
{
    /// <summary>
    /// Contains properties with repositories, grant access to repositories and can save db state.
    /// Implementation of <see cref="IUnitOfWork"/>.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        #region Fields

        private readonly ApplicationDbContext _context;

        private bool _disposed;

        #endregion

        #region Properties

        /// <summary>
        /// Gets application user repository.
        /// </summary>
        public IRepository<ApplicationUser> IdentityUsers { get; protected set; }

        /// <summary>
        /// Gets user repository.
        /// </summary>
        public IRepository<User> Users { get; protected set; }

        /// <summary>
        /// Gets photo repository.
        /// </summary>
        public IRepository<Photo> Photos { get; protected set; }

        /// <summary>
        /// Gets comment repository.
        /// </summary>
        public IRepository<Comment> Comments { get; protected set; }

        /// <summary>
        /// Gets like repository.
        /// </summary>
        public IRepository<Like> Likes { get; protected set; }

        /// <summary>
        /// Gets confirmed repository.
        /// </summary>
        public IRepository<Confirmed> Confirmations { get; protected set; }

        /// <summary>
        /// Gets following repository.
        /// </summary>
        public IRepository<Following> Followings { get; protected set; }

        /// <summary>
        /// Gets blacklist repository.
        /// </summary>
        public IRepository<BlackList> Blockings { get; protected set; }

        /// <summary>
        /// Gets bookmark repository.
        /// </summary>
        public IRepository<Bookmark> Bookmarks { get; protected set; }

        /// <summary>
        /// Gets filter repository.
        /// </summary>
        public IRepository<Filter> Filters { get; protected set; }

        /// <summary>
        /// Gets taging repository.
        /// </summary>
        public IRepository<Taging> Tagings { get; protected set; }

        /// <summary>
        /// Gets tag repository.
        /// </summary>
        public IRepository<Tag> Tags { get; protected set; }

        /// <summary>
        /// Gets photo report repository.
        /// </summary>
        public IRepository<PhotoReport> PhotoReports { get; protected set; }

        /// <summary>
        /// Gets user report repository.
        /// </summary>
        public IRepository<UserReport> UserReports { get; protected set; }

        #endregion

        #region .ctors

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork"/>.
        /// </summary>
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;

            IdentityUsers = new UsersIdentityRepository(context);
            Users = new UsersRepository(context);
            Photos = new PhotosRepository(context);
            Comments = new CommentsRepository(context);
            Likes = new LikesRepository(context);
            Followings = new FollowingsRepository(context);
            Blockings = new BlockingsRepository(context);
            Confirmations = new ConfirmationsRepository(context);
            Bookmarks = new BookmarksRepository(context);
            Filters = new FiltersRepository(context);
            Tagings = new TagingsRepository(context);
            Tags = new TagsRepository(context);
            PhotoReports = new PhotoReportsRepository(context);
            UserReports = new UserReportsRepository(context);
        }

        #endregion

        #region Logic

        /// <summary>
        /// Saves DB changes.
        /// </summary>
        public void Save()
        {
            _context.SaveChanges();
        }

        /// <summary>
        /// Saves DB changes async.
        /// </summary>
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        #endregion

        #region Disposing
        
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }

                _disposed = true;
            }
        }

        ~UnitOfWork() {
            Dispose(false);
        }

        #endregion
    }
}
