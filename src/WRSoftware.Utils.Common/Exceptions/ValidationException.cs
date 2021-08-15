using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using WRSoftware.Utils.Common.Constants;
using WRSoftware.Utils.Common.Localization;

namespace WRSoftware.Utils.Common.Exceptions
{
    /// <summary>
    /// My Validation Exception, will check all the Rule Validations
    /// </summary>
    /// <seealso cref="System.Exception" />
    [Serializable]
    public class MyValidationException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MyValidationException" /> class.
        /// </summary>
        public MyValidationException()
            : base(ValidationConstants.OneOrMoreErrors)
        {
            Errors = new Dictionary<string, string[]>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MyValidationException" /> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public MyValidationException(string message) : base(message ?? ValidationConstants.OneOrMoreErrors)
        {
            Errors = new Dictionary<string, string[]>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MyValidationException"/> class.
        /// </summary>
        /// <param name="failures">The failures.</param>
        public MyValidationException(IEnumerable<ValidationFailure> failures, CommonLocalizationService localizer)
            : this(localizer?.Get(ValidationConstants.OneOrMoreErrors) ?? ValidationConstants.OneOrMoreErrors)
        {
            var errorCodeList = failures.GroupBy(s => s.ErrorCode).OrderBy(s => s.Key).Select(s => s.Key).ToList();
            foreach (var item in errorCodeList)
            {
                var isNumeric = int.TryParse(item, out int statusCode);
                if (isNumeric)
                {
                    var statList = new List<string> { item };
                    Errors.Add("StatusCode", statList.ToArray());
                    break;
                }

            }

            var failureGroups = failures
                .GroupBy(e => e.PropertyName, e => e.ErrorMessage);

            foreach (var failureGroup in failureGroups)
            {
                var propertyName = failureGroup.Key;
                var propertyFailures = failureGroup.ToArray();

                Errors.Add(propertyName, propertyFailures);
            }
        }

        /// <summary>
        /// Gets the errors.
        /// </summary>
        /// <value>
        /// The errors.
        /// </value>
        public IDictionary<string, string[]> Errors { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MyValidationException"/> class.
        /// </summary>
        /// <param name="info">The information.</param>
        /// <param name="context">The context.</param>
        protected MyValidationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {

        }
    }
}