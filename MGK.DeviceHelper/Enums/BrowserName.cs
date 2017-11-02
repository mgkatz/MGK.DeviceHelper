namespace MGK.DeviceHelper.Enums
{
	/// <summary>
	/// It is the enumeration of the generic name for the Browsers considered here.
	/// </summary>
	public enum BrowserName
	{
		AndroidBrowser = 1,
		AvantIEMobileSlimBrowserBaidu = 2,
		Chrome = 45,
		ChromeAndroidiOS = 3,
		ChromeOmniWebAroraTizenNokia = 4,
		ChromeWebView = 5,
		ChromiumFlockRockMeltMidoriEpiphanySilkSkyfireBoltIronIridiumPhantomJS = 6,
		ComodoDragon = 7,
		Dolphin = 8,
		Facebook = 46,
		FacebookAppiOS = 9,
		Firefox = 47,
		FirefoxiOS = 10,
		FirefoxSeaMonkeyKMeleonIceCatIceApeFirebirdPhoenix = 11,
		GoBrowser = 12,
		ICEBrowser = 13,
		IceDragonIceweaselCaminoChimeraFennecMaemoMinimoConkeror = 14,
		IE11 = 15,
		InternetExplorer = 16,
		Kindle = 17,
		Konqueror = 18,
		Links = 19,
		LunascapeMaxthonNetfrontJasmineBlazer = 20,
		MicrosoftEdge = 21,
		MIUIBrowser = 22,
		MobileSafari = 23,
		Mosaic = 24,
		Mozilla = 25,
		Netscape = 26,
		Opera = 44,
		OperaGT980 = 27,
		OperaLT980 = 28,
		OperaMini = 29,
		OperaMiniIphoneGTE80 = 30,
		OperaMobiTablet = 31,
		OperaWebkit = 32,
		PolarisLynxDilloiCabDorisAmayaw3mNetSurfSleipnir = 33,
		QQBrowser = 34,
		Rekonq = 35,
		SafariAndSafariMobile = 36,
		SafariLT30 = 37,
		SamsungBrowser = 38,
		Swiftfox = 39,
		UCBrowser = 40,
		Unknown = 0,
		Webkit = 41,
		WeChat = 42,
		Yandex = 43
	}

	public static class BrowserNameExtensions
	{
		/// <summary>
		/// Gets a generic name to display for every value in the enumaration.
		/// </summary>
		/// <param name="source">The value of the enumeration.</param>
		/// <returns>A string with generic name to display.</returns>
		public static string GetDisplayName(this BrowserName source)
		{
			switch (source)
			{
				case BrowserName.AndroidBrowser:
					return "Android Browser";

				case BrowserName.AvantIEMobileSlimBrowserBaidu:
					return "Avant/IEMobile/SlimBrowser/Baidu";

				case BrowserName.ChromeAndroidiOS:
					return "Chrome for Android/iOS";

				case BrowserName.ChromeOmniWebAroraTizenNokia:
					return "Chrome/OmniWeb/Arora/Tizen/Nokia";

				case BrowserName.ChromeWebView:
					return "Chrome WebView";

				case BrowserName.ChromiumFlockRockMeltMidoriEpiphanySilkSkyfireBoltIronIridiumPhantomJS:
					return "Chromium/Flock/RockMelt/Midori/Epiphany/Silk/Skyfire/Bolt/Iron/Iridium/PhantomJS";

				case BrowserName.ComodoDragon:
					return "Comodo Dragon";

				case BrowserName.FacebookAppiOS:
					return "Facebook App for iOS";

				case BrowserName.FirefoxiOS:
					return "Firefox for iOS";

				case BrowserName.FirefoxSeaMonkeyKMeleonIceCatIceApeFirebirdPhoenix:
					return "Firefox/SeaMonkey/K-Meleon/IceCat/IceApe/Firebird/Phoenix";

				case BrowserName.ICEBrowser:
					return "ICE Browser";

				case BrowserName.IceDragonIceweaselCaminoChimeraFennecMaemoMinimoConkeror:
					return "IceDragon/Iceweasel/Camino/Chimera/Fennec/Maemo/Minimo/Conkeror";

				case BrowserName.IE11:
					return "Internet Explorer 11";

				case BrowserName.InternetExplorer:
					return "Internet Explorer";

				case BrowserName.LunascapeMaxthonNetfrontJasmineBlazer:
					return "Lunascape/Maxthon/Netfront/Jasmine/Blazer";

				case BrowserName.MicrosoftEdge:
					return "Microsoft Edge";

				case BrowserName.MIUIBrowser:
					return "MIUI Browser";

				case BrowserName.MobileSafari:
					return "Mobile Safari";

				case BrowserName.OperaGT980:
					return "Opera > 9.80";

				case BrowserName.OperaLT980:
					return "Opera < 9.80";

				case BrowserName.OperaMini:
					return "Opera Mini";

				case BrowserName.OperaMiniIphoneGTE80:
					return "Opera mini on iphone >= 8.0";

				case BrowserName.OperaMobiTablet:
					return "Opera Mobi/Tablet";

				case BrowserName.OperaWebkit:
					return "Opera Webkit";

				case BrowserName.PolarisLynxDilloiCabDorisAmayaw3mNetSurfSleipnir:
					return "Polaris/Lynx/Dillo/iCab/Doris/Amaya/w3m/NetSurf/Sleipnir";

				case BrowserName.SafariAndSafariMobile:
					return "Safari & Safari Mobile";

				case BrowserName.SafariLT30:
					return "Safari < 3.0";

				case BrowserName.SamsungBrowser:
					return "Samsung Browser";

				default:
					return source.ToString();
			}
		}
	}
}
