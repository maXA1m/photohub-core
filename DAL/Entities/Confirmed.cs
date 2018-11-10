namespace PhotoHub.DAL.Entities
{
    /// <summary>
    /// Confirmed entity.
    /// Contains <see cref="UserId"/> and <see cref="AdminId"/>.
    /// </summary>
    public class Confirmed : BaseEntity
    {
        #region Properties

        /// <summary>
        /// Gets and sets foreign key to user by id.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets and sets user by <see cref="UserId"/> foreign key.
        /// </summary>
        public virtual User User { get; set; }

        /// <summary>
        /// Gets and sets foreign key to admin by id.
        /// </summary>
        public int AdminId { get; set; }

        /// <summary>
        /// Gets and sets admin by <see cref="AdminId"/> foreign key.
        /// </summary>
        public virtual User Admin { get; set; }

        #endregion
    }
}