using System;
using System.Threading.Tasks;

using PhotoHub.DAL.Data;
using PhotoHub.DAL.Entities;
using PhotoHub.DAL.Interfaces;

namespace PhotoHub.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        private readonly IRepository<ApplicationUser> _usersRepository;
        private readonly IRepository<Photo> _photosRepository;
        private readonly IRepository<Giveaway> _giveawaysRepository;
        private readonly IRepository<Comment> _commentsRepository;
        private readonly IRepository<Like> _likesRepository;
        private readonly IRepository<Following> _followingsRepository;
        private readonly IRepository<BlackList> _blockingRepository;
        private readonly IRepository<Confirmed> _confirmationsRepository;
        private readonly IRepository<GiveawayOwner> _giveawayOwnersRepository;
        private readonly IRepository<Participant> _participantsRepository;
        private readonly IRepository<PhotoView> _photoViewsRepository;
        private readonly IRepository<Filter> _filtersRepository;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;

            _usersRepository = new UsersRepository(context);
            _photosRepository = new PhotosRepository(context);
            _giveawaysRepository = new GiveawaysRepository(context);
            _commentsRepository = new CommentsRepository(context);
            _likesRepository = new LikesRepository(context);
            _followingsRepository = new FollowingsRepository(context);
            _blockingRepository = new BlockingsRepository(context);
            _confirmationsRepository = new ConfirmationsRepository(context);
            _giveawayOwnersRepository = new GiveawayOwnersRepository(context);
            _participantsRepository = new ParticipantsRepository(context);
            _photoViewsRepository = new PhotoViewsRepository(context);
            _filtersRepository = new FiltersRepository(context);
        }

        public IRepository<ApplicationUser> Users => _usersRepository;
        public IRepository<Photo> Photos => _photosRepository;
        public IRepository<Giveaway> Giveaways => _giveawaysRepository;
        public IRepository<Comment> Comments => _commentsRepository;
        public IRepository<Like> Likes => _likesRepository;
        public IRepository<Confirmed> Confirmations => _confirmationsRepository;
        public IRepository<Following> Followings => _followingsRepository;
        public IRepository<BlackList> Blockings => _blockingRepository;
        public IRepository<GiveawayOwner> GiveawayOwners => _giveawayOwnersRepository;
        public IRepository<Participant> Participants => _participantsRepository;
        public IRepository<PhotoView> PhotoViews => _photoViewsRepository;
        public IRepository<Filter> Filters => _filtersRepository;

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
