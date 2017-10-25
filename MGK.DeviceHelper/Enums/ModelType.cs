namespace MGK.DeviceHelper.Enums
{
	/// <summary>
	/// It is the enumeration of the Json models considered here.
	/// </summary>
	public enum ModelType
	{
		Browser = 1,
		OS = 2
	}

	public static class ModelTypeExtensions
	{
		/// <summary>
		/// Gets the file name of the Json files.
		/// </summary>
		/// <param name="source">The value of the enumeration.</param>
		/// <returns>The file name without the path.</returns>
		public static string GetModelFileName(this ModelType source)
		{
			switch (source)
			{
				case ModelType.Browser:
					return "regexbrowserrecords.json";

				default:
					return "regexosrecords.json";
			}
		}
	}
}
