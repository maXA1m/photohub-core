namespace PhotoHub.DAL.Entities
{
    public class Following : BaseEntity
    {
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        
        public string FollowedUserId { get; set; }
        public ApplicationUser FollowedUser { get; set; }
    }
}