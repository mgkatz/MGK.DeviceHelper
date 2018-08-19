using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace MGK.DeviceHelper.Helpers
{
	/// <summary>
	/// This is a helper to serialize/deserialze the Json data but it could be used with any kind of data which has a class with a DataContract and DataMembers.
	/// </summary>
	public static class SerializationHelper
	{
		/// <summary>
		/// Serialize a class to a string. In this project it is used to serialize Json data.
		/// </summary>
		/// <typeparam name="T">The type of entity to serialize.</typeparam>
		/// <param name="entity">The entity to serialize.</param>
		public static string JsonSerialize<T>(T entity) where T : class
		{
			var ms = new MemoryStream();
			var serializer = new DataContractJsonSerializer(typeof(T));
			serializer.WriteObject(ms, entity);

			ms.Position = 0;
			var sr = new StreamReader(ms);
			var serializedData = sr.ReadToEnd();

			return serializedData;
		}

		/// <summary>
		/// Deserialize a string to a class. In this project it is used to deserialize Json data.
		/// </summary>
		/// <typeparam name="T">The entity to return.</typeparam>
		/// <param name="serializedData">The data to deserialize.</param>
		public static T JsonDeserialize<T>(string serializedData) where T : class
		{
			T entity = (T)Activator.CreateInstance<T>();
			var ms = new MemoryStream(Encoding.UTF8.GetBytes(serializedData));
			var serializer = new DataContractJsonSerializer(entity.GetType());
			entity = serializer.ReadObject(ms) as T;
			ms.Close();

			return entity;
		}
	}
}
