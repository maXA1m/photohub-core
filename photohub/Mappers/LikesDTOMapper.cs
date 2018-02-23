using PhotoHub.BLL.DTO;
using PhotoHub.WEB.ViewModels;
using System.Collections.Generic;

namespace PhotoHub.WEB.Mappers
{
    public static class LikesDTOMapper
    {
        public static LikeViewModel ToLikeViewModel(LikeDTO like)
        {
            return new LikeViewModel()
            {
                Id = like.Id,
                Date = like.Date.ToString(),
                Owner = UserDTOMapper.ToUserViewModel(like.Owner)
            };
        }

        public static List<LikeViewModel> ToLikeViewModels(ICollection<LikeDTO> likes)
        {
            List<LikeViewModel> likeViewModels = new List<LikeViewModel>(likes.Count);

            foreach (LikeDTO like in likes)
            {
                likeViewModels.Add(new LikeViewModel()
                {
                    Id = like.Id,
                    Date = like.Date.ToString(),
                    Owner = UserDTOMapper.ToUserViewModel(like.Owner)
                });
            }

            return likeViewModels;
        }
    }
}