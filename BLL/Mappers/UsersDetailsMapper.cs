using PhotoHub.BLL.DTO;
using PhotoHub.BLL.Interfaces;
using PhotoHub.DAL.Entities;
using System;
using System.Collections.Generic;

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
                Confirmed = false,
                Followed = false,
                Blocked = false,
                IBlocked = false,
                Followings = null,
                Followers = null,
            };
        }
        public UserDetailsDTO Map(ApplicationUser item, bool confirmed, bool followed, bool blocked, bool iBlocked, ICollection<UserDTO> followings, ICollection<UserDTO> followers)
        {
            return new UserDetailsDTO()
            {
                RealName = item.RealName,
                UserName = item.UserName,
                Email = item.Email,
                Avatar = item.Avatar,
                About = item.About,
                Date = item.Date,
                Confirmed = confirmed,
                Followed = followed,
                Blocked = blocked,
                IBlocked = iBlocked,
                Followings = followings,
                Followers = followers,
            };
        }

        public List<UserDetailsDTO> MapRange(IEnumerable<ApplicationUser> items)
        {
            throw new NotImplementedException();
        }
    }
}
