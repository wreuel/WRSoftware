using System;
using WRSoftware.Utils.Common.Constants;

namespace WRSoftware.Utils.Common.Models.Errors
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="WRSoftware.Utils.Common.Models.Errors.Error" />
    public class Critical : Error
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Critical"/> class.
        /// </summary>
        /// <param name="description">The description.</param>
        public Critical(string description) : base(TextsConstants.Critical, description)
        {
            if (string.IsNullOrEmpty(description))
            {
                throw new ArgumentNullException(nameof(description));
            }

            Description = description;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Critical"/> class.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="description">The description.</param>
        public Critical(string key, string description) : base(key, description)
        {
            Description = description;
        }

        /// <summary>
        /// Gets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString() => Description;
    }
}
