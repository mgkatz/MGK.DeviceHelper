using MGK.DeviceHelper.Helpers;
using MGK.DeviceHelper.Models;
using System;
using System.Collections.Generic;
using System.IO;
using MGK.DeviceHelper.Enums;

namespace MGK.DeviceHelper.Console
{
	public class Program
	{
	    private static List<string> _userAgentsToTest = new List<string>()
	    {
	        "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:56.0) Gecko/20100101 Firefox/56.0", //It should be Firefox browser and Windows 10 OS
            "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/52.0.2743.116 Safari/537.36 Edge/15.15063", //It should be Edge browser and Windows 10 OS
            "Mozilla/5.0 (Windows NT 10.0; WOW64; Trident/7.0; rv:11.0) like Gecko", //It should be Internet Explorer 11 browser and Windows 10 OS
            "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/61.0.3163.100 Safari/537.36", //It should be Chrome browser and Windows 10 OS
            "Mozilla/5.0 (Windows Phone 10.0; Android 6.0.1; Microsoft; Lumia 950 XL Dual SIM) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/52.0.2743.116 Mobile Safari/537.36 Edge/15.15063", //It should be Edge browser and Windows 10 Phone OS on mobile phone
			"Mozilla/5.0 (Linux; Android 7.0; SAMSUNG SM-A310M Build/NRD90M) AppleWebKit/537.36 (KHTML, like Gecko) SamsungBrowser/5.4 Chrome/51.0.2704.106 Mobile Safari/537.36", //It should be Chrome browser and Android OS on mobile phone
			"Mozilla/5.0 (Linux; Android 5.1.1; Lenovo YT3-X50F Build/LMY47V) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/61.0.3163.98 Safari/537.36" //It should be Chrome browser and Android OS on tablet
		};

		public static void Main(string[] args)
		{
		    SerializationChecking<RegexBrowserRecordModel>("regexbrowserrecords.json", ModelType.Browser);

			System.Console.WriteLine();

		    SerializationChecking<RegexOSRecordModel>("regexosrecords.json", ModelType.OS);

			System.Console.WriteLine();

			UserAgentsChecking();

			System.Console.ReadKey();
		}

	    private static void SerializationChecking<T>(string fileName, ModelType modelType) where T : class
	    {
	        var model = new List<T>();

	        System.Console.WriteLine("Model list initialized.");

	        System.Console.WriteLine("Serialization Helper initialized.");

	        var jsonModel = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName));
	        System.Console.WriteLine($"Json file with the {modelType.ToString().ToLower()} records loaded.");

			//Change SerializationHelper from internal to public in order to test it.
			var regexModel = SerializationHelper.JsonDeserialize<RegexModel<T>>(jsonModel);
	        System.Console.WriteLine($"Json file with the {modelType.ToString()} records deserialized.");

	        model.AddRange(regexModel.Records);
	        System.Console.WriteLine($"{modelType.ToString()} records loaded into model.");
	        System.Console.WriteLine($"Count of {modelType.ToString()} records loaded: {model.Count}.");
        }

	    private static void UserAgentsChecking()
	    {
			foreach (var userAgent in _userAgentsToTest)
			{
				var device = new Device(userAgent);
				System.Console.WriteLine($"Browser detected: {device.Browser.Name} / Version: {device.Browser.Version} / Major: {device.Browser.Major}");
				System.Console.WriteLine($"OS detected: {device.OS.Name} / Version: {device.OS.Version}");
				System.Console.WriteLine($"Device type detected: {device.DeviceType.ToString()}");
				System.Console.WriteLine();
			}
		}
    }
}
