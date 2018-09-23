using Newtonsoft.Json;

namespace PhotoHub.WEB.ViewModels
{
    public class FilterViewModel
    {
        #region Properties

        [JsonProperty("$id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        #endregion
    }
}
