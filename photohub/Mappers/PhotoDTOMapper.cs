using PhotoHub.BLL.DTO;
using PhotoHub.WEB.ViewModels;
using System.Collections.Generic;

namespace PhotoHub.WEB.Mappers
{
    public static class PhotoDTOMapper
    {
        public static PhotoViewModel ToPhotoViewModel(PhotoDTO photo)
        {
            return new PhotoViewModel()
            {
                Id = photo.Id,
                Path = photo.Path,
                Filter = photo.Filter,
                Description = photo.Description,
                Liked = photo.Liked,
                Date = photo.Date.ToString(),
                Owner = UserDTOMapper.ToUserViewModel(photo.Owner),

                Likes = LikesDTOMapper.ToLikeViewModels(photo.Likes),
                Comments = CommentsDTOMapper.ToCommentViewModels(photo.Comments)
            };
        }

        public static List<PhotoViewModel> ToPhotoViewModels(IEnumerable<PhotoDTO> photos)
        {
            List<PhotoViewModel> photoViewModels = new List<PhotoViewModel>();

            foreach(PhotoDTO photo in photos)
            {
                photoViewModels.Add(new PhotoViewModel()
                {
                    Id = photo.Id,
                    Path = photo.Path,
                    Filter = photo.Filter,
                    Description = photo.Description,
                    Liked = photo.Liked,
                    Date = photo.Date.ToString(),
                    Owner = UserDTOMapper.ToUserViewModel(photo.Owner),

                    Likes = LikesDTOMapper.ToLikeViewModels(photo.Likes),
                    Comments = CommentsDTOMapper.ToCommentViewModels(photo.Comments)
                });
            }

            return photoViewModels;
        }
    }
}