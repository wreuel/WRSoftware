using Microsoft.Extensions.Localization;
using System;
using System.Linq;
using System.Reflection;
using WRSoftware.Utils.Common.Resources;

namespace WRSoftware.Utils.Common.Localization
{
    /// <summary>
    /// 
    /// </summary>
    public class CommonLocalizationService
    {
        /// <summary>
        /// The localizer
        /// </summary>
        private readonly IStringLocalizer _localizer;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommonLocalizationFinder"/> class.
        /// </summary>
        /// <param name="factory">The factory.</param>
        public CommonLocalizationService(IStringLocalizerFactory factory)
        {
            var resource = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes())
                .Where(x => x.FullName != null && x.BaseType == typeof(Resource) && x.FullName.EndsWith("Resources"))
                .Distinct().FirstOrDefault(x => x.FullName.Contains("CommonResources"));

            if (resource != null)
            {
                var assemblyName = new AssemblyName(resource.Assembly.FullName).Name;
                var typeName = resource.FullName.Replace(assemblyName + ".", "");
                _localizer = factory.Create(typeName, new AssemblyName(resource.Assembly.FullName).FullName);
            }
        }

        /// <summary>
        /// Gets the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public string Get(string key)
        {
            return _localizer[key];
        }
    }
}
