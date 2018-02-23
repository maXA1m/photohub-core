using PhotoHub.BLL.DTO;
using PhotoHub.WEB.ViewModels;
using System.Collections.Generic;

namespace PhotoHub.WEB.Mappers
{
    public static class UserDTOMapper
    {
        public static UserViewModel ToUserViewModel(UserDTO user)
        {
            return new UserViewModel()
            {
                UserName = user.UserName,
                Email = user.Email,
                Avatar = user.Avatar,
                Date = user.Date.ToString(),
                Confirmed = user.Confirmed,
                Followed = user.Followed,
                Blocked = user.Blocked
            };
        }
        public static List<UserViewModel> ToUserViewModels(IEnumerable<UserDTO> users)
        {
            List<UserViewModel> userViewModels = new List<UserViewModel>();

            foreach (UserDTO user in users)
            {
                userViewModels.Add(new UserViewModel()
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    Avatar = user.Avatar,
                    Date = user.Date.ToString(),
                    Confirmed = user.Confirmed,
                    Followed = user.Followed,
                    Blocked = user.Blocked
                });
            }

            return userViewModels;
        }

        public static UserDetailsViewModel ToUserDetailsViewModel(UserDetailsDTO user)
        {
            return new UserDetailsViewModel()
            {
                UserName = user.UserName,
                Email = user.Email,
                Avatar = user.Avatar,
                About = user.About,
                Date = user.Date.ToString(),
                Confirmed = user.Confirmed,
                Followed = user.Followed,
                Blocked = user.Blocked,
                Followings = ToUserViewModels(user.Followings),
                Followers = ToUserViewModels(user.Followers),
            };
        }
        public static List<UserDetailsViewModel> ToUserDetailsViewModels(IEnumerable<UserDetailsDTO> users)
        {
            List<UserDetailsViewModel> userDetailsViewModels = new List<UserDetailsViewModel>();

            foreach (UserDetailsDTO user in users)
            {
                userDetailsViewModels.Add(new UserDetailsViewModel()
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    Avatar = user.Avatar,
                    About = user.About,
                    Date = user.Date.ToString(),
                    Confirmed = user.Confirmed,
                    Followed = user.Followed,
                    Blocked = user.Blocked,
                    Followings = ToUserViewModels(user.Followings),
                    Followers = ToUserViewModels(user.Followers),
                });
            }

            return userDetailsViewModels;
        }
    }
}