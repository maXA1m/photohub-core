namespace PhotoHub.DAL.Entities
{
    /// <summary>
    /// Tag entity.
    /// Contains <see cref="Name"/> of <see cref="Tag"/>.
    /// </summary>
    public class Tag : BaseEntity
    {
        #region Properties

        /// <summary>
        /// Gets and sets tag name.
        /// </summary>
        public string Name { get; set; }

        #endregion
    }
}
