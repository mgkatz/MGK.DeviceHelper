namespace MGK.DeviceHelper
{
	/// <summary>
	/// An interface that represents anything that supports versioning.
	/// </summary>
    public interface IHasVersion
    {
		/// <summary>
		/// Gets the build number from the version.
		/// </summary>
		string Build { get; }

		/// <summary>
		/// Gets the major from the version.
		/// </summary>
		string Major { get; }

		/// <summary>
		/// Gets the minor from the version.
		/// </summary>
		string Minor { get; }

		/// <summary>
		/// Gets the Version.
		/// </summary>
		string Version { get; }
	}
}
