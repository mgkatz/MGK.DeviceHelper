namespace MGK.DeviceHelper.Enums
{
	/// <summary>
	/// It is the enumeration of the generic name for the Operating Systems considered here.
	/// </summary>
	public enum OSName
	{
		AIX = 1,
		AndroidWebOSPalmQNXBadaRIMMeeGoContiki = 2,
		Blackberry = 3,
		BlackBerry10 = 4,
		ChromiumOS = 5,
        Default = 0, //This is a fake for internal use of the library
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
		Unknown = 0,
		Windows = 24,
		WindowsiTunes = 25,
		WindowsMobile = 26,
		WindowsPhone = 27,
		WindowsRT = 28
	}

	public static class OSNameExtensions
	{
		/// <summary>
		/// Gets a generic name to display for every value in the enumaration.
		/// </summary>
		/// <param name="source">The value of the enumeration.</param>
		/// <returns>A string with generic name to display.</returns>
		public static string GetDisplayName(this OSName source)
		{
			switch (source)
			{
				case OSName.AndroidWebOSPalmQNXBadaRIMMeeGoContiki:
					return "Android/WebOS/Palm/QNX/Bada/RIM/MeeGo/Contiki";

				case OSName.BlackBerry10:
					return OSName.Blackberry.ToString();

				case OSName.ChromiumOS:
					return "Chromium OS";

				case OSName.FirefoxOS:
					return "Firefox OS";

				case OSName.FreeBSDNetBSDOpenBSDPCBSDDragonFlyHaiku:
					return "FreeBSD/NetBSD/OpenBSD/PC-BSD/DragonFly";

				case OSName.HurdLinux:
					return "Hurd/Linux";

				case OSName.JoliUbuntuDebianSUSEGentooArchSlackwareFedoraMandrivaCentOSPCLinuxOSRedHatZenwalkLinpus:
					return "Joli/Ubuntu/Debian/SUSE/Gentoo/Arch/Slackware/Fedora/Mandriva/CentOS/PCLinuxOS/RedHat/Zenwalk/Linpus";

				case OSName.MacOS:
					return "Mac OS";

				case OSName.MageiaVectorLinux:
					return "Mageia/VectorLinux";

				case OSName.NintendoPlaystation:
					return "Nintendo/Playstation";

				case OSName.Plan9MinixBeOSOS2AmigaOSMorphOSRISCOSOpenVMS:
					return "Plan9/Minix/BeOS/OS2/AmigaOS/MorphOS/RISCOS/OpenVMS";

				case OSName.SailfishOS:
					return "Sailfish OS";

				case OSName.Series40:
					return "Series 40";

                case OSName.Solaris1:
                case OSName.Solaris2:
                    return "Solaris";

				case OSName.WindowsiTunes:
					return "Windows (iTunes)";

				case OSName.WindowsMobile:
					return "Windows Mobile";

				case OSName.WindowsPhone:
					return "Windows Phone";

				case OSName.WindowsRT:
					return "Windows RT";

				default:
					return source.ToString();
			}
		}
	}
}
