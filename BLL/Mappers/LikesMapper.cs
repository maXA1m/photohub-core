﻿using System.Collections.Generic;
using PhotoHub.DAL.Entities;
#region using PhotoHub.BLL
using PhotoHub.BLL.DTO;
using PhotoHub.BLL.Interfaces;
#endregion

namespace PhotoHub.BLL.Mappers
{
    public class LikesMapper : IMapper<LikeDTO, Like>
    {
        public LikeDTO Map(Like item)
        {
            return new LikeDTO()
            {
                Id = item.Id,
                Owner = null,
                Date = item.Date
            };
        }
        public LikeDTO Map(Like item, UserDTO owner)
        {
            return new LikeDTO()
            {
                Id = item.Id,
                Owner = owner,
                Date = item.Date
            };
        }


        public List<LikeDTO> MapRange(IEnumerable<Like> items)
        {
            throw new System.NotImplementedException();
        }
    }
}
