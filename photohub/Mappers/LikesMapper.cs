using PhotoHub.BLL.DTO;
using PhotoHub.BLL.Interfaces;
using PhotoHub.WEB.ViewModels;
using System.Collections.Generic;

namespace PhotoHub.WEB.Mappers
{
    public class LikesMapper : IMapper<LikeViewModel, LikeDTO>
    {
        private readonly UsersMapper _usersMapper;

        public LikesMapper() => _usersMapper = new UsersMapper();

        public LikeViewModel Map(LikeDTO item)
        {
            return new LikeViewModel()
            {
                Id = item.Id,
                Date = item.Date.ToString("MMMM dd, yyyy"),
                Owner = _usersMapper.Map(item.Owner)
            };
        }

        public List<LikeViewModel> MapRange(IEnumerable<LikeDTO> items)
        {
            List<LikeViewModel> likes = new List<LikeViewModel>();

            foreach (LikeDTO item in items)
            {
                likes.Add(new LikeViewModel()
                {
                    Id = item.Id,
                    Date = item.Date.ToString("MMMM dd, yyyy"),
                    Owner = _usersMapper.Map(item.Owner)
                });
            }

            return likes;
        }
    }
}