namespace MGK.DeviceHelper
{
	/// <summary>
	/// An interface that represents the Browser.
	/// </summary>
	public interface IBrowser
	{
		/// <summary>
		/// Gets the major from the Browser version.
		/// </summary>
		string Major { get; }

		/// <summary>
		/// Gets the name of the Browser.
		/// </summary>
		string Name { get; }

		/// <summary>
		/// Gets the Version of the Browser.
		/// </summary>
		string Version { get; }
	}
}