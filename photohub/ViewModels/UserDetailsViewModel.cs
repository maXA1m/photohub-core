using System.Collections.Generic;
using Newtonsoft.Json;

namespace PhotoHub.WEB.ViewModels
{
    public class UserDetailsViewModel : UserViewModel
    {
        #region Properties

        [JsonProperty("about")]
        public string About { get; set; }

        [JsonProperty("gender")]
        public string Gender { get; set; }

        [JsonProperty("webSite")]
        public string WebSite { get; set; }

        [JsonProperty("followings")]
        public List<UserViewModel> Followings { get; set; }

        [JsonProperty("followers")]
        public List<UserViewModel> Followers { get; set; }

        [JsonProperty("mutuals")]
        public List<UserViewModel> Mutuals { get; set; }

        #endregion
    }
}