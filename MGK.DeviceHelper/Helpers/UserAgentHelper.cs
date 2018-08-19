using MGK.DeviceHelper.Enums;

namespace MGK.DeviceHelper.Helpers
{
    internal static class UserAgentHelper
    {
		#region Public Methods
		public static UserAgentOrigin GetOrigin(DeviceType deviceType, BrowserName? browserName)
		{
			if (!browserName.HasValue)
				return UserAgentOrigin.Unknown;

			if (IsCloudPlatform(browserName.Value))
				return UserAgentOrigin.CloudPlatform;

			if (IsConsole(browserName.Value) || deviceType == DeviceType.GamingConsole)
				return UserAgentOrigin.Console;

			if (IsEmailClient(browserName.Value))
				return UserAgentOrigin.EmailClient;

			if (IsEmailCollector(browserName.Value))
				return UserAgentOrigin.EmailCollector;

			if (IsFeedReader(browserName.Value))
				return UserAgentOrigin.FeedReader;

			if (IsLibraryOrApp(browserName.Value))
				return UserAgentOrigin.LibraryOrApp;

			if (IsLinkChecker(browserName.Value))
				return UserAgentOrigin.LinkChecker;

			if (IsOfflineBrowser(browserName.Value))
				return UserAgentOrigin.OfflineBrowser;

			if (IsWebCrawler(browserName.Value))
				return UserAgentOrigin.WebCrawler;

			//I can only guarantee the origin in this two cases without
			//analyzing the browser name obtained.
			switch (deviceType)
			{
				case DeviceType.Desktop:
					return UserAgentOrigin.WebBrowser;

				case DeviceType.Mobile:
					return UserAgentOrigin.MobileBrowser;
			}

			return UserAgentOrigin.Unknown;
		}
		#endregion

		#region Private methods
		private static bool IsCloudPlatform(BrowserName browserName)
		{
			switch (browserName)
			{
				case BrowserName.AppEngineGoogle:
					return true;

				default:
					return false;
			}
		}

		private static bool IsConsole(BrowserName browserName)
		{
			switch (browserName)
			{
				case BrowserName.Bunjalloo:
				case BrowserName.Playstation3:
				case BrowserName.PlaystationPortable:
				case BrowserName.WiiLibnup:
					return true;

				default:
					return false;
			}
		}

		private static bool IsEmailClient(BrowserName browserName)
		{
			switch (browserName)
			{
				case BrowserName.Thunderbird:
					return true;

				default:
					return false;
			}
		}

		private static bool IsEmailCollector(BrowserName browserName)
		{
			switch (browserName)
			{
				case BrowserName.EmailSiphon:
					return true;

				default:
					return false;
			}
		}

		private static bool IsFeedReader(BrowserName browserName)
		{
			switch (browserName)
			{
				case BrowserName.Bloglines:
				case BrowserName.EveryfeedSpider:
				case BrowserName.FeedFetcherGoogle:
				case BrowserName.GreatNews:
				case BrowserName.Gregarius:
				case BrowserName.MagpieRSS:
				case BrowserName.NFReader:
				case BrowserName.UniversalFeedParser:
					return true;

				default:
					return false;
			}
		}

		private static bool IsLibraryOrApp(BrowserName browserName)
		{
			switch (browserName)
			{
				case BrowserName.Amaya:
				case BrowserName.BinGet:
				case BrowserName.Cocoalicious:
				case BrowserName.CURL:
				case BrowserName.ITunes:
				case BrowserName.Java:
				case BrowserName.Lftp:
				case BrowserName.MetaURI:
				case BrowserName.MicrosoftURLControl:
				case BrowserName.NitroPDF:
				case BrowserName.Peach:
				case BrowserName.Perl:
				case BrowserName.PHP:
				case BrowserName.Pxyscand:
				case BrowserName.Python:
				case BrowserName.Snoopy:
				case BrowserName.Susie:
				case BrowserName.URDMAGPIE:
				case BrowserName.WebCapture:
				case BrowserName.WindowsMediaPlayer:
					return true;

				default:
					return false;
			}
		}

