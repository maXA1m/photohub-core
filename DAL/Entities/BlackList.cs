namespace PhotoHub.DAL.Entities
{
    /// <summary>
    /// Black list entity.
    /// Contains <see cref="UserId"/> and <see cref="BlockedUserId"/>.
    /// </summary>
    public class BlackList : BaseEntity
    {
        /// <summary>
        /// Gets and sets foreign key to user by id.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets and sets user entity by foreign key.
        /// </summary>
        public virtual User User { get; set; }

        /// <summary>
        /// Gets and sets foreign key to blocked user by id.
        /// </summary>
        public int BlockedUserId { get; set; }

        /// <summary>
        /// Gets and sets user entity by BlockedUserId foreign key.
        /// </summary>
        public virtual User BlockedUser { get; set; }
    }
}