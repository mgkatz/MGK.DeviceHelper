using MGK.DeviceHelper.Enums;
using System;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;

namespace MGK.DeviceHelper.Models
{
	/// <summary>
	/// It represents the record with the basics to read the Browser information from the User Agent. Here the information of a record is taken from the Json file regexbrowserrecords.json.
	/// </summary>
	[DataContract]
	public class RegexBrowserRecordModel : IRegexRecordModel
    {
		#region Properties
		/// <summary>
		/// Gets the text for the Regex object needed to understand the User Agent information about the Browser.
		/// </summary>
		[DataMember]
		public string RegexExpression { get; private set; }

		/// <summary>
		/// Gets the Regular Expression needed to understand the User Agent information about the Browser.
		/// </summary>
		public Regex Regex => new Regex(RegexExpression, RegexOptions);

		/// <summary>
		/// Gets the generic name for the Browser/s represented in the present record.
		/// </summary>
		[DataMember]
		public BrowserName RegexName { get; private set; }

		/// <summary>
		/// Gets the Regular Expression options for the Regex object needed to understand the User Agent information about the Browser.
		/// </summary>
		[DataMember]
		public RegexOptions RegexOptions { get; private set; }
		#endregion

		#region Constructors
		/// <summary>
		/// Creates an instance of RegexBrowserRecordModel class.
		/// </summary>
		public RegexBrowserRecordModel()
		{
		}

		/// <summary>
		/// Creates an instance of RegexBrowserRecordModel class based on the parameters.
		/// </summary>
		/// <param name="regexExpression">The regular expression which will be used with against the User Agent.</param>
		/// <param name="regexName">The generic Browser name to which belongs the regular expression.</param>
		/// <param name="regexOptions">The Regex options.</param>
		public RegexBrowserRecordModel(string regexExpression, BrowserName regexName, RegexOptions regexOptions)
		{
			RegexExpression = regexExpression;
			RegexName = regexName;
			RegexOptions = regexOptions;
		}
		#endregion

		#region Equals and GetHashCode
		/// <summary>
		/// Determines whether the specified object is equal to the current object.
		/// </summary>
		/// <param name="other">The object.</param>
		/// <returns>True if the specified object is equal to the current object; otherwise, false.</returns>
		protected bool Equals(RegexBrowserRecordModel other)
		{
			if (other == null)
				return false;

			return string.Equals(RegexExpression, other.RegexExpression) &&
				Regex == other.Regex &&
				Equals(RegexName, other.RegexName) &&
				Equals(RegexOptions, other.RegexOptions);
		}

		/// <summary>
		/// Determines whether the specified object is equal to the current object.
		/// </summary>
		/// <param name="obj">The object.</param>
		/// <returns>True if the specified object is equal to the current object; otherwise, false.</returns>
		public override bool Equals(object obj)
		{
			return Equals(obj as RegexBrowserRecordModel);
		}

		/// <summary>
		/// Serves as the default hash function.
		/// </summary>
		/// <returns>A hash code for the current object.</returns>
		public override int GetHashCode()
		{
			unchecked
			{
				var hashCode = (RegexExpression != null ? RegexExpression.GetHashCode() : 0);
				hashCode = (hashCode * 397) ^ (Regex != null ? Regex.GetHashCode() : 0);
				hashCode = (hashCode * 397) ^ RegexName.GetHashCode();
				hashCode = (hashCode * 397) ^ RegexOptions.GetHashCode();
				return hashCode;
			}
		}
		#endregion
	}
}
