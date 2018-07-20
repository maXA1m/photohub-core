#region using System/Microsoft
using System;
using System.Linq;
using System.Collections.Generic;
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
using PhotoHub.BLL.Helpers;
#endregion

namespace PhotoHub.BLL.Services
{
    public class PhotosService : IPhotosService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICurrentUserService _currentUserService;
        #region private readonly mappers
        private readonly UsersMapper _usersMapper;
        private readonly FiltersMapper _filtersMapper;
        private readonly PhotosMapper _photosMapper;
        private readonly LikesMapper _likesMapper;
        private readonly CommentsMapper _commentsMapper;
        private readonly TagsMapper _tagsMapper;
        private readonly ITagParser _tagParser;
        #endregion

        public List<FilterDTO> Filters => _filtersMapper.MapRange(_unitOfWork.Filters.GetAll());
        public List<TagDTO> Tags => _tagsMapper.MapRange(_unitOfWork.Tags.GetAll());

        public PhotosService(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            _usersMapper = new UsersMapper();
            _filtersMapper = new FiltersMapper();
            _photosMapper = new PhotosMapper();
            _commentsMapper = new CommentsMapper();
            _likesMapper = new LikesMapper();
            _tagsMapper = new TagsMapper();
            _tagParser = new TagParser();
            _currentUserService = new CurrentUserService(unitOfWork, httpContextAccessor);
        }

        public IEnumerable<PhotoDTO> GetAll(int page, int pageSize)
        {
            IEnumerable<Photo> photos = _unitOfWork.Photos.GetAll().OrderByDescending(p => p.Likes.Count).Skip(page * pageSize).Take(pageSize);

            List<PhotoDTO> photoDTOs = new List<PhotoDTO>(pageSize);

            foreach (Photo photo in photos)
                photoDTOs.Add(MapPhoto(photo));

            return photoDTOs;
        }

        public PhotoDTO Get(int id)
        {
            Photo photo = _unitOfWork.Photos.Get(id);

            photo.CountViews++;
            _unitOfWork.Photos.Update(photo);
            _unitOfWork.Save();

            return MapPhoto(photo);
        }
        public async Task<PhotoDTO> GetAsync(int id)
        {
            Photo photo =  await _unitOfWork.Photos.GetAsync(id);

            photo.CountViews++;
            _unitOfWork.Photos.Update(photo);
            await _unitOfWork.SaveAsync();

            return MapPhoto(photo);
        }

        public IEnumerable<PhotoDTO> GetPhotosHome(int page, int pageSize)
        {
            User currentUser = _currentUserService.Get;
            IEnumerable<Following> followings = _unitOfWork.Followings.Find(f => f.UserId == currentUser.Id);
            List<Photo> photos = new List<Photo>();

            foreach (var follow in followings)
                photos.AddRange(_unitOfWork.Photos.Find(p => p.OwnerId == follow.FollowedUserId));

            photos.Sort((p, p2) => p2.Date.CompareTo(p.Date));
            photos = photos.Skip(page * pageSize).Take(pageSize).ToList();

            List<PhotoDTO> photoDTOs = new List<PhotoDTO>(pageSize);

            foreach (Photo photo in photos)
                photoDTOs.Add(MapPhoto(photo));

            return photoDTOs;
        }

        public IEnumerable<PhotoDTO> GetForUser(int page, string userName, int pageSize)
        {
            User currentUser = _currentUserService.Get;
            User user = _unitOfWork.Users.Find(u => u.UserName == userName).FirstOrDefault();

            IEnumerable<Photo> photos = _unitOfWork.Photos.Find(p => p.OwnerId == user.Id).OrderByDescending(p => p.Date).Skip(page * pageSize).Take(pageSize);

            List<PhotoDTO> photoDTOs = new List<PhotoDTO>(pageSize);

            foreach (Photo photo in photos)
                photoDTOs.Add(MapPhoto(photo));

            return photoDTOs;
        }

        public IEnumerable<PhotoDTO> GetForTag(string tagName, int pageSize)
        {
            if (String.IsNullOrEmpty(tagName))
                return null;

            User currentUser = _currentUserService.Get;
            Tag tag = _unitOfWork.Tags.Find(t => t.Name.ToLower() == tagName.ToLower()).FirstOrDefault();

            if (tag == null)
                return null;

            IEnumerable<Taging> tagings = _unitOfWork.Tagings.Find(t => t.TagId == tag.Id).OrderByDescending(t => t.Photo.Date);

            IEnumerable<Photo> photos = tagings.Select(t => t.Photo).Skip(0).Take(pageSize);

            List<PhotoDTO> photoDTOs = new List<PhotoDTO>(pageSize);

            foreach (Photo photo in photos)
                photoDTOs.Add(MapPhoto(photo));

            return photoDTOs;
        }

