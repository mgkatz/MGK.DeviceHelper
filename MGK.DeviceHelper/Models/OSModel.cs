using MGK.DeviceHelper.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace MGK.DeviceHelper.Models
{
	[DataContract]
	public class OSModel : ComplexModel<OSComplexRecord, OSName>
	{
		#region Constructors
		/// <summary>
		/// Creates an instance of the OS model.
		/// </summary>
		public OSModel() : base()
		{
		}

		/// <summary>
		/// Creates an instance of the OS model with its records.
		/// </summary>
		/// <param name="records">The records to assign to the model.</param>
		protected OSModel(OSComplexRecord[] records) : base(records)
		{
		}
		#endregion

		#region Public methods
		public override IEnumerable<ISimpleRecord<OSName>> GetSummarizedRecords()
		{
			return Records.Select(x => new OSSimpleRecord(x.Name, x.Regex));
		}
		#endregion
	}
}
