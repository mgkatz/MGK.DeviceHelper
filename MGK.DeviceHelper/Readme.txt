*********************************
* Device Helper - Documentation *
*********************************

This project has been made on .NET Standard 2.0 using C# 7.1.

I was tired of third-party software complex to use or extremely big in size, so i created MGK.DeviceHelper to get some information very useful on web projects. The info is short but practical to solve any issue on web design.

The available information is the type of the device that is making the request, its operating system name and version and its browser name, version and major.

The information is obtained through the User Agent of the browser and using Regular Expressions and some logic to read it.

To get this information all you have to do is to declare a new instance of the Device class using the User Agent in its constructor.

The Device instance will give us three properties:
	1. Browser: this property is an instance of the Browser class and have three properties which type is string:
		1. Name
		2. Version
		3. Major
	2. DeviceType: this property is an enumeration defined on MGK.DeviceHelper.Enums.DeviceType and return the type of the device which is making the request.
	3. OS: this property is an instance of the OS (Operating System) class and have two properties which type is string:
		1. Name
		2. Version

For example, what i do to use it on my projects is the following:

	1. I create my own Controller class based on the System.Web.Mvc.Controller class.
	2. I override the Initialize method like this:

		protected override void Initialize(RequestContext requestContext)
		{
			base.Initialize(requestContext);

			var userAgent = GetUserAgent();
			var device = new MGK.DeviceHelper.Device(userAgent);

			ViewBag.UserAgent = userAgent;
			ViewBag.Device = device;
		}

		private string GetUserAgent()
		{
			return Request.ServerVariables["HTTP_USER_AGENT"];
		}

	   As you can see i obtain the user agent from the request and then i create the instance of Device using it. Finally i store both values in the ViewBag because in this way i am able to use it in any view.

And that's all. I hope that this library becomes useful for everyone.
