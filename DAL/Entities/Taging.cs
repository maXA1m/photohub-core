namespace PhotoHub.DAL.Entities
{
    public class Taging : BaseEntity
    {
        public Photo Photo { get; set; }
        public int PhotoId { get; set; }

        public Tag Tag { get; set; }
        public int TagId { get; set; }
    }
}
