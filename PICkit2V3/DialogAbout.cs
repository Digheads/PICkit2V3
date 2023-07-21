using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;

namespace PICkit2V3
{
	public partial class DialogAbout : Form
	{
		public DialogAbout()
		{
			InitializeComponent();
			displayAppVer.Text = Assembly.GetExecutingAssembly().GetName().Version.ToString();
			displayDevFileVer.Text = PICkitFunctions.DeviceFileVersion;
			displayPk2FWVer.Text = PICkitFunctions.FirmwareVersion;
			textBox1.Select(0, 0);
		}

		private void ClickOK(object sender, EventArgs e)
		{
			Close();
		}

		private void MicrochipLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			try
			{
				VisitMicrochipSite();
			}
			catch
			{
				MessageBox.Show("Unable to open link that was clicked.");
			}
		}

		private void VisitMicrochipSite()
		{
			linkLabel1.LinkVisited = true;
			Process.Start("http://www.microchip.com");
		}
	}
}