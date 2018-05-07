using Newtonsoft.Json;

namespace PhotoHub.WEB.ViewModels.Manage
{
    public class ThemeColorViewModel
    {
        [JsonProperty("color")]
        public string Color { get; set; }

        [JsonProperty("class")]
        public string CssClass { get; set; }
    }
}