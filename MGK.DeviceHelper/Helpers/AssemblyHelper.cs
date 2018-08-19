using System.IO;
using System.Reflection;

namespace MGK.DeviceHelper.Helpers
{
    internal static class AssemblyHelper
    {
		public static Stream GetResourceFromAssembly(string resourceName)
		{
			var executingAssembly = Assembly.GetExecutingAssembly();
			var stream = executingAssembly.GetResource(resourceName);
			return stream;
		}
	}
}
