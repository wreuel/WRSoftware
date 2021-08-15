using System.Net;

namespace WRSoftware.Utils.Common.Constants
{
    /// <summary>
    /// Tha class the hold the common 
    /// Constant errors to be easier to create 
    /// a Pattern
    /// </summary>
    public class ValidationConstants
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationConstants"/> class.
        /// </summary>
        protected ValidationConstants()
        {

        }

        /// <summary>
        /// The bad request
        /// </summary>
        public static readonly string BadRequest = ((int)HttpStatusCode.BadRequest).ToString();

        /// <summary>
        /// The one or more errors
        /// </summary>
        public static readonly string OneOrMoreErrors = "One or more validation failures have occurred.";
    }
}