using System.Collections.Generic;
using PhotoHub.BLL.DTO;
using PhotoHub.WEB.ViewModels;

namespace PhotoHub.WEB.Extensions
{
    /// <summary>
    /// Methods for mapping comment DTOs to comment view models.
    /// </summary>
    public static class CommentsExtensions
    {
        /// <summary>
        /// Maps comment DTO to comment view model.
        /// </summary>
        public static CommentViewModel ToViewModel(this CommentDTO item)
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
                Owner = item.Owner.ToViewModel()
            };
        }

        /// <summary>
        /// Maps comment DTOs to comment view models.
        /// </summary>
        public static List<CommentViewModel> ToViewModels(this IEnumerable<CommentDTO> items)
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
                    Owner = item.Owner.ToViewModel()
                });
            }

            return comments;
        }
    }
}