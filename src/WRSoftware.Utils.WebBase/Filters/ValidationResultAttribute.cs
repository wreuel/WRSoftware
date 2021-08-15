using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Net;
using WRSoftware.Utils.Common.Constants;
using WRSoftware.Utils.Common.DTO;

namespace WRSoftware.Utils.WebBase.Filters
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Filters.ActionFilterAttribute" />
    public class ValidationResultAttribute : ActionFilterAttribute
    {

        /// <summary>
        /// </summary>
        /// <param name="context"></param>
        /// <inheritdoc />
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.HttpContext.Items.TryGetValue(ConfigConstants.ValidationResult, out var value))
            {
                return;
            }

            if (!(value is ValidationResult vldResult))
            {
                return;
            }

            ///var response = new ResponseDto()
            ///{
            ///StatusCode = StatusCodes.Status400BadRequest,
            ///Succeeded = false,
            ///Errors = exception?.Errors,
            ///Messages = StringHelper.ConvertStringToCollection(exception?.Message)
            ///};

            var response = new ResponseDto
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                Messages = vldResult.Errors.Select(x => x.ErrorMessage).ToList(),
                Succeeded = false
            };

            var objectResult = new ObjectResult(response.StatusCode)
            {
                StatusCode = response.StatusCode,
                Value = response
            };

            context.Result = objectResult;
        }
    }
}
