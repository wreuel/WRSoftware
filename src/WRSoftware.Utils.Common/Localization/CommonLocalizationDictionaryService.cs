using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using WRSoftware.Utils.Common.Resources;

namespace WRSoftware.Utils.Common.Localization
{
    /// <summary>
    /// 
    /// </summary>
    public class CommonLocalizationDictionaryService
    {
        /// <summary>
        /// The localizers
        /// </summary>
        private readonly Dictionary<string, IStringLocalizer> _localizers;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommonLocalizationFinder"/> class.
        /// </summary>
        /// <param name="factory">The factory.</param>
        public CommonLocalizationDictionaryService(IStringLocalizerFactory factory)
        {
            _localizers = new Dictionary<string, IStringLocalizer>();

            var typos = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes())
                .Where(x => x.FullName != null && x.BaseType == typeof(Resource) && x.FullName.EndsWith("Resources"))
                .Distinct().ToList();

            foreach (var type in typos)
            {
                var assemblyName = new AssemblyName(type.Assembly.FullName).Name;
                var typeName = type.FullName.Replace(assemblyName + ".", "");


                _localizers.Add(type.Name, factory.Create(typeName, new AssemblyName(type.Assembly.FullName).FullName));
            }
        }


        /// <summary>
        /// Gets the specified resources.
        /// </summary>
        /// <param name="resources">The resources.</param>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public string Get(string resources, string key)
        {
            var loc = _localizers[resources];
            return loc[key];
        }
    }
}
