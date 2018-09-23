using PhotoHub.DAL.Entities;
using PhotoHub.BLL.DTO;

namespace PhotoHub.BLL.Mappers
{
    /// <summary>
    /// Methods for mapping like entities to like data transfer objects.
    /// </summary>
    public static class LikesMapper
    {
        #region Logic

        /// <summary>
        /// Maps like entity to like DTO without owner.
        /// </summary>
        public static LikeDTO Map(Like item)
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
        public static LikeDTO Map(Like item, UserDTO owner)
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

        #endregion
    }
}
