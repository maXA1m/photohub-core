namespace PhotoHub.DAL.Entities
{
    public class Confirmed : BaseEntity
    {
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public string AdminId { get; set; }
        public ApplicationUser Admin { get; set; }
    }
}