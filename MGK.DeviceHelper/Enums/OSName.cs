using MGK.DeviceHelper.Helpers;

namespace MGK.DeviceHelper.Enums
{
	/// <summary>
	/// It is the enumeration of the generic name for the Operating Systems considered here.
	/// </summary>
	public enum OSName
	{
		Unknown = 0,
		AIX = 1,
		AndroidWebOSPalmQNXBadaRIMMeeGoContiki = 2,
		Blackberry = 3,
		BlackBerry10 = 4,
		ChromiumOS = 5,
		FirefoxOS = 6,
		FreeBSDNetBSDOpenBSDPCBSDDragonFlyHaiku = 7,
		GNU = 8,
		HurdLinux = 9,
		iOS = 10,
		JoliUbuntuDebianSUSEGentooArchSlackwareFedoraMandrivaCentOSPCLinuxOSRedHatZenwalkLinpus = 11,
		MacOS = 12,
		MageiaVectorLinux = 13,
		Mint = 14,
		NintendoPlaystation = 15,
		Plan9MinixBeOSOS2AmigaOSMorphOSRISCOSOpenVMS = 16,
		SailfishOS = 17,
		Series40 = 18,
		Solaris1 = 19,
		Solaris2 = 20,
		Symbian = 21,
		Tizen = 22,
		UNIX = 23,
		Windows = 24,
		WindowsiTunes = 25,
		WindowsMobile = 26,
		WindowsPhone = 27,
		WindowsRT = 28,
		NintendoDS = 29,
		Playstation3 = 30,
		Default = 1000000
	}

	public static class OSNameExtensions
	{
		/// <summary>
		/// Gets a generic name to display for every value in the enumeration.
		/// </summary>
		/// <param name="source">The value of the enumeration.</param>
		/// <returns>A string with generic name to display.</returns>
		public static string GetDisplayName(this OSName source)
		{
			var sourceName = source.ToString();
			sourceName = source == OSName.Solaris1 || source == OSName.Solaris2
				? sourceName.Substring(0, sourceName.Length - 1)
				: sourceName;
			return ResourcesHelper.GetDisplayName(sourceName, Resources.DefaultResources.OSNamePrefix);
		}
	}
}
