using System;

namespace PhotoHub.DAL.Entities
{
    public class Bookmark : BaseEntity
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public int PhotoId { get; set; }
        public Photo Photo { get; set; }

        public DateTime Date { get; set; }

        public Bookmark()
        {
            Date = DateTime.Now;
        }
    }
}
