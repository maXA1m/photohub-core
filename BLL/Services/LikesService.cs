using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System;
using PhotoHub.DAL.Interfaces;
using PhotoHub.DAL.Entities;
using PhotoHub.BLL.Interfaces;
using PhotoHub.BLL.DTO;
using PhotoHub.BLL.Mappers;

namespace PhotoHub.BLL.Services
{
    public class LikesService : ILikesService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ApplicationUser CurrentUser => _unitOfWork.Users.Find(u => u.UserName == _httpContextAccessor.HttpContext.User.Identity.Name).FirstOrDefault();
        public UserDTO CurrentUserDTO
        {
            get
            {
                ApplicationUser user = CurrentUser;

                return UserMapper.ToUserDTO(
                    user,
                    _unitOfWork.Confirmations.Find(c => c.UserId == user.Id).FirstOrDefault() != null,
                    _unitOfWork.Followings.Find(f => f.FollowedUserId == user.Id && f.UserId == user.Id).FirstOrDefault() != null,
                    _unitOfWork.Blockings.Find(b => b.BlockedUserId == user.Id && b.UserId == user.Id).FirstOrDefault() != null
                );
            }
        }

        public LikesService(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }

        public void Add(int photoId)
        {
            Photo photo = _unitOfWork.Photos.Get(photoId);
            ApplicationUser user = CurrentUser;
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
            ApplicationUser user = CurrentUser;
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
            ApplicationUser user = CurrentUser;
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
            ApplicationUser user = CurrentUser;
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