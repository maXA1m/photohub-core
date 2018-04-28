#region using System
using System;
using System.Threading.Tasks;
#endregion
#region using PhotoHub.DAL
using PhotoHub.DAL.Entities;
#endregion

namespace PhotoHub.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Photo> Photos { get; }
        IRepository<ApplicationUser> IdentityUsers { get; }
        IRepository<User> Users { get; }
        IRepository<Comment> Comments { get; }
        IRepository<Like> Likes { get; }
        IRepository<Confirmed> Confirmations { get; }
        IRepository<Following> Followings { get; }
        IRepository<BlackList> Blockings { get; }
        IRepository<Bookmark> Bookmarks { get; }
        IRepository<Filter> Filters { get; }
        IRepository<Tag> Tags { get; }
        IRepository<Taging> Tagings { get; }

        void Save();
        Task SaveAsync();
    }
}