		private static bool IsLinkChecker(BrowserName browserName)
		{
			switch (browserName)
			{
				case BrowserName.AbiLogicBot:
				case BrowserName.LinkValet:
				case BrowserName.LinkValidityCheck:
				case BrowserName.LinkExaminer:
				case BrowserName.LinksManagerComBot:
				case BrowserName.MojooRobot:
				case BrowserName.Notifixious:
				case BrowserName.OnlineLinkValidator:
				case BrowserName.PloetzZeller:
				case BrowserName.ReciprocalLinkSystemPRO:
				case BrowserName.RELLinkCheckerLite:
				case BrowserName.SiteBar:
				case BrowserName.VivanteLinkChecker:
				case BrowserName.W3CChecklink:
				case BrowserName.XenuLinkSleuth:
					return true;

				default:
					return false;
			}
		}

		private static bool IsOfflineBrowser(BrowserName browserName)
		{
			switch (browserName)
			{
				case BrowserName.OfflineExplorer:
				case BrowserName.SuperBot:
				case BrowserName.WebDownloader:
				case BrowserName.WebCopier:
				case BrowserName.WebZIP:
				case BrowserName.Wget:
					return true;

				default:
					return false;
			}
		}

		private static bool IsWebCrawler(BrowserName browserName)
		{
			switch (browserName)
			{
				case BrowserName.ABACHOBot:
				case BrowserName.AccoonaAIAgent:
				case BrowserName.AddSugarSpiderBot:
				case BrowserName.AnyApexBot:
				case BrowserName.Arachmo:
				case BrowserName.Baiduspider:
				case BrowserName.BecomeBot:
				case BrowserName.BeslistBot:
				case BrowserName.BillyBobBot:
				case BrowserName.Bimbot:
				case BrowserName.Bingbot:
				case BrowserName.BlitzBOT:
				case BrowserName.BoithoComDc:
				case BrowserName.BoithoComRobot:
				case BrowserName.Btbot:
				case BrowserName.CatchBot:
				case BrowserName.CerberianDrtrs:
				case BrowserName.Charlotte:
				case BrowserName.ConveraCrawler:
				case BrowserName.Cosmos:
				case BrowserName.CovarioIDS:
				case BrowserName.DataparkSearch:
				case BrowserName.DiamondBot:
				case BrowserName.Discobot:
				case BrowserName.DomainsDBMetaCrawler:
				case BrowserName.Dotbot:
				case BrowserName.EmeraldShieldComWebBot:
				case BrowserName.EnvolkITSspider:
				case BrowserName.EsperanzaBot:
				case BrowserName.Exabot:
				case BrowserName.FASTEnterpriseCrawler:
				case BrowserName.FASTWebCrawler:
				case BrowserName.FDSERobot:
				case BrowserName.FindLinks:
				case BrowserName.FurlBot:
				case BrowserName.FyberSpider:
				case BrowserName.G2Crawler:
				case BrowserName.Gaisbot:
				case BrowserName.GalaxyBot:
				case BrowserName.GenieBot:
				case BrowserName.Gigabot:
				case BrowserName.Girafabot:
				case BrowserName.Googlebot:
				case BrowserName.GooglebotImage:
				case BrowserName.GSiteCrawler:
				case BrowserName.GurujiBot:
				case BrowserName.HappyFunBot:
				case BrowserName.HlFtienSpider:
				case BrowserName.Holmes:
				case BrowserName.Htdig:
				case BrowserName.Iaskspider:
				case BrowserName.IaArchiver:
				case BrowserName.ICCrawler:
				case BrowserName.Ichiro:
				case BrowserName.IgdeSpyder:
				case BrowserName.IRLbot:
				case BrowserName.IssueCrawler:
				case BrowserName.JaxifiedBot:
				case BrowserName.Jyxobot:
				case BrowserName.KoepaBot:
				case BrowserName.LWebis:
				case BrowserName.LapozzBot:
				case BrowserName.Larbin:
				case BrowserName.LDSpider:
				case BrowserName.LexxeBot:
				case BrowserName.LingueeBot:
				case BrowserName.LinkWalker:
				case BrowserName.Lmspider:
				case BrowserName.LwpTrivial:
				case BrowserName.Mabontland:
				case BrowserName.MagpieCrawler:
				case BrowserName.MediapartnersGoogle:
				case BrowserName.MJ12bot:
				case BrowserName.Mnogosearch:
				case BrowserName.Mogi:
				case BrowserName.MojeekBot:
				case BrowserName.Moreoverbot:
				case BrowserName.MorningPaper:
				case BrowserName.Msnbot:
				case BrowserName.MSRBot:
				case BrowserName.MVAClient:
				case BrowserName.Mxbot:
				case BrowserName.NetResearchServer:
				case BrowserName.NetSeerCrawler:
				case BrowserName.NewsGator:
				case BrowserName.NGSearch:
				case BrowserName.Nicebot:
				case BrowserName.Noxtrumbot:
				case BrowserName.NusearchSpider:
				case BrowserName.NutchCVS:
				case BrowserName.Nymesis:
				case BrowserName.Obot:
				case BrowserName.Oegp:
				case BrowserName.Omgilibot:
				case BrowserName.OmniExplorerBot:
				case BrowserName.OOZBOT:
				case BrowserName.Orbiter:
				case BrowserName.PageBitesHyperBot:
				case BrowserName.Peew:
				case BrowserName.Polybot:
				case BrowserName.Pompos:
				case BrowserName.Post:
				case BrowserName.Psbot:
				case BrowserName.PycURL:
				case BrowserName.Qseero:
				case BrowserName.Radian6:
				case BrowserName.RAMPyBot:
				case BrowserName.RufusBot:
				case BrowserName.SandCrawler:
				case BrowserName.SBIder:
				case BrowserName.ScoutJet:
				case BrowserName.Scrubby:
				case BrowserName.SearchSight:
				case BrowserName.Seekbot:
				case BrowserName.SemanticDiscovery:
				case BrowserName.SensisWebCrawler:
				case BrowserName.SEOChatBot:
				case BrowserName.SeznamBot:
				case BrowserName.ShimCrawler:
				case BrowserName.ShopWiki:
				case BrowserName.ShoulaRobot:
				case BrowserName.Silk:
				case BrowserName.Sitebot:
				case BrowserName.Snappy:
				case BrowserName.SogouSpider:
				case BrowserName.Sosospider:
				case BrowserName.SpeedySpider:
				case BrowserName.Sqworm:
				case BrowserName.StackRambler:
				case BrowserName.Suggybot:
				case BrowserName.SurveyBot:
				case BrowserName.SynooBot:
				case BrowserName.Teoma:
				case BrowserName.TerrawizBot:
				case BrowserName.TheSuBot:
				case BrowserName.ThumbnailCZRobot:
				case BrowserName.TinEye:
				case BrowserName.TruwoGPS:
				case BrowserName.TurnitinBot:
				case BrowserName.TweetedTimesBot:
				case BrowserName.TwengaBot:
				case BrowserName.Updated:
				case BrowserName.Urlfilebot:
				case BrowserName.Vagabondo:
				case BrowserName.VoilaBot:
				case BrowserName.Vortex:
				case BrowserName.Voyager:
				case BrowserName.VYU2:
				case BrowserName.Webcollage:
				case BrowserName.WebsquashCom:
				case BrowserName.Wf84:
				case BrowserName.WoFindeIchRobot:
				case BrowserName.WomlpeFactory:
				case BrowserName.XaldonWebSpider:
				case BrowserName.Yacy:
				case BrowserName.YahooSeeker:
				case BrowserName.YahooSeekerTesting:
				case BrowserName.YahooSlurp:
				case BrowserName.YahooSlurpChina:
				case BrowserName.YandexBot:
				case BrowserName.YandexImages:
				case BrowserName.Yasaklibot:
				case BrowserName.Yeti:
				case BrowserName.YodaoBot:
				case BrowserName.YoogliFetchAgent:
				case BrowserName.YoudaoBot:
				case BrowserName.Zao:
				case BrowserName.Zealbot:
				case BrowserName.ZeroZeroEight:
				case BrowserName.Zspider:
				case BrowserName.ZyBorg:
					return true;

				default:
					return false;
			}
		}
		#endregion
	}
}
