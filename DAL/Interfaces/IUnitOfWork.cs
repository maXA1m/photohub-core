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
        IRepository<Exposure> Exposures { get; }
        IRepository<Aperture> Apertures { get; }
        IRepository<ISO> Isos { get; }
        IRepository<ApplicationUser> Users { get; }
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
