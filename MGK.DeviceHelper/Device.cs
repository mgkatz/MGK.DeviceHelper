using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using MGK.DeviceHelper.Enums;
using MGK.DeviceHelper.Helpers;
using MGK.DeviceHelper.Models;

namespace MGK.DeviceHelper
{
	/// <summary>
	/// It represents the device information obtained by the User Agent.
	/// </summary>
    public class Device : IDevice
    {
        #region Private variables and objects
        private readonly string _userAgent;

        private readonly IEnumerable<BrowserSimpleRecord> _browserModel =
            BrowserHelper.GetBrowserModel();

        private readonly IEnumerable<OSSimpleRecord> _osModel =
            OSHelper.GetOSModel();
		#endregion

		#region Constructors
		/// <summary>
		/// Creates an instance of Device class through the User Agent.
		/// </summary>
		/// <param name="userAgent">The User Agent.</param>
		public Device(string userAgent)
        {
            _userAgent = userAgent;
			DeviceType = GetDeviceType();
            Browser = GetBrowser();
            OS = GetOS();
			Origin = GetOrigin();
        }
		#endregion

		#region Properties
		public Browser Browser { get; }

		public DeviceType DeviceType { get; }

		public UserAgentOrigin Origin { get; }

		public OS OS { get; }
		#endregion

		#region Private methods
		private Browser GetBrowser()
		{
			var browserRecord = _browserModel.FirstOrDefault(b => b.Regex.IsMatch(_userAgent));
			return browserRecord == null
				? new Browser()
				: new Browser(_userAgent, browserRecord.Regex, browserRecord.Name);
		}

		private DeviceType GetDeviceType()
		{
			// Check if user agent is a smart TV - http://goo.gl/FocDk
			if (Regex.IsMatch(_userAgent, @"GoogleTV|SmartTV|Internet.TV|NetCast|NETTV|AppleTV|boxee|Kylo|Roku|DLNADOC|CE\-HTML", RegexOptions.IgnoreCase))
				return DeviceType.TV;

			// Check if user agent is a TV Based Gaming Console
			if (Regex.IsMatch(_userAgent, "Xbox|PLAYSTATION.3|Wii", RegexOptions.IgnoreCase))
				return DeviceType.GamingConsole;

			// Check if user agent is a Tablet (iPad, Android tablet, Kindle / Kindle Fire o pre Android 3.0 Tablet
			if (((Regex.IsMatch(_userAgent, "iP(a|ro)d", RegexOptions.IgnoreCase) || (Regex.IsMatch(_userAgent, "tablet", RegexOptions.IgnoreCase)) && (!Regex.IsMatch(_userAgent, "RX-34", RegexOptions.IgnoreCase)) || (Regex.IsMatch(_userAgent, "FOLIO", RegexOptions.IgnoreCase))))
				|| ((Regex.IsMatch(_userAgent, "Linux", RegexOptions.IgnoreCase)) && (Regex.IsMatch(_userAgent, "Android", RegexOptions.IgnoreCase)) && (!Regex.IsMatch(_userAgent, "Fennec|mobi|HTC.Magic|HTCX06HT|Nexus.One|SC-02B|fone.945", RegexOptions.IgnoreCase)))
				|| ((Regex.IsMatch(_userAgent, "Kindle", RegexOptions.IgnoreCase)) || (Regex.IsMatch(_userAgent, "Mac.OS", RegexOptions.IgnoreCase)) && (Regex.IsMatch(_userAgent, "Silk", RegexOptions.IgnoreCase)))
				|| ((Regex.IsMatch(_userAgent, @"GT-P10|SC-01C|SHW-M180S|SGH-T849|SCH-I800|SHW-M180L|SPH-P100|SGH-I987|zt180|HTC(.Flyer|\\_Flyer)|Sprint.ATP51|ViewPad7|pandigital(sprnova|nova)|Ideos.S7|Dell.Streak.7|Advent.Vega|A101IT|A70BHT|MID7015|Next2|nook", RegexOptions.IgnoreCase)) || (Regex.IsMatch(_userAgent, "MB511", RegexOptions.IgnoreCase)) && (Regex.IsMatch(_userAgent, "RUTEM", RegexOptions.IgnoreCase))))
				return DeviceType.Tablet;

			// Check if user agent is unique Mobile User Agent
			if (((Regex.IsMatch(_userAgent, "BOLT|Fennec|Iris|Maemo|Minimo|Mobi|mowser|NetFront|Novarra|Prism|RX-34|Skyfire|Tear|XV6875|XV6975|Google.Wireless.Transcoder", RegexOptions.IgnoreCase)))
				|| ((Regex.IsMatch(_userAgent, "Opera", RegexOptions.IgnoreCase)) && (Regex.IsMatch(_userAgent, "Windows.NT.5", RegexOptions.IgnoreCase)) && (Regex.IsMatch(_userAgent, @"HTC|Xda|Mini|Vario|SAMSUNG\-GT\-i8000|SAMSUNG\-SGH\-i9", RegexOptions.IgnoreCase))))
				return DeviceType.Mobile;

			// Check if user agent is Desktop (Windows, Mac, Linux, Solaris / SunOS / BSD o BOT/Crawler/Spider)
			if (((Regex.IsMatch(_userAgent, "Windows.(NT|XP|ME|9)")) && (!Regex.IsMatch(_userAgent, "Phone", RegexOptions.IgnoreCase)) || (Regex.IsMatch(_userAgent, "Win(9|.9|NT)", RegexOptions.IgnoreCase)))
				|| ((Regex.IsMatch(_userAgent, "Macintosh|PowerPC", RegexOptions.IgnoreCase)) && (!Regex.IsMatch(_userAgent, "Silk", RegexOptions.IgnoreCase)))
				|| ((Regex.IsMatch(_userAgent, "Linux", RegexOptions.IgnoreCase)) && (Regex.IsMatch(_userAgent, "X11", RegexOptions.IgnoreCase)))
				|| ((Regex.IsMatch(_userAgent, "Solaris|SunOS|BSD", RegexOptions.IgnoreCase)))
				|| ((Regex.IsMatch(_userAgent, "Bot|Crawler|Spider|Yahoo|ia_archiver|Covario-IDS|findlinks|DataparkSearch|larbin|Mediapartners-Google|NG-Search|Snappy|Teoma|Jeeves|TinEye", RegexOptions.IgnoreCase)) && (!Regex.IsMatch(_userAgent, "Mobile", RegexOptions.IgnoreCase))))
				return DeviceType.Desktop;

			return DeviceType.Unknown;
		}

