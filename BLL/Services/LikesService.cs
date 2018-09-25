using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using PhotoHub.DAL.Interfaces;
using PhotoHub.DAL.Entities;
using PhotoHub.BLL.Interfaces;

namespace PhotoHub.BLL.Services
{
    /// <summary>
    /// Contains methods with likes processing logic.
    /// Realization of ILikesService.
    /// </summary>
    public class LikesService : ILikesService
    {
        #region Fields

        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICurrentUserService _currentUserService;

        private bool _disposed;

        #endregion

        #region .ctors

        /// <summary>
        /// Initializes a new instance of the <see cref="LikesService"/>.
        /// </summary>
        public LikesService(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            _currentUserService = new CurrentUserService(unitOfWork, httpContextAccessor);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Adds like by liked photo id.
        /// </summary>
        public void Add(int photoId)
        {
            Photo photo = _unitOfWork.Photos.Get(photoId);
            User user = _currentUserService.Get;
            Like like = _unitOfWork.Likes.Find(l => l.OwnerId == user.Id && l.PhotoId == photo.Id).FirstOrDefault();

            if (photo != null && user != null && like == null)
            {
                _unitOfWork.Likes.Create(
                    new Like
                    {
                        PhotoId = photo.Id,
                        Date = DateTime.Now,
                        OwnerId = user.Id
                    }
                );

                _unitOfWork.Save();
            }
        }

        /// <summary>
        /// Async adds like by liked photo id.
        /// </summary>
        public async Task AddAsync(int photoId)
        {
            Photo photo = await _unitOfWork.Photos.GetAsync(photoId);
            User user = _currentUserService.Get;
            Like like = _unitOfWork.Likes.Find(l => l.OwnerId == user.Id && l.PhotoId == photo.Id).FirstOrDefault();

            if (photo != null && user != null && like == null)
            {
                await _unitOfWork.Likes.CreateAsync(
                    new Like
                    {
                        PhotoId = photo.Id,
                        Date = DateTime.Now,
                        OwnerId = user.Id
                    }
                );

                await _unitOfWork.SaveAsync();
            }
        }

        /// <summary>
        /// Deletes like by photo id.
        /// </summary>
        public void Delete(int photoId)
        {
            Photo photo = _unitOfWork.Photos.Get(photoId);
            User user = _currentUserService.Get;
            Like like = _unitOfWork.Likes.Find(l => l.OwnerId == user.Id && l.PhotoId == photo.Id).FirstOrDefault();

            if (photo != null && user != null && like != null)
            {
                _unitOfWork.Likes.Delete(like.Id);
                _unitOfWork.Save();
            }
        }

        /// <summary>
        /// Async deletes like by photo id.
        /// </summary>
        public async Task DeleteAsync(int photoId)
        {
            Photo photo = await _unitOfWork.Photos.GetAsync(photoId);
            User user = _currentUserService.Get;
            Like like = _unitOfWork.Likes.Find(l => l.OwnerId == user.Id && l.PhotoId == photo.Id).FirstOrDefault();

            if (photo != null && user != null && like != null)
            {
                await _unitOfWork.Likes.DeleteAsync(like.Id);
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

        ~LikesService()
        {
            Dispose(false);
        }

        #endregion
    }
}