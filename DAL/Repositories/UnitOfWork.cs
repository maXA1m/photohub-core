#region using System
using System;
using System.Threading.Tasks;
#endregion
#region using PhotoHub.DAL
using PhotoHub.DAL.Interfaces;
using PhotoHub.DAL.Data;
using PhotoHub.DAL.Entities;
#endregion

namespace PhotoHub.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        private readonly IRepository<ApplicationUser> _usersRepository;
        private readonly IRepository<Photo> _photosRepository;
        private readonly IRepository<Exposure> _exposuresRepository;
        private readonly IRepository<Aperture> _aperturesRepository;
        private readonly IRepository<ISO> _isosRepository;
        private readonly IRepository<Comment> _commentsRepository;
        private readonly IRepository<Like> _likesRepository;
        private readonly IRepository<Following> _followingsRepository;
        private readonly IRepository<BlackList> _blockingRepository;
        private readonly IRepository<Confirmed> _confirmationsRepository;
        private readonly IRepository<Bookmark> _bookmarksRepository;
        private readonly IRepository<Filter> _filtersRepository;
        private readonly IRepository<Taging> _tagingsRepository;
        private readonly IRepository<Tag> _tagsRepository;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;

            _usersRepository = new UsersRepository(context);
            _photosRepository = new PhotosRepository(context);
            _commentsRepository = new CommentsRepository(context);
            _likesRepository = new LikesRepository(context);
            _followingsRepository = new FollowingsRepository(context);
            _blockingRepository = new BlockingsRepository(context);
            _confirmationsRepository = new ConfirmationsRepository(context);
            _bookmarksRepository = new BookmarksRepository(context);
            _filtersRepository = new FiltersRepository(context);
            _exposuresRepository = new ExposureRepository(context);
            _aperturesRepository = new ApertureRepository(context);
            _isosRepository = new IsoRepository(context);
            _tagingsRepository = new TagingsRepository(context);
            _tagsRepository = new TagsRepository(context);
        }

        public IRepository<ApplicationUser> Users => _usersRepository;
        public IRepository<Photo> Photos => _photosRepository;
        public IRepository<Exposure> Exposures => _exposuresRepository;
        public IRepository<Aperture> Apertures => _aperturesRepository;
        public IRepository<ISO> Isos => _isosRepository;
        public IRepository<Comment> Comments => _commentsRepository;
        public IRepository<Like> Likes => _likesRepository;
        public IRepository<Confirmed> Confirmations => _confirmationsRepository;
        public IRepository<Following> Followings => _followingsRepository;
        public IRepository<BlackList> Blockings => _blockingRepository;
        public IRepository<Bookmark> Bookmarks => _bookmarksRepository;
        public IRepository<Filter> Filters => _filtersRepository;
        public IRepository<Taging> Tagings => _tagingsRepository;
        public IRepository<Tag> Tags => _tagsRepository;

        public void Save() => _context.SaveChanges();
        public async Task SaveAsync() => await _context.SaveChangesAsync();

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                    _context.Dispose();
                disposed = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
