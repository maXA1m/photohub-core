using PhotoHub.BLL.DTO;
using PhotoHub.WEB.ViewModels;
using System.Collections.Generic;

namespace PhotoHub.WEB.Mappers
{
    public static class CommentsDTOMapper
    {
        public static CommentViewModel ToCommentViewModel(CommentDTO comment)
        {
            return new CommentViewModel()
            {
                Id = comment.Id,
                Text = comment.Text,
                Date = comment.Date.ToString("MMMM dd, yyyy"),
                Owner = UserDTOMapper.ToUserViewModel(comment.Owner)
            };
        }

        public static List<CommentViewModel> ToCommentViewModels(ICollection<CommentDTO> comments)
        {
            List<CommentViewModel> commentViewModels = new List<CommentViewModel>(comments.Count);

            foreach (CommentDTO comment in comments)
            {
                commentViewModels.Add(new CommentViewModel()
                {
                    Id = comment.Id,
                    Text = comment.Text,
                    Date = comment.Date.ToString("MMMM dd, yyyy"),
                    Owner = UserDTOMapper.ToUserViewModel(comment.Owner)
                });
            }

            return commentViewModels;
        }
    }
}