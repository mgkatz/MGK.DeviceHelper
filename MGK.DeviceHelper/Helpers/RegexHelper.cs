using MGK.DeviceHelper.Enums;
using MGK.DeviceHelper.Models;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace MGK.DeviceHelper.Helpers
{
	/// <summary>
	/// This is a helper to process the data of the User Agent using Regular Expressions.
	/// </summary>
	internal static class RegexHelper
    {
		#region Public methods
		/// <summary>
		/// Gets the value of a regular expression with some part of it replaced.
		/// </summary>
		/// <param name="pattern">The pattern for the regular expression.</param>
		/// <param name="oldValue">The string part of the value to replace.</param>
		/// <param name="newValue">The replacing string.</param>
		/// <returns>The value updated.</returns>
		public static string GetReplacedValue(string pattern, string oldValue, string newValue)
		{
			return GetReplacedValue(pattern, oldValue, newValue, RegexOptions.IgnoreCase);
		}

		/// <summary>
		/// Gets the value of a regular expression with some part of it replaced.
		/// </summary>
		/// <param name="pattern">The pattern for the regular expression.</param>
		/// <param name="oldValue">The string part of the value to replace.</param>
		/// <param name="newValue">The replacing string.</param>
		/// <param name="options">The options for the regular expression.</param>
		/// <returns>The value updated.</returns>
		public static string GetReplacedValue(string pattern, string oldValue, string newValue, RegexOptions options = RegexOptions.IgnoreCase)
		{
			return new Regex(pattern, options)
				.Replace(oldValue, newValue);
		}

		/// <summary>
		/// Gets the value of a regular expression.
		/// </summary>
		/// <param name="pattern">The pattern for the regular expression.</param>
		/// <param name="match">The match for the regular expression.</param>
		/// <returns>The value of the regular expression.</returns>
		public static string GetValue(string pattern, string match)
		{
			return GetValue(pattern, match, RegexOptions.None);
		}

		/// <summary>
		/// Gets the value of a regular expression.
		/// </summary>
		/// <param name="pattern">The pattern for the regular expression.</param>
		/// <param name="match">The match for the regular expression.</param>
		/// <param name="options">The options for the regular expression.</param>
		/// <returns></returns>
		public static string GetValue(string pattern, string match, RegexOptions options = RegexOptions.IgnoreCase)
		{
			return new Regex($@"{pattern}", options)
				.Match(match)
				.Value;
		}

		/// <summary>
		/// Gets the model with the data needed to obtain the information considered for the objects in this project.
		/// </summary>
		/// <typeparam name="T">The type of the object.</typeparam>
		/// <typeparam name="TName">The name of the object.</typeparam>
		/// <param name="modelType">The type of the model to obtain.</param>
		/// <returns>A list of records that represent the model for the object.</returns>
		public static IEnumerable<ISimpleRecord<TName>> GetRegExModel<TModel, TRecord, TName>(ModelType modelType)
			where TModel : ComplexModel<TRecord, TName>
			where TRecord : class, IComplexRecord<TName>
			where TName : struct
		{
			var stream = AssemblyHelper.GetResourceFromAssembly(modelType.GetModelFileName());

			using (var reader = new StreamReader(stream))
			{
				var regexModel = SerializationHelper.JsonDeserialize<TModel>(reader.ReadToEnd());
				return regexModel.GetSummarizedRecords();
			}
		}
		#endregion
	}
}
