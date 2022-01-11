namespace WRSoftware.Utils.Common.DTO
{
    /// <summary>
    /// Class that will hold the info
    /// to show in in a combobox/dropdown
    /// </summary>
    public class DropdownDto
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DropdownDto"/> class.
        /// </summary>
        public DropdownDto()
        {

        }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }
    }
}