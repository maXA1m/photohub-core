using PhotoHub.DAL.Entities;
using PhotoHub.BLL.DTO;

namespace PhotoHub.BLL.Mappers
{
    /// <summary>
    /// Methods for mapping comment entities to comment data transfer objects.
    /// </summary>
    public static class CommentsMapper
    {
        #region Logic

        /// <summary>
        /// Maps comment entity to comment DTO without owner.
        /// </summary>
        public static CommentDTO Map(Comment item)
        {
            if (item == null)
            {
                return null;
            }

            return new CommentDTO
            {
                Id = item.Id,
                Text = item.Text,
                Owner = null,
                Date = item.Date
            };
        }

        /// <summary>
        /// Maps comment entity to comment DTO with owner.
        /// </summary>
        public static CommentDTO Map(Comment item, UserDTO owner)
        {
            if (item == null)
            {
                return null;
            }

            return new CommentDTO
            {
                Id = item.Id,
                Text = item.Text,
                Owner = owner,
                Date = item.Date
            };
        }

        #endregion
    }
}
