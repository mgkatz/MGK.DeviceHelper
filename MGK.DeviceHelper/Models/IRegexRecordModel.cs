using System.Text.RegularExpressions;

namespace MGK.DeviceHelper.Models
{
	/// <summary>
	/// An interface that represents the record with the basics to read the information from the User Agent.
	/// </summary>
	public interface IRegexRecordModel
    {
		#region Properties
		/// <summary>
		/// Gets the text for the Regex object needed to understand the User Agent information.
		/// </summary>
		string RegexExpression { get; }

		/// <summary>
		/// Gets the Regular Expression needed to understand the User Agent information.
		/// </summary>
		Regex Regex { get; }

		/// <summary>
		/// Gets the Regular Expression options for the Regex object needed to understand the User Agent information.
		/// </summary>
		RegexOptions RegexOptions { get; }
		#endregion
	}
}
