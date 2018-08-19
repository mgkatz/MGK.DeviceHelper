using MGK.DeviceHelper.Enums;
using MGK.DeviceHelper.Helpers;
using MGK.DeviceHelper.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Path = System.IO.Path;

namespace MGK.DeviceHelper.UserAgentTest
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			if (!string.IsNullOrWhiteSpace(TxtUserAgent.Text))
			{
				var device = new Device(TxtUserAgent.Text);
				TxtDeviceType.Text = device.DeviceType.ToString();
				TxtOSName.Text = device.OS.Name;
				TxtOSVersion.Text = device.OS.Version;
				TxtBrowserName.Text = device.Browser.Name;
				TxtBrowserVersion.Text = device.Browser.Version;
			}
		}

		private void CntMainOptions_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (sender is TabControl tc)
			{
				if (tc.SelectedItem is TabItem tcItem)
				{
					if(tcItem.Name.Equals(TabSingleCheck.Name, StringComparison.InvariantCulture))
					{
						FrmUserAgentTest.Height = 429;
						FrmUserAgentTest.Width = 358;
					}
					else
					{
						FrmUserAgentTest.Height = 644;
						FrmUserAgentTest.Width = 1074;
					}
				}
			}
		}
	}
}
