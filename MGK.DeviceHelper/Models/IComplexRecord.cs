using System.Text.RegularExpressions;

namespace MGK.DeviceHelper.Models
{
	/// <summary>
	/// An interface that represents the record with the basics to read the information from the User Agent.
	/// </summary>
	public interface IComplexRecord<T> : ISimpleRecord<T> where T : struct
    {
		#region Properties
		/// <summary>
		/// Gets the text for the Regex object needed to understand the User Agent information.
		/// </summary>
		string Expression { get; }

		/// <summary>
		/// Gets the Regular Expression options for the Regex object needed to understand the User Agent information.
		/// </summary>
		RegexOptions Options { get; }
		#endregion
	}
}
