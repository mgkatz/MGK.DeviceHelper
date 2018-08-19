using MGK.DeviceHelper.Enums;
using MGK.DeviceHelper.Helpers;
using System;
using System.Text.RegularExpressions;

namespace MGK.DeviceHelper
{
	/// <summary>
	/// It represents the Browser.
	/// </summary>
	public class Browser : IHasName, IHasVersion
	{
		#region Variables and private objects
		private BrowserName _browserName;
		private Regex _regex;
		private string _userAgent;
		#endregion

		#region Constructors
		/// <summary>
		/// Creates an instance of Browser class defining the Browser information as Unknown.
		/// </summary>
		public Browser()
		{
			LoadBrowserInfo();
		}

		/// <summary>
		/// Creates an instance of Browser class defining the Browser information through the parameters.
		/// </summary>
		/// <param name="userAgent">The User Agent.</param>
		/// <param name="regex">The Regex needed to understand the User Agent.</param>
		/// <param name="browserName">The generic Browser name to which belongs the User Agent.</param>
		public Browser(string userAgent, Regex regex, BrowserName browserName) : this()
		{
			_browserName = browserName;
			_regex = regex;
			_userAgent = userAgent;
			LoadBrowserInfo();
		}
		#endregion

		#region Properties
		public string Build => VersionHelper.GetVersionPart(VersionPart.RevisionOrBuild, Version);

		public string Major => VersionHelper.GetVersionPart(VersionPart.Major, Version);

		public string Minor => VersionHelper.GetVersionPart(VersionPart.Minor, Version);

		public string Name { get; private set; }

		public string Version { get; private set; }
		#endregion

		#region Private methods
		private void LoadBrowserInfo()
		{
			if (!string.IsNullOrWhiteSpace(_userAgent) && _regex.IsMatch(_userAgent))
			{
				var match = _regex.Match(_userAgent);
				var browserInfo = BrowserHelper.GetBrowserInfo(_browserName, match);
				Name = browserInfo.Name;
				Version = browserInfo.Version;
			}
			else
			{
				Name = BrowserName.Unknown.GetDisplayName();
				Version = string.Empty;
			}
		}
		#endregion

		#region Equals and GetHashCode
		/// <summary>
		/// Determines whether the specified object is equal to the current object.
		/// </summary>
		/// <param name="other">The object.</param>
		/// <returns>True if the specified object is equal to the current object; otherwise, false.</returns>
		protected bool Equals(Browser other)
		{
			if (other == null)
				return false;

			return string.Equals(Name, other.Name) &&
				string.Equals(Version, other.Version);
		}

		/// <summary>
		/// Determines whether the specified object is equal to the current object.
		/// </summary>
		/// <param name="obj">The object.</param>
		/// <returns>True if the specified object is equal to the current object; otherwise, false.</returns>
		public override bool Equals(object obj)
		{
			return Equals(obj as Browser);
		}

		/// <summary>
		/// Serves as the default hash function.
		/// </summary>
		/// <returns>A hash code for the current object.</returns>
		public override int GetHashCode()
		{
			unchecked
			{
				var hashCode = (Name != null ? Name.GetHashCode() : 0);
				hashCode = (hashCode * 397) ^ (Version != null ? Version.GetHashCode() : 0);
				return hashCode;
			}
		}
		#endregion
	}
}
