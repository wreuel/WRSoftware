using System;
using System.Runtime.Serialization;
using System.Security;

namespace WRSoftware.Utils.EntityFrameworkCore.Exceptions
{
    /// <summary>
    /// Exception about the Duplicated Key
    /// </summary>
    /// <seealso cref="System.Exception" />
    [Serializable]
    public class DuplicateKeyViolationException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DuplicateKeyViolationException"/> class.
        /// </summary>
        public DuplicateKeyViolationException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DuplicateKeyViolationException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public DuplicateKeyViolationException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DuplicateKeyViolationException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
        public DuplicateKeyViolationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DuplicateKeyViolationException"/> class.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo"></see> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext"></see> that contains contextual information about the source or destination.</param>
        [SecuritySafeCritical]
        protected DuplicateKeyViolationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}