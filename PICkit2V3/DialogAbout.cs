using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace PICkit2V3
{
	// Token: 0x02000036 RID: 54
	public partial class DialogAbout : Form
	{
		// Token: 0x0600023B RID: 571 RVA: 0x000450A8 File Offset: 0x000440A8
		public DialogAbout()
		{
			this.InitializeComponent();
			this.displayAppVer.Text = "2.61.00";
			this.displayDevFileVer.Text = PICkitFunctions.DeviceFileVersion;
			this.displayPk2FWVer.Text = PICkitFunctions.FirmwareVersion;
			this.textBox1.Select(0, 0);
		}

		// Token: 0x0600023C RID: 572 RVA: 0x000450FE File Offset: 0x000440FE
		private void clickOK(object sender, EventArgs e)
		{
			base.Close();
		}

		// Token: 0x0600023D RID: 573 RVA: 0x00045108 File Offset: 0x00044108
		private void microchipLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			try
			{
				this.visitMicrochipSite();
			}
			catch
			{
				MessageBox.Show("Unable to open link that was clicked.");
			}
		}

		// Token: 0x0600023E RID: 574 RVA: 0x0004513C File Offset: 0x0004413C
		private void visitMicrochipSite()
		{
			this.linkLabel1.LinkVisited = true;
			Process.Start("http://www.microchip.com");
		}
	}
}
