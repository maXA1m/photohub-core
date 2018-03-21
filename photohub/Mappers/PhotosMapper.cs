using PhotoHub.BLL.DTO;
using PhotoHub.WEB.ViewModels;
using System;
using System.Collections.Generic;

namespace PhotoHub.WEB.Mappers
{
    public class PhotosMapper
    {
        private readonly UsersMapper _usersMapper;
        private readonly CommentsMapper _commentsMapper;
        private readonly LikesMapper _likesMapper;

        public PhotosMapper()
        {
            _usersMapper = new UsersMapper();
            _commentsMapper = new CommentsMapper();
            _likesMapper = new LikesMapper();
        }

        public PhotoViewModel Map(PhotoDTO item)
        {
            return new PhotoViewModel()
            {
                Id = item.Id,
                Path = item.Path,
                Filter = item.Filter,
                Description = item.Description,
                Date = item.Date.ToString("MMMM dd, yyyy"),
                Owner = _usersMapper.Map(item.Owner),
                CountViews = item.CountViews,

                Liked = item.Liked,
                Bookmarked = item.Bookmarked,

                Manufacturer = String.IsNullOrEmpty(item.Manufacturer)?"Unknown": item.Manufacturer,
                Model = String.IsNullOrEmpty(item.Model) ? "Unknown" : item.Model,
                Iso = String.IsNullOrEmpty(item.Iso) ? "Unknown" : item.Iso,
                Exposure = String.IsNullOrEmpty(item.Exposure) ? "Unknown" : item.Exposure,
                Aperture = String.IsNullOrEmpty(item.Aperture) ? "Unknown" : item.Aperture,

                Likes = _likesMapper.MapRange(item.Likes),
                Comments = _commentsMapper.MapRange(item.Comments)
            };
        }

        public List<PhotoViewModel> MapRange(IEnumerable<PhotoDTO> items)
        {
            List<PhotoViewModel> photos = new List<PhotoViewModel>();

            foreach(PhotoDTO item in items)
            {
                photos.Add(new PhotoViewModel()
                {
                    Id = item.Id,
                    Path = item.Path,
                    Filter = item.Filter,
                    Description = item.Description,
                    Date = item.Date.ToString("MMMM dd, yyyy"),
                    Owner = _usersMapper.Map(item.Owner),
                    CountViews = item.CountViews,

                    Liked = item.Liked,
                    Bookmarked = item.Bookmarked,

                    Manufacturer = String.IsNullOrEmpty(item.Manufacturer) ? "Unknown" : item.Manufacturer,
                    Model = String.IsNullOrEmpty(item.Model) ? "Unknown" : item.Model,
                    Iso = String.IsNullOrEmpty(item.Iso) ? "Unknown" : item.Iso,
                    Exposure = String.IsNullOrEmpty(item.Exposure) ? "Unknown" : item.Exposure,
                    Aperture = String.IsNullOrEmpty(item.Aperture) ? "Unknown" : item.Aperture,

                    Likes = _likesMapper.MapRange(item.Likes),
                    Comments = _commentsMapper.MapRange(item.Comments)
                });
            }

            return photos;
        }
    }
}