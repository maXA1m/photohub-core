using System.Collections.Generic;
using PhotoHub.BLL.DTO;
using PhotoHub.WEB.ViewModels;

namespace PhotoHub.WEB.Extensions
{
    /// <summary>
    /// Methods for mapping user details DTOs to user details view models.
    /// </summary>
    public static class UsersDetailsExtensions
    {
        /// <summary>
        /// Maps user details DTO to user details view model.
        /// </summary>
        public static UserDetailsViewModel ToViewModel(this UserDetailsDTO item)
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
                Followings = item.Followings.ToViewModels(),
                Followers = item.Followers.ToViewModels(),
                Mutuals = item.Mutuals?.ToViewModels()
            };
        }

        /// <summary>
        /// Maps user details DTOs to user details view models.
        /// </summary>
        public static List<UserDetailsViewModel> ToViewModels(this IEnumerable<UserDetailsDTO> items)
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
                    Followings = item.Followings.ToViewModels(),
                    Followers = item.Followers.ToViewModels(),
                    Mutuals = item.Mutuals?.ToViewModels()
                });
            }

            return users;
        }
    }
}
