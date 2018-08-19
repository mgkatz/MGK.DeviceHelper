using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MGK.DeviceHelper.Models
{
	/// <summary>
	/// It represents the model with the records taken from the Json file regexosrecords.json when you are getting Operating System information or the Json file regexbrowserrecords.json when you are getting Browser information.
	/// </summary>
	/// <typeparam name="T">A class that represents a RegexOSRecordModel or a RegexBrowserRecordModel.</typeparam>
	[DataContract]
	public abstract class ComplexModel<T, TName> : IModel<T>
		where T : class, IComplexRecord<TName>
		where TName : struct
	{
		#region Constructors
		/// <summary>
		/// Creates an instance of RegexModel class.
		/// </summary>
		protected ComplexModel()
		{
		}

		/// <summary>
		/// Creates an instance of RegexModel class based on the parameters.
		/// </summary>
		/// <param name="records">The records to assign to the model.</param>
		protected ComplexModel(T[] records)
		{
			Records = records;
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets the records with the data needed to get the Operating System or the Browser information.
		/// </summary>
		[DataMember]
		public T[] Records { get; private set; }
		#endregion

		#region Public methods
		/// <summary>
		/// Gets the list of records summarized (only the name and regex).
		/// </summary>
		/// <returns></returns>
		public abstract IEnumerable<ISimpleRecord<TName>> GetSummarizedRecords();
		#endregion

		#region Equals and GetHashCode
		/// <summary>
		/// Determines whether the specified object is equal to the current object.
		/// </summary>
		/// <param name="other">The object.</param>
		/// <returns>True if the specified object is equal to the current object; otherwise, false.</returns>
		protected bool Equals(ComplexModel<T, TName> other)
		{
			if (other == null)
				return false;

			return Equals(Records, other.Records);
		}

		/// <summary>
		/// Determines whether the specified object is equal to the current object.
		/// </summary>
		/// <param name="obj">The object.</param>
		/// <returns>True if the specified object is equal to the current object; otherwise, false.</returns>
		public override bool Equals(object obj)
		{
			return Equals(obj as ComplexModel<T, TName>);
		}

		/// <summary>
		/// Serves as the default hash function.
		/// </summary>
		/// <returns>A hash code for the current object.</returns>
		public override int GetHashCode()
		{
			unchecked
			{
				var hashCode = (Records != null ? Records.GetHashCode() : 0);
				return hashCode;
			}
		}
		#endregion
	}
}
