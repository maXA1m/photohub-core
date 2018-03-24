using System;
using System.Collections.Generic;

namespace PhotoHub.BLL.DTO
{
    public class PhotoDTO
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public string Filter { get; set; }
        public string Description { get; set; }
        public int CountViews { get; set; }
        public DateTime Date { get; set; }
        public UserDTO Owner { get; set; }

        public bool Liked { get; set; }
        public bool Bookmarked { get; set; }

        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public string Iso { get; set; }
        public string Exposure { get; set; }
        public string Aperture { get; set; }
        public string FocalLength { get; set; }

        public IEnumerable<LikeDTO> Likes { get; set; }
        public IEnumerable<CommentDTO> Comments { get; set; }
    }
}