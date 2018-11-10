using System;

namespace PhotoHub.DAL.Entities
{
    /// <summary>
    /// User report entity.
    /// Contains report <see cref="Text"/>, <see cref="Date"/>, <see cref="UserId"/> and <see cref="ReportedUserId"/>.
    /// </summary>
    public class UserReport : BaseEntity
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
        /// Gets and sets foreign key to user by id.
        /// </summary>
        public int ReportedUserId { get; set; }

        /// <summary>
        /// Gets and sets user entity by foreign key.
        /// </summary>
        public virtual User ReportedUser { get; set; }

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
        /// Initializes a new instance of the <see cref="UserReport"/>, sets current date.
        /// </summary>
        public UserReport()
        {
            Date = DateTime.Now;
        }

        #endregion
    }
}
