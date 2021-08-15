using System;

namespace WRSoftware.Utils.Helper
{
    /// <summary>
    /// 
    /// </summary>
    public static class TypeHelper
    {
        /// <summary>
        /// Determines whether [is inherited from] [the specified lookup].
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="Lookup">The lookup.</param>
        /// <returns>
        ///   <c>true</c> if [is inherited from] [the specified lookup]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsInheritedFrom(this Type type, Type Lookup)
        {
            var baseType = type.BaseType;
            if (baseType == null)
                return false;

            if (baseType.IsGenericType
                    && baseType.GetGenericTypeDefinition() == Lookup)
                return true;

            return baseType.IsInheritedFrom(Lookup);
        }
    }

}
