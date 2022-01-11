using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace WRSoftware.Utils.IoC.Framework.Modules
{
    /// <summary>
    /// 
    /// </summary>
    public static class MediatorValidatorModule
    {
        /// <summary>
        /// Registers the specified services.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns></returns>
        public static void RegisterMediatorValidator<TMediator, TValidator>(this IServiceCollection services)
        {

            services.RegisterMediator<TMediator>();

            AssemblyScanner.FindValidatorsInAssembly(typeof(TValidator).Assembly).ForEach(item => services.AddScoped(item.InterfaceType, item.ValidatorType));
        }

        public static void RegisterMediatorValidatorExecutingAssembly<TMediator, TValidator>(this IServiceCollection services)
        {
            services.RegisterMediatorExecutingAssembly<TMediator>();

            AssemblyScanner.FindValidatorsInAssembly(typeof(TValidator).Assembly)
               .ForEach(delegate (AssemblyScanner.AssemblyScanResult item)
               {
                   services.AddScoped(item.InterfaceType, item.ValidatorType);
               });
        }
    }
}
