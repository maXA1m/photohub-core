using System.Collections.Generic;
using PhotoHub.BLL.DTO;
using PhotoHub.BLL.Interfaces;
using PhotoHub.DAL.Entities;

namespace PhotoHub.BLL.Mappers
{
    public class CommentsMapper : IMapper<CommentDTO, Comment>
    {
        public CommentDTO Map(Comment item)
        {
            return new CommentDTO()
            {
                Id = item.Id,
                Text = item.Text,
                Owner = null,
                Date = item.Date
            };
        }
        public CommentDTO Map(Comment comment, UserDTO owner)
        {
            return new CommentDTO()
            {
                Id = comment.Id,
                Text = comment.Text,
                Owner = owner,
                Date = comment.Date
            };
        }

        public List<CommentDTO> MapRange(IEnumerable<Comment> items)
        {
            throw new System.NotImplementedException();
        }
    }
}