        public IEnumerable<PhotoDTO> GetBookmarks(int page, int pageSize)
        {
            User currentUser = _currentUserService.Get;
            IEnumerable<Photo> photos = _unitOfWork.Bookmarks.Find(b => b.UserId == currentUser.Id).OrderByDescending(b => b.Date).Select(b => b.Photo).Skip(page * pageSize).Take(pageSize);

            List<PhotoDTO> photoDTOs = new List<PhotoDTO>(pageSize);

            foreach (Photo photo in photos)
                photoDTOs.Add(MapPhoto(photo));

            return photoDTOs;
        }

        public IEnumerable<PhotoDTO> GetTags(int tagId, int page, int pageSize)
        {
            User currentUser = _currentUserService.Get;
            if (_unitOfWork.Tags.Find(t => t.Id == tagId).FirstOrDefault() == null)
                return null;

            IEnumerable<Photo> photos = _unitOfWork.Tagings.Find(t => t.TagId == tagId).Select(t => t.Photo).OrderByDescending(p => p.Date).Skip(page * pageSize).Take(pageSize);

            List<PhotoDTO> photoDTOs = new List<PhotoDTO>(pageSize);

            foreach (Photo photo in photos)
                photoDTOs.Add(MapPhoto(photo));

            return photoDTOs;
        }

        public IEnumerable<PhotoDTO> Search(int page, string search, int pageSize, int? iso, double? exposure, double? aperture, double? focalLength)
        {
            User currentUser = _currentUserService.Get;
            IEnumerable<Photo> photos;
            if (String.IsNullOrEmpty(search))
            {
                photos = _unitOfWork.Photos.GetAll().OrderByDescending(p => p.Likes.Count).Skip(page * pageSize).Take(pageSize);
            }
            else
            {
                photos = _unitOfWork.Photos.Find(p =>
                    (!String.IsNullOrEmpty(p.Model) ? p.Model.ToLower().Contains(search.ToLower()) : false) ||
                    (!String.IsNullOrEmpty(p.Manufacturer) ? p.Manufacturer.ToLower().Contains(search.ToLower()) : false) ||
                    ((!String.IsNullOrEmpty(p.Manufacturer) && !String.IsNullOrEmpty(p.Model)) ? String.Format("{0} {1}", p.Manufacturer.ToLower(), p.Model.ToLower()).Contains(search.ToLower()) : false)
                ).OrderByDescending(p => p.Likes.Count).Skip(page * pageSize).Take(pageSize);
            }

            if(iso != null)
                photos = photos.Where(p => p.Iso == iso);
            if (exposure != null)
                photos = photos.Where(p => p.Exposure == exposure);
            if (aperture != null)
                photos = photos.Where(p => p.Aperture == aperture);
            if (focalLength != null)
                photos = photos.Where(p => p.FocalLength == focalLength);

            List<PhotoDTO> photoDTOs = new List<PhotoDTO>(pageSize);

            foreach (Photo photo in photos)
                photoDTOs.Add(MapPhoto(photo));

            return photoDTOs;
        }

        public void Bookmark(int id)
        {
            User currentUser = _currentUserService.Get;
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
            User currentUser = _currentUserService.Get;
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
            User currentUser = _currentUserService.Get;
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
            User currentUser = _currentUserService.Get;
            Photo bookmarkedPhoto = _unitOfWork.Photos.Find(p => p.Id == id).FirstOrDefault();
            Bookmark bookmark = _unitOfWork.Bookmarks.Find(b => b.UserId == currentUser.Id && b.PhotoId == id).FirstOrDefault();

            if (currentUser != null && bookmarkedPhoto != null && bookmark != null)
            {
                await _unitOfWork.Bookmarks.DeleteAsync(bookmark.Id);
                await _unitOfWork.SaveAsync();
            }
        }

