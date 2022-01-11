using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace WRSoftware.Utils.Common.Mapping
{
    /// <summary>
    /// Responsible to find all the Mapping Profiles
    /// and register when the app launch
    /// </summary>
    /// <seealso cref="AutoMapper.Profile" />
    public class MappingProfile : Profile
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="MappingProfile"/> class.
        /// </summary>
        /// <param name="assemblies">The assemblies.</param>
        public MappingProfile(IList<Assembly> assemblies)
        {
            foreach (var assembly in assemblies)
            {
                ApplyMappingsFromAssembly(assembly);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MappingProfile"/> class.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        public MappingProfile(Assembly assembly)
        {
            ApplyMappingsFromAssembly(assembly);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MappingProfile"/> class.
        /// </summary>
        public MappingProfile()
        {
            ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
        }

        /// <summary>
        /// Applies the mappings from assembly.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        private void ApplyMappingsFromAssembly(Assembly assembly)
        {
            var types = assembly.GetExportedTypes()
                .Where(t => t.GetInterfaces().Any(i =>
                    i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>)))
                .ToList();

            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type);

                var methodInfo = type.GetMethod("Mapping")
                                 ?? type.GetInterface("IMapFrom`1").GetMethod("Mapping");

                methodInfo?.Invoke(instance, new object[] { this });
            }
        }
    }
}
