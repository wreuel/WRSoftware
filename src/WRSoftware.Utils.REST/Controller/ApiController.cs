using AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using WRSoftware.Utils.Logging;
using WRSoftware.Utils.WebBase.Controllers;

namespace WRSoftware.Utils.REST.Controller
{
    /// <summary>
    /// The API Controller that inherit the ResponseBaseController
    /// When create API controllers, it should inherit from this.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    public abstract class ApiController : ResponseBaseController
    {
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
        /// Gets or sets the language.
        /// </summary>
        /// <value>
        /// The language.
        /// </value>
        internal string Language { get; set; }

        /// <summary>
        /// Gets the mediator.
        /// </summary>
        /// <value>
        /// The mediator.
        /// </value>
        protected IMediator Mediator =>
            _mediator ??= HttpContextAccessor.HttpContext.RequestServices.GetService<IMediator>();

        /// <summary>
        /// Gets the logger configuration.
        /// </summary>
        /// <value>
        /// The logger configuration.
        /// </value>
        protected LoggerConfig LoggerConfig =>
            _loggerConfig ??= HttpContextAccessor.HttpContext.RequestServices.GetService<Func<object, LoggerConfig>>()(this);

        /// <summary>
        /// Gets the mapper.
        /// </summary>
        /// <value>
        /// The mapper.
        /// </value>
        protected IMapper Mapper => _mapper ??= HttpContextAccessor.HttpContext.RequestServices.GetService<IMapper>();

    }
}