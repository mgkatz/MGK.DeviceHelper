using System.IO;
using System.Reflection;

namespace MGK.DeviceHelper
{
    public static class AssemblyExtensions
    {
		public static string GetRootNamespace(this Assembly source)
			=> Resources.DefaultResources.RootNamespace;

		public static Stream GetResource(this Assembly source, string resourceName)
		{
			var fullResourceName = string.Join(
				Resources.DefaultResources.Dot,
				new[] { source.GetRootNamespace(), resourceName });
			
			var stream = source.GetManifestResourceStream(fullResourceName);

			return stream;
		}
	}
}
