using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using PhotoHub.BLL.Interfaces;
using PhotoHub.DAL.Interfaces;
using PhotoHub.DAL.Entities;
using PhotoHub.BLL.Mappers;
using PhotoHub.BLL.DTO;

namespace PhotoHub.BLL.Services
{
    public class CommentsService : ICommentsService
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
                    _unitOfWork.Blockings.Find(b => b.BlockedUserId == user.Id && b.UserId == user.Id).FirstOrDefault() != null,
                    _unitOfWork.Blockings.Find(b => b.BlockedUserId == user.Id && b.UserId == user.Id).FirstOrDefault() != null
                );
            }
        }

        public CommentsService(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }

        public int? Add(int photoId, string text)
        {
            ApplicationUser user = CurrentUser;
            Photo photo = _unitOfWork.Photos.Get(photoId);

            if (!String.IsNullOrEmpty(text) && user != null && photo != null)
            {
                Comment comment = new Comment()
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
        public async Task<int?> AddAsync(int photoId, string text)
        {
            ApplicationUser user = CurrentUser;
            Photo photo = await _unitOfWork.Photos.GetAsync(photoId);

            if(!String.IsNullOrEmpty(text) && user != null && photo != null)
            {
                Comment comment = new Comment()
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

        public void Delete(int id)
        {
            ApplicationUser user = CurrentUser;
            Comment comment = _unitOfWork.Comments.Find(c => c.OwnerId == user.Id && c.Id == id).FirstOrDefault();

            if (user != null && comment != null)
            {
                _unitOfWork.Comments.Delete(id);
                _unitOfWork.Save();
            }
        }
        public async Task DeleteAsync(int id)
        {
            ApplicationUser user = CurrentUser;
            Comment comment = _unitOfWork.Comments.Find(c => c.OwnerId == user.Id && c.Id == id).FirstOrDefault();

            if (user != null && comment != null)
            {
                await _unitOfWork.Comments.DeleteAsync(id);
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