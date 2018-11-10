using System.Collections.Generic;
using PhotoHub.BLL.DTO;
using PhotoHub.WEB.ViewModels;

namespace PhotoHub.WEB.Extensions
{
    /// <summary>
    /// Methods for mapping comment DTOs to comment view models.
    /// </summary>
    public static class PhotosExtensions
    {
        /// <summary>
        /// Maps photo DTO to photo view model.
        /// </summary>
        public static PhotoViewModel ToViewModel(this PhotoDTO item)
        {
            if (item == null)
            {
                return null;
            }

            return new PhotoViewModel
            {
                Id = item.Id,
                Path = item.Path,
                Filter = item.Filter,
                Description = item.Description,
                Date = item.Date.ToString("MMMM dd, yyyy"),
                Owner = item.Owner.ToViewModel(),
                CountViews = item.CountViews,

                Liked = item.Liked,
                Bookmarked = item.Bookmarked,

                Manufacturer = string.IsNullOrEmpty(item.Manufacturer) ? "Unknown" : item.Manufacturer,
                Model = string.IsNullOrEmpty(item.Model) ? "Unknown" : item.Model,
                Iso = item.Iso != null ? item.Iso.ToString() : "Unknown",
                Exposure = item.Exposure != null ? $"{string.Format("{0:0.00000}", item.Exposure)} sec" : "Unknown",
                Aperture = item.Aperture != null ? $"f/{item.Aperture.ToString()}" : "Unknown",
                FocalLength = item.FocalLength != null ? $"{item.FocalLength.ToString()}mm" : "Unknown",

                Likes = item.Likes.ToViewModels(),
                Comments = item.Comments.ToViewModels(),
                Tags = item.Tags.ToViewModels()
            };
        }

        /// <summary>
        /// Maps photo DTOs to photo view models.
        /// </summary>
        public static List<PhotoViewModel> ToViewModels(this IEnumerable<PhotoDTO> items)
        {
            if (items == null)
            {
                return null;
            }

            var photos = new List<PhotoViewModel>();

            foreach(var item in items)
            {
                photos.Add(new PhotoViewModel
                {
                    Id = item.Id,
                    Path = item.Path,
                    Filter = item.Filter,
                    Description = item.Description,
                    Date = item.Date.ToString("MMMM dd, yyyy"),
                    Owner = item.Owner.ToViewModel(),
                    CountViews = item.CountViews,

                    Liked = item.Liked,
                    Bookmarked = item.Bookmarked,

                    Manufacturer = string.IsNullOrEmpty(item.Manufacturer) ? "Unknown" : item.Manufacturer,
                    Model = string.IsNullOrEmpty(item.Model) ? "Unknown" : item.Model,
                    Iso = item.Iso != null ? item.Iso.ToString() : "Unknown",
                    Exposure = item.Exposure != null ? $"{string.Format("{0:0.00000}", item.Exposure)} sec" : "Unknown",
                    Aperture = item.Aperture != null ? $"f/{item.Aperture.ToString()}" : "Unknown",
                    FocalLength = item.FocalLength != null ? $"{item.FocalLength.ToString()}mm" : "Unknown",

                    Likes = item.Likes.ToViewModels(),
                    Comments = item.Comments.ToViewModels(),
                    Tags = item.Tags.ToViewModels()
                });
            }

            return photos;
        }
    }
}