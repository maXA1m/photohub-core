using PhotoHub.BLL.DTO;
using PhotoHub.BLL.Interfaces;
using PhotoHub.DAL.Entities;
using System;
using System.Collections.Generic;

namespace PhotoHub.BLL.Mappers
{
    public class UsersMapper : IMapper<UserDTO, ApplicationUser>
    {

        public UserDTO Map(ApplicationUser item)
        {
            return new UserDTO()
            {
                RealName = item.RealName,
                UserName = item.UserName,
                Email = item.Email,
                Avatar = item.Avatar,
                Date = item.Date,

                Confirmed = false,
                Followed = false,
                Blocked = false,
                IBlocked = false
            };
        }
        public UserDTO Map(ApplicationUser item, bool confirmed, bool followed, bool blocked, bool iBlocked)
        {
            return new UserDTO()
            {
                RealName = item.RealName,
                UserName = item.UserName,
                Email = item.Email,
                Avatar = item.Avatar,
                Date = item.Date,

                Confirmed = confirmed,
                Followed = followed,
                Blocked = blocked,
                IBlocked = iBlocked
            };
        }

        public List<UserDTO> MapRange(IEnumerable<ApplicationUser> items)
        {
            throw new NotImplementedException();
        }
    }
}
