using System;

namespace PhotoHub.DAL.Entities
{
    /// <summary>
    /// Comment entity.
    /// Contains <see cref="Text"/>, <see cref="Date"/>, <see cref="PhotoId"/> and <see cref="OwnerId"/>.
    /// </summary>
    public class Comment : BaseEntity
    {
        #region Properties

        /// <summary>
        /// Gets and sets comment text.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Gets and sets comment date.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets and sets foreign key to photo by id.
        /// </summary>
        public int PhotoId { get; set; }

        /// <summary>
        /// Gets and sets photo by <see cref="PhotoId"/> foreign key.
        /// </summary>
        public virtual Photo Photo { get; set; }

        /// <summary>
        /// Gets and sets foreign key to owner by id.
        /// </summary>
        public int OwnerId { get; set; }

        /// <summary>
        /// Gets and sets owner by <see cref="OwnerId"/> foreign key.
        /// </summary>
        public virtual User Owner { get; set; }

        #endregion

        #region .ctors

        /// <summary>
        /// Initializes a new instance of the <see cref="Bookmark"/>, sets current <see cref="Date"/>.
        /// </summary>
        public Comment()
        {
            Date = DateTime.Now;
        }

        #endregion
    }
}