using Microsoft.Extensions.DependencyInjection;
using System;
using WRSoftware.Utils.Common.Interfaces;
using WRSoftware.Utils.Common.Models;
using WRSoftware.Utils.Logging;

namespace WRSoftware.Utils.IoC.Framework.Modules
{
    /// <summary>
    /// 
    /// </summary>
    public static class CrossCuttingModule
    {
        /// <summary>
        /// Registers the cross cutting.
        /// </summary>
        /// <param name="services">The services.</param>
        public static void RegisterCrossCutting(this IServiceCollection services)
        {
            services.AddScoped<ServiceLogger>();
            services.AddScoped<ExceptionLogger>();
            services.AddScoped<IServiceContext, ServiceContext>();

            services.AddScoped<Func<object, LoggerConfig>>(x => (Func<object, LoggerConfig>)(t => new LoggerConfig(t.GetType().Name, x.GetRequiredService<IServiceContext>())));
        }
    }
}
