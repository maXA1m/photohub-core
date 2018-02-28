using System.Linq;
using System.Collections.Generic;
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
    public class PhotosService : IPhotosService
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

        public List<FilterDTO> Filters => FilterMapper.ToFilterDTOs(_unitOfWork.Filters.GetAll(0, 14));

        public PhotosService(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }

        public IEnumerable<PhotoDTO> GetAll(int page, int pageSize)
        {
            ApplicationUser currentUser = CurrentUser;
            IEnumerable<Photo> photos = _unitOfWork.Photos.GetAll(page, pageSize);

            List<PhotoDTO> photoDTOs = new List<PhotoDTO>(pageSize);

            foreach (Photo photo in photos)
            {
                List<LikeDTO> likes = new List<LikeDTO>(photo.Likes.Count);
                foreach (Like like in photo.Likes)
                {
                    likes.Add(LikeMapper.ToLikeDTO(like,
                        UserMapper.ToUserDTO(
                            like.Owner,
                            _unitOfWork.Confirmations.Find(c => c.UserId == like.OwnerId).FirstOrDefault() != null,
                            _unitOfWork.Followings.Find(f => f.FollowedUserId == like.OwnerId && f.UserId == currentUser.Id).FirstOrDefault() != null,
                            _unitOfWork.Blockings.Find(b => b.BlockedUserId == like.OwnerId && b.UserId == currentUser.Id).FirstOrDefault() != null,
                            _unitOfWork.Blockings.Find(b => b.BlockedUserId == currentUser.Id && b.UserId == currentUser.Id).FirstOrDefault() != null
                    )));
                }

                List<CommentDTO> comments = new List<CommentDTO>(photo.Comments.Count);
                foreach (Comment comment in photo.Comments)
                {
                    comments.Add(CommentMapper.ToCommentDTO(
                        comment,
                        UserMapper.ToUserDTO(
                            comment.Owner,
                            _unitOfWork.Confirmations.Find(c => c.UserId == comment.OwnerId).FirstOrDefault() != null,
                            _unitOfWork.Followings.Find(f => f.FollowedUserId == comment.OwnerId && f.UserId == currentUser.Id).FirstOrDefault() != null,
                            _unitOfWork.Blockings.Find(b => b.BlockedUserId == comment.OwnerId && b.UserId == currentUser.Id).FirstOrDefault() != null,
                            _unitOfWork.Blockings.Find(b => b.BlockedUserId == currentUser.Id && b.UserId == currentUser.Id).FirstOrDefault() != null
                    )));
                }

                photoDTOs.Add(PhotoMapper.ToPhotoDTO(
                    photo,
                    _unitOfWork.Likes.Find(l => l.OwnerId == currentUser.Id && l.PhotoId == photo.Id).FirstOrDefault() != null,
                    UserMapper.ToUserDTO(
                        photo.Owner,
                        _unitOfWork.Confirmations.Find(c => c.UserId == photo.OwnerId).FirstOrDefault() != null,
                        _unitOfWork.Followings.Find(f => f.FollowedUserId == photo.OwnerId && f.UserId == currentUser.Id).FirstOrDefault() != null,
                        _unitOfWork.Blockings.Find(b => b.BlockedUserId == photo.OwnerId && b.UserId == currentUser.Id).FirstOrDefault() != null,
                            _unitOfWork.Blockings.Find(b => b.BlockedUserId == currentUser.Id && b.UserId == currentUser.Id).FirstOrDefault() != null
                    ),
                    likes,
                    comments));
            }

            return photoDTOs;
        }
        public async Task<IEnumerable<PhotoDTO>> GetAllAsync(int page, int pageSize)
        {
            ApplicationUser currentUser = CurrentUser;
            IEnumerable<Photo> photos = await _unitOfWork.Photos.GetAllAsync(page, pageSize);

            List<PhotoDTO> photoDTOs = new List<PhotoDTO>(pageSize);

            foreach (Photo photo in photos)
            {
                List<LikeDTO> likes = new List<LikeDTO>(photo.Likes.Count);
                foreach (Like like in photo.Likes)
                {
                    likes.Add(LikeMapper.ToLikeDTO(like, 
                        UserMapper.ToUserDTO(
                            like.Owner,
                            _unitOfWork.Confirmations.Find(c => c.UserId == like.OwnerId).FirstOrDefault() != null,
                            _unitOfWork.Followings.Find(f => f.FollowedUserId == like.OwnerId && f.UserId == currentUser.Id).FirstOrDefault() != null,
                            _unitOfWork.Blockings.Find(b => b.BlockedUserId == like.OwnerId && b.UserId == currentUser.Id).FirstOrDefault() != null,
                            _unitOfWork.Blockings.Find(b => b.BlockedUserId == currentUser.Id && b.UserId == currentUser.Id).FirstOrDefault() != null
                    )));
                }

                List<CommentDTO> comments = new List<CommentDTO>(photo.Comments.Count);
                foreach (Comment comment in photo.Comments)
                {
                    comments.Add(CommentMapper.ToCommentDTO(
                        comment,
                        UserMapper.ToUserDTO(
                            comment.Owner,
                            _unitOfWork.Confirmations.Find(c => c.UserId == comment.OwnerId).FirstOrDefault() != null,
                            _unitOfWork.Followings.Find(f => f.FollowedUserId == comment.OwnerId && f.UserId == currentUser.Id).FirstOrDefault() != null,
                            _unitOfWork.Blockings.Find(b => b.BlockedUserId == comment.OwnerId && b.UserId == currentUser.Id).FirstOrDefault() != null,
                            _unitOfWork.Blockings.Find(b => b.BlockedUserId == currentUser.Id && b.UserId == currentUser.Id).FirstOrDefault() != null
                    )));
                }

                photoDTOs.Add(PhotoMapper.ToPhotoDTO(
                    photo,
                    _unitOfWork.Likes.Find(l => l.OwnerId == currentUser.Id && l.PhotoId == photo.Id).FirstOrDefault() != null, 
                    UserMapper.ToUserDTO(
                        photo.Owner,
                        _unitOfWork.Confirmations.Find(c => c.UserId == photo.OwnerId).FirstOrDefault() != null,
                        _unitOfWork.Followings.Find(f => f.FollowedUserId == photo.OwnerId && f.UserId == currentUser.Id).FirstOrDefault() != null,
                        _unitOfWork.Blockings.Find(b => b.BlockedUserId == photo.OwnerId && b.UserId == currentUser.Id).FirstOrDefault() != null,
                            _unitOfWork.Blockings.Find(b => b.BlockedUserId == currentUser.Id && b.UserId == currentUser.Id).FirstOrDefault() != null
                    ),
                    likes, 
                    comments));
            }

            return photoDTOs;
        }

        public PhotoDTO Get(int id)
        {
            ApplicationUser currentUser = CurrentUser;
            Photo photo = _unitOfWork.Photos.Get(id);

            PhotoView photoView = _unitOfWork.PhotoViews.Get(photo.PhotoViewId);
            photoView.Count++;
            _unitOfWork.PhotoViews.Update(photoView);
            _unitOfWork.Save();

            List<LikeDTO> likes = new List<LikeDTO>(photo.Likes.Count);
            foreach (Like like in photo.Likes)
            {
                likes.Add(LikeMapper.ToLikeDTO(like,
                    UserMapper.ToUserDTO(
                        like.Owner,
                        _unitOfWork.Confirmations.Find(c => c.UserId == like.OwnerId).FirstOrDefault() != null,
                        _unitOfWork.Followings.Find(f => f.FollowedUserId == like.OwnerId && f.UserId == currentUser.Id).FirstOrDefault() != null,
                        _unitOfWork.Blockings.Find(b => b.BlockedUserId == like.OwnerId && b.UserId == currentUser.Id).FirstOrDefault() != null,
                            _unitOfWork.Blockings.Find(b => b.BlockedUserId == currentUser.Id && b.UserId == currentUser.Id).FirstOrDefault() != null
                )));
            }

            List<CommentDTO> comments = new List<CommentDTO>(photo.Comments.Count);
            foreach (Comment comment in photo.Comments)
            {
                comments.Add(CommentMapper.ToCommentDTO(
                    comment,
                    UserMapper.ToUserDTO(
                        comment.Owner,
                        _unitOfWork.Confirmations.Find(c => c.UserId == comment.OwnerId).FirstOrDefault() != null,
                        _unitOfWork.Followings.Find(f => f.FollowedUserId == comment.OwnerId && f.UserId == currentUser.Id).FirstOrDefault() != null,
                        _unitOfWork.Blockings.Find(b => b.BlockedUserId == comment.OwnerId && b.UserId == currentUser.Id).FirstOrDefault() != null,
                            _unitOfWork.Blockings.Find(b => b.BlockedUserId == currentUser.Id && b.UserId == currentUser.Id).FirstOrDefault() != null
                )));
            }

            return PhotoMapper.ToPhotoDTO(
                        photo,
                        _unitOfWork.Likes.Find(l => l.OwnerId == currentUser.Id && l.PhotoId == photo.Id).FirstOrDefault() != null,
                        UserMapper.ToUserDTO(
                            photo.Owner,
                            _unitOfWork.Confirmations.Find(c => c.UserId == photo.OwnerId).FirstOrDefault() != null,
                            _unitOfWork.Followings.Find(f => f.FollowedUserId == photo.OwnerId && f.UserId == currentUser.Id).FirstOrDefault() != null,
                            _unitOfWork.Blockings.Find(b => b.BlockedUserId == photo.OwnerId && b.UserId == currentUser.Id).FirstOrDefault() != null,
                            _unitOfWork.Blockings.Find(b => b.BlockedUserId == currentUser.Id && b.UserId == currentUser.Id).FirstOrDefault() != null
                        ),
                        likes,
                        comments
            );
        }
        public async Task<PhotoDTO> GetAsync(int id)
        {
            ApplicationUser currentUser = CurrentUser;
            Photo photo =  await _unitOfWork.Photos.GetAsync(id);

            PhotoView photoView = await _unitOfWork.PhotoViews.GetAsync(photo.PhotoViewId);
            photoView.Count++;
            _unitOfWork.PhotoViews.Update(photoView);
            await _unitOfWork.SaveAsync();

            List<LikeDTO> likes = new List<LikeDTO>(photo.Likes.Count);
            foreach (Like like in photo.Likes)
            {
                likes.Add(LikeMapper.ToLikeDTO(like,
                    UserMapper.ToUserDTO(
                        like.Owner,
                        _unitOfWork.Confirmations.Find(c => c.UserId == like.OwnerId).FirstOrDefault() != null,
                        _unitOfWork.Followings.Find(f => f.FollowedUserId == like.OwnerId && f.UserId == currentUser.Id).FirstOrDefault() != null,
                        _unitOfWork.Blockings.Find(b => b.BlockedUserId == like.OwnerId && b.UserId == currentUser.Id).FirstOrDefault() != null,
                            _unitOfWork.Blockings.Find(b => b.BlockedUserId == currentUser.Id && b.UserId == currentUser.Id).FirstOrDefault() != null
                )));
            }

            List<CommentDTO> comments = new List<CommentDTO>(photo.Comments.Count);
            foreach (Comment comment in photo.Comments)
            {
                comments.Add(CommentMapper.ToCommentDTO(
                    comment,
                    UserMapper.ToUserDTO(
                        comment.Owner,
                        _unitOfWork.Confirmations.Find(c => c.UserId == comment.OwnerId).FirstOrDefault() != null,
                        _unitOfWork.Followings.Find(f => f.FollowedUserId == comment.OwnerId && f.UserId == currentUser.Id).FirstOrDefault() != null,
                        _unitOfWork.Blockings.Find(b => b.BlockedUserId == comment.OwnerId && b.UserId == currentUser.Id).FirstOrDefault() != null,
                            _unitOfWork.Blockings.Find(b => b.BlockedUserId == currentUser.Id && b.UserId == currentUser.Id).FirstOrDefault() != null
                )));
            }

            return PhotoMapper.ToPhotoDTO(
                        photo,
                        _unitOfWork.Likes.Find(l => l.OwnerId == currentUser.Id && l.PhotoId == photo.Id).FirstOrDefault() != null,
                        UserMapper.ToUserDTO(
                            photo.Owner,
                            _unitOfWork.Confirmations.Find(c => c.UserId == photo.OwnerId).FirstOrDefault() != null,
                            _unitOfWork.Followings.Find(f => f.FollowedUserId == photo.OwnerId && f.UserId == currentUser.Id).FirstOrDefault() != null,
                            _unitOfWork.Blockings.Find(b => b.BlockedUserId == photo.OwnerId && b.UserId == currentUser.Id).FirstOrDefault() != null,
                            _unitOfWork.Blockings.Find(b => b.BlockedUserId == currentUser.Id && b.UserId == currentUser.Id).FirstOrDefault() != null
                        ),
                        likes,
                        comments
            );
        }

        public IEnumerable<PhotoDTO> GetPhotosHome(int page, int pageSize)
        {
            ApplicationUser currentUser = CurrentUser;
            List<Following> followings = _unitOfWork.Followings.Find(f => f.UserId == currentUser.Id).ToList();
            List<Photo> photos = new List<Photo>();

            foreach (var follow in followings)
                photos.AddRange(_unitOfWork.Photos.Find(p => p.OwnerId == follow.FollowedUserId));

            List<PhotoDTO> photoDTOs = new List<PhotoDTO>(pageSize);

            foreach (Photo photo in photos)
            {
                List<LikeDTO> likes = new List<LikeDTO>(photo.Likes.Count);
                foreach (Like like in photo.Likes)
                {
                    likes.Add(LikeMapper.ToLikeDTO(like,
                        UserMapper.ToUserDTO(
                            like.Owner,
                            _unitOfWork.Confirmations.Find(c => c.UserId == like.OwnerId).FirstOrDefault() != null,
                            _unitOfWork.Followings.Find(f => f.FollowedUserId == like.OwnerId && f.UserId == currentUser.Id).FirstOrDefault() != null,
                            _unitOfWork.Blockings.Find(b => b.BlockedUserId == like.OwnerId && b.UserId == currentUser.Id).FirstOrDefault() != null,
                            _unitOfWork.Blockings.Find(b => b.BlockedUserId == currentUser.Id && b.UserId == currentUser.Id).FirstOrDefault() != null
                    )));
                }

                List<CommentDTO> comments = new List<CommentDTO>(photo.Comments.Count);
                foreach (Comment comment in photo.Comments)
                {
                    comments.Add(CommentMapper.ToCommentDTO(
                        comment,
                        UserMapper.ToUserDTO(
                            comment.Owner,
                            _unitOfWork.Confirmations.Find(c => c.UserId == comment.OwnerId).FirstOrDefault() != null,
                            _unitOfWork.Followings.Find(f => f.FollowedUserId == comment.OwnerId && f.UserId == currentUser.Id).FirstOrDefault() != null,
                            _unitOfWork.Blockings.Find(b => b.BlockedUserId == comment.OwnerId && b.UserId == currentUser.Id).FirstOrDefault() != null,
                            _unitOfWork.Blockings.Find(b => b.BlockedUserId == currentUser.Id && b.UserId == currentUser.Id).FirstOrDefault() != null
                    )));
                }

                photoDTOs.Add(PhotoMapper.ToPhotoDTO(
                    photo,
                    _unitOfWork.Likes.Find(l => l.OwnerId == currentUser.Id && l.PhotoId == photo.Id).FirstOrDefault() != null,
                    UserMapper.ToUserDTO(
                        photo.Owner,
                        _unitOfWork.Confirmations.Find(c => c.UserId == photo.OwnerId).FirstOrDefault() != null,
                        _unitOfWork.Followings.Find(f => f.FollowedUserId == photo.OwnerId && f.UserId == currentUser.Id).FirstOrDefault() != null,
                        _unitOfWork.Blockings.Find(b => b.BlockedUserId == photo.OwnerId && b.UserId == currentUser.Id).FirstOrDefault() != null,
                            _unitOfWork.Blockings.Find(b => b.BlockedUserId == currentUser.Id && b.UserId == currentUser.Id).FirstOrDefault() != null
                    ),
                    likes,
                    comments));
            }

            return photoDTOs;
        }
        public IEnumerable<PhotoDTO> GetForUser(int page, string userName, int pageSize)
        {
            ApplicationUser currentUser = CurrentUser;
            ApplicationUser user = _unitOfWork.Users.Find(u => u.UserName == userName).FirstOrDefault();

            List<Photo> photos = _unitOfWork.Photos.Find(p => p.OwnerId == user.Id).Skip(page * pageSize).Take(pageSize).ToList();

            List<PhotoDTO> photoDTOs = new List<PhotoDTO>(pageSize);

            foreach (Photo photo in photos)
            {
                List<LikeDTO> likes = new List<LikeDTO>(photo.Likes.Count);
                foreach (Like like in photo.Likes)
                {
                    likes.Add(LikeMapper.ToLikeDTO(like,
                        UserMapper.ToUserDTO(
                            like.Owner,
                            _unitOfWork.Confirmations.Find(c => c.UserId == like.OwnerId).FirstOrDefault() != null,
                            _unitOfWork.Followings.Find(f => f.FollowedUserId == like.OwnerId && f.UserId == currentUser.Id).FirstOrDefault() != null,
                            _unitOfWork.Blockings.Find(b => b.BlockedUserId == like.OwnerId && b.UserId == currentUser.Id).FirstOrDefault() != null,
                            _unitOfWork.Blockings.Find(b => b.BlockedUserId == currentUser.Id && b.UserId == currentUser.Id).FirstOrDefault() != null
                    )));
                }

                List<CommentDTO> comments = new List<CommentDTO>(photo.Comments.Count);
                foreach (Comment comment in photo.Comments)
                {
                    comments.Add(CommentMapper.ToCommentDTO(
                        comment,
                        UserMapper.ToUserDTO(
                            comment.Owner,
                            _unitOfWork.Confirmations.Find(c => c.UserId == comment.OwnerId).FirstOrDefault() != null,
                            _unitOfWork.Followings.Find(f => f.FollowedUserId == comment.OwnerId && f.UserId == currentUser.Id).FirstOrDefault() != null,
                            _unitOfWork.Blockings.Find(b => b.BlockedUserId == comment.OwnerId && b.UserId == currentUser.Id).FirstOrDefault() != null,
                            _unitOfWork.Blockings.Find(b => b.BlockedUserId == currentUser.Id && b.UserId == currentUser.Id).FirstOrDefault() != null
                    )));
                }

                photoDTOs.Add(PhotoMapper.ToPhotoDTO(
                    photo,
                    _unitOfWork.Likes.Find(l => l.OwnerId == currentUser.Id && l.PhotoId == photo.Id).FirstOrDefault() != null,
                    UserMapper.ToUserDTO(
                        photo.Owner,
                        _unitOfWork.Confirmations.Find(c => c.UserId == photo.OwnerId).FirstOrDefault() != null,
                        _unitOfWork.Followings.Find(f => f.FollowedUserId == photo.OwnerId && f.UserId == currentUser.Id).FirstOrDefault() != null,
                        _unitOfWork.Blockings.Find(b => b.BlockedUserId == photo.OwnerId && b.UserId == currentUser.Id).FirstOrDefault() != null,
                            _unitOfWork.Blockings.Find(b => b.BlockedUserId == currentUser.Id && b.UserId == currentUser.Id).FirstOrDefault() != null
                    ),
                    likes,
                    comments));
            }

            return photoDTOs;
        }
        public IEnumerable<PhotoDTO> GetForGiveaway(int page, int giveawayId, int pageSize)
        {
            ApplicationUser currentUser = CurrentUser;
            List<Photo> photos = _unitOfWork.Photos.Find(p => p.GiveawayId == giveawayId).Skip(page * pageSize).Take(pageSize).ToList();

            List<PhotoDTO> photoDTOs = new List<PhotoDTO>(pageSize);

            foreach (Photo photo in photos)
            {
                List<LikeDTO> likes = new List<LikeDTO>(photo.Likes.Count);
                foreach (Like like in photo.Likes)
                {
                    likes.Add(LikeMapper.ToLikeDTO(like,
                        UserMapper.ToUserDTO(
                            like.Owner,
                            _unitOfWork.Confirmations.Find(c => c.UserId == like.OwnerId).FirstOrDefault() != null,
                            _unitOfWork.Followings.Find(f => f.FollowedUserId == like.OwnerId && f.UserId == currentUser.Id).FirstOrDefault() != null,
                            _unitOfWork.Blockings.Find(b => b.BlockedUserId == like.OwnerId && b.UserId == currentUser.Id).FirstOrDefault() != null,
                            _unitOfWork.Blockings.Find(b => b.BlockedUserId == currentUser.Id && b.UserId == currentUser.Id).FirstOrDefault() != null
                    )));
                }

                List<CommentDTO> comments = new List<CommentDTO>(photo.Comments.Count);
                foreach (Comment comment in photo.Comments)
                {
                    comments.Add(CommentMapper.ToCommentDTO(
                        comment,
                        UserMapper.ToUserDTO(
                            comment.Owner,
                            _unitOfWork.Confirmations.Find(c => c.UserId == comment.OwnerId).FirstOrDefault() != null,
                            _unitOfWork.Followings.Find(f => f.FollowedUserId == comment.OwnerId && f.UserId == currentUser.Id).FirstOrDefault() != null,
                            _unitOfWork.Blockings.Find(b => b.BlockedUserId == comment.OwnerId && b.UserId == currentUser.Id).FirstOrDefault() != null,
                            _unitOfWork.Blockings.Find(b => b.BlockedUserId == currentUser.Id && b.UserId == currentUser.Id).FirstOrDefault() != null
                    )));
                }

                photoDTOs.Add(PhotoMapper.ToPhotoDTO(
                    photo,
                    _unitOfWork.Likes.Find(l => l.OwnerId == currentUser.Id && l.PhotoId == photo.Id).FirstOrDefault() != null,
                    UserMapper.ToUserDTO(
                        photo.Owner,
                        _unitOfWork.Confirmations.Find(c => c.UserId == photo.OwnerId).FirstOrDefault() != null,
                        _unitOfWork.Followings.Find(f => f.FollowedUserId == photo.OwnerId && f.UserId == currentUser.Id).FirstOrDefault() != null,
                        _unitOfWork.Blockings.Find(b => b.BlockedUserId == photo.OwnerId && b.UserId == currentUser.Id).FirstOrDefault() != null,
                            _unitOfWork.Blockings.Find(b => b.BlockedUserId == currentUser.Id && b.UserId == currentUser.Id).FirstOrDefault() != null
                    ),
                    likes,
                    comments));
            }

            return photoDTOs;
        }

        public void RemoveFromGiveaway(int id)
        {
            Photo photo = _unitOfWork.Photos.Get(id);

            if (photo != null)
            {
                photo.GiveawayId = null;
                _unitOfWork.Photos.Update(photo);

                _unitOfWork.Save();
            }
        }
        public async Task RemoveFromGiveawayAsync(int id)
        {
            Photo photo = await _unitOfWork.Photos.GetAsync(id);

            if(photo != null)
            {
                photo.GiveawayId = null;
                _unitOfWork.Photos.Update(photo);

                await _unitOfWork.SaveAsync();
            }
        }

        public void AddToGiveaway(int giveawayId, int id)
        {
            Photo photo = _unitOfWork.Photos.Get(id);
            Giveaway giveaway = _unitOfWork.Giveaways.Get(giveawayId);

            if (photo != null && giveaway != null)
            {
                photo.GiveawayId = giveaway.Id;
                _unitOfWork.Photos.Update(photo);

                _unitOfWork.Save();
            }
        }
        public async Task AddToGiveawayAsync(int giveawayId, int id)
        {
            Photo photo = await _unitOfWork.Photos.GetAsync(id);
            Giveaway giveaway = await _unitOfWork.Giveaways.GetAsync(giveawayId);

            if(photo != null && giveaway != null)
            {
                photo.GiveawayId = giveaway.Id;
                _unitOfWork.Photos.Update(photo);

                await _unitOfWork.SaveAsync();
            }
        }

        public int Create(string filter, string description, string path)
        {
            Photo photo = new Photo()
            {
                FilterId = _unitOfWork.Filters.Find(f => f.Name == filter).FirstOrDefault().Id,
                Description = description,
                Path = path,
                OwnerId = CurrentUser.Id
            };

            PhotoView pv = new PhotoView() { Count = 0 };

            _unitOfWork.PhotoViews.Create(pv);
            _unitOfWork.Save();

            photo.PhotoViewId = pv.Id;

            _unitOfWork.Photos.Create(photo);
            _unitOfWork.Save();

            return photo.Id;
        }
        public async Task<int> CreateAsync(string filter, string description, string path)
        {
            Photo photo = new Photo()
            {
                FilterId = _unitOfWork.Filters.Find(f => f.Name == filter).FirstOrDefault().Id,
                Description = description,
                Path = path,
                OwnerId = CurrentUser.Id
            };

            PhotoView pv = new PhotoView() { Count = 0 };

            await _unitOfWork.PhotoViews.CreateAsync(pv);
            await _unitOfWork.SaveAsync();

            photo.PhotoViewId = pv.Id;

            await _unitOfWork.Photos.CreateAsync(photo);
            await _unitOfWork.SaveAsync();

            return photo.Id;
        }

        public void Edit(int id, string filter, string description)
        {
            Photo photo = _unitOfWork.Photos.Get(id);

            if (photo != null)
            {
                Filter flt = _unitOfWork.Filters.Find(f => f.Name == filter).FirstOrDefault();
                if (flt != null)
                    photo.FilterId = flt.Id;

                if (description != null)
                    photo.Description = description;

                _unitOfWork.Photos.Update(photo);
                _unitOfWork.Save();
            }
        }
        public async Task EditAsync(int id, string filter, string description)
        {
            Photo photo = await _unitOfWork.Photos.GetAsync(id);

            if(photo != null)
            {
                Filter flt = _unitOfWork.Filters.Find(f => f.Name == filter).FirstOrDefault();
                if (flt != null)
                    photo.FilterId = flt.Id;
                
                photo.Description = description;

                _unitOfWork.Photos.Update(photo);
                await _unitOfWork.SaveAsync();
            }
        }

        public void Delete(int id)
        {
            Photo photo = _unitOfWork.Photos.Get(id);
            int pvId = photo.PhotoViewId;

            _unitOfWork.Photos.Delete(photo.Id);
            _unitOfWork.PhotoViews.Delete(pvId);

            _unitOfWork.Save();
        }
        public async Task DeleteAsync(int id)
        {
            Photo photo = await _unitOfWork.Photos.GetAsync(id);
            int pvId = photo.PhotoViewId;

            await _unitOfWork.Photos.DeleteAsync(photo.Id);
            await _unitOfWork.PhotoViews.DeleteAsync(pvId);

            await _unitOfWork.SaveAsync();
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}