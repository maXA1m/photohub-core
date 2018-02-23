using System.Collections.Generic;

namespace PhotoHub.BLL.DTO
{
    public class GiveawayDetailsDTO : GiveawayDTO
    {
        public ICollection<UserDTO> Winners { get; set; }
        public ICollection<UserDTO> Participants { get; set; }
        public ICollection<UserDTO> Owners { get; set; }
    }
}