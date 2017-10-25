using MGK.DeviceHelper.Enums;
using MGK.DeviceHelper.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace MGK.DeviceHelper.Helpers
{
	/// <summary>
	/// This is a helper to process the data of the User Agent using Regular Expressions.
	/// </summary>
	internal sealed class RegexHelper
    {
        #region Private variables and objects
        private static readonly Dictionary<OSName, string> OSPatterns = new Dictionary<OSName, string>
        {
            { OSName.Default, @"^[a-zA-Z]+" },
            { OSName.WindowsMobile, @"(^[a-zA-Z]+)" },
            { OSName.WindowsPhone, @"(^[a-zA-Z]+\s[a-zA-Z]+)" },
            { OSName.WindowsRT, @"(^[a-zA-Z]+\s[a-zA-Z]+)" }
        };

        private static readonly Dictionary<string, string> SafariLt30Versions = new Dictionary<string, string>
        {
			{"/8","1.0" },
			{ "/1","1.2"},
			{ "/3","1.3"},
			{ "/412","2.0"},
			{ "/416","2.0.2"},
			{ "/417","2.0.3"},
			{ "/419","2.0.4"},
			{"?","/" }
		};

        private static readonly Dictionary<string, string> WindowsVersions = new Dictionary<string, string>
        {
			{"4.90","ME" },
			{ "NT3.51","NT 3.11"},
			{ "NT4.0","NT 4.0"},
			{ "NT 5.0","2000"},
			{ "NT 5.1","XP"},
			{ "NT 5.2","XP"},
			{ "NT 6.0","Vista"},
			{ "NT 6.1","7"},
			{ "NT 6.2","8"},
			{ "NT 6.3","8.1"},
			{ "NT 6.4","10"},
			{ "NT 10.0","10"},
			{ "ARM","RT"}
		};
		#endregion

		#region Public methods
		/// <summary>
		/// Gets the information of the Browser.
		/// </summary>
		/// <param name="regexBrowserName">The generic name for the Browser/s.</param>
		/// <param name="match">The match of the Regex.</param>
		/// <returns>A tuple with the Name, Version and Major of the Browser.</returns>
		public static (string Name, string Version, string Major) GetBrowserInfo(BrowserName regexBrowserName, Match match)
		{
			var browserMajor = string.Empty;
			var browserName = string.Empty;
			var browserVersion = string.Empty;
			var regexBrowserNameAux = GetAlternativeBrowserName(regexBrowserName);
			var nameAndVersion = GetBrowserNameVersion(regexBrowserNameAux, match);

			browserName = nameAndVersion.Name;
			browserVersion = nameAndVersion.Version;
			browserMajor = new Regex(@"\d*").Match(browserVersion).Value;

			return (Name: browserName, Version: browserVersion, Major: browserMajor);
		}

		/// <summary>
		/// Gets the information of the Operating Browser.
		/// </summary>
		/// <param name="regexOSName">The generic name for the Operating System/s.</param>
		/// <param name="match">The match of the Regex.</param>
		/// <returns>A tuple with the Name and Version of the Operating System.</returns>
		public static (string Name, string Version) GetOSInfo(OSName regexOSName, Match match)
		{
		    var osName = string.Empty;
		    var osVersion = string.Empty;
		    var specialNames = new []
		    {
		        OSName.ChromiumOS,
		        OSName.FirefoxOS,
		        OSName.iOS,
		        OSName.MacOS,
		        OSName.Solaris1,
		        OSName.Symbian,
		        OSName.Windows
		    };

		    osName = GetOSName(regexOSName, match);
		    osVersion = GetOSVersion(regexOSName, osName, match, !specialNames.Contains(regexOSName));

            return (Name: osName, Version: osVersion);
        }

		/// <summary>
		/// Gets the model with the Regex information needed to obtain the information for every Browser considered in this project.
		/// </summary>
		/// <returns>A tuple list with the generic name for the Browser/s and the Regex expression needed to read the User Agent.</returns>
		public static IEnumerable<(BrowserName Name, Regex Expression)> GetRegexBrowserModel()
		{
			var jsonModel = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ModelType.Browser.GetModelFileName()));
			var regexModel = SerializationHelper.JsonDeserialize<RegexModel<RegexBrowserRecordModel>>(jsonModel);
			var results = (from a in regexModel.Records select (a.RegexName, new Regex(a.RegexExpression, a.RegexOptions)));

			return results;
		}

		/// <summary>
		/// Gets the model with the Regex information needed to obtain the information for every Operating System considered in this project.
		/// </summary>
		/// <returns>A tuple list with the generic name for the Operating System/s and the Regex expression needed to read the User Agent.</returns>
		public static IEnumerable<(OSName Name, Regex Expression)> GetRegexOSModel()
		{
			var jsonModel = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ModelType.OS.GetModelFileName()));
			var regexModel = SerializationHelper.JsonDeserialize<RegexModel<RegexOSRecordModel>>(jsonModel);
			var results = (from a in regexModel.Records select (a.RegexName, new Regex(a.RegexExpression, a.RegexOptions)));

			return results;
		}
		#endregion

		#region Private methods
		private static BrowserName GetAlternativeBrowserName(BrowserName regexBrowserName)
		{
			switch (regexBrowserName)
			{
				case BrowserName.ChromeAndroidiOS:
					return BrowserName.Chrome;

				case BrowserName.FacebookAppiOS:
					return BrowserName.Facebook;

				case BrowserName.FirefoxiOS:
					return BrowserName.Firefox;

				case BrowserName.OperaWebkit:
					return BrowserName.Opera;

				case BrowserName.SamsungBrowser:
				case BrowserName.UCBrowser:
					return BrowserName.AndroidBrowser;

				default:
					return regexBrowserName;
			}
		}

        private static (string Name, string Version) GetBrowserNameVersion(BrowserName regexBrowserName, Capture match)
		{
			var browserName = "";
			var browserVersion = "";
			var hasVersion = true;
			var indexNameVersionList = 1;
			var nameVersionList = match.Value.Split('/');

			switch (regexBrowserName)
			{
				case BrowserName.AvantIEMobileSlimBrowserBaidu:
				case BrowserName.ChromeOmniWebAroraTizenNokia:
				case BrowserName.ChromiumFlockRockMeltMidoriEpiphanySilkSkyfireBoltIronIridiumPhantomJS:
				case BrowserName.FirefoxSeaMonkeyKMeleonIceCatIceApeFirebirdPhoenix:
				case BrowserName.GoBrowser:
				case BrowserName.ICEBrowser:
				case BrowserName.IceDragonIceweaselCaminoChimeraFennecMaemoMinimoConkeror:
				case BrowserName.InternetExplorer:
				case BrowserName.Kindle:
				case BrowserName.Konqueror:
				case BrowserName.Links:
				case BrowserName.LunascapeMaxthonNetfrontJasmineBlazer:
				case BrowserName.MicrosoftEdge:
				case BrowserName.Mosaic:
				case BrowserName.Mozilla:
				case BrowserName.OperaGT980:
				case BrowserName.OperaLT980:
				case BrowserName.OperaMini:
				case BrowserName.OperaMobiTablet:
				case BrowserName.PolarisLynxDilloiCabDorisAmayaw3mNetSurfSleipnir:
				case BrowserName.QQBrowser:
				case BrowserName.Rekonq:
				case BrowserName.Swiftfox:
				case BrowserName.Webkit:
					browserName = new Regex(@"^[a-zA-Z]+", RegexOptions.IgnoreCase).Match(match.Value).Value;
					hasVersion = match.Value.Length > browserName.Length;
					browserVersion = hasVersion ? match.Value.Substring(browserName.Length + 1) : "";
					break;

				case BrowserName.ChromeWebView:
					browserName = new Regex("(.+)").Replace(nameVersionList[0], "$1 WebView");
					break;

				case BrowserName.ComodoDragon:
					browserName = nameVersionList[0].Replace('_', ' ');
					break;

				case BrowserName.IE11:
					nameVersionList = regexBrowserName.GetDisplayName().Split(' ');
					browserName = nameVersionList[0] + " " + nameVersionList[1];
					indexNameVersionList = 2;
					break;

				case BrowserName.SafariLT30:
					browserName = nameVersionList[0];
					break;

				default:
					var indexZeroBrowsers = new BrowserName[]
					{
						BrowserName.AndroidBrowser,
						BrowserName.FacebookAppiOS,
						BrowserName.FirefoxiOS,
						BrowserName.MIUIBrowser,
						BrowserName.SafariAndSafariMobile,
						BrowserName.SamsungBrowser
					};

					if (indexZeroBrowsers.Contains(regexBrowserName))
						indexNameVersionList = 0;

					browserName = regexBrowserName == BrowserName.SafariAndSafariMobile ? nameVersionList[1] : regexBrowserName.GetDisplayName();
					break;
			}

			if (regexBrowserName == BrowserName.SafariLT30 && SafariLt30Versions.Keys.Any(m => m == nameVersionList[1]))
				browserVersion = SafariLt30Versions[nameVersionList[1]];
			else if ((hasVersion && string.IsNullOrWhiteSpace(browserVersion)) || regexBrowserName == BrowserName.IE11)
				browserVersion = nameVersionList[indexNameVersionList];

			return (Name: browserName, Version: browserVersion);
		}

        private static string GetOSName(OSName regexOSName, Capture match, RegexOptions regexOptions = RegexOptions.IgnoreCase)
        {
            var osName = "";

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
                    var pattern = OSPatterns.Keys.Any(x => x == regexOSName) ? OSPatterns[regexOSName] : "";

                    osName = string.IsNullOrWhiteSpace(pattern) ? regexOSName.GetDisplayName() : new Regex(pattern, regexOptions).Match(match.Value).Value;
                    break;
            }

            return osName;
        }

        private static string GetOSVersion(OSName regexOSName, string osName, Capture match, bool isDefaultSearch = true)
        {
            var osVersionList = isDefaultSearch ?
                new[] { "", match.Value.Substring(osName.Length + 1) } :
                new[] { match.Value.Substring(0, match.Value.IndexOf(" ", StringComparison.InvariantCultureIgnoreCase)), match.Value.Substring(match.Value.IndexOf(" ", StringComparison.InvariantCultureIgnoreCase) + 1) };
            var osVersion = osVersionList[1];

            switch (regexOSName)
            {
                case OSName.BlackBerry10:
                    osVersion = "BB10";
                    break;

                case OSName.iOS:
                    osVersion = new Regex(@"\d+(?:\.\d+)*").Match(osVersion.Replace("_", ".")).Value;
                    break;

                case OSName.MacOS:
                    osVersion = osVersion.Replace('_', '.');
                    break;

                case OSName.Series40:
                    osVersion = "";
                    break;

                case OSName.Windows:
                    osVersion = WindowsVersions.Keys.Any(m => m == osVersion) ? WindowsVersions[osVersion] : osVersion;
                    break;

                case OSName.WindowsMobile:
                case OSName.WindowsPhone:
                case OSName.WindowsRT:
                    if (osName.Length < match.Value.Length)
                    {
                        osVersion = WindowsVersions.Keys.Any(m => m == osVersion)
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
