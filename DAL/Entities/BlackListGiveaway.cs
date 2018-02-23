namespace PhotoHub.DAL.Entities
{
    public class BlackListGiveaway : BaseEntity
    {
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public int BlockedGiveawayId { get; set; }
        public Giveaway BlockedGiveaway { get; set; }
    }
}