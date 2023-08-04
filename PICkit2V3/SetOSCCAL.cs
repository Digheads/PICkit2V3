using System;
using System.Windows.Forms;

namespace PICkit2V3
{
	public partial class SetOSCCAL : Form
	{
		public SetOSCCAL()
		{
			InitializeComponent();
			textBoxOSCCAL.Text = string.Format("{0:X4}", PICkitFunctions.DeviceBuffers.OSCCAL);
			textBoxOSCCAL.SelectAll();
		}

		private void ClickSet(object sender, EventArgs e)
		{
			try
			{
				string p_value;
				if (textBoxOSCCAL.Text.Substring(0, 2) == "0x")
				{
					p_value = textBoxOSCCAL.Text;
				}
				else if (textBoxOSCCAL.Text.Substring(0, 1) == "x")
				{
					p_value = "0" + textBoxOSCCAL.Text;
				}
				else
				{
					p_value = "0x" + textBoxOSCCAL.Text;
				}
				int osccal = Utilities.Convert_Value_To_Int(p_value);
				PICkitFunctions.DeviceBuffers.OSCCAL = (uint)osccal;
				FormPICkit2.setOSCCALValue = true;
				Close();
			}
			catch
			{
                textBoxOSCCAL.Text = string.Format("{0:X4}", PICkitFunctions.DeviceBuffers.OSCCAL);
			}
		}

		private void ClickCancel(object sender, EventArgs e)
		{
			FormPICkit2.setOSCCALValue = false;
			Close();
		}
	}
}
