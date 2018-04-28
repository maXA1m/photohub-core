using System;

namespace PhotoHub.DAL.Entities
{
    public class Like : BaseEntity
    {
        public DateTime Date { get; set; }

        public int PhotoId { get; set; }
        public Photo Photo { get; set; }

        public int OwnerId { get; set; }
        public User Owner { get; set; }

        public Like()
        {
            Date = DateTime.Now;
        }
    }
}