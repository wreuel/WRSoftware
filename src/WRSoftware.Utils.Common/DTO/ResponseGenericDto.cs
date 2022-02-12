using System.Collections.Generic;
using System.Net;

namespace WRSoftware.Utils.Common.DTO
{
    /// <summary>
    /// A DTO to hold the basic data of a Generic response
    /// This means Messages, HttpStatus and it has succeeded or not. 
    /// So the others Responses should inherit from this class making easier
    /// to create a pattern on the answers/responses
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ResponseGenericDto<T> where T : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResponseGenericDto{T}"/> class.
        /// </summary>
        public ResponseGenericDto()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResponseGenericDto{T}"/> class.
        /// </summary>
        /// <param name="data">The data.</param>
        public ResponseGenericDto(T data)
        {
            Data = data;
            StatusCode = data != null ? (int)HttpStatusCode.OK : (int)HttpStatusCode.NotFound;
        }

        public ResponseGenericDto(bool success)
        {
            Succeeded = success;
            StatusCode = success ? (int)HttpStatusCode.OK : (int)HttpStatusCode.NotFound;
        }

        /// <summary>
        /// The response data
        /// </summary>
        public T Data { get; set; }

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