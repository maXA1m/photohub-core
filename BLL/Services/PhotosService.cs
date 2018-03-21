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
        private readonly UsersMapper _usersMapper;
        private readonly FiltersMapper _filtersMapper;
        private readonly PhotosMapper _photosMapper;
        private readonly LikesMapper _likesMapper;
        private readonly CommentsMapper _commentsMapper;

        public ApplicationUser CurrentUser => _unitOfWork.Users.Find(u => u.UserName == _httpContextAccessor.HttpContext.User.Identity.Name).FirstOrDefault();
        public UserDTO CurrentUserDTO
        {
            get
            {
                ApplicationUser user = CurrentUser;

                return _usersMapper.Map(
                    user,
                    _unitOfWork.Confirmations.Find(c => c.UserId == user.Id).FirstOrDefault() != null,
                    _unitOfWork.Followings.Find(f => f.FollowedUserId == user.Id && f.UserId == user.Id).FirstOrDefault() != null,
                    _unitOfWork.Blockings.Find(b => b.BlockedUserId == user.Id && b.UserId == user.Id).FirstOrDefault() != null,
                    _unitOfWork.Blockings.Find(b => b.BlockedUserId == user.Id && b.UserId == user.Id).FirstOrDefault() != null
                );
            }
        }

        public List<FilterDTO> Filters => _filtersMapper.MapRange(_unitOfWork.Filters.GetAll(0, 14));

        public PhotosService(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            _usersMapper = new UsersMapper();
            _filtersMapper = new FiltersMapper();
            _photosMapper = new PhotosMapper();
            _commentsMapper = new CommentsMapper();
            _likesMapper = new LikesMapper();
        }

        public IEnumerable<PhotoDTO> GetAll(int page, int pageSize)
        {
            ApplicationUser currentUser = CurrentUser;
            IEnumerable<Photo> photos = _unitOfWork.Photos.GetAll(page, pageSize);

            List<PhotoDTO> photoDTOs = new List<PhotoDTO>(pageSize);

            if (currentUser == null)
            {
                foreach (Photo photo in photos)
                {
                    List<LikeDTO> likes = new List<LikeDTO>(photo.Likes.Count);
                    foreach (Like like in photo.Likes)
                    {
                        likes.Add(_likesMapper.Map(like,
                            _usersMapper.Map(
                                like.Owner,
                                _unitOfWork.Confirmations.Find(c => c.UserId == like.OwnerId).FirstOrDefault() != null,
                                false, false, false
                        )));
                    }

                    List<CommentDTO> comments = new List<CommentDTO>(photo.Comments.Count);
                    foreach (Comment comment in photo.Comments)
                    {
                        comments.Add(_commentsMapper.Map(
                            comment,
                            _usersMapper.Map(
                                comment.Owner,
                                _unitOfWork.Confirmations.Find(c => c.UserId == comment.OwnerId).FirstOrDefault() != null,
                                false, false, false
                        )));
                    }

                    photoDTOs.Add(_photosMapper.Map(
                        photo,
                        false,
                        false,
                        _usersMapper.Map(
                            photo.Owner,
                            _unitOfWork.Confirmations.Find(c => c.UserId == photo.OwnerId).FirstOrDefault() != null,
                            false, false, false
                        ),
                        likes,
                        comments));
                }
            }
            else
            {
                foreach (Photo photo in photos)
                {
                    List<LikeDTO> likes = new List<LikeDTO>(photo.Likes.Count);
                    foreach (Like like in photo.Likes)
                    {
                        likes.Add(_likesMapper.Map(like,
                            _usersMapper.Map(
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
                        comments.Add(_commentsMapper.Map(
                            comment,
                            _usersMapper.Map(
                                comment.Owner,
                                _unitOfWork.Confirmations.Find(c => c.UserId == comment.OwnerId).FirstOrDefault() != null,
                                _unitOfWork.Followings.Find(f => f.FollowedUserId == comment.OwnerId && f.UserId == currentUser.Id).FirstOrDefault() != null,
                                _unitOfWork.Blockings.Find(b => b.BlockedUserId == comment.OwnerId && b.UserId == currentUser.Id).FirstOrDefault() != null,
                                _unitOfWork.Blockings.Find(b => b.BlockedUserId == currentUser.Id && b.UserId == currentUser.Id).FirstOrDefault() != null
                        )));
                    }

                    photoDTOs.Add(_photosMapper.Map(
                        photo,
                        _unitOfWork.Likes.Find(l => l.OwnerId == currentUser.Id && l.PhotoId == photo.Id).FirstOrDefault() != null,
                        _unitOfWork.Bookmarks.Find(b => b.UserId == currentUser.Id && b.PhotoId == photo.Id).FirstOrDefault() != null,
                        _usersMapper.Map(
                            photo.Owner,
                            _unitOfWork.Confirmations.Find(c => c.UserId == photo.OwnerId).FirstOrDefault() != null,
                            _unitOfWork.Followings.Find(f => f.FollowedUserId == photo.OwnerId && f.UserId == currentUser.Id).FirstOrDefault() != null,
                            _unitOfWork.Blockings.Find(b => b.BlockedUserId == photo.OwnerId && b.UserId == currentUser.Id).FirstOrDefault() != null,
                                _unitOfWork.Blockings.Find(b => b.BlockedUserId == currentUser.Id && b.UserId == currentUser.Id).FirstOrDefault() != null
                        ),
                        likes,
                        comments));
                }
            }

            return photoDTOs;
        }

        public PhotoDTO Get(int id)
        {
            ApplicationUser currentUser = CurrentUser;
            Photo photo = _unitOfWork.Photos.Get(id);

            photo.CountViews++;
            _unitOfWork.Photos.Update(photo);
            _unitOfWork.Save();

            if(currentUser == null)
            {
                List<LikeDTO> likes = new List<LikeDTO>(photo.Likes.Count);
                foreach (Like like in photo.Likes)
                {
                    likes.Add(_likesMapper.Map(like,
                        _usersMapper.Map(
                            like.Owner,
                            _unitOfWork.Confirmations.Find(c => c.UserId == like.OwnerId).FirstOrDefault() != null,
                            false, false, false
                    )));
                }

                List<CommentDTO> comments = new List<CommentDTO>(photo.Comments.Count);
                foreach (Comment comment in photo.Comments)
                {
                    comments.Add(_commentsMapper.Map(
                        comment,
                        _usersMapper.Map(
                            comment.Owner,
                            _unitOfWork.Confirmations.Find(c => c.UserId == comment.OwnerId).FirstOrDefault() != null,
                            false, false, false
                    )));
                }

                return _photosMapper.Map(
                            photo,
                            false,
                            false,
                            _usersMapper.Map(
                                photo.Owner,
                                _unitOfWork.Confirmations.Find(c => c.UserId == photo.OwnerId).FirstOrDefault() != null,
                                false, false, false
                            ),
                            likes,
                            comments
                );
            }
            else
            {
                List<LikeDTO> likes = new List<LikeDTO>(photo.Likes.Count);
                foreach (Like like in photo.Likes)
                {
                    likes.Add(_likesMapper.Map(like,
                        _usersMapper.Map(
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
                    comments.Add(_commentsMapper.Map(
                        comment,
                        _usersMapper.Map(
                            comment.Owner,
                            _unitOfWork.Confirmations.Find(c => c.UserId == comment.OwnerId).FirstOrDefault() != null,
                            _unitOfWork.Followings.Find(f => f.FollowedUserId == comment.OwnerId && f.UserId == currentUser.Id).FirstOrDefault() != null,
                            _unitOfWork.Blockings.Find(b => b.BlockedUserId == comment.OwnerId && b.UserId == currentUser.Id).FirstOrDefault() != null,
                                _unitOfWork.Blockings.Find(b => b.BlockedUserId == currentUser.Id && b.UserId == currentUser.Id).FirstOrDefault() != null
                    )));
                }

                return _photosMapper.Map(
                            photo,
                            _unitOfWork.Likes.Find(l => l.OwnerId == currentUser.Id && l.PhotoId == photo.Id).FirstOrDefault() != null,
                            _unitOfWork.Bookmarks.Find(b => b.UserId == currentUser.Id && b.PhotoId == photo.Id).FirstOrDefault() != null,
                            _usersMapper.Map(
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
        }
        public async Task<PhotoDTO> GetAsync(int id)
        {
            ApplicationUser currentUser = CurrentUser;
            Photo photo =  await _unitOfWork.Photos.GetAsync(id);

            photo.CountViews++;
            _unitOfWork.Photos.Update(photo);
            await _unitOfWork.SaveAsync();

            if (currentUser == null)
            {
                List<LikeDTO> likes = new List<LikeDTO>(photo.Likes.Count);
                foreach (Like like in photo.Likes)
                {
                    likes.Add(_likesMapper.Map(like,
                        _usersMapper.Map(
                            like.Owner,
                            _unitOfWork.Confirmations.Find(c => c.UserId == like.OwnerId).FirstOrDefault() != null,
                            false, false, false
                    )));
                }

                List<CommentDTO> comments = new List<CommentDTO>(photo.Comments.Count);
                foreach (Comment comment in photo.Comments)
                {
                    comments.Add(_commentsMapper.Map(
                        comment,
                        _usersMapper.Map(
                            comment.Owner,
                            _unitOfWork.Confirmations.Find(c => c.UserId == comment.OwnerId).FirstOrDefault() != null,
                            false, false, false
                    )));
                }

                return _photosMapper.Map(
                            photo,
                            false,
                            false,
                            _usersMapper.Map(
                                photo.Owner,
                                _unitOfWork.Confirmations.Find(c => c.UserId == photo.OwnerId).FirstOrDefault() != null,
                                false, false, false
                            ),
                            likes,
                            comments
                );
            }
            else
            {
                List<LikeDTO> likes = new List<LikeDTO>(photo.Likes.Count);
                foreach (Like like in photo.Likes)
                {
                    likes.Add(_likesMapper.Map(like,
                        _usersMapper.Map(
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
                    comments.Add(_commentsMapper.Map(
                        comment,
                        _usersMapper.Map(
                            comment.Owner,
                            _unitOfWork.Confirmations.Find(c => c.UserId == comment.OwnerId).FirstOrDefault() != null,
                            _unitOfWork.Followings.Find(f => f.FollowedUserId == comment.OwnerId && f.UserId == currentUser.Id).FirstOrDefault() != null,
                            _unitOfWork.Blockings.Find(b => b.BlockedUserId == comment.OwnerId && b.UserId == currentUser.Id).FirstOrDefault() != null,
                                _unitOfWork.Blockings.Find(b => b.BlockedUserId == currentUser.Id && b.UserId == currentUser.Id).FirstOrDefault() != null
                    )));
                }

                return _photosMapper.Map(
                            photo,
                            _unitOfWork.Likes.Find(l => l.OwnerId == currentUser.Id && l.PhotoId == photo.Id).FirstOrDefault() != null,
                            _unitOfWork.Bookmarks.Find(b => b.UserId == currentUser.Id && b.PhotoId == photo.Id).FirstOrDefault() != null,
                            _usersMapper.Map(
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
        }

        public IEnumerable<PhotoDTO> GetPhotosHome(int page, int pageSize)
        {
            ApplicationUser currentUser = CurrentUser;
            List<Following> followings = _unitOfWork.Followings.Find(f => f.UserId == currentUser.Id).ToList();
            List<Photo> photos = new List<Photo>();

            foreach (var follow in followings)
            {
                photos.AddRange(_unitOfWork.Photos.Find(p => p.OwnerId == follow.FollowedUserId));
            }

            photos.Skip(page * pageSize).Take(pageSize);
            photos.Sort((p, p2) => p2.Date.CompareTo(p.Date));

            List<PhotoDTO> photoDTOs = new List<PhotoDTO>(pageSize);

            foreach (Photo photo in photos)
            {
                List<LikeDTO> likes = new List<LikeDTO>(photo.Likes.Count);
                foreach (Like like in photo.Likes)
                {
                    likes.Add(_likesMapper.Map(like,
                        _usersMapper.Map(
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
                    comments.Add(_commentsMapper.Map(
                        comment,
                        _usersMapper.Map(
                            comment.Owner,
                            _unitOfWork.Confirmations.Find(c => c.UserId == comment.OwnerId).FirstOrDefault() != null,
                            _unitOfWork.Followings.Find(f => f.FollowedUserId == comment.OwnerId && f.UserId == currentUser.Id).FirstOrDefault() != null,
                            _unitOfWork.Blockings.Find(b => b.BlockedUserId == comment.OwnerId && b.UserId == currentUser.Id).FirstOrDefault() != null,
                            _unitOfWork.Blockings.Find(b => b.BlockedUserId == currentUser.Id && b.UserId == currentUser.Id).FirstOrDefault() != null
                    )));
                }

                photoDTOs.Add(_photosMapper.Map(
                    photo,
                    _unitOfWork.Likes.Find(l => l.OwnerId == currentUser.Id && l.PhotoId == photo.Id).FirstOrDefault() != null,
                    _unitOfWork.Bookmarks.Find(b => b.UserId == currentUser.Id && b.PhotoId == photo.Id).FirstOrDefault() != null,
                    _usersMapper.Map(
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

            IEnumerable<Photo> photos = _unitOfWork.Photos.Find(p => p.OwnerId == user.Id).Skip(page * pageSize).Take(pageSize);

            List<PhotoDTO> photoDTOs = new List<PhotoDTO>(pageSize);

            if(currentUser == null)
            {
                foreach (Photo photo in photos)
                {
                    List<LikeDTO> likes = new List<LikeDTO>(photo.Likes.Count);
                    foreach (Like like in photo.Likes)
                    {
                        likes.Add(_likesMapper.Map(like,
                            _usersMapper.Map(
                                like.Owner,
                                _unitOfWork.Confirmations.Find(c => c.UserId == like.OwnerId).FirstOrDefault() != null,
                                false, false, false
                        )));
                    }

                    List<CommentDTO> comments = new List<CommentDTO>(photo.Comments.Count);
                    foreach (Comment comment in photo.Comments)
                    {
                        comments.Add(_commentsMapper.Map(
                            comment,
                            _usersMapper.Map(
                                comment.Owner,
                                _unitOfWork.Confirmations.Find(c => c.UserId == comment.OwnerId).FirstOrDefault() != null,
                                false, false, false
                        )));
                    }

                    photoDTOs.Add(_photosMapper.Map(
                        photo,
                        false,
                        false,
                        _usersMapper.Map(
                            photo.Owner,
                            _unitOfWork.Confirmations.Find(c => c.UserId == photo.OwnerId).FirstOrDefault() != null,
                            false, false, false
                        ),
                        likes,
                        comments));
                }
            }
            else
            {
                foreach (Photo photo in photos)
                {
                    List<LikeDTO> likes = new List<LikeDTO>(photo.Likes.Count);
                    foreach (Like like in photo.Likes)
                    {
                        likes.Add(_likesMapper.Map(like,
                            _usersMapper.Map(
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
                        comments.Add(_commentsMapper.Map(
                            comment,
                            _usersMapper.Map(
                                comment.Owner,
                                _unitOfWork.Confirmations.Find(c => c.UserId == comment.OwnerId).FirstOrDefault() != null,
                                _unitOfWork.Followings.Find(f => f.FollowedUserId == comment.OwnerId && f.UserId == currentUser.Id).FirstOrDefault() != null,
                                _unitOfWork.Blockings.Find(b => b.BlockedUserId == comment.OwnerId && b.UserId == currentUser.Id).FirstOrDefault() != null,
                                _unitOfWork.Blockings.Find(b => b.BlockedUserId == currentUser.Id && b.UserId == currentUser.Id).FirstOrDefault() != null
                        )));
                    }

                    photoDTOs.Add(_photosMapper.Map(
                        photo,
                        _unitOfWork.Likes.Find(l => l.OwnerId == currentUser.Id && l.PhotoId == photo.Id).FirstOrDefault() != null,
                        _unitOfWork.Bookmarks.Find(b => b.UserId == currentUser.Id && b.PhotoId == photo.Id).FirstOrDefault() != null,
                        _usersMapper.Map(
                            photo.Owner,
                            _unitOfWork.Confirmations.Find(c => c.UserId == photo.OwnerId).FirstOrDefault() != null,
                            _unitOfWork.Followings.Find(f => f.FollowedUserId == photo.OwnerId && f.UserId == currentUser.Id).FirstOrDefault() != null,
                            _unitOfWork.Blockings.Find(b => b.BlockedUserId == photo.OwnerId && b.UserId == currentUser.Id).FirstOrDefault() != null,
                                _unitOfWork.Blockings.Find(b => b.BlockedUserId == currentUser.Id && b.UserId == currentUser.Id).FirstOrDefault() != null
                        ),
                        likes,
                        comments));
                }
            }

            return photoDTOs;
        }

        public IEnumerable<PhotoDTO> GetBookmarks(int page, int pageSize)
        {
            ApplicationUser currentUser = CurrentUser;
            IEnumerable<Bookmark> bookmarks = _unitOfWork.Bookmarks.Find(b => b.UserId == currentUser.Id);
            List<Photo> photos = new List<Photo>();

            foreach (var bookmark in bookmarks)
                photos.Add(bookmark.Photo);

            photos.Skip(page * pageSize).Take(pageSize);
            photos.Sort((p, p2) => p2.Date.CompareTo(p.Date));

            List<PhotoDTO> photoDTOs = new List<PhotoDTO>(pageSize);

            foreach (Photo photo in photos)
            {
                List<LikeDTO> likes = new List<LikeDTO>(photo.Likes.Count);
                foreach (Like like in photo.Likes)
                {
                    likes.Add(_likesMapper.Map(like,
                        _usersMapper.Map(
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
                    comments.Add(_commentsMapper.Map(
                        comment,
                        _usersMapper.Map(
                            comment.Owner,
                            _unitOfWork.Confirmations.Find(c => c.UserId == comment.OwnerId).FirstOrDefault() != null,
                            _unitOfWork.Followings.Find(f => f.FollowedUserId == comment.OwnerId && f.UserId == currentUser.Id).FirstOrDefault() != null,
                            _unitOfWork.Blockings.Find(b => b.BlockedUserId == comment.OwnerId && b.UserId == currentUser.Id).FirstOrDefault() != null,
                            _unitOfWork.Blockings.Find(b => b.BlockedUserId == currentUser.Id && b.UserId == currentUser.Id).FirstOrDefault() != null
                    )));
                }

                photoDTOs.Add(_photosMapper.Map(
                    photo,
                    _unitOfWork.Likes.Find(l => l.OwnerId == currentUser.Id && l.PhotoId == photo.Id).FirstOrDefault() != null,
                    _unitOfWork.Bookmarks.Find(b => b.UserId == currentUser.Id && b.PhotoId == photo.Id).FirstOrDefault() != null,
                    _usersMapper.Map(
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

        public void Bookmark(int id)
        {
            ApplicationUser currentUser = CurrentUser;
            Photo bookmarkedPhoto = _unitOfWork.Photos.Find(p => p.Id == id).FirstOrDefault();

            if (currentUser != null && bookmarkedPhoto != null && _unitOfWork.Bookmarks.Find(b => b.UserId == currentUser.Id && b.PhotoId == id).FirstOrDefault() == null)
            {
                _unitOfWork.Bookmarks.Create(new Bookmark()
                {
                    UserId = currentUser.Id,
                    PhotoId = bookmarkedPhoto.Id
                });

                _unitOfWork.Save();
            }
        }
        public async Task BookmarkAsync(int id)
        {
            ApplicationUser currentUser = CurrentUser;
            Photo bookmarkedPhoto = _unitOfWork.Photos.Find(p => p.Id == id).FirstOrDefault();

            if (currentUser != null && bookmarkedPhoto != null && _unitOfWork.Bookmarks.Find(b => b.UserId == currentUser.Id && b.PhotoId == id).FirstOrDefault() == null)
            {
                _unitOfWork.Bookmarks.Create(new Bookmark()
                {
                    UserId = currentUser.Id,
                    PhotoId = bookmarkedPhoto.Id
                });

                await _unitOfWork.SaveAsync();
            }
        }

        public void DismissBookmark(int id)
        {
            ApplicationUser currentUser = CurrentUser;
            Photo bookmarkedPhoto = _unitOfWork.Photos.Find(p => p.Id == id).FirstOrDefault();
            Bookmark bookmark = _unitOfWork.Bookmarks.Find(b => b.UserId == currentUser.Id && b.PhotoId == id).FirstOrDefault();

            if (currentUser != null && bookmarkedPhoto != null && bookmark != null)
            {
                _unitOfWork.Bookmarks.Delete(bookmark.Id);
                _unitOfWork.Save();
            }
        }
        public async Task DismissBookmarkAsync(int id)
        {
            ApplicationUser currentUser = CurrentUser;
            Photo bookmarkedPhoto = _unitOfWork.Photos.Find(p => p.Id == id).FirstOrDefault();
            Bookmark bookmark = _unitOfWork.Bookmarks.Find(b => b.UserId == currentUser.Id && b.PhotoId == id).FirstOrDefault();

            if (currentUser != null && bookmarkedPhoto != null && bookmark != null)
            {
                await _unitOfWork.Bookmarks.DeleteAsync(bookmark.Id);
                await _unitOfWork.SaveAsync();
            }
        }

        public int Create(string filter, string description, string path, string manufacturer, string model, string iso, string exposure, string aperture)
        {
            Photo photo = new Photo()
            {
                FilterId = _unitOfWork.Filters.Find(f => f.Name == filter).FirstOrDefault().Id,
                Description = description,
                Path = path,
                OwnerId = CurrentUser.Id,
                CountViews = 0,

                Manufacturer = manufacturer,
                Model = model,
                Iso = iso,
                Exposure = exposure,
                Aperture = aperture
            };

            _unitOfWork.Photos.Create(photo);
            _unitOfWork.Save();

            return photo.Id;
        }
        public async ValueTask<int> CreateAsync(string filter, string description, string path, string manufacturer, string model, string iso, string exposure, string aperture)
        {
            Photo photo = new Photo()
            {
                FilterId = _unitOfWork.Filters.Find(f => f.Name == filter).FirstOrDefault().Id,
                Description = description,
                Path = path,
                OwnerId = CurrentUser.Id,
                CountViews = 0,

                Manufacturer = manufacturer,
                Model = model,
                Iso = iso,
                Exposure = exposure,
                Aperture = aperture
            };

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

            if(photo != null)
            {
                _unitOfWork.Photos.Delete(photo.Id);
                _unitOfWork.Save();
            }
        }
        public async Task DeleteAsync(int id)
        {
            Photo photo = await _unitOfWork.Photos.GetAsync(id);

            if(photo != null)
            {
                await _unitOfWork.Photos.DeleteAsync(photo.Id);
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