using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using WRSoftware.Utils.Common.DTO;

namespace WRSoftware.Utils.Helper
{
    /// <summary>
    /// Enum Helper that can get the display name, the localization name and others
    /// </summary>
    public class EnumHelper
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EnumHelper"/> class.
        /// </summary>
        protected EnumHelper()
        {

        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static string GetName<T>(object value) => Enum.GetName(typeof(T), value);

        /// <summary>
        /// Gets the display name.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static string GetDisplayName<T>(object value)
        {
            if (Enum.IsDefined(typeof(T), value))
            {
                var fieldInfo = typeof(T).GetField(value.ToString());


                var descriptionAttributes =
                    fieldInfo.GetCustomAttributes(typeof(DisplayAttribute), false) as DisplayAttribute[];
                if (descriptionAttributes.Length > 0)
                {
                    return descriptionAttributes[0].Name;
                }

                return string.Empty;
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Gets the display names.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<string> GetDisplayNames<T>()
        {
            var displayAttributes = typeof(T).GetFields()
                .SelectMany(x => x.GetCustomAttributes(typeof(DisplayAttribute), false));
            var names = new List<string>();
            foreach (var item in displayAttributes)
            {
                names.Add(((DisplayAttribute)item).Name);
            }

            return names;
        }

        /// <summary>
        /// Gets the dropdown values from enum.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<DropdownDto> GetDropdownValuesFromEnum<T>()
        {
            IList<DropdownDto> values = new List<DropdownDto>();
            var enumArrays = Enum.GetValues(typeof(T));
            for (var i = 0; i < enumArrays.Length; i++)
            {
                var name = GetDisplayName<T>(((T[])enumArrays)[i]);
                values.Add(new DropdownDto
                {
                    Id = Convert.ToInt32(((T[])enumArrays)[i]),
                    Name = string.IsNullOrEmpty(name) ? ((T[])enumArrays)[i].ToString() : name
                });
            }

            return values;
        }

        /// <summary>
        /// Gets the dropdown localized values from enum.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="localizer">The localizer.</param>
        /// <returns></returns>
        public static IEnumerable<DropdownDto> GetDropdownLocalizedValuesFromEnum<T>(IStringLocalizer localizer)
        {
            IList<DropdownDto> values = new List<DropdownDto>();
            var enumArrays = Enum.GetValues(typeof(T));
            for (var i = 0; i < enumArrays.Length; i++)
            {
                var name = GetDisplayName<T>(((T[])enumArrays)[i]);
                var translate = localizer[name];
                values.Add(new DropdownDto
                {
                    Id = Convert.ToInt32(((T[])enumArrays)[i]),
                    Name = string.IsNullOrEmpty(translate) ? ((T[])enumArrays)[i].ToString() : translate
                });
            }

            return values;
        }
    }
}