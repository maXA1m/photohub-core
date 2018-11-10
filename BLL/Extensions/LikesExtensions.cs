using PhotoHub.DAL.Entities;
using PhotoHub.BLL.DTO;

namespace PhotoHub.BLL.Extensions
{
    /// <summary>
    /// Methods for mapping like entities to like data transfer objects.
    /// </summary>
    public static class LikesExtensions
    {
        /// <summary>
        /// Maps like entity to like DTO without owner.
        /// </summary>
        public static LikeDTO ToDTO(this Like item)
        {
            if (item == null)
            {
                return null;
            }

            return new LikeDTO
            {
                Id = item.Id,
                Owner = null,
                Date = item.Date
            };
        }

        /// <summary>
        /// Maps like entity to like DTO with owner.
        /// </summary>
        public static LikeDTO ToDTO(this Like item, UserDTO owner)
        {
            if (item == null)
            {
                return null;
            }

            return new LikeDTO
            {
                Id = item.Id,
                Owner = owner,
                Date = item.Date
            };
        }
    }
}
