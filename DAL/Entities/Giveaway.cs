using PhotoHub.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PhotoHub.DAL.Entities
{
    public class Giveaway : BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Avatar { get; set; }
        public string About { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        
        public ICollection<Photo> Photos { get; set; }
        public ICollection<Winner> Winners { get; set; }
        public ICollection<Participant> Participants { get; set; }
        public ICollection<GiveawayOwner> Owners { get; set; }

        public Giveaway()
        {
            DateStart = DateTime.Now;
            DateEnd = DateTime.Now.AddDays(7);
        }
    }
}