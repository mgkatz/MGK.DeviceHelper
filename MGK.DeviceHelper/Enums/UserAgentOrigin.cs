using MGK.DeviceHelper.Helpers;

namespace MGK.DeviceHelper.Enums
{
	/// <summary>
	/// It is the enumeration of the generic name for the origin of the user agent.
	/// </summary>
	public enum UserAgentOrigin
	{
		CloudPlatform = 1,
		Console = 2,
		EmailClient = 3,
		EmailCollector = 4,
		FeedReader = 5,
		LibraryOrApp = 6,
		LinkChecker = 7,
		MobileBrowser = 8,
		OfflineBrowser = 9,
		WebBrowser = 10,
		WebCrawler = 11,
		Unknown = 0
	}

	public static class UserAgentOriginExtensions
	{
		/// <summary>
		/// Gets a generic name to display for every value in the enumeration.
		/// </summary>
		/// <param name="source">The value of the enumeration.</param>
		/// <returns>A string with generic name to display.</returns>
		public static string GetDisplayName(this UserAgentOrigin source)
		{
			return ResourcesHelper.GetDisplayName(source.ToString(), Resources.DefaultResources.UserAgentOriginPrefix);
		}
	}
}