        public void Report(int id, string text)
        {
            User currentUser = _currentUserService.Get;
            Photo reportedPhoto = _unitOfWork.Photos.Find(p => p.Id == id).FirstOrDefault();

            if (currentUser != null && reportedPhoto != null && _unitOfWork.PhotoReports.Find(pr => pr.UserId == currentUser.Id && pr.PhotoId == id).FirstOrDefault() == null)
            {
                _unitOfWork.PhotoReports.Create(new PhotoReport()
                {
                    UserId = currentUser.Id,
                    PhotoId = reportedPhoto.Id,
                    Text = text
                });

                _unitOfWork.Save();
            }
        }
        public async Task ReportAsync(int id, string text)
        {
            User currentUser = _currentUserService.Get;
            Photo reportedPhoto = _unitOfWork.Photos.Find(p => p.Id == id).FirstOrDefault();

            if (currentUser != null && reportedPhoto != null && _unitOfWork.PhotoReports.Find(pr => pr.UserId == currentUser.Id && pr.PhotoId == id).FirstOrDefault() == null)
            {
                _unitOfWork.PhotoReports.Create(new PhotoReport()
                {
                    UserId = currentUser.Id,
                    PhotoId = reportedPhoto.Id,
                    Text = text
                });

                await _unitOfWork.SaveAsync();
            }
        }

        public int Create(string filter, string description, string path, string manufacturer, string model, int? iso, double? exposure, double? aperture, double? focalLength, string tags)
        {
            Photo photo = new Photo()
            {
                FilterId = _unitOfWork.Filters.Find(f => f.Name == filter).FirstOrDefault().Id,
                Description = description,
                Path = path,
                OwnerId = _currentUserService.Get.Id,
                CountViews = 0,

                Manufacturer = manufacturer,
                Model = model,
                Iso = iso,
                Exposure = exposure,
                Aperture = aperture,
                FocalLength = null
            };

            if (focalLength >= 3)
                photo.FocalLength = focalLength;

            _unitOfWork.Photos.Create(photo);

            if (!String.IsNullOrEmpty(tags))
            {
                foreach (string tag in _tagParser.Parse(tags, _unitOfWork.Tags.GetAll()))
                {
                    Tag tg = _unitOfWork.Tags.Find(t => t.Name == tag).FirstOrDefault();
                    if (tg != null)
                    {
                        _unitOfWork.Tagings.Create(new Taging()
                        {
                            TagId = tg.Id,
                            PhotoId = photo.Id
                        });
                    }
                }
            }

            _unitOfWork.Save();

            return photo.Id;
        }
        public async ValueTask<int> CreateAsync(string filter, string description, string path, string manufacturer, string model, int? iso, double? exposure, double? aperture, double? focalLength, string tags)
        {
            Photo photo = new Photo()
            {
                FilterId = _unitOfWork.Filters.Find(f => f.Name == filter).FirstOrDefault().Id,
                Description = description,
                Path = path,
                OwnerId = _currentUserService.Get.Id,
                CountViews = 0,

                Manufacturer = manufacturer,
                Model = model,
                Iso = iso,
                Exposure = exposure,
                Aperture = aperture,
                FocalLength = null
            };

            if (focalLength >= 3)
                photo.FocalLength = focalLength;

            await _unitOfWork.Photos.CreateAsync(photo);

            if (!String.IsNullOrEmpty(tags))
            {
                foreach (string tag in _tagParser.Parse(tags, _unitOfWork.Tags.GetAll()))
                {
                    Tag tg = _unitOfWork.Tags.Find(t => t.Name == tag).FirstOrDefault();
                    if (tg != null)
                    {
                        await _unitOfWork.Tagings.CreateAsync(new Taging()
                        {
                            TagId = tg.Id,
                            PhotoId = photo.Id
                        });
                    }
                }
            }

            await _unitOfWork.SaveAsync();

            return photo.Id;
        }

