using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace PICkit2V3
{
	// Token: 0x02000005 RID: 5
	public partial class DialogUnitSelect : Form
	{
		// Token: 0x0600001C RID: 28 RVA: 0x00003A5C File Offset: 0x00002A5C
		public DialogUnitSelect()
		{
			this.InitializeComponent();
			base.Size = new Size(base.Size.Width, (int)(FormPICkit2.ScalefactH * (float)base.Size.Height));
			for (ushort num = 0; num < 8; num += 1)
			{
				Constants.PICkit2USB pickit2USB = PICkitFunctions.DetectPICkit2Device(num, false);
				if (pickit2USB == Constants.PICkit2USB.notFound)
				{
					break;
				}
				string text = PICkitFunctions.GetSerialUnitID();
				if (text == "PIC18F2550")
				{
					text = "<bootloader>";
				}
				this.listBoxUnits.Items.Add("  " + num.ToString() + "                " + text);
			}
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00003AFF File Offset: 0x00002AFF
		private void listBoxUnits_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			FormPICkit2.pk2number = (ushort)this.listBoxUnits.SelectedIndex;
			base.Close();
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00003B18 File Offset: 0x00002B18
		private void listBoxUnits_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.buttonSelectUnit.Enabled = true;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00003B26 File Offset: 0x00002B26
		private void buttonSelectUnit_Click(object sender, EventArgs e)
		{
			FormPICkit2.pk2number = (ushort)this.listBoxUnits.SelectedIndex;
			base.Close();
		}
	}
}
