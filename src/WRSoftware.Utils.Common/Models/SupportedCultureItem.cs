using System.Globalization;

namespace WRSoftware.Utils.Common.Models
{
    /// <summary>
    /// Holds the Supported Culture
    /// </summary>
    public class SupportedCultureItem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SupportedCultureItem"/> class.
        /// </summary>
        public SupportedCultureItem()
        {

        }

        /// <summary>
        /// Gets or sets the culture information.
        /// </summary>
        /// <value>
        /// The culture information.
        /// </value>
        public CultureInfo CultureInfo { get; set; }

        /// <summary>
        /// Gets or sets the image CSS.
        /// </summary>
        /// <value>
        /// The image CSS.
        /// </value>
        public string ImageCss { get; set; }
    }
}
