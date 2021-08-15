using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace WRSoftware.Utils.IoC.Framework.Modules
{
    /// <summary>
    /// 
    /// </summary>
    public static class AutoMapperModule
    {
        /// <summary>
        /// Automatics the mapper register.
        /// </summary>
        /// <param name="services">The services.</param>
        public static void RegisterAutoMapper(this IServiceCollection services)
        {
            var list = AppDomain.CurrentDomain.GetAssemblies()
                         .Where(
                                x => x.GetTypes().Contains(typeof(Profile)) ||
                                x.GetTypes().Contains(typeof(MapperConfiguration))
                                )
                         .Distinct()
                         .ToArray();

            services.AddAutoMapper(list);
        }

        /// <summary>
        /// Registers the automatic mapper.
        /// </summary>
        /// <typeparam name="AutoMapper">The type of the uto mapper.</typeparam>
        /// <param name="services">The services.</param>
        public static void RegisterAutoMapper<AutoMapper>(this IServiceCollection services)
        {

            var lista = System.AppDomain.CurrentDomain.GetAssemblies()
                         .Where(x => x.GetTypes().Contains(typeof(AutoMapper)))
                         .ToArray();

            services.AddAutoMapper(lista);
        }
    }
}
