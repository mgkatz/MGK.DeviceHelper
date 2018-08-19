using MGK.DeviceHelper.Helpers;

namespace MGK.DeviceHelper.Enums
{
    public enum VersionPart
    {
		Major = 0,
		Minor = 1,
		RevisionOrBuild = 2
    }

	public static class VersionPartExtensions
	{
		/// <summary>
		/// Gets a generic name to display for every value in the enumeration.
		/// </summary>
		/// <param name="source">The value of the enumeration.</param>
		/// <returns>A string with generic name to display.</returns>
		public static string GetDisplayName(this VersionPart source)
		{
			return ResourcesHelper.GetDisplayName(source.ToString(), Resources.DefaultResources.VersionPartPrefix);
		}
	}
}
