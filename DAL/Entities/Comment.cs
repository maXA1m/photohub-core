using System;

namespace PhotoHub.DAL.Entities
{
    public class Comment : BaseEntity
    {
        public string Text { get; set; }
        public DateTime Date { get; set; }

        public int PhotoId { get; set; }
        public Photo Photo { get; set; }

        public string OwnerId { get; set; }
        public ApplicationUser Owner { get; set; }

        public Comment()
        {
            Date = DateTime.Now;
        }
    }
}