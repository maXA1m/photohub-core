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

        public IRepository<ApplicationUser> IdentityUsers { get; protected set; }
        public IRepository<User> Users { get; protected set; }
        public IRepository<Photo> Photos { get; protected set; }
        public IRepository<Comment> Comments { get; protected set; }
        public IRepository<Like> Likes { get; protected set; }
        public IRepository<Confirmed> Confirmations { get; protected set; }
        public IRepository<Following> Followings { get; protected set; }
        public IRepository<BlackList> Blockings { get; protected set; }
        public IRepository<Bookmark> Bookmarks { get; protected set; }
        public IRepository<Filter> Filters { get; protected set; }
        public IRepository<Taging> Tagings { get; protected set; }
        public IRepository<Tag> Tags { get; protected set; }
        public IRepository<PhotoReport> PhotoReports { get; protected set; }
        public IRepository<UserReport> UserReports { get; protected set; }

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