		private UserAgentOrigin GetOrigin()
		{
			var browserRecord = _browserModel.FirstOrDefault(b => b.Regex.IsMatch(_userAgent));
			var deviceType = GetDeviceType();
			var origin = UserAgentHelper.GetOrigin(deviceType, browserRecord.Name);

			return origin;
		}

		private OS GetOS()
        {
			foreach (var osRecord in _osModel)
			{
				if (osRecord.Regex.IsMatch(_userAgent))
				{
					return new OS(_userAgent, osRecord.Regex, osRecord.Name);
				}
			}

			return new OS();
		}
		#endregion

		#region Equals and GetHashCode
		/// <summary>
		/// Determines whether the specified object is equal to the current object.
		/// </summary>
		/// <param name="other">The object.</param>
		/// <returns>True if the specified object is equal to the current object; otherwise, false.</returns>
		protected bool Equals(Device other)
		{
			if (other == null)
				return false;

			return Browser.Equals(other.Browser) &&
				DeviceType == other.DeviceType &&
				OS.Equals(other.OS) &&
				Origin == other.Origin;
		}

		/// <summary>
		/// Determines whether the specified object is equal to the current object.
		/// </summary>
		/// <param name="obj">The object.</param>
		/// <returns>True if the specified object is equal to the current object; otherwise, false.</returns>
		public override bool Equals(object obj)
		{
			return Equals(obj as Device);
		}

		/// <summary>
		/// Serves as the default hash function.
		/// </summary>
		/// <returns>A hash code for the current object.</returns>
		public override int GetHashCode()
		{
			unchecked
			{
				var hashCode = (Browser != null ? Browser.GetHashCode() : 0);
				hashCode = (hashCode * 397) ^ DeviceType.GetHashCode();
				hashCode = (hashCode * 397) ^ (OS != null ? OS.GetHashCode() : 0);
				hashCode = (hashCode * 397) ^ Origin.GetHashCode();
				return hashCode;
			}
		}
		#endregion
	}
}
