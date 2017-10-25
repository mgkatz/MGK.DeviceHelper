namespace MGK.DeviceHelper
{
	/// <summary>
	/// An interface that represents the Operating System.
	/// </summary>
	public interface IOS
	{
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