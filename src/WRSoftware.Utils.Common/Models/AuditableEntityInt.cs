using System;

namespace WRSoftware.Utils.Common.Models
{
    /// <summary>
    /// An class that should be inherited
    /// if the class would be Auditable and the 
    /// User PK is a LONG
    /// </summary>
    public abstract class AuditableEntityInt
    {
        /// <summary>
        /// Gets or sets the created by.
        /// </summary>
        /// <value>
        /// The created by.
        /// </value>
        public long CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the created at.
        /// </summary>
        /// <value>
        /// The created at.
        /// </value>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the last modified by.
        /// </summary>
        /// <value>
        /// The last modified by.
        /// </value>
        public long LastModifiedBy { get; set; }

        /// <summary>
        /// Gets or sets the last modified at.
        /// </summary>
        /// <value>
        /// The last modified at.
        /// </value>
        public DateTime? LastModifiedAt { get; set; }
    }
}