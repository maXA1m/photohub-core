using System;
using System.Threading.Tasks;

namespace PhotoHub.BLL.Interfaces
{
    /// <summary>
    /// Interface for comments services.
    /// Contains methods with comments processing logic.
    /// </summary>
    public interface ICommentsService : IDisposable
    {
        /// <summary>
        /// Adds comment by commented photo id and comment text.
        /// </summary>
        int? Add(int photoId, string text);

        /// <summary>
        /// Async adds comment by commented photo id and comment text.
        /// </summary>
        Task<int?> AddAsync(int photoId, string text);

        /// <summary>
        /// Deletes comment by comment id.
        /// </summary>
        void Delete(int id);

        /// <summary>
        /// Async deletes comment by comment id.
        /// </summary>
        Task DeleteAsync(int id);
    }
}
