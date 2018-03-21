namespace PhotoHub.DAL.Entities
{
    public class Bookmark : BaseEntity
    {
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public int PhotoId { get; set; }
        public Photo Photo { get; set; }
    }
}
