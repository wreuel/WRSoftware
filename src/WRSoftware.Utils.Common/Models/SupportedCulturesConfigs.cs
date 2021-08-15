using System.Globalization;

namespace WRSoftware.Utils.Common.Models
{
    /// <summary>
    /// Application's Supported Cultures
    /// </summary>
    public class SupportedCulturesConfigs
    {
        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        /// <value>
        /// The country.
        /// </value>
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets the culture information.
        /// </summary>
        /// <value>
        /// The culture information.
        /// </value>
        public string CultureInfoName { get; set; }

        /// <summary>
        /// Gets or sets the initials.
        /// </summary>
        /// <value>
        /// The initials.
        /// </value>
        public string Initials { get; set; }

        /// <summary>
        /// Gets or sets the culture information.
        /// </summary>
        /// <value>
        /// The culture information.
        /// </value>
        public CultureInfo CultureInfo => new CultureInfo(CultureInfoName);
    }
}