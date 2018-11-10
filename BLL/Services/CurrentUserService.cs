using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using PhotoHub.DAL.Interfaces;
using PhotoHub.DAL.Entities;
using PhotoHub.BLL.Interfaces;
using PhotoHub.BLL.DTO;
using PhotoHub.BLL.Extensions;

namespace PhotoHub.BLL.Services
{
    /// <summary>
    /// Contains properties that returns current user.
    /// Realization of <see cref="ICurrentUserService"/>.
    /// </summary>
    public class CurrentUserService : ICurrentUserService
    {
        #region Fields

        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private bool _isDisposed;

        #endregion

        #region Properties

        /// <returns>
        /// Returns current user entity.
        /// </returns>
        public User CurrentUser => _unitOfWork.Users.Find(u => u.UserName == _httpContextAccessor.HttpContext.User.Identity.Name).FirstOrDefault();

        /// <returns>
        /// Returns current user data transfer object.
        /// </returns>
        public UserDTO CurrentUserDTO
        {
            get
            {
                var user = CurrentUser;

                return user.ToDTO(_unitOfWork.Confirmations.Find(c => c.UserId == user.Id).FirstOrDefault() != null,
                                  _unitOfWork.Followings.Find(f => f.FollowedUserId == user.Id && f.UserId == user.Id).FirstOrDefault() != null,
                                  _unitOfWork.Blockings.Find(b => b.BlockedUserId == user.Id && b.UserId == user.Id).FirstOrDefault() != null,
                                  _unitOfWork.Blockings.Find(b => b.BlockedUserId == user.Id && b.UserId == user.Id).FirstOrDefault() != null);
            }
        }

        #endregion

        #region .ctors

        /// <summary>
        /// Initializes a new instance of the <see cref="CurrentUserService"/>.
        /// </summary>
        public CurrentUserService(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
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
            if (!_isDisposed)
            {
                if (disposing)
                {
                    _unitOfWork.Dispose();
                }

                _isDisposed = true;
            }
        }

        ~CurrentUserService()
        {
            Dispose(false);
        }

        #endregion
    }
}