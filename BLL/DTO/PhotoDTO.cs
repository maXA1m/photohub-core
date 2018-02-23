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
        public bool Liked { get; set; }
        public DateTime Date { get; set; }
        public UserDTO Owner { get; set; }
        
        public ICollection<LikeDTO> Likes { get; set; }
        public ICollection<CommentDTO> Comments { get; set; }
    }
}