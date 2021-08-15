using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;
using System;
using WRSoftware.Utils.Logging;

namespace WRSoftware.Utils.MVC.Controllers
{
    /// <summary>
    /// The PageModel using the MediaTR 
    /// When using Mediator Pattern with MediaTR
    /// The Razors Page need to inherit this class
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.RazorPages.PageModel" />
    public class MediaTrPageModel : PageModel
    {
        /// <summary>
        /// The HTTP context accessor
        /// </summary>
        private IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// The logger configuration
        /// </summary>
        private LoggerConfig _loggerConfig;

        /// <summary>
        /// The mapper
        /// </summary>
        private IMapper _mapper;

        /// <summary>
        /// The mediator
        /// </summary>
        private IMediator _mediator;

        /// <summary>
        /// Gets the logger configuration.
        /// </summary>
        /// <value>
        /// The logger configuration.
        /// </value>
        protected LoggerConfig LoggerConfig =>
            _loggerConfig ??= HttpContext.RequestServices.GetService<Func<object, LoggerConfig>>()(this);

        /// <summary>
        /// Gets the mediator.
        /// </summary>
        /// <value>
        /// The mediator.
        /// </value>
        protected IMediator Mediator =>
            _mediator ??= HttpContextAccessor.HttpContext.RequestServices.GetService<IMediator>();


        /// <summary>
        /// Gets the HTTP context accessor.
        /// </summary>
        /// <value>
        /// The HTTP context accessor.
        /// </value>
        protected IHttpContextAccessor HttpContextAccessor => _httpContextAccessor ??=
            HttpContext.RequestServices.GetService<IHttpContextAccessor>();

        /// <summary>
        /// Gets the mapper.
        /// </summary>
        /// <value>
        /// The mapper.
        /// </value>
        protected IMapper Mapper => _mapper ??= HttpContext.RequestServices.GetService<IMapper>();
    }
}
