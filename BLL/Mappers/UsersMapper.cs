#region using System
using System;
using System.Collections.Generic;
#endregion
using PhotoHub.DAL.Entities;
#region using PhotoHub.BLL
using PhotoHub.BLL.DTO;
using PhotoHub.BLL.Interfaces;
#endregion

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
                PrivateAccount = item.PrivateAccount,
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
                PrivateAccount = item.PrivateAccount,
                IBlocked = iBlocked
            };
        }

        public List<UserDTO> MapRange(IEnumerable<ApplicationUser> items)
        {
            throw new NotImplementedException();
        }
    }
}
