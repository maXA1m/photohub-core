namespace PhotoHub.DAL.Entities
{
    /// <summary>
    /// Base class for entities.
    /// Contains <see cref="Id"/> property.
    /// </summary>
    public class BaseEntity
    {
        #region Properties

        /// <summary>
        /// Gets and sets id.
        /// </summary>
        public int Id { get; set; }

        #endregion
    }
}
