using System.Linq;

namespace MGK.DeviceHelper.Helpers
{
	internal static class StringHelper
    {
		public static readonly string Dot = Resources.DefaultResources.Dot;

		public static readonly char DotChar = Dot.ToCharArray().FirstOrDefault();

		public static readonly string ForwardSlash = Resources.DefaultResources.ForwardSlash;

		public static readonly char ForwardSlashChar = ForwardSlash.ToCharArray().FirstOrDefault();

		public static readonly string Underscore = Resources.DefaultResources.Underscore;

		public static readonly char UnderscoreChar = Underscore.ToCharArray().FirstOrDefault();

		public static readonly string WhiteSpace = Resources.DefaultResources.WhiteSpace;

		public static readonly char WhiteSpaceChar = WhiteSpace.ToCharArray().FirstOrDefault();
	}
}
