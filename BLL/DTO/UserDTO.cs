using System;

namespace PhotoHub.BLL.DTO
{
    public class UserDTO
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Avatar { get; set; }
        public DateTime Date { get; set; }
        public bool Confirmed { get; set; }
        public bool Followed { get; set; }
        public bool Blocked { get; set; }
        public bool IBlocked { get; set; }
    }
}
