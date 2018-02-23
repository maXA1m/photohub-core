using Newtonsoft.Json;

namespace PhotoHub.WEB.ViewModels
{
    public class GiveawayViewModel
    {
        [JsonProperty("$id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("avatar")]
        public string Avatar { get; set; }
        [JsonProperty("about")]
        public string About { get; set; }
        [JsonProperty("start")]
        public string DateStart { get; set; }
        [JsonProperty("end")]
        public string DateEnd { get; set; }
    }
}