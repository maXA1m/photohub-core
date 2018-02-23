using PhotoHub.BLL.DTO;
using PhotoHub.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhotoHub.BLL.Mappers
{
    public static class GiveawayMapper
    {
        public static GiveawayDTO ToGiveawayDTO(Giveaway giveaway)
        {
            return new GiveawayDTO()
            {
                Id = giveaway.Id,
                Name = giveaway.Name,
                Email = giveaway.Email,
                Avatar = giveaway.Avatar,
                About = giveaway.About,
                DateStart = giveaway.DateStart,
                DateEnd = giveaway.DateEnd
            };
        }

        public static GiveawayDetailsDTO ToGiveawayDetailsDTO(Giveaway giveaway, ICollection<UserDTO> winners, ICollection<UserDTO> participants, ICollection<UserDTO> owners)
        {
            return new GiveawayDetailsDTO()
            {
                Id = giveaway.Id,
                Name = giveaway.Name,
                Email = giveaway.Email,
                Avatar = giveaway.Avatar,
                About = giveaway.About,
                DateStart = giveaway.DateStart,
                DateEnd = giveaway.DateEnd,
                Winners = winners,
                Participants = participants,
                Owners = owners
            };
        }
    }
}
