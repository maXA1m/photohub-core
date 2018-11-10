using System.Collections.Generic;
using PhotoHub.BLL.DTO;
using PhotoHub.WEB.ViewModels;

namespace PhotoHub.WEB.Extensions
{
    /// <summary>
    /// Methods for mapping user DTOs to user view models.
    /// </summary>
    public static class UsersExtensions
    {
        /// <summary>
        /// Maps user DTO to user view model.
        /// </summary>
        public static UserViewModel ToViewModel(this UserDTO item)
        {
            if (item == null)
            {
                return null;
            }

            return new UserViewModel
            {
                RealName = item.RealName,
                UserName = item.UserName,
                Avatar = item.Avatar != null ? $"/data/avatars/{item.UserName}/{item.Avatar}" : string.IsNullOrEmpty(item.Gender) || item.Gender == "Male" ? "/images/defaults/def-male-logo.png" : "/images/defaults/def-female-logo.png",
                Date = item.Date.ToString("MMMM dd, yyyy"),
                Confirmed = item.Confirmed,
                Followed = item.Followed,
                Blocked = item.Blocked,
                PrivateAccount = item.PrivateAccount,
                IBlocked = item.IBlocked
            };
        }

        /// <summary>
        /// Maps user DTOs to user view models.
        /// </summary>
        public static List<UserViewModel> ToViewModels(this IEnumerable<UserDTO> items)
        {
            if (items == null)
            {
                return null;
            }

            var users = new List<UserViewModel>();

            foreach (var item in items)
            {
                users.Add(new UserViewModel
                {
                    RealName = item.RealName,
                    UserName = item.UserName,
                    Avatar = item.Avatar != null ? $"/data/avatars/{item.UserName}/{item.Avatar}" : string.IsNullOrEmpty(item.Gender) || item.Gender == "Male" ? "/images/defaults/def-male-logo.png" : "/images/defaults/def-female-logo.png",
                    Date = item.Date.ToString("MMMM dd, yyyy"),
                    Confirmed = item.Confirmed,
                    Followed = item.Followed,
                    Blocked = item.Blocked,
                    PrivateAccount = item.PrivateAccount,
                    IBlocked = item.IBlocked
                });
            }

            return users;
        }
    }
}