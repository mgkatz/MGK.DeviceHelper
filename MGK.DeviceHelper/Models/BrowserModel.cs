using MGK.DeviceHelper.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace MGK.DeviceHelper.Models
{
	[DataContract]
	public class BrowserModel : ComplexModel<BrowserComplexRecord, BrowserName>
	{
		#region Constructors
		/// <summary>
		/// Creates an instance of the Browser model.
		/// </summary>
		public BrowserModel() : base()
		{
		}

		/// <summary>
		/// Creates an instance of the Browser model with its records.
		/// </summary>
		/// <param name="records">The records to assign to the model.</param>
		protected BrowserModel(BrowserComplexRecord[] records) : base(records)
		{
		}
		#endregion

		#region Public methods
		public override IEnumerable<ISimpleRecord<BrowserName>> GetSummarizedRecords()
		{
			return Records.Select(x => new BrowserSimpleRecord(x.Name, x.Regex));
		}
		#endregion
	}
}
