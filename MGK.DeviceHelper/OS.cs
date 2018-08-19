using MGK.DeviceHelper.Enums;
using MGK.DeviceHelper.Helpers;
using System;
using System.Text.RegularExpressions;

namespace MGK.DeviceHelper
{
	/// <summary>
	/// It represents the Operating System.
	/// </summary>
    public class OS : IHasName, IHasVersion
	{
		#region Variables and private objects
		private OSName _osName;
		private Regex _regex;
		private string _userAgent;
		#endregion

		#region Constructors
		/// <summary>
		/// Creates an instance of OS class defining the OS information as Unknown.
		/// </summary>
		public OS()
		{
			Name = OSName.Unknown.GetDisplayName();
			Version = "";
		}

		/// <summary>
		/// Creates an instance of OS class defining the OS information through the parameters.
		/// </summary>
		/// <param name="userAgent">The User Agent.</param>
		/// <param name="regex">The Regex needed to understand the User Agent.</param>
		/// <param name="osName">The generic OS name to which belongs the User Agent.</param>
		public OS(string userAgent, Regex regex, OSName osName)
		{
			_osName = osName;
			_regex = regex;
			_userAgent = userAgent;
			LoadOSInfo();
		}
		#endregion

		#region Properties
		public string Build => VersionHelper.GetVersionPart(VersionPart.RevisionOrBuild, Version);

		public string Major => VersionHelper.GetVersionPart(VersionPart.Major, Version);

		public string Minor => VersionHelper.GetVersionPart(VersionPart.Minor, Version);

		/// <summary>
		/// Gets the name of Operating System.
		/// </summary>
		public string Name { get; private set; }

		/// <summary>
		/// Gets the version of Operating System.
		/// </summary>
		public string Version { get; private set; }
		#endregion

		#region Private methods
		private void LoadOSInfo()
		{
			if (_regex.IsMatch(_userAgent))
			{
				var match = _regex.Match(_userAgent);
				var osInfo = OSHelper.GetOSInfo(_osName, match);
				Name = osInfo.Name;
				Version = osInfo.Version;
			}
		}
		#endregion

		#region Equals and GetHashCode
		/// <summary>
		/// Determines whether the specified object is equal to the current object.
		/// </summary>
		/// <param name="other">The object.</param>
		/// <returns>True if the specified object is equal to the current object; otherwise, false.</returns>
		protected bool Equals(OS other)
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
			return Equals(obj as OS);
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
