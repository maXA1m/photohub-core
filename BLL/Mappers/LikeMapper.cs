using PhotoHub.BLL.DTO;
using PhotoHub.DAL.Entities;

namespace PhotoHub.BLL.Mappers
{
    public class LikeMapper
    {
        public static LikeDTO ToLikeDTO(Like like, UserDTO owner)
        {
            return new LikeDTO()
            {
                Id = like.Id,
                Owner = owner,
                Date = like.Date
            };
        }
    }
}
