using Newtonsoft.Json;
using System.Collections.Generic;

namespace PhotoHub.WEB.ViewModels
{
    public class UserDetailsViewModel : UserViewModel
    {
        [JsonProperty("about")]
        public string About { get; set; }

        [JsonProperty("followings")]
        public List<UserViewModel> Followings { get; set; }
        [JsonProperty("followers")]
        public List<UserViewModel> Followers { get; set; }
    }
}