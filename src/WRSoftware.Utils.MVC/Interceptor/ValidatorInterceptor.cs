using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using Utils.Common.Exceptions;
using Utils.Logging;

namespace Utils.MVC.Filters.Interceptor
{
    /// <summary>
    /// Validator Interceptor used to check
    /// if exists a error within the object.
    /// When the object is not valid, the result
    /// is added to the Controller's HttpContext
    /// so can be managed by an CUSTOMFilterAttribute 
    /// </summary>
    public class ValidatorInterceptor : IValidatorInterceptor
    {
        /// <summary>
        /// Invoked before MVC validation takes place which allows the ValidationContext to be customized prior to validation.
        /// It should return a ValidationContext object.
        /// </summary>
        /// <param name="controllerContext">Controller Context</param>
        /// <param name="validationContext">Validation Context</param>
        /// <returns>
        /// Validation Context
        /// </returns>
        public ValidationContext BeforeMvcValidation(ControllerContext controllerContext,
            ValidationContext validationContext)
        {
            LoggerConfig _loggerConfigs =
                controllerContext.HttpContext.RequestServices.GetService<Func<object, LoggerConfig>>()(this);

            LoggerSingleton.Debug(_loggerConfigs,
                "Validator::Validating " + validationContext.InstanceToValidate.ToString() + " TraceIdentifier: " +
                controllerContext.HttpContext.TraceIdentifier.ToString());
            return validationContext;
        }


        /// <summary>
        /// Invoked after MVC validation takes place which allows the result to be customized.
        /// It should return a ValidationResult.
        /// </summary>
        /// <param name="controllerContext">Controller Context</param>
        /// <param name="validationContext">Validation Context</param>
        /// <param name="result">The result of validation.</param>
        /// <returns>
        /// Validation Context
        /// </returns>
        public ValidationResult AfterMvcValidation(ControllerContext controllerContext,
            ValidationContext validationContext,
            ValidationResult result)
        {
            LoggerConfig _loggerConfigs =
                controllerContext.HttpContext.RequestServices.GetService<Func<object, LoggerConfig>>()(this);
            if (!result.IsValid)
            {
                LoggerSingleton.Warn(_loggerConfigs, string.Format(
                    "Validator::Validation errors - {0} - TraceIdentifier {1} - Command: {2} - Errors: {3}",
                    validationContext.InstanceToValidate, controllerContext.HttpContext.TraceIdentifier,
                    (object)controllerContext.ActionDescriptor.DisplayName,
                    (object)string.Join("; ", result.Errors)));


                var exception = new MyValidationException(result.Errors);

                LoggerSingleton.Error(_loggerConfigs, exception);

                throw exception;
                //controllerContext.HttpContext.Items.Add(ConfigConstants.ValidationResult, result);
            }

            LoggerSingleton.Debug(_loggerConfigs, " Validator::Validated " +
                                                       validationContext.InstanceToValidate.ToString() +
                                                       " TraceIdentifier: " + controllerContext.HttpContext
                                                           .TraceIdentifier.ToString());
            return result;
        }
    }
}
