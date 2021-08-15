using System;

namespace WRSoftware.Utils.Common.Models
{
    /// <summary>
    /// An class that should be inherited
    /// if the class would be Auditable and the 
    /// preference is to use the userName
    /// </summary>
    public abstract class AuditableEntityString
    {
        /// <summary>
        /// Gets or sets the created by.
        /// </summary>
        /// <value>
        /// The created by.
        /// </value>
        public string CreatedBy { get; set; }

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
        public string LastModifiedBy { get; set; }

        /// <summary>
        /// Gets or sets the last modified.
        /// </summary>
        /// <value>
        /// The last modified.
        /// </value>
        public DateTime? LastModifiedAt { get; set; }
    }
}