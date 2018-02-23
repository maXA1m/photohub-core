namespace PhotoHub.DAL.Entities
{
    public class Winner : BaseEntity
    {
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public int GiveawayId { get; set; }
        public Giveaway Giveaway { get; set; }
    }
}