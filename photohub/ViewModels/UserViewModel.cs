using Newtonsoft.Json;

namespace PhotoHub.WEB.ViewModels
{
    public class UserViewModel
    {
        #region Properties

        [JsonProperty("realName")]
        public string RealName { get; set; }

        [JsonProperty("userName")]
        public string UserName { get; set; }

        [JsonProperty("avatar")]
        public string Avatar { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("confirmed")]
        public bool Confirmed { get; set; }

        [JsonProperty("followed")]
        public bool Followed { get; set; }

        [JsonProperty("blocked")]
        public bool Blocked { get; set; }

        [JsonProperty("private")]
        public bool PrivateAccount { get; set; }

        [JsonProperty("iBlocked")]
        public bool IBlocked { get; set; }

        #endregion
    }
}