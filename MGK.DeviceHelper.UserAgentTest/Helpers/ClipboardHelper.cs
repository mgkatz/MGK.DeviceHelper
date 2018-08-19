using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace MGK.DeviceHelper.UserAgentTest.Helpers
{
	public static class ClipboardHelper
	{
		public delegate IEnumerable<string> ParseFormat(string value);

		public static IEnumerable<IEnumerable<string>> Parse()
		{
			var clipboardData = new List<IEnumerable<string>>();
			object clipboardRawData = null;
			ParseFormat parseFormat = null;

			// get the data and set the parsing method based on the format
			// currently works with CSV and Text DataFormats            
			var dataObj = Clipboard.GetDataObject();

			if ((clipboardRawData = dataObj.GetData(DataFormats.CommaSeparatedValue)) != null)
			{
				parseFormat = ParseCsvFormat;
			}
			else if ((clipboardRawData = dataObj.GetData(DataFormats.Text)) != null)
			{
				parseFormat = ParseTextFormat;
			}

			if (parseFormat != null)
			{
				var rawDataStr = clipboardRawData as string;

				if (rawDataStr == null && clipboardRawData is MemoryStream)
				{
					// cannot convert to a string so try a MemoryStream
					var ms = clipboardRawData as MemoryStream;
					var sr = new StreamReader(ms);
					rawDataStr = sr.ReadToEnd();
				}

				if (rawDataStr == null)
				{
					throw new Exception($"The data cannot be converted to a supported format.\r\nThe data was:\r\n{clipboardRawData}");
				}

				var rows = rawDataStr.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

				if (rows?.Length > 0)
				{
					clipboardData = new List<IEnumerable<string>>();
					foreach (var row in rows)
					{
						clipboardData.Add(parseFormat(row));
					}
				}
				else
				{
					throw new Exception("Unable to parse row data. Possibly there aren't rows.");
				}
			}

			return clipboardData;
		}

		public static IEnumerable<string> ParseCsvFormat(string value)
		{
			return ParseCsvOrTextFormat(value, true);
		}

		public static IEnumerable<string> ParseTextFormat(string value)
		{
			return ParseCsvOrTextFormat(value, false);
		}

		private static IEnumerable<string> ParseCsvOrTextFormat(string value, bool isCsv)
		{
			List<string> outputList = new List<string>();
			var separator = isCsv ? ',' : '\t';
			var startIndex = 0;
			var endIndex = 0;

			for (var i = 0; i < value.Length; i++)
			{
				if (value[i].Equals(separator))
				{
					outputList.Add(value.Substring(startIndex, endIndex - startIndex));
					startIndex = endIndex + 1;
					endIndex = startIndex;
				}
				else if (value[i].Equals('\"') && isCsv)
				{
					// skip until the ending quotes
					i++;

					if (i >= value.Length)
					{
						throw new FormatException($"Value: {value} had a format exception.");
					}

					while (!value[i].Equals('\"') && i < value.Length) i++;

					endIndex = i;
				}
				else if (i + 1 == value.Length)
				{
					// add the last value
					outputList.Add(value.Substring(startIndex));
					break;
				}
				else
				{
					endIndex++;
				}
			}

			return outputList;
		}
	}
}
