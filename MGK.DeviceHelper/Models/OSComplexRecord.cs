﻿using MGK.DeviceHelper.Enums;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;

namespace MGK.DeviceHelper.Models
{
	/// <summary>
	/// It represents the record with the basics to read the Operating System information from the User Agent. Here the information of a record is taken from the Json file regexosrecords.json.
	/// </summary>
	[DataContract]
	public class OSComplexRecord : IComplexRecord<OSName>
    {
		#region Constructors
		/// <summary>
		/// Creates an instance of RegexOSRecordModel class.
		/// </summary>
		public OSComplexRecord()
		{
		}

		/// <summary>
		/// Creates an instance of RegexOSRecordModel class based on the parameters.
		/// </summary>
		/// <param name="expression">The regular expression which will be used with against the User Agent.</param>
		/// <param name="name">The generic OS name to which belongs the regular expression.</param>
		/// <param name="options">The Regex options.</param>
		public OSComplexRecord(string expression, OSName name, RegexOptions options)
		{
			Expression = expression;
			Name = name;
			Options = options;
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets the text for the Regex object needed to understand the User Agent information about the Operating System.
		/// </summary>
		[DataMember]
		public string Expression { get; private set; }

		/// <summary>
		/// Gets the Regular Expression needed to understand the User Agent information about the Operating System.
		/// </summary>
		public Regex Regex => new Regex(Expression, Options);

		/// <summary>
		/// Gets the generic name for the Operating System/s represented in the present record information about the Operating System.
		/// </summary>
		[DataMember]
		public OSName Name { get; private set; }

		/// <summary>
		/// Gets the Regular Expression options for the Regex object needed to understand the User Agent information about the Operating System.
		/// </summary>
		[DataMember]
		public RegexOptions Options { get; private set; }
		#endregion

		#region Equals and GetHashCode
		/// <summary>
		/// Determines whether the specified object is equal to the current object.
		/// </summary>
		/// <param name="other">The object.</param>
		/// <returns>True if the specified object is equal to the current object; otherwise, false.</returns>
		protected bool Equals(OSComplexRecord other)
		{
			if (other == null)
				return false;

			return string.Equals(Expression, other.Expression) &&
				Regex == other.Regex &&
				Equals(Name, other.Name) &&
				Equals(Options, other.Options);
		}

		/// <summary>
		/// Determines whether the specified object is equal to the current object.
		/// </summary>
		/// <param name="obj">The object.</param>
		/// <returns>True if the specified object is equal to the current object; otherwise, false.</returns>
		public override bool Equals(object obj)
		{
			return Equals(obj as OSComplexRecord);
		}

		/// <summary>
		/// Serves as the default hash function.
		/// </summary>
		/// <returns>A hash code for the current object.</returns>
		public override int GetHashCode()
		{
			unchecked
			{
				var hashCode = (Expression != null ? Expression.GetHashCode() : 0);
				hashCode = (hashCode * 397) ^ (Regex != null ? Regex.GetHashCode() : 0);
				hashCode = (hashCode * 397) ^ Name.GetHashCode();
				hashCode = (hashCode * 397) ^ Options.GetHashCode();
				return hashCode;
			}
		}
		#endregion
	}
}