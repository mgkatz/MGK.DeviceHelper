using MGK.DeviceHelper.Enums;

namespace MGK.DeviceHelper
{
	/// <summary>
	/// An interface that represents the device information obtained by the User Agent.
	/// </summary>
	public interface IDevice
    {
		/// <summary>
		/// Gets the Browser information.
		/// </summary>
		Browser Browser { get; }

		/// <summary>
		/// Gets the type of the Device. For a list of devices type <see cref="Enums.DeviceType"/>.
		/// </summary>
		DeviceType DeviceType { get; }

		/// <summary>
		/// Gets the Operating System information.
		/// </summary>
		OS OS { get; }
    }
}