using PhotoHub.BLL.DTO;
using PhotoHub.BLL.Interfaces;
using PhotoHub.DAL.Entities;
using System.Collections.Generic;

namespace PhotoHub.BLL.Mappers
{
    public class PhotosMapper : IMapper<PhotoDTO, Photo>
    {
        public PhotoDTO Map(Photo item)
        {
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

                Owner = null,
                Likes = null,
                Comments = null
            };
        }
        public PhotoDTO Map(Photo item, bool liked, bool bookmarked, UserDTO owner, ICollection<LikeDTO> likes, ICollection<CommentDTO> comments)
        {
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

                Owner = owner,
                Likes = likes,
                Comments = comments
            };
        }

        public List<PhotoDTO> MapRange(IEnumerable<Photo> items)
        {
            throw new System.NotImplementedException();
        }
    }
}
