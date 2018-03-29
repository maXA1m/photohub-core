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
    public class UsersDetailsMapper : IMapper<UserDetailsDTO, ApplicationUser>
    {
        public UserDetailsDTO Map(ApplicationUser item)
        {
            return new UserDetailsDTO()
            {
                RealName = item.RealName,
                UserName = item.UserName,
                Email = item.Email,
                Avatar = item.Avatar,
                About = item.About,
                Date = item.Date,
                Gender = item.Gender,
                WebSite = item.WebSite,
                Confirmed = false,
                Followed = false,
                Blocked = false,
                PrivateAccount = item.PrivateAccount,
                IBlocked = false,
                Followings = null,
                Followers = null,
                Mutuals = null
            };
        }
        public UserDetailsDTO Map(ApplicationUser item, bool confirmed, bool followed, bool blocked, bool iBlocked, ICollection<UserDTO> followings, ICollection<UserDTO> followers, ICollection<UserDTO> mutuals)
        {
            return new UserDetailsDTO()
            {
                RealName = item.RealName,
                UserName = item.UserName,
                Email = item.Email,
                Avatar = item.Avatar,
                About = item.About,
                Date = item.Date,
                Gender = item.Gender,
                WebSite = item.WebSite,
                Confirmed = confirmed,
                Followed = followed,
                Blocked = blocked,
                PrivateAccount = item.PrivateAccount,
                IBlocked = iBlocked,
                Followings = followings,
                Followers = followers,
                Mutuals = mutuals
            };
        }

        public List<UserDetailsDTO> MapRange(IEnumerable<ApplicationUser> items)
        {
            throw new NotImplementedException();
        }
    }
}
