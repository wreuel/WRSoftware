using System.Collections.Generic;

namespace WRSoftware.Utils.Common.Models
{
    /// <summary>
    /// The Culture Switcher Model, used to change the 
    /// CultureInfo if the project has RazorPages
    /// </summary>
    public class CultureSwitcherModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CultureSwitcherModel"/> class.
        /// </summary>
        public CultureSwitcherModel()
        {
            SupportedCultures = new List<SupportedCultureItem>();
        }

        /// <summary>
        /// Gets or sets the current UI culture.
        /// </summary>
        /// <value>
        /// The current UI culture.
        /// </value>
        public SupportedCultureItem CurrentUICulture { get; set; }

        /// <summary>
        /// Gets or sets the supported cultures.
        /// </summary>
        /// <value>
        /// The supported cultures.
        /// </value>
        public List<SupportedCultureItem> SupportedCultures { get; set; }

    }


}