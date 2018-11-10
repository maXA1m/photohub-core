using System.Collections.Generic;

namespace PhotoHub.DAL.Entities
{
    /// <summary>
    /// Filter entity.
    /// Contains <see cref="Name"/> of filter.
    /// </summary>
    public class Filter : BaseEntity
    {
        #region Properties

        /// <summary>
        /// Gets and sets filter name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets and sets collection with photos.
        /// </summary>
        public virtual ICollection<Photo> Photos { get; set; }

        #endregion
    }
}