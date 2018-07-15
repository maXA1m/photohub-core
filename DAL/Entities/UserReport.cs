using System;

namespace PhotoHub.DAL.Entities
{
    public class UserReport : BaseEntity
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public int ReportedUserId { get; set; }
        public User ReportedUser { get; set; }

        public string Text { get; set; }

        public DateTime Date { get; set; }

        public UserReport()
        {
            Date = DateTime.Now;
        }
    }
}
