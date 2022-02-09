using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;

namespace WRSoftware.Utils.Common.Config
{
    /// <summary>
    /// The Class responsible to get The Version 
    /// and the date that has been published.
    /// </summary>
    public static class ApplicationInformation
    {

        /// <summary>
        /// Gets the version date.
        /// </summary>
        /// <returns></returns>
        public static string GetVersionDate()
        {
            return Assembly.GetExecutingAssembly().GetName().Version.ToString() + " " + Assembly.GetExecutingAssembly()
                .GetLinkerTime()
                .ToString("yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// Gets the version date.
        /// </summary>
        /// <param name="assemblyName">Name of the assembly.</param>
        /// <returns></returns>
        public static DateTime GetVersionDate(Assembly assembly)
        {
            const string BuildVersionMetadataPrefix = "+build";

            var attribute = assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>();
            if (attribute?.InformationalVersion != null)
            {
                var value = attribute.InformationalVersion;
                var index = value.IndexOf(BuildVersionMetadataPrefix);
                if (index > 0)
                {
                    value = value.Substring(index + BuildVersionMetadataPrefix.Length);
                    if (DateTime.TryParseExact(value, "yyyyMMddHHmmss", CultureInfo.InvariantCulture, DateTimeStyles.None, out var result))
                    {
                        return result;
                    }
                }
            }

            return default;
        }

        /// <summary>
        /// Retrieves the linker timestamp.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        /// <param name="target">The target.</param>
        /// <returns></returns>
        /// <remarks>
        /// https://intellitect.com/displaying-deploymentbuild-date-web-pages/
        /// </remarks>
        private static DateTime GetLinkerTime(this Assembly assembly)
        {
            var filePath = assembly.Location;
            const int c_PeHeaderOffset = 60;
            const int c_LinkerTimestampOffset = 8;

            var buffer = new byte[2048];

            using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                stream.Read(buffer, 0, 2048);

            var offset = BitConverter.ToInt32(buffer, c_PeHeaderOffset);
            var secondsSince1970 = BitConverter.ToInt32(buffer, offset + c_LinkerTimestampOffset);
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            var linkTimeUtc = epoch.AddSeconds(secondsSince1970);

            return linkTimeUtc;
        }


        /// <summary>
        /// Gits the hash.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        /// <returns></returns>
        public static string GitHash(this Assembly assembly)
        {
            var version = assembly.GetName().Version + "LOCALBUILD";
            var infoVerAttr = (AssemblyInformationalVersionAttribute)assembly
                      .GetCustomAttributes(typeof(AssemblyInformationalVersionAttribute)).FirstOrDefault();
            if (infoVerAttr != null && infoVerAttr.InformationalVersion.Length > 6)
            {
                // Hash is embedded in the version after a '+' symbol, e.g. 1.0.0+a34a913742f8845d3da5309b7b17242222d41a21
                version = infoVerAttr.InformationalVersion;
            }

            return version.Substring(version.IndexOf('+') + 1);
        }

        /// <summary>
        /// Shorts the git hash.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        /// <returns></returns>
        public static string ShortGitHash(this Assembly assembly)
        {
            var gitHash = assembly.GitHash();
            return gitHash.Substring(gitHash.Length - 6, 6);
        }
    }
}
