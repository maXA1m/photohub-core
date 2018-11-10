using System;

namespace PhotoHub.DAL.Entities
{
    /// <summary>
    /// Like entity.
    /// Contains <see cref="PhotoId"/>, <see cref="OwnerId"/> and <see cref="Date"/>.
    /// </summary>
    public class Like : BaseEntity
    {
        #region Properties

        /// <summary>
        /// Gets and sets like date.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets and sets foreign key to photo by id.
        /// </summary>
        public int PhotoId { get; set; }

        /// <summary>
        /// Gets and sets photo entity by foreign key.
        /// </summary>
        public virtual Photo Photo { get; set; }

        /// <summary>
        /// Gets and sets foreign key to owner by id.
        /// </summary>
        public int OwnerId { get; set; }

        /// <summary>
        /// Gets and sets user entity by foreign key.
        /// </summary>
        public virtual User Owner { get; set; }

        #endregion

        #region .ctors

        /// <summary>
        /// Initializes a new instance of the <see cref="Like"/>, sets current <see cref="Date"/>.
        /// </summary>
        public Like()
        {
            Date = DateTime.Now;
        }

        #endregion
    }
}