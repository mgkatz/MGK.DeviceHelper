using MGK.DeviceHelper.Enums;
using System.Text.RegularExpressions;

namespace MGK.DeviceHelper.Models
{
	public class BrowserSimpleRecord : ISimpleRecord<BrowserName>
	{
		#region Constructors
		public BrowserSimpleRecord(BrowserName name, Regex regex)
		{
			Name = name;
			Regex = regex;
		}
		#endregion

		#region Properties
		public Regex Regex { get; }

		public BrowserName Name { get; }
		#endregion
	}
}
