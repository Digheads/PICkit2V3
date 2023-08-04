using System;
using System.Drawing;
using System.Windows.Forms;

namespace PICkit2V3
{
	public partial class DialogUnitSelect : Form
	{
		public DialogUnitSelect()
		{
			InitializeComponent();
			Size = new Size(Size.Width, (int)(FormPICkit2.scalefactH * Size.Height));
			for (ushort num = 0; num < 8; num += 1)
			{
				Constants.PICkit2USB pickit2USB = PICkitFunctions.DetectPICkit2Device(num, false);
				if (pickit2USB == Constants.PICkit2USB.notFound)
					break;

				string text = PICkitFunctions.GetSerialUnitID();
				if (text == "PIC18F2550")
					text = "<bootloader>";

				listBoxUnits.Items.Add("  " + num.ToString() + "                " + text);
			}
		}

		private void ListBoxUnits_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			FormPICkit2.pk2number = (ushort)listBoxUnits.SelectedIndex;
			Close();
		}

		private void ListBoxUnits_SelectedIndexChanged(object sender, EventArgs e)
		{
			buttonSelectUnit.Enabled = true;
		}

		private void ButtonSelectUnit_Click(object sender, EventArgs e)
		{
			FormPICkit2.pk2number = (ushort)listBoxUnits.SelectedIndex;
			Close();
		}
	}
}
