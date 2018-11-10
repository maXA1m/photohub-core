using System;

namespace PhotoHub.DAL.Entities
{
    /// <summary>
    /// Photo report entity.
    /// Contains report <see cref="Text"/>, <see cref="Date"/>, <see cref="UserId"/> and <see cref="PhotoId"/>.
    /// </summary>
    public class PhotoReport : BaseEntity
    {
        #region Properties

        /// <summary>
        /// Gets and sets foreign key to user by id.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets and sets user entity by foreign key.
        /// </summary>
        public virtual User User { get; set; }

        /// <summary>
        /// Gets and sets foreign key to photo by id.
        /// </summary>
        public int PhotoId { get; set; }

        /// <summary>
        /// Gets and sets photo entity by foreign key.
        /// </summary>
        public virtual Photo Photo { get; set; }

        /// <summary>
        /// Gets and sets report text.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Gets and sets report date.
        /// </summary>
        public DateTime Date { get; set; }

        #endregion

        #region .ctors

        /// <summary>
        /// Initializes a new instance of the <see cref="PhotoReport"/>, sets date property to current date.
        /// </summary>
        public PhotoReport()
        {
            Date = DateTime.Now;
        }

        #endregion
    }
}
