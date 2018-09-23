using System.Collections.Generic;
using PhotoHub.BLL.DTO;
using PhotoHub.WEB.ViewModels;

namespace PhotoHub.WEB.Mappers
{
    /// <summary>
    /// Methods for mapping user details DTOs to user details view models.
    /// </summary>
    public static class UsersDetailsMapper
    {
        #region Logic

        /// <summary>
        /// Maps user details DTO to user details view model.
        /// </summary>
        public static UserDetailsViewModel Map(UserDetailsDTO item)
        {
            if (item == null)
            {
                return null;
            }

            return new UserDetailsViewModel
            {
                RealName = item.RealName,
                UserName = item.UserName,
                Avatar = item.Avatar != null ? $"/data/avatars/{item.UserName}/{item.Avatar}" : string.IsNullOrEmpty(item.Gender) || item.Gender == "Male" ? "/images/defaults/def-male-logo.png" : "/images/defaults/def-female-logo.png",
                About = item.About != null ? item.About : string.Empty,
                Date = item.Date.ToString("MMMM dd, yyyy"),
                Confirmed = item.Confirmed,
                Followed = item.Followed,
                Gender = item.Gender,
                WebSite = item.WebSite,
                Blocked = item.Blocked,
                PrivateAccount = item.PrivateAccount,
                IBlocked = item.IBlocked,
                Followings = UsersMapper.MapRange(item.Followings),
                Followers = UsersMapper.MapRange(item.Followers),
                Mutuals = item.Mutuals != null ? UsersMapper.MapRange(item.Mutuals) : null
            };
        }

        /// <summary>
        /// Maps user details DTOs to user details view models.
        /// </summary>
        public static List<UserDetailsViewModel> MapRange(IEnumerable<UserDetailsDTO> items)
        {
            if (items == null)
            {
                return null;
            }

            var users = new List<UserDetailsViewModel>();

            foreach (var item in items)
            {
                users.Add(new UserDetailsViewModel
                {
                    RealName = item.RealName,
                    UserName = item.UserName,
                    Avatar = item.Avatar != null ? $"/data/avatars/{item.UserName}/{item.Avatar}" : string.IsNullOrEmpty(item.Gender) || item.Gender == "Male" ? "/images/defaults/def-male-logo.png" : "/images/defaults/def-female-logo.png",
                    About = item.About != null ? item.About : string.Empty,
                    Date = item.Date.ToString("MMMM dd, yyyy"),
                    Confirmed = item.Confirmed,
                    Followed = item.Followed,
                    Gender = item.Gender,
                    WebSite = item.WebSite,
                    Blocked = item.Blocked,
                    PrivateAccount = item.PrivateAccount,
                    IBlocked = item.IBlocked,
                    Followings = UsersMapper.MapRange(item.Followings),
                    Followers = UsersMapper.MapRange(item.Followers),
                    Mutuals = item.Mutuals != null ? UsersMapper.MapRange(item.Mutuals) : null
                });
            }

            return users;
        }

        #endregion
    }
}
