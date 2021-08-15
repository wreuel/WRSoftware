namespace WRSoftware.Utils.Common.Constants
{
    /// <summary>
    /// Class the hold the common constant configs 
    /// such as page size and others.
    /// If the new project doesn't specify anything 
    /// You can use this. The List will grow with time
    /// </summary>
    public class ConfigConstants
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigConstants"/> class.
        /// </summary>
        protected ConfigConstants() { }

        /// <summary>
        /// The page size
        /// </summary>
        public const int PageSize = 10;

        /// <summary>
        /// The validation result
        /// </summary>
        public static readonly string ValidationResult = "ValidationResult";
    }
}