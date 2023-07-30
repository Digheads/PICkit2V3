using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace PICkit2V3
{
	// Token: 0x02000032 RID: 50
	public partial class DialogVDDErase : Form
	{
		// Token: 0x0600021A RID: 538 RVA: 0x00040438 File Offset: 0x0003F438
		public DialogVDDErase()
		{
			this.InitializeComponent();
		}

		// Token: 0x0600021B RID: 539 RVA: 0x00040446 File Offset: 0x0003F446
		public void UpdateText()
		{
			this.label2.Text = "This device requires a minimum VDD of " + PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].VddErase.ToString() + "V\nfor Bulk Erase operations.";
		}

		// Token: 0x0600021C RID: 540 RVA: 0x00040480 File Offset: 0x0003F480
		private void continueClick(object sender, EventArgs e)
		{
			if (this.checkBoxDoNotShow.Checked)
			{
				FormPICkit2.showWriteEraseVddDialog = false;
			}
			FormPICkit2.continueWriteErase = true;
			base.Close();
		}

		// Token: 0x0600021D RID: 541 RVA: 0x000404A1 File Offset: 0x0003F4A1
		private void cancelClick(object sender, EventArgs e)
		{
			FormPICkit2.continueWriteErase = false;
			base.Close();
		}
	}
}
