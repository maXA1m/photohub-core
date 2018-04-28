namespace PhotoHub.DAL.Entities
{
    public class Following : BaseEntity
    {
        public int UserId { get; set; }
        public User User { get; set; }
        
        public int FollowedUserId { get; set; }
        public User FollowedUser { get; set; }
    }
}