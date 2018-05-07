#region using System
using System;
using System.Collections.Generic;
#endregion
#region using PhotoHub.BLL
using PhotoHub.BLL.DTO;
using PhotoHub.BLL.Interfaces;
#endregion
using PhotoHub.WEB.ViewModels;

namespace PhotoHub.WEB.Mappers
{
    public class PhotosMapper
    {
        private readonly UsersMapper _usersMapper;
        private readonly CommentsMapper _commentsMapper;
        private readonly LikesMapper _likesMapper;
        private readonly TagsMapper _tagsMapper;

        public PhotosMapper()
        {
            _usersMapper = new UsersMapper();
            _commentsMapper = new CommentsMapper();
            _likesMapper = new LikesMapper();
            _tagsMapper = new TagsMapper();
        }

        public PhotoViewModel Map(PhotoDTO item)
        {
            if (item == null)
                return null;

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

                Manufacturer = String.IsNullOrEmpty(item.Manufacturer) ? "Unknown" : item.Manufacturer,
                Model = String.IsNullOrEmpty(item.Model) ? "Unknown" : item.Model,
                Iso = item.Iso != null ? item.Iso.ToString() : "Unknown",
                Exposure = item.Exposure != null ? item.Exposure.ToString() + " sec" : "Unknown",
                Aperture = item.Aperture != null ? "f/" + item.Aperture.ToString() : "Unknown",
                FocalLength = item.FocalLength != null ? item.FocalLength.ToString() + "mm" : "Unknown",

                Likes = _likesMapper.MapRange(item.Likes),
                Comments = _commentsMapper.MapRange(item.Comments),
                Tags = _tagsMapper.MapRange(item.Tags)
            };
        }

        public List<PhotoViewModel> MapRange(IEnumerable<PhotoDTO> items)
        {
            if (items == null)
                return null;

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
                    Iso = item.Iso != null ? item.Iso.ToString() : "Unknown",
                    Exposure = item.Exposure != null ? item.Exposure.ToString() + " sec" : "Unknown",
                    Aperture = item.Aperture != null ? "f/" + item.Aperture.ToString() : "Unknown",
                    FocalLength = item.FocalLength != null ? item.FocalLength.ToString() + "mm" : "Unknown",

                    Likes = _likesMapper.MapRange(item.Likes),
                    Comments = _commentsMapper.MapRange(item.Comments),
                    Tags = _tagsMapper.MapRange(item.Tags)
                });
            }

            return photos;
        }
    }
}