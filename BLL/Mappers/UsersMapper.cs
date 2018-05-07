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
    public class UsersMapper : IMapper<UserDTO, User>
    {

        public UserDTO Map(User item)
        {
            if (item == null)
                return null;

            return new UserDTO()
            {
                RealName = item.RealName,
                UserName = item.UserName,
                Avatar = item.Avatar,
                Date = item.Date,
                Gender = item.Gender,

                Confirmed = false,
                Followed = false,
                Blocked = false,
                PrivateAccount = item.PrivateAccount,
                IBlocked = false
            };
        }
        public UserDTO Map(User item, bool confirmed, bool followed, bool blocked, bool iBlocked)
        {
            if (item == null)
                return null;

            return new UserDTO()
            {
                RealName = item.RealName,
                UserName = item.UserName,
                Avatar = item.Avatar,
                Date = item.Date,
                Gender = item.Gender,

                Confirmed = confirmed,
                Followed = followed,
                Blocked = blocked,
                PrivateAccount = item.PrivateAccount,
                IBlocked = iBlocked
            };
        }

        public List<UserDTO> MapRange(IEnumerable<User> items)
        {
            if (items == null)
                return null;

            return null;
        }
    }
}
