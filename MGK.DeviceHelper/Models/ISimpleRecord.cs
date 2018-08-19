using System.Text.RegularExpressions;

namespace MGK.DeviceHelper.Models
{
	/// <summary>
	/// An interface that represents the record with the basic information of an object.
	/// </summary>
	public interface ISimpleRecord<T> where T : struct
    {
		#region Properties
		/// <summary>
		/// Gets the Regular Expression needed to understand the User Agent information.
		/// </summary>
		Regex Regex { get; }

		/// <summary>
		/// Gets the generic name for the type/s represented in the present record.
		/// </summary>
		T Name { get; }
		#endregion
	}
}
