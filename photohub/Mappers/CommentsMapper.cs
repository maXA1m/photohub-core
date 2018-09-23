using System.Collections.Generic;
using PhotoHub.BLL.DTO;
using PhotoHub.WEB.ViewModels;

namespace PhotoHub.WEB.Mappers
{
    /// <summary>
    /// Methods for mapping comment DTOs to comment view models.
    /// </summary>
    public static class CommentsMapper
    {
        #region Logic

        /// <summary>
        /// Maps comment DTO to comment view model.
        /// </summary>
        public static CommentViewModel Map(CommentDTO item)
        {
            if (item == null)
            {
                return null;
            }

            return new CommentViewModel
            {
                Id = item.Id,
                Text = item.Text,
                Date = item.Date.ToString("MMMM dd, yyyy"),
                Owner = UsersMapper.Map(item.Owner)
            };
        }

        /// <summary>
        /// Maps comment DTOs to comment view models.
        /// </summary>
        public static List<CommentViewModel> MapRange(IEnumerable<CommentDTO> items)
        {
            if (items == null)
            {
                return null;
            }

            var comments = new List<CommentViewModel>();

            foreach (var item in items)
            {
                comments.Add(new CommentViewModel
                {
                    Id = item.Id,
                    Text = item.Text,
                    Date = item.Date.ToString("MMMM dd, yyyy"),
                    Owner = UsersMapper.Map(item.Owner)
                });
            }

            return comments;
        }

        #endregion
    }
}