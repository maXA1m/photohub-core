using PhotoHub.BLL.DTO;
using PhotoHub.DAL.Entities;
using System.Collections.Generic;

namespace PhotoHub.BLL.Mappers
{
    public class PhotoMapper
    {
        public static PhotoDTO ToPhotoDTO(Photo photo, bool liked, UserDTO owner, ICollection<LikeDTO> likes, ICollection<CommentDTO> comments)
        {
            return new PhotoDTO()
            {
                Id = photo.Id,
                Path = photo.Path,
                Filter = photo.Filter.Name,
                Description = photo.Description,
                Liked = liked,
                Date = photo.Date,
                Owner = owner,
                Likes = likes,
                Comments = comments
            };
        }
    }
}
