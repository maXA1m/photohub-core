using System.Collections.Generic;
using PhotoHub.DAL.Entities;
#region using PhotoHub.BLL
using PhotoHub.BLL.DTO;
using PhotoHub.BLL.Interfaces;
#endregion

namespace PhotoHub.BLL.Mappers
{
    public class PhotosMapper : IMapper<PhotoDTO, Photo>
    {
        public PhotoDTO Map(Photo item)
        {
            if (item == null)
                return null;

            return new PhotoDTO()
            {
                Id = item.Id,
                Path = item.Path,
                Filter = item.Filter.Name,
                Description = item.Description,
                Date = item.Date,
                CountViews = item.CountViews,

                Liked = false,
                Bookmarked = false,

                Manufacturer = item.Manufacturer,
                Model = item.Model,
                Iso = item.Iso,
                Exposure = item.Exposure,
                Aperture = item.Aperture,
                FocalLength = item.Aperture,

                Owner = null,
                Likes = null,
                Comments = null,
                Tags = null
            };
        }
        public PhotoDTO Map(Photo item, bool liked, bool bookmarked, UserDTO owner, ICollection<LikeDTO> likes, ICollection<CommentDTO> comments, ICollection<TagDTO> tags)
        {
            if (item == null)
                return null;

            return new PhotoDTO()
            {
                Id = item.Id,
                Path = item.Path,
                Filter = item.Filter.Name,
                Description = item.Description,
                Date = item.Date,
                CountViews = item.CountViews,

                Liked = liked,
                Bookmarked = bookmarked,

                Manufacturer = item.Manufacturer,
                Model = item.Model,
                Iso = item.Iso,
                Exposure = item.Exposure,
                Aperture = item.Aperture,
                FocalLength = item.FocalLength,

                Owner = owner,
                Likes = likes,
                Comments = comments,
                Tags = tags
            };
        }

        public List<PhotoDTO> MapRange(IEnumerable<Photo> items)
        {
            if (items == null)
                return null;

            return null;
        }
    }
}
