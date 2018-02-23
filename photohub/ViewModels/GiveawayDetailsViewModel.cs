using System.Collections.Generic;
using Newtonsoft.Json;

namespace PhotoHub.WEB.ViewModels
{
    public class GiveawayDetailsViewModel : GiveawayViewModel
    {
        [JsonProperty("winners")]
        public List<UserViewModel> Winners { get; set; }
        [JsonProperty("participants")]
        public List<UserViewModel> Participants { get; set; }
        [JsonProperty("owners")]
        public List<UserViewModel> Owners { get; set; }
    }
}