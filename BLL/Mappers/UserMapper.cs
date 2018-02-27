using PhotoHub.BLL.DTO;
using PhotoHub.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhotoHub.BLL.Mappers
{
    public static class UserMapper
    {
        public static UserDTO ToUserDTO(ApplicationUser user, bool confirmed, bool followed, bool blocked, bool iBlocked)
        {
            return new UserDTO()
            {
                UserName = user.UserName,
                Email = user.Email,
                Avatar = user.Avatar,
                Date = user.Date,
                Confirmed = confirmed,
                Followed = followed,
                Blocked = blocked,
                IBlocked = iBlocked
            };
    }

        public static UserDetailsDTO ToUserDetailsDTO(ApplicationUser user, bool confirmed, bool followed, bool blocked, bool iBlocked, ICollection<UserDTO> followings, ICollection<UserDTO> followers)
        {
            return new UserDetailsDTO()
            {
                UserName = user.UserName,
                Email = user.Email,
                Avatar = user.Avatar,
                About = user.About,
                Date = user.Date,
                Confirmed = confirmed,
                Followed = followed,
                Blocked = blocked,
                IBlocked = iBlocked,
                Followings = followings,
                Followers = followers,
            };
        }
    }
}
