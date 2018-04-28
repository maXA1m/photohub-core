#region using System/Microsoft
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
#endregion
#region using PhotoHub.DAL
using PhotoHub.DAL.Interfaces;
using PhotoHub.DAL.Entities;
#endregion
#region using PhotoHub.BLL
using PhotoHub.BLL.Interfaces;
using PhotoHub.BLL.DTO;
using PhotoHub.BLL.Mappers;
#endregion

namespace PhotoHub.BLL.Services
{
    public class LikesService : ILikesService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UsersMapper _usersMapper;

        public User CurrentUser => _unitOfWork.Users.Find(u => u.UserName == _httpContextAccessor.HttpContext.User.Identity.Name).FirstOrDefault();
        public UserDTO CurrentUserDTO
        {
            get
            {
                User user = CurrentUser;

                return _usersMapper.Map(
                    user,
                    _unitOfWork.Confirmations.Find(c => c.UserId == user.Id).FirstOrDefault() != null,
                    _unitOfWork.Followings.Find(f => f.FollowedUserId == user.Id && f.UserId == user.Id).FirstOrDefault() != null,
                    _unitOfWork.Blockings.Find(b => b.BlockedUserId == user.Id && b.UserId == user.Id).FirstOrDefault() != null,
                    _unitOfWork.Blockings.Find(b => b.BlockedUserId == user.Id && b.UserId == user.Id).FirstOrDefault() != null
                );
            }
        }

        public LikesService(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            _usersMapper = new UsersMapper();
        }

        public void Add(int photoId)
        {
            Photo photo = _unitOfWork.Photos.Get(photoId);
            User user = CurrentUser;
            Like like = _unitOfWork.Likes.Find(l => l.OwnerId == user.Id && l.PhotoId == photo.Id).FirstOrDefault();

            if (photo != null && user != null && like == null)
            {
                _unitOfWork.Likes.Create(
                    new Like()
                    {
                        PhotoId = photo.Id,
                        Date = DateTime.Now,
                        OwnerId = user.Id
                    }
                );

                _unitOfWork.Save();
            }
        }
        public async Task AddAsync(int photoId)
        {
            Photo photo = await _unitOfWork.Photos.GetAsync(photoId);
            User user = CurrentUser;
            Like like = _unitOfWork.Likes.Find(l => l.OwnerId == user.Id && l.PhotoId == photo.Id).FirstOrDefault();

            if (photo != null && user != null && like == null)
            {
                await _unitOfWork.Likes.CreateAsync(
                    new Like() {
                        PhotoId = photo.Id,
                        Date = DateTime.Now,
                        OwnerId = user.Id
                    }
                );

                await _unitOfWork.SaveAsync();
            }
        }

        public void Delete(int photoId)
        {
            Photo photo = _unitOfWork.Photos.Get(photoId);
            User user = CurrentUser;
            Like like = _unitOfWork.Likes.Find(l => l.OwnerId == user.Id && l.PhotoId == photo.Id).FirstOrDefault();

            if (photo != null && user != null && like != null)
            {
                _unitOfWork.Likes.Delete(like.Id);
                _unitOfWork.Save();
            }
        }
        public async Task DeleteAsync(int photoId)
        {
            Photo photo = await _unitOfWork.Photos.GetAsync(photoId);
            User user = CurrentUser;
            Like like = _unitOfWork.Likes.Find(l => l.OwnerId == user.Id && l.PhotoId == photo.Id).FirstOrDefault();

            if (photo != null && user != null && like != null)
            {
                await _unitOfWork.Likes.DeleteAsync(like.Id);
                await _unitOfWork.SaveAsync();
            }
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}