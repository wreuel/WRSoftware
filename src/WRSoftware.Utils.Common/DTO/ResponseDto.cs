using System.Collections.Generic;

namespace WRSoftware.Utils.Common.DTO
{
    /// <summary>
    /// A DTO to hold the basic data of a response
    /// This means Messages, HttpStatus and it has succeeded or not. 
    /// So the others Responses should inherit from this class making easier
    /// to create a pattern on the answers/responses    .
    /// </summary>
    public class ResponseDto
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResponseDto"/> class.
        /// </summary>
        public ResponseDto()
        {
            StatusCode = 200;
        }

        /// <summary>
        /// Gets or sets the messages.
        /// </summary>
        /// <value>
        /// The messages.
        /// </value>
        public IEnumerable<string> Messages { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="ResponseDto"/> is succeeded.
        /// </summary>
        /// <value>
        ///   <c>true</c> if succeeded; otherwise, <c>false</c>.
        /// </value>
        public bool Succeeded { get; set; }

        /// <summary>
        /// Gets or sets the HTTP status code.
        /// </summary>
        /// <value>
        /// The HTTP status code.
        /// </value>
        public int StatusCode { get; set; }

        /// <summary>
        /// Gets or sets the errors.
        /// </summary>
        /// <value>
        /// The errors.
        /// </value>
        public IDictionary<string, string[]> Errors { get; set; }
    }
}