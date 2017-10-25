using System.Text.RegularExpressions;

namespace MGK.DeviceHelper.Models
{
	/// <summary>
	/// An interface that represents the model with the records.
	/// </summary>
	/// <typeparam name="T">A class that represents a RegexOSRecordModel or a RegexBrowserRecordModel.</typeparam>
	public interface IRegexModel<T> where T : class
    {
		#region Properties
		/// <summary>
		/// Gets the records with the data needed to get the Operating System or the Browser information.
		/// </summary>
		T[] Records { get; }
		#endregion
	}
}
