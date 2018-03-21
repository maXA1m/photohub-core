using PhotoHub.DAL.Entities;
using System;
using System.Threading.Tasks;

namespace PhotoHub.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Photo> Photos { get; }
        IRepository<ApplicationUser> Users { get; }
        IRepository<Comment> Comments { get; }
        IRepository<Like> Likes { get; }
        IRepository<Confirmed> Confirmations { get; }
        IRepository<Following> Followings { get; }
        IRepository<BlackList> Blockings { get; }
        IRepository<Bookmark> Bookmarks { get; }
        IRepository<Filter> Filters { get; }

        void Save();
        Task SaveAsync();
    }
}
