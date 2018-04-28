namespace PhotoHub.DAL.Entities
{
    public class BlackList : BaseEntity
    { 
        public int UserId { get; set; }
        public User User { get; set; }
        
        public int BlockedUserId { get; set; }
        public User BlockedUser { get; set; }
    }
}