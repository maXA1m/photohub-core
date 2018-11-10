using System;

namespace PhotoHub.DAL.Entities
{
    /// <summary>
    /// Bookmark entity.
    /// Contains <see cref="UserId"/>, <see cref="PhotoId"/> and <see cref="Date"/> of bookmark.
    /// </summary>
    public class Bookmark : BaseEntity
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
        /// Gets and sets foreign key to photo by id.
        /// </summary>
        public int PhotoId { get; set; }

        /// <summary>
        /// Gets and sets photo by <see cref="PhotoId"/> foreign key.
        /// </summary>
        public virtual Photo Photo { get; set; }

        /// <summary>
        /// Gets and sets date bookmark was made.
        /// </summary>
        public DateTime Date { get; set; }

        #endregion

        #region .ctors

        /// <summary>
        /// Initializes a new instance of the <see cref="Bookmark"/>, sets current <see cref="Date"/>.
        /// </summary>
        public Bookmark()
        {
            Date = DateTime.Now;
        }

        #endregion
    }
}
