using System.Collections.Generic;
using PhotoHub.DAL.Entities;
using PhotoHub.BLL.DTO;

namespace PhotoHub.BLL.Extensions
{
    /// <summary>
    /// Methods for mapping photo entities to photo data transfer objects.
    /// </summary>
    public static class PhotosExtensions
    {
        /// <summary>
        /// Maps photo entity to photo DTO.
        /// </summary>
        public static PhotoDTO ToDTO(this Photo item)
        {
            if (item == null)
            {
                return null;
            }

            return new PhotoDTO
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

        /// <summary>
        /// Maps photo entity to photo DTO.
        /// </summary>
        public static PhotoDTO ToDTO(this Photo item, bool liked, bool bookmarked, UserDTO owner, ICollection<LikeDTO> likes, ICollection<CommentDTO> comments, ICollection<TagDTO> tags)
        {
            if (item == null)
            {
                return null;
            }

            return new PhotoDTO
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
    }
}
