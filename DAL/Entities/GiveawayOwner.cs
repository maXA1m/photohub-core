namespace PhotoHub.DAL.Entities
{
    public class GiveawayOwner : BaseEntity
    {
        public string OwnerId { get; set; }
        public ApplicationUser Owner { get; set; }

        public int GiveawayId { get; set; }
        public Giveaway Giveaway { get; set; }
    }
}