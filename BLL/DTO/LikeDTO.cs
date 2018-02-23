using System;

namespace PhotoHub.BLL.DTO
{
    public class LikeDTO
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public UserDTO Owner { get; set; }
    }
}