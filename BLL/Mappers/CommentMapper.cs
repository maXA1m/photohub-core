using PhotoHub.BLL.DTO;
using PhotoHub.DAL.Entities;

namespace PhotoHub.BLL.Mappers
{
    public class CommentMapper
    {
        public static CommentDTO ToCommentDTO(Comment comment, UserDTO owner)
        {
            return new CommentDTO()
            {
                Id = comment.Id,
                Text = comment.Text,
                Owner = owner,
                Date = comment.Date
            };
        }
    }
}
