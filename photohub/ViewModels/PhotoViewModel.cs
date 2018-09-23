using System.Collections.Generic;
using Newtonsoft.Json;

namespace PhotoHub.WEB.ViewModels
{
    public class PhotoViewModel
    {
        #region .ctors

        public PhotoViewModel()
        {
            CommentActive = false;
        }

        #endregion

        #region Properties

        [JsonProperty("$id")]
        public int Id { get; set; }

        [JsonProperty("path")]
        public string Path { get; set; }

        [JsonProperty("filter")]
        public string Filter { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("countViews")]
        public int CountViews { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("owner")]
        public UserViewModel Owner { get; set; }

        [JsonProperty("liked")]
        public bool Liked { get; set; }

        [JsonProperty("bookmarked")]
        public bool Bookmarked { get; set; }

        [JsonProperty("manufacturer")]
        public string Manufacturer { get; set; }

        [JsonProperty("model")]
        public string Model { get; set; }

        [JsonProperty("iso")]
        public string Iso { get; set; }

        [JsonProperty("exposure")]
        public string Exposure { get; set; }

        [JsonProperty("aperture")]
        public string Aperture { get; set; }

        [JsonProperty("focalLength")]
        public string FocalLength { get; set; }

        [JsonProperty("commentActive")]
        public bool CommentActive { get; set; }

        [JsonProperty("likes")]
        public List<LikeViewModel> Likes { get; set; }

        [JsonProperty("comments")]
        public List<CommentViewModel> Comments { get; set; }

        [JsonProperty("tags")]
        public List<TagViewModel> Tags { get; set; }

        #endregion
    }
}