using System;
using System.Windows.Forms;

namespace PICkit2V3
{
	public partial class DialogVDDErase : Form
	{
		public DialogVDDErase()
		{
			InitializeComponent();
		}

		public void UpdateText()
		{
            label2.Text = "This device requires a minimum VDD of " + PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].VddErase.ToString() + "V\nfor Bulk Erase operations.";
		}

		private void ContinueClick(object sender, EventArgs e)
		{
			if (checkBoxDoNotShow.Checked)
				FormPICkit2.showWriteEraseVddDialog = false;

			FormPICkit2.continueWriteErase = true;
			Close();
		}

		private void CancelClick(object sender, EventArgs e)
		{
			FormPICkit2.continueWriteErase = false;
			Close();
		}
	}
}
