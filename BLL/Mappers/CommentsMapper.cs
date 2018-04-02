using System.Collections.Generic;
using PhotoHub.DAL.Entities;
#region using PhotoHub.BLL
using PhotoHub.BLL.DTO;
using PhotoHub.BLL.Interfaces;
#endregion

namespace PhotoHub.BLL.Mappers
{
    public class CommentsMapper : IMapper<CommentDTO, Comment>
    {
        public CommentDTO Map(Comment item)
        {
            if (item == null)
                return null;

            return new CommentDTO()
            {
                Id = item.Id,
                Text = item.Text,
                Owner = null,
                Date = item.Date
            };
        }
        public CommentDTO Map(Comment item, UserDTO owner)
        {
            if (item == null)
                return null;

            return new CommentDTO()
            {
                Id = item.Id,
                Text = item.Text,
                Owner = owner,
                Date = item.Date
            };
        }

        public List<CommentDTO> MapRange(IEnumerable<Comment> items)
        {
            if (items == null)
                return null;

            return null;
        }
    }
}
