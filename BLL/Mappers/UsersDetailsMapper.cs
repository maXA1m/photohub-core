using System.Collections.Generic;
using PhotoHub.DAL.Entities;
using PhotoHub.BLL.DTO;

namespace PhotoHub.BLL.Mappers
{
    /// <summary>
    /// Methods for mapping user entities to user data transfer objects.
    /// </summary>
    public static class UsersDetailsMapper
    {
        #region Logic

        /// <summary>
        /// Maps user entity to user details DTO.
        /// </summary>
        public static UserDetailsDTO Map(User item)
        {
            if (item == null)
            {
                return null;
            }

            return new UserDetailsDTO
            {
                RealName = item.RealName,
                UserName = item.UserName,
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

        /// <summary>
        /// Maps user entity to user details DTO.
        /// </summary>
        public static UserDetailsDTO Map(User item, bool confirmed, bool followed, bool blocked, bool iBlocked, ICollection<UserDTO> followings, ICollection<UserDTO> followers, ICollection<UserDTO> mutuals)
        {
            if (item == null)
            {
                return null;
            }

            return new UserDetailsDTO
            {
                RealName = item.RealName,
                UserName = item.UserName,
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

        #endregion
    }
}
