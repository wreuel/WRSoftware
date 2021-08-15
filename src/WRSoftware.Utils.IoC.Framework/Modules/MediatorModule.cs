using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace WRSoftware.Utils.IoC.Framework.Modules
{
    /// <summary>
    /// https://github.com/jbogard/MediatR.Extensions.Microsoft.DependencyInjection
    /// </summary>
    public static class MediatorModule
    {
        /// <summary>
        /// Registers the specified services.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns></returns>
        public static void RegisterMediator<TMediator>(this IServiceCollection services)
        {
            var lista = System.AppDomain.CurrentDomain.GetAssemblies()
                         .Where(x => x.GetTypes().Contains(typeof(TMediator)))
                         .ToArray();

            services.AddMediatR(lista);
        }
    }
}
