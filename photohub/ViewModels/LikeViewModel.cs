using Newtonsoft.Json;

namespace PhotoHub.WEB.ViewModels
{
    public class LikeViewModel
    {
        [JsonProperty("$id")]
        public int Id { get; set; }
        [JsonProperty("date")]
        public string Date { get; set; }
        [JsonProperty("owner")]
        public UserViewModel Owner { get; set; }
    }
}