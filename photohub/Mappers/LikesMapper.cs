using System.Collections.Generic;
using PhotoHub.BLL.DTO;
using PhotoHub.WEB.ViewModels;

namespace PhotoHub.WEB.Mappers
{
    /// <summary>
    /// Methods for mapping like DTOs to like view models.
    /// </summary>
    public static class LikesMapper
    {
        #region Logic

        /// <summary>
        /// Maps like DTO to like view model.
        /// </summary>
        public static LikeViewModel Map(LikeDTO item)
        {
            if (item == null)
            {
                return null;
            }

            return new LikeViewModel
            {
                Id = item.Id,
                Date = item.Date.ToString("MMMM dd, yyyy"),
                Owner = UsersMapper.Map(item.Owner)
            };
        }

        /// <summary>
        /// Maps like DTOs to like view models.
        /// </summary>
        public static List<LikeViewModel> MapRange(IEnumerable<LikeDTO> items)
        {
            if (items == null)
            {
                return null;
            }

            var likes = new List<LikeViewModel>();

            foreach (var item in items)
            {
                likes.Add(new LikeViewModel
                {
                    Id = item.Id,
                    Date = item.Date.ToString("MMMM dd, yyyy"),
                    Owner = UsersMapper.Map(item.Owner)
                });
            }

            return likes;
        }

        #endregion
    }
}