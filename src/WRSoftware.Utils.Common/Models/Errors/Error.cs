using System;
using WRSoftware.Utils.Common.Models.Interfaces;

namespace WRSoftware.Utils.Common.Models.Errors
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="WRSoftware.Utils.Common.Models.Interfaces.IError" />
    public class Error : IError
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Error"/> class.
        /// </summary>
        public Error()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Error"/> class.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="message">The message.</param>
        /// <exception cref="System.ArgumentNullException">
        /// key
        /// or
        /// message
        /// </exception>
        protected Error(string key, string message)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            if (string.IsNullOrEmpty(message))
                throw new ArgumentNullException(nameof(message));

            Key = key;
            Message = message;
        }

        /// <summary>
        /// Gets the key.
        /// </summary>
        /// <value>
        /// The key.
        /// </value>
        public string Key { get; }

        /// <summary>
        /// Gets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string Message { get; }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString() => Message;
    }
}
