using MGK.DeviceHelper.Enums;
using System;

namespace MGK.DeviceHelper.Helpers
{
    internal static class VersionHelper
    {
		public static string GetVersionPart(VersionPart versionPart, string version)
		{
			var versionSections = GetVersionSections(version);

			return versionSections.Length == 0
				? version
				: ((int)versionPart >= versionSections.Length ? "" : versionSections[(int)versionPart]);
		}

		private static string[] GetVersionSections(string version)
		{
			return version?
				.Split(new[] { Resources.DefaultResources.Dot }, StringSplitOptions.RemoveEmptyEntries)
				?? new string[0];
		}
	}
}
