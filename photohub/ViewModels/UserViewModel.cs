﻿using Newtonsoft.Json;
using System.Collections.Generic;

namespace PhotoHub.WEB.ViewModels
{
    public class UserViewModel
    {
        [JsonProperty("userName")]
        public string UserName { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
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
    }
}