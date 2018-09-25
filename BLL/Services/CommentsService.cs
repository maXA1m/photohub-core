using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using PhotoHub.DAL.Interfaces;
using PhotoHub.DAL.Entities;
using PhotoHub.BLL.Interfaces;

namespace PhotoHub.BLL.Services
{
    /// <summary>
    /// Contains methods with comments processing logic.
    /// Realization of ICommentsService.
    /// </summary>
    public class CommentsService : ICommentsService
    {
        #region Fields

        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICurrentUserService _currentUserService;

        private bool _disposed;

        #endregion

        #region .ctors

        /// <summary>
        /// Initializes a new instance of the <see cref="CommentsService"/>.
        /// </summary>
        public CommentsService(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            _currentUserService = new CurrentUserService(unitOfWork, httpContextAccessor);
        }

        #endregion

        #region Logic

        /// <summary>
        /// Adds comment by commented photo id and comment text.
        /// </summary>
        public int? Add(int photoId, string text)
        {
            User user = _currentUserService.Get;
            Photo photo = _unitOfWork.Photos.Get(photoId);

            if (!string.IsNullOrEmpty(text) && user != null && photo != null)
            {
                var comment = new Comment
                {
                    Text = text,
                    OwnerId = user.Id,
                    PhotoId = photoId,
                    Date = DateTime.Now
                };

                _unitOfWork.Comments.Create(comment);

                _unitOfWork.Save();

                return comment.Id;
            }

            return null;
        }

        /// <summary>
        /// Async adds comment by commented photo id and comment text.
        /// </summary>
        public async Task<int?> AddAsync(int photoId, string text)
        {
            User user = _currentUserService.Get;
            Photo photo = await _unitOfWork.Photos.GetAsync(photoId);

            if(!string.IsNullOrEmpty(text) && user != null && photo != null)
            {
                var comment = new Comment
                {
                    Text = text,
                    OwnerId = user.Id,
                    PhotoId = photoId,
                    Date = DateTime.Now
                };

                await _unitOfWork.Comments.CreateAsync(comment);

                await _unitOfWork.SaveAsync();

                return comment.Id;
            }

            return null;
        }

        /// <summary>
        /// Deletes comment by comment id.
        /// </summary>
        public void Delete(int id)
        {
            User user = _currentUserService.Get;
            Comment comment = _unitOfWork.Comments.Get(id);
            Photo photo = _unitOfWork.Photos.Get(comment.PhotoId);

            if (user != null && (photo.OwnerId == user.Id || comment.OwnerId == user.Id || _httpContextAccessor.HttpContext.User.IsInRole("Admin")))
            {
                _unitOfWork.Comments.Delete(id);
                _unitOfWork.Save();
            }
        }

        /// <summary>
        /// Async deletes comment by comment id.
        /// </summary>
        public async Task DeleteAsync(int id)
        {
            User user = _currentUserService.Get;
            Comment comment = _unitOfWork.Comments.Get(id);
            Photo photo = _unitOfWork.Photos.Get(comment.PhotoId);

            if (user != null && (photo.OwnerId == user.Id || comment.OwnerId == user.Id || _httpContextAccessor.HttpContext.User.IsInRole("Admin")))
            {
                await _unitOfWork.Comments.DeleteAsync(id);
                await _unitOfWork.SaveAsync();
            }
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
            if (!_disposed)
            {
                if (disposing)
                {
                    _unitOfWork.Dispose();
                    _currentUserService.Dispose();
                }

                _disposed = true;
            }
        }

        ~CommentsService()
        {
            Dispose(false);
        }

        #endregion
    }
}