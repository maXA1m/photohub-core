﻿#region using System/Microsoft
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
    public class CommentsService : ICommentsService
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

        public CommentsService(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            _usersMapper = new UsersMapper();
        }

        public int? Add(int photoId, string text)
        {
            User user = CurrentUser;
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
            User user = CurrentUser;
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
            User user = CurrentUser;
            Comment comment = _unitOfWork.Comments.Get(id);
            Photo photo = _unitOfWork.Photos.Get(comment.PhotoId);

            if (user != null && (photo.OwnerId == user.Id || comment.OwnerId != user.Id))
            {
                _unitOfWork.Comments.Delete(id);
                _unitOfWork.Save();
            }
        }
        public async Task DeleteAsync(int id)
        {
            User user = CurrentUser;
            Comment comment = _unitOfWork.Comments.Get(id);
            Photo photo = _unitOfWork.Photos.Get(comment.PhotoId);

            if (user != null && (photo.OwnerId == user.Id || comment.OwnerId != user.Id))
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