using MGK.DeviceHelper.Enums;
using System.Text.RegularExpressions;

namespace MGK.DeviceHelper.Models
{
	public class OSSimpleRecord : ISimpleRecord<OSName>
	{
		#region Constructors
		public OSSimpleRecord(OSName name, Regex regex)
		{
			Name = name;
			Regex = regex;
		}
		#endregion

		#region Properties
		public Regex Regex { get; }

		public OSName Name { get; }
		#endregion
	}
}
