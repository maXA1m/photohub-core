using System;

namespace PhotoHub.BLL.DTO
{
    public class CommentDTO
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public UserDTO Owner { get; set; }
    }
}