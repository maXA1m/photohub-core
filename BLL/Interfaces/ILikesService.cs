using System;
using System.Threading.Tasks;

namespace PhotoHub.BLL.Interfaces
{
    /// <summary>
    /// Interface for likes services.
    /// Contains methods with likes processing logic.
    /// </summary>
    public interface ILikesService : IDisposable
    {
        /// <summary>
        /// Adds like by liked photo id.
        /// </summary>
        void Add(int photoId);

        /// <summary>
        /// Async adds like by liked photo id.
        /// </summary>
        Task AddAsync(int photoId);

        /// <summary>
        /// Deletes like by photo id.
        /// </summary>
        void Delete(int photoId);

        /// <summary>
        /// Async deletes like by photo id.
        /// </summary>
        Task DeleteAsync(int photoId);
    }
}
