namespace MGK.DeviceHelper.Helpers
{
	internal static class ResourcesHelper
	{
		public static string GetDisplayName(string baseValue, string prefix)
		{
			var displayName = Resources
				.DefaultResources
				.ResourceManager
				.GetString(string.Format(prefix, baseValue));

			return string.IsNullOrWhiteSpace(displayName) ? baseValue : displayName;
		}
	}
}
