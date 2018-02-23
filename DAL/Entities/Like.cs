using System;

namespace PhotoHub.DAL.Entities
{
    public class Like : BaseEntity
    {
        public DateTime Date { get; set; }

        public int PhotoId { get; set; }
        public Photo Photo { get; set; }

        public string OwnerId { get; set; }
        public ApplicationUser Owner { get; set; }

        public Like()
        {
            Date = DateTime.Now;
        }
    }
}