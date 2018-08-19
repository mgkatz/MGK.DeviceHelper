using MGK.DeviceHelper.Helpers;

namespace MGK.DeviceHelper.Enums
{
	/// <summary>
	/// It is the enumeration of the device types considered here.
	/// </summary>
	public enum DeviceType
	{
		Desktop = 1,
		GamingConsole = 2,
		Mobile = 3,
		Tablet = 4,
		TV = 5,
		Unknown = 0
	}

	public static class DeviceTypeExtensions
	{
		/// <summary>
		/// Gets a generic name to display for every value in the enumeration.
		/// </summary>
		/// <param name="source">The value of the enumeration.</param>
		/// <returns>A string with generic name to display.</returns>
		public static string GetDisplayName(this DeviceType source)
		{
			return ResourcesHelper.GetDisplayName(source.ToString(), Resources.DefaultResources.DeviceTypePrefix);
		}
	}
}
