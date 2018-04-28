using System;

namespace PhotoHub.DAL.Entities
{
    public class Comment : BaseEntity
    {
        public string Text { get; set; }
        public DateTime Date { get; set; }

        public int PhotoId { get; set; }
        public Photo Photo { get; set; }

        public int OwnerId { get; set; }
        public User Owner { get; set; }

        public Comment()
        {
            Date = DateTime.Now;
        }
    }
}