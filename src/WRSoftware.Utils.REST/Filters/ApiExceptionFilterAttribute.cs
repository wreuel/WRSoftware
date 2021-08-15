using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using WRSoftware.Utils.Common.DTO;
using WRSoftware.Utils.Common.Exceptions;
using WRSoftware.Utils.Helpers;
using WRSoftware.Utils.Logging;
using WRSoftware.Utils.WebBase.Exceptions;

namespace WRSoftware.Utils.REST.Filters
{
    /// <summary>
    /// Filter that will handler the Exceptions
    /// not treated by the API
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Filters.ExceptionFilterAttribute" />
    public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        /// <summary>
        /// The exception handlers
        /// </summary>
        private readonly IDictionary<Type, Action<ExceptionContext>> _exceptionHandlers;



        public ApiExceptionFilterAttribute()
        {
            _exceptionHandlers = new Dictionary<Type, Action<ExceptionContext>>
            {
                {typeof(MyValidationException), HandleValidationException},
                {typeof(NotFoundException), HandleNotFoundException}
            };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiExceptionFilterAttribute" /> class.
        /// </summary>
        /// <param name="stringLocalizer">The string localizer.</param>
        public ApiExceptionFilterAttribute(IStringLocalizer localizer)
        {
            // Register known exception types and handlers.
            _exceptionHandlers = new Dictionary<Type, Action<ExceptionContext>>
            {
                {typeof(MyValidationException), HandleValidationException},
                {typeof(NotFoundException), HandleNotFoundException}
            };
        }

        /// <summary>
        /// </summary>
        /// <param name="context"></param>
        /// <inheritdoc />
        public override void OnException(ExceptionContext context)
        {
            HandleException(context);

            base.OnException(context);
        }

        /// <summary>
        /// Handles the exception.
        /// </summary>
        /// <param name="context">The context.</param>
        private void HandleException(ExceptionContext context)
        {
            Type type = context.Exception.GetType();
            if (_exceptionHandlers.ContainsKey(type))
            {
                _exceptionHandlers[type].Invoke(context);
                return;
            }

            HandleUnknownException(context);
        }

        /// <summary>
        /// Handles the unknown exception.
        /// </summary>
        /// <param name="context">The context.</param>
        private void HandleUnknownException(ExceptionContext context)
        {
            LoggerConfig _loggerConfigs = context.HttpContext.RequestServices.GetService<Func<object, LoggerConfig>>()(this);

            LoggerSingleton.Error(_loggerConfigs, context.Exception);
            var response = new ResponseDto()
            {
                StatusCode = StatusCodes.Status500InternalServerError,
                Succeeded = false,
                Messages = StringHelper.ConvertStringToCollection(context.Exception)
            };


            context.Result = new ObjectResult(response)
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };

            context.ExceptionHandled = true;
        }

        /// <summary>
        /// Handles the not found exception.
        /// </summary>
        /// <param name="context">The context.</param>
        private void HandleNotFoundException(ExceptionContext context)
        {
            var exception = context.Exception as NotFoundException;

            var response = new ResponseDto()
            {
                StatusCode = StatusCodes.Status404NotFound,
                Succeeded = false,
                Messages = StringHelper.ConvertStringToCollection(exception?.Message)
            };

            context.Result = new NotFoundObjectResult(response);

            context.ExceptionHandled = true;
        }

        /// <summary>
        /// Handles the validation exception.
        /// </summary>
        /// <param name="context">The context.</param>
        private void HandleValidationException(ExceptionContext context)
        {
            var exception = context.Exception as MyValidationException;

            //var _localizer = context.HttpContext.RequestServices.GetService<IStringLocalizer<>()(this);


            var statusCode = exception?.Errors["StatusCode"]?.FirstOrDefault();

            exception?.Errors.Remove("StatusCode");
            //Messages = StringHelper.ConvertStringToCollection(_localizer != null ? _localizer[exception?.Message] : exception?.Message)
            var response = new ResponseDto()
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Succeeded = false,
                Errors = exception?.Errors,
                Messages = StringHelper.ConvertStringToCollection(exception?.Message)
            };


            if (!string.IsNullOrEmpty(statusCode))
            {
                response.StatusCode = Convert.ToInt16(statusCode);
            }

            context.Result = new ObjectResult(response)
            {
                StatusCode = response.StatusCode
            };

            ///context.Result = new BadRequestObjectResult(response);

            context.ExceptionHandled = true;
        }
    }
}