using Newtonsoft.Json;

namespace PhotoHub.WEB.ViewModels
{
    public class ExposureViewModel
    {
        [JsonProperty("$id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}