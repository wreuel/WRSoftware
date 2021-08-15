using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using WRSoftware.Utils.Common.DTO;


namespace WRSoftware.Utils.WebBase.Controllers
{
    /// <summary>
    /// ResponseBase, responsible to keep 
    /// the methods that will return the answer
    /// so it will be easy to return the 
    /// correct status code for each request
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    public class ResponseBaseController : ControllerBase
    {
        /// <summary>
        /// The HTTP context accessor
        /// </summary>
        private IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// Gets the HTTP context accessor.
        /// </summary>
        /// <value>
        /// The HTTP context accessor.
        /// </value>
        protected IHttpContextAccessor HttpContextAccessor => _httpContextAccessor ??=
            HttpContext.RequestServices.GetService<IHttpContextAccessor>();

        /// <summary>
        /// Responses the back.
        /// </summary>
        /// <param name="response">The response.</param>
        /// <returns></returns>
        [ApiExplorerSettings(IgnoreApi = true)]
        public ObjectResult ResponseBack(ResponseDto response)
        {
            if (response.StatusCode == (int)HttpStatusCode.InternalServerError ||
                response.StatusCode == (int)HttpStatusCode.BadRequest)
            {
                response.Succeeded = false;
            }
            else
            {
                response.Succeeded = true;
            }

            return StatusCode(response.StatusCode, response);
        }


        /// <summary>
        /// Responses the back.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="response">The response.</param>
        /// <returns></returns>
        [ApiExplorerSettings(IgnoreApi = true)]
        public ObjectResult ResponseBack<T>(ResponseGenericDto<T> response) where T : class
        {
            if (response.StatusCode == (int)HttpStatusCode.InternalServerError ||
                response.StatusCode == (int)HttpStatusCode.BadRequest)
            {
                response.Succeeded = false;
            }
            else
            {
                response.Succeeded = true;
            }

            return StatusCode(response.StatusCode, response);
        }
    }
}
