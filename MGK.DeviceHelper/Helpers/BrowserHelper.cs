using MGK.DeviceHelper.Enums;
using MGK.DeviceHelper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace MGK.DeviceHelper.Helpers
{
    internal static class BrowserHelper
    {
		#region Variables and private objects
		private static readonly Dictionary<string, string> SafariLt30Versions = new Dictionary<string, string>
		{
			{ Resources.DefaultResources.SafariLt30Version8, Resources.DefaultResources.SafariLt30CommercialVersion8 },
			{ Resources.DefaultResources.SafariLt30Version1, Resources.DefaultResources.SafariLt30CommercialVersion1 },
			{ Resources.DefaultResources.SafariLt30Version3, Resources.DefaultResources.SafariLt30CommercialVersion3 },
			{ Resources.DefaultResources.SafariLt30Version412, Resources.DefaultResources.SafariLt30CommercialVersion412 },
			{ Resources.DefaultResources.SafariLt30Version416, Resources.DefaultResources.SafariLt30CommercialVersion416 },
			{ Resources.DefaultResources.SafariLt30Version417, Resources.DefaultResources.SafariLt30CommercialVersion417 },
			{ Resources.DefaultResources.SafariLt30Version419, Resources.DefaultResources.SafariLt30CommercialVersion419 },
			{ Resources.DefaultResources.SafariLt30VersionUnknown, Resources.DefaultResources.SafariLt30CommercialVersionUnknown }
		};
		#endregion

		#region Public methods
		/// <summary>
		/// Gets the information of the Browser.
		/// </summary>
		/// <param name="regexBrowserName">The generic name for the Browser/s.</param>
		/// <param name="match">The match of the Regex.</param>
		/// <returns>A tuple with the Name, Version and Major of the Browser.</returns>
		public static (string Name, string Version) GetBrowserInfo(BrowserName regexBrowserName, Match match)
		{
			var regexBrowserNameAux = GetAlternativeBrowserName(regexBrowserName);
			var nameAndVersion = GetBrowserNameVersion(regexBrowserNameAux, match);
			var browserName = nameAndVersion.Name;
			var browserVersion = nameAndVersion.Version;

			return (Name: browserName, Version: browserVersion);
		}

		/// <summary>
		/// Gets the model with the information needed to obtain the information for every Browser considered in this project.
		/// </summary>
		/// <returns>A list with the generic name for the Browser/s and the regular expression needed to read the User Agent.</returns>
		public static IEnumerable<BrowserSimpleRecord> GetBrowserModel()
		{
			return (IEnumerable<BrowserSimpleRecord>)RegexHelper.GetRegExModel<BrowserModel, BrowserComplexRecord, BrowserName>(ModelType.Browser);
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
			var browserName = string.Empty;
			var browserVersion = string.Empty;
			var hasVersion = true;
			var indexNameVersionList = 1;
			var nameVersionList = match.Value.Split(StringHelper.ForwardSlashChar);

			switch (regexBrowserName)
			{
				case BrowserName.Amaya:
				case BrowserName.AvantIEMobileSlimBrowserBaidu:
				case BrowserName.BecomeBot:
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
				case BrowserName.PolarisLynxDilloiCabDorisw3mNetSurfSleipnir:
				case BrowserName.QQBrowser:
				case BrowserName.Rekonq:
				case BrowserName.Swiftfox:
				case BrowserName.Webkit:
					browserName = RegexHelper.GetValue(
						Resources.DefaultResources.RegexBrowserNamePatternMozilla,
						match.Value);
					hasVersion = match.Value.Length > browserName.Length;
					browserVersion = hasVersion
						? match.Value.Substring(browserName.Length + 1)
						: string.Empty;
					break;

				case BrowserName.ChromeWebView:
					browserName = RegexHelper.GetReplacedValue(
						Resources.DefaultResources.RegexBrowserNamePatternChromeWebView,
						nameVersionList[0],
						Resources.DefaultResources.RegexBrowserNameMatchEvaluatorChromeWebView);
					break;

				case BrowserName.ComodoDragon:
					browserName = nameVersionList[0]
						.Replace(StringHelper.UnderscoreChar, StringHelper.WhiteSpaceChar);
					break;

				case BrowserName.IE11:
					nameVersionList = regexBrowserName
						.GetDisplayName()
						.Split(StringHelper.WhiteSpaceChar);
					browserName = string.Join(StringHelper.WhiteSpace, nameVersionList[0], nameVersionList[1]);
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

					hasVersion = regexBrowserName == BrowserName.AppEngineGoogle ? false : hasVersion;

					if (indexZeroBrowsers.Contains(regexBrowserName))
						indexNameVersionList = 0;

					browserName = regexBrowserName == BrowserName.SafariAndSafariMobile
						? nameVersionList[1]
						: regexBrowserName.GetDisplayName();
					break;
			}

			if (regexBrowserName == BrowserName.SafariLT30
				&& SafariLt30Versions.Keys.Any(m => m == nameVersionList[1]))
			{
				browserVersion = SafariLt30Versions[nameVersionList[1]];
			}
			else if ((hasVersion && string.IsNullOrWhiteSpace(browserVersion))
				|| regexBrowserName == BrowserName.IE11)
			{
				browserVersion = nameVersionList[indexNameVersionList];
			}

			return (Name: browserName, Version: browserVersion);
		}
		#endregion
	}
}
