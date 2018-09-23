using System;
using System.Threading.Tasks;
using PhotoHub.DAL.Entities;

namespace PhotoHub.DAL.Interfaces
{
    /// <summary>
    /// Interface for accessing DB by repositories.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        #region Properties

        /// <summary>
        /// Gets photo repository.
        /// </summary>
        IRepository<Photo> Photos { get; }

        /// <summary>
        /// Gets user identity repository.
        /// </summary>
        IRepository<ApplicationUser> IdentityUsers { get; }

        /// <summary>
        /// Gets user repository.
        /// </summary>
        IRepository<User> Users { get; }

        /// <summary>
        /// Gets comment repository.
        /// </summary>
        IRepository<Comment> Comments { get; }

        /// <summary>
        /// Gets like repository.
        /// </summary>
        IRepository<Like> Likes { get; }

        /// <summary>
        /// Gets confirmations repository.
        /// </summary>
        IRepository<Confirmed> Confirmations { get; }

        /// <summary>
        /// Gets following repository.
        /// </summary>
        IRepository<Following> Followings { get; }

        /// <summary>
        /// Gets blacklist repository.
        /// </summary>
        IRepository<BlackList> Blockings { get; }

        /// <summary>
        /// Gets bookmark repository.
        /// </summary>
        IRepository<Bookmark> Bookmarks { get; }

        /// <summary>
        /// Gets filter repository.
        /// </summary>
        IRepository<Filter> Filters { get; }

        /// <summary>
        /// Gets tag repository.
        /// </summary>
        IRepository<Tag> Tags { get; }

        /// <summary>
        /// Gets taging repository.
        /// </summary>
        IRepository<Taging> Tagings { get; }

        /// <summary>
        /// Gets photo report repository.
        /// </summary>
        IRepository<PhotoReport> PhotoReports { get; }

        /// <summary>
        /// Gets user report repository.
        /// </summary>
        IRepository<UserReport> UserReports { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Method for saving db changes.
        /// </summary>
        void Save();

        /// <summary>
        /// Async method for saving db changes.
        /// </summary>
        Task SaveAsync();

        #endregion
    }
}
