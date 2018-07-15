using System;

namespace PhotoHub.DAL.Entities
{
    public class PhotoReport : BaseEntity
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public int PhotoId { get; set; }
        public Photo Photo { get; set; }

        public string Text { get; set; }

        public DateTime Date { get; set; }

        public PhotoReport()
        {
            Date = DateTime.Now;
        }
    }
}
