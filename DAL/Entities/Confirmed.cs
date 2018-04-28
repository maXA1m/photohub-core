namespace PhotoHub.DAL.Entities
{
    public class Confirmed : BaseEntity
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public int AdminId { get; set; }
        public User Admin { get; set; }
    }
}