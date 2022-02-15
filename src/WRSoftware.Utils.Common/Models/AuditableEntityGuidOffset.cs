using System;

namespace WRSoftware.Utils.Common.Models
{
    public class AuditableEntityGuidOffset
    {
        public AuditableEntityGuidOffset()
        {

        }

        /// <summary>
        /// Gets or sets the created by.
        /// </summary>
        /// <value>
        /// The created by.
        /// </value>
        public Guid CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the created at.
        /// </summary>
        /// <value>
        /// The created at.
        /// </value>
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the last modified by.
        /// </summary>
        /// <value>
        /// The last modified by.
        /// </value>
        public Guid LastModifiedBy { get; set; }

        /// <summary>
        /// Gets or sets the last modified at.
        /// </summary>
        /// <value>
        /// The last modified at.
        /// </value>
        public DateTimeOffset? LastModifiedAt { get; set; }
    }
}