        public void Edit(int id, string filter, string description, string tags, string model, string brand, int? iso, double? aperture, double? exposure, double? focalLength)
        {
            Photo photo = _unitOfWork.Photos.Get(id);

            if (photo != null)
            {
                Filter flt = _unitOfWork.Filters.Find(f => f.Name == filter).FirstOrDefault();
                if (flt != null)
                    photo.FilterId = flt.Id;

                foreach (Taging taging in _unitOfWork.Tagings.Find(t => t.PhotoId == photo.Id))
                    _unitOfWork.Tagings.Delete(taging.Id);

                if (!String.IsNullOrEmpty(tags))
                {
                    foreach (string tag in _tagParser.Parse(tags, _unitOfWork.Tags.GetAll()))
                    {
                        Tag tg = _unitOfWork.Tags.Find(t => t.Name == tag).FirstOrDefault();
                        if (tg != null)
                        {
                            _unitOfWork.Tagings.Create(new Taging()
                            {
                                TagId = tg.Id,
                                PhotoId = photo.Id
                            });
                        }
                    }
                }

                if (!String.IsNullOrEmpty(description) && description != photo.Description)
                    photo.Description = description;

                if (String.IsNullOrEmpty(photo.Manufacturer) && !String.IsNullOrEmpty(brand))
                    photo.Manufacturer = brand;

                if (String.IsNullOrEmpty(photo.Model) && !String.IsNullOrEmpty(model))
                    photo.Model = model;

                if (photo.Iso == null && iso != null)
                    photo.Iso = iso;

                if (photo.Aperture == null && aperture != null)
                    photo.Aperture = aperture;

                if (photo.Exposure == null && exposure != null)
                    photo.Exposure = exposure;

                if (photo.FocalLength == null && focalLength != null)
                    photo.FocalLength = focalLength;

                _unitOfWork.Photos.Update(photo);
                _unitOfWork.Save();
            }
        }
        public async Task EditAsync(int id, string filter, string description, string tags, string model, string brand, int? iso, double? aperture, double? exposure, double? focalLength)
        {
            Photo photo = await _unitOfWork.Photos.GetAsync(id);

            if(photo != null)
            {
                Filter flt = _unitOfWork.Filters.Find(f => f.Name == filter).FirstOrDefault();
                if (flt != null)
                    photo.FilterId = flt.Id;

                foreach (Taging taging in _unitOfWork.Tagings.Find(t => t.PhotoId == photo.Id))
                    await _unitOfWork.Tagings.DeleteAsync(taging.Id);

                if (!String.IsNullOrEmpty(tags))
                {
                    foreach (string tag in _tagParser.Parse(tags, _unitOfWork.Tags.GetAll()))
                    {
                        Tag tg = _unitOfWork.Tags.Find(t => t.Name == tag).FirstOrDefault();
                        if (tg != null)
                        {
                            await _unitOfWork.Tagings.CreateAsync(new Taging()
                            {
                                TagId = tg.Id,
                                PhotoId = photo.Id
                            });
                        }
                    }
                }

                if (!String.IsNullOrEmpty(description) && description != photo.Description)
                    photo.Description = description;

                if (String.IsNullOrEmpty(photo.Manufacturer) && !String.IsNullOrEmpty(brand))
                    photo.Manufacturer = brand;

                if (String.IsNullOrEmpty(photo.Model) && !String.IsNullOrEmpty(model))
                    photo.Model = model;

                if (photo.Iso == null && iso != null)
                    photo.Iso = iso;

                if (photo.Aperture == null && aperture != null)
                    photo.Aperture = aperture;

                if (photo.Exposure == null && exposure != null)
                    photo.Exposure = exposure;

                if (photo.FocalLength == null && focalLength != null)
                    photo.FocalLength = focalLength;

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

        #region Helpers
        protected PhotoDTO MapPhoto(Photo photo)
        {
            User currentUser = _currentUserService.Get;

            PhotoDTO value = null;

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

                value = _photosMapper.Map(
                    photo,
                    false,
                    false,
                    _usersMapper.Map(
                        photo.Owner,
                        _unitOfWork.Confirmations.Find(c => c.UserId == photo.OwnerId).FirstOrDefault() != null,
                        false, false, false
                    ),
                    likes,
                    comments,
                    _tagsMapper.MapRange(_unitOfWork.Tagings.Find(t => t.PhotoId == photo.Id).Select(t => t.Tag)));
            }
            else
            {
                if (_unitOfWork.Blockings.Find(b => b.BlockedUserId == currentUser.Id && b.UserId == photo.OwnerId).FirstOrDefault() == null)
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

                    value = _photosMapper.Map(
                        photo,
                        _unitOfWork.Likes.Find(l => l.OwnerId == currentUser.Id && l.PhotoId == photo.Id).FirstOrDefault() != null,
                        _unitOfWork.Bookmarks.Find(b => b.UserId == currentUser.Id && b.PhotoId == photo.Id).FirstOrDefault() != null,
                        _usersMapper.Map(
                            photo.Owner,
                            _unitOfWork.Confirmations.Find(c => c.UserId == photo.OwnerId).FirstOrDefault() != null,
                            _unitOfWork.Followings.Find(f => f.FollowedUserId == photo.OwnerId && f.UserId == currentUser.Id).FirstOrDefault() != null,
                            _unitOfWork.Blockings.Find(b => b.BlockedUserId == photo.OwnerId && b.UserId == currentUser.Id).FirstOrDefault() != null,
                            _unitOfWork.Blockings.Find(b => b.BlockedUserId == currentUser.Id && b.UserId == photo.OwnerId).FirstOrDefault() != null
                        ),
                        likes,
                        comments,
                        _tagsMapper.MapRange(_unitOfWork.Tagings.Find(t => t.PhotoId == photo.Id).Select(t => t.Tag)));
                }
            }

            return value;
        }
        #endregion

        public void Dispose()
        {
            _unitOfWork.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}