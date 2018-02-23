namespace PhotoHub.DAL.Entities
{
    public class BlackList : BaseEntity
    { 
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        
        public string BlockedUserId { get; set; }
        public ApplicationUser BlockedUser { get; set; }
    }
}