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

        private IRepository<ApplicationUser> _identityUsersRepository;
        private IRepository<User> _usersRepository;
        private IRepository<Photo> _photosRepository;
        private IRepository<Comment> _commentsRepository;
        private IRepository<Like> _likesRepository;
        private IRepository<Confirmed> _confirmationsRepository;
        private IRepository<Following> _followingsRepository;
        private IRepository<BlackList> _blockingsRepository;
        private IRepository<Bookmark> _bookmarksRepository;
        private IRepository<Filter> _filtersRepository; 
        private IRepository<Taging> _tagingsRepository; 
        private IRepository<Tag> _tagsRepository;
        private IRepository<PhotoReport> _photoReportsRepository;
        private IRepository<UserReport> _userReportsRepository;

        private bool _isDisposed;

        #endregion

        #region Properties

        /// <summary>
        /// Gets application user repository.
        /// </summary>
        public IRepository<ApplicationUser> IdentityUsers => _identityUsersRepository ?? (_identityUsersRepository = new UsersIdentityRepository(_context));

        /// <summary>
        /// Gets user repository.
        /// </summary>
        public IRepository<User> Users => _usersRepository ?? (_usersRepository = new UsersRepository(_context));

        /// <summary>
        /// Gets photo repository.
        /// </summary>
        public IRepository<Photo> Photos => _photosRepository ?? (_photosRepository = new PhotosRepository(_context));

        /// <summary>
        /// Gets comment repository.
        /// </summary>
        public IRepository<Comment> Comments => _commentsRepository ?? (_commentsRepository = new CommentsRepository(_context));

        /// <summary>
        /// Gets like repository.
        /// </summary>
        public IRepository<Like> Likes => _likesRepository ?? (_likesRepository = new LikesRepository(_context));

        /// <summary>
        /// Gets confirmed repository.
        /// </summary>
        public IRepository<Confirmed> Confirmations => _confirmationsRepository ?? (_confirmationsRepository = new ConfirmationsRepository(_context));

        /// <summary>
        /// Gets following repository.
        /// </summary>
        public IRepository<Following> Followings => _followingsRepository ??(_followingsRepository = new FollowingsRepository(_context));

        /// <summary>
        /// Gets blacklist repository.
        /// </summary>
        public IRepository<BlackList> Blockings => _blockingsRepository ?? (_blockingsRepository = new BlockingsRepository(_context));

        /// <summary>
        /// Gets bookmark repository.
        /// </summary>
        public IRepository<Bookmark> Bookmarks => _bookmarksRepository ?? (_bookmarksRepository = new BookmarksRepository(_context));

        /// <summary>
        /// Gets filter repository.
        /// </summary>
        public IRepository<Filter> Filters => _filtersRepository ?? (_filtersRepository = new FiltersRepository(_context));

        /// <summary>
        /// Gets taging repository.
        /// </summary>
        public IRepository<Taging> Tagings => _tagingsRepository ?? (_tagingsRepository = new TagingsRepository(_context));

        /// <summary>
        /// Gets tag repository.
        /// </summary>
        public IRepository<Tag> Tags => _tagsRepository ?? (_tagsRepository = new TagsRepository(_context));

        /// <summary>
        /// Gets photo report repository.
        /// </summary>
        public IRepository<PhotoReport> PhotoReports => _photoReportsRepository ?? (_photoReportsRepository = new PhotoReportsRepository(_context));

        /// <summary>
        /// Gets user report repository.
        /// </summary>
        public IRepository<UserReport> UserReports => _userReportsRepository ?? (_userReportsRepository = new UserReportsRepository(_context));

        #endregion

        #region .ctors

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork"/>.
        /// </summary>
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
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
            if (!_isDisposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }

                _isDisposed = true;
            }
        }

        ~UnitOfWork() {
            Dispose(false);
        }

        #endregion
    }
}
