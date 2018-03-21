using PhotoHub.BLL.DTO;
using PhotoHub.BLL.Interfaces;
using PhotoHub.WEB.ViewModels;
using System.Collections.Generic;

namespace PhotoHub.WEB.Mappers
{
    public class CommentsMapper : IMapper<CommentViewModel, CommentDTO>
    {
        private readonly UsersMapper _usersMapper;

        public CommentsMapper() => _usersMapper = new UsersMapper();

        public CommentViewModel Map(CommentDTO item)
        {
            return new CommentViewModel()
            {
                Id = item.Id,
                Text = item.Text,
                Date = item.Date.ToString("MMMM dd, yyyy"),
                Owner = _usersMapper.Map(item.Owner)
            };
        }

        public List<CommentViewModel> MapRange(IEnumerable<CommentDTO> items)
        {
            List<CommentViewModel> comments = new List<CommentViewModel>();

            foreach (CommentDTO item in items)
            {
                comments.Add(new CommentViewModel()
                {
                    Id = item.Id,
                    Text = item.Text,
                    Date = item.Date.ToString("MMMM dd, yyyy"),
                    Owner = _usersMapper.Map(item.Owner)
                });
            }

            return comments;
        }
    }
}