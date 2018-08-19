using MGK.DeviceHelper.Enums;
using MGK.DeviceHelper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace MGK.DeviceHelper.Helpers
{
    internal static class OSHelper
    {
		#region Variables and private objects
		private static readonly Dictionary<OSName, string> OSPatterns = new Dictionary<OSName, string>
		{
			{ OSName.Default, Resources.DefaultResources.RegexOSNamePatternDefault },
			{ OSName.WindowsMobile, Resources.DefaultResources.RegexOSNamePatternWindowsMobile },
			{ OSName.WindowsPhone, Resources.DefaultResources.RegexOSNamePatternWindowsPhone },
			{ OSName.WindowsRT, Resources.DefaultResources.RegexOSNamePatternWindowsRT }
		};

		private static readonly OSName[] SpecialNamesForOSInfo = new[]
		{
			OSName.ChromiumOS,
			OSName.FirefoxOS,
			OSName.iOS,
			OSName.MacOS,
			OSName.Solaris1,
			OSName.Symbian,
			OSName.Windows
		};

		private static readonly Dictionary<string, string> WindowsVersions = new Dictionary<string, string>
		{
			{ Resources.DefaultResources.WindowsVersion490, Resources.DefaultResources.WindowsCommercialVersion490 },
			{ Resources.DefaultResources.WindowsVersionNT351, Resources.DefaultResources.WindowsCommercialVersionNT351 },
			{ Resources.DefaultResources.WindowsVersionNT40, Resources.DefaultResources.WindowsCommercialVersionNT40 },
			{ Resources.DefaultResources.WindowsVersionNT50, Resources.DefaultResources.WindowsCommercialVersionNT50 },
			{ Resources.DefaultResources.WindowsVersionNT51, Resources.DefaultResources.WindowsCommercialVersionNT51 },
			{ Resources.DefaultResources.WindowsVersionNT52, Resources.DefaultResources.WindowsCommercialVersionNT52 },
			{ Resources.DefaultResources.WindowsVersionNT60, Resources.DefaultResources.WindowsCommercialVersionNT60 },
			{ Resources.DefaultResources.WindowsVersionNT61, Resources.DefaultResources.WindowsCommercialVersionNT61 },
			{ Resources.DefaultResources.WindowsVersionNT62, Resources.DefaultResources.WindowsCommercialVersionNT62 },
			{ Resources.DefaultResources.WindowsVersionNT63, Resources.DefaultResources.WindowsCommercialVersionNT63 },
			{ Resources.DefaultResources.WindowsVersionNT64, Resources.DefaultResources.WindowsCommercialVersionNT64 },
			{ Resources.DefaultResources.WindowsVersionNT100, Resources.DefaultResources.WindowsCommercialVersionNT100 },
			{ Resources.DefaultResources.WindowsVersionARM, Resources.DefaultResources.WindowsCommercialVersionARM }
		};
		#endregion

		#region Public methods
		/// <summary>
		/// Gets the information of the Operating Browser.
		/// </summary>
		/// <param name="regexOSName">The generic name for the Operating System/s.</param>
		/// <param name="match">The match of the Regex.</param>
		/// <returns>A tuple with the Name and Version of the Operating System.</returns>
		public static (string Name, string Version) GetOSInfo(OSName regexOSName, Match match)
		{
			var osName = GetOSName(regexOSName, match);
			var osVersion = GetOSVersion(regexOSName, osName, match);

			return (Name: osName, Version: osVersion);
		}

		/// <summary>
		/// Gets the model with the information needed to obtain the information for every Operating System considered in this project.
		/// </summary>
		/// <returns>A list with the generic name for the Operating System/s and the regular expression needed to read the User Agent.</returns>
		public static IEnumerable<OSSimpleRecord> GetOSModel()
		{
			return (IEnumerable<OSSimpleRecord>)RegexHelper.GetRegExModel<OSModel, OSComplexRecord, OSName>(ModelType.OS);
		}
		#endregion

		#region Private methods
		private static string GetOSName(OSName regexOSName, Capture match, RegexOptions regexOptions = RegexOptions.IgnoreCase)
		{
			var osName = string.Empty;

			switch (regexOSName)
			{
				case OSName.BlackBerry10:
				case OSName.ChromiumOS:
				case OSName.FirefoxOS:
				case OSName.iOS:
				case OSName.MacOS:
				case OSName.Solaris1:
				case OSName.Symbian:
				case OSName.Windows:
					osName = regexOSName.GetDisplayName();
					break;

				case OSName.Series40:
					osName = match.Value;
					break;

				default:
					regexOSName =
						regexOSName != OSName.WindowsMobile &&
						regexOSName != OSName.WindowsPhone &&
						regexOSName != OSName.WindowsRT
							? OSName.Default
							: regexOSName;
					var pattern = OSPatterns.Keys.Any(x => x == regexOSName)
						? OSPatterns[regexOSName]
						: string.Empty;

					osName = string.IsNullOrWhiteSpace(pattern)
						? regexOSName.GetDisplayName()
						: new Regex(pattern, regexOptions).Match(match.Value).Value;
					break;
			}

			return osName;
		}

		private static string GetOSVersion(OSName regexOSName, string osName, Capture match)
		{
			var isDefaultSearch = !SpecialNamesForOSInfo.Contains(regexOSName);

			var osVersionList = isDefaultSearch
				? new[]
				{
					string.Empty,
					match.Value.Substring(osName.Length + 1)
				}
				: new[]
				{
					match.Value.Substring(0, match.Value.IndexOf(StringHelper.WhiteSpace, StringComparison.InvariantCultureIgnoreCase)),
					match.Value.Substring(match.Value.IndexOf(StringHelper.WhiteSpace, StringComparison.InvariantCultureIgnoreCase) + 1)
				};

			var osVersion = osVersionList[1];
			var osIsWindows = regexOSName == OSName.Windows;

			switch (regexOSName)
			{
				case OSName.BlackBerry10:
					osVersion = Resources.DefaultResources.OSNameBlackberry10Default;
					break;

				case OSName.iOS:
					osVersion = RegexHelper
						.GetValue(
							Resources.DefaultResources.RegexOSNamePatterniOs,
							osVersion.Replace(StringHelper.Underscore, StringHelper.Dot));
					break;

				case OSName.MacOS:
					osVersion = osVersion.Replace(StringHelper.UnderscoreChar, StringHelper.DotChar);
					break;

				case OSName.Series40:
					osVersion = string.Empty;
					break;

				case OSName.Windows:
				case OSName.WindowsMobile:
				case OSName.WindowsPhone:
				case OSName.WindowsRT:
					if (osIsWindows || osName.Length < match.Value.Length)
					{
						osVersion = WindowsVersions.Keys.Any(m => m.Equals(osVersion, StringComparison.InvariantCultureIgnoreCase))
							? WindowsVersions[osVersion]
							: osVersion;
					}
					break;
			}

			return osVersion;
		}
		#endregion
	}
}
