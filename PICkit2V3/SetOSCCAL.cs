using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace PICkit2V3
{
	// Token: 0x02000025 RID: 37
	public partial class SetOSCCAL : Form
	{
		// Token: 0x06000200 RID: 512 RVA: 0x0003C6E7 File Offset: 0x0003B6E7
		public SetOSCCAL()
		{
			this.InitializeComponent();
			this.textBoxOSCCAL.Text = string.Format("{0:X4}", PICkitFunctions.DeviceBuffers.OSCCAL);
			this.textBoxOSCCAL.SelectAll();
		}

		// Token: 0x06000201 RID: 513 RVA: 0x0003C724 File Offset: 0x0003B724
		private void clickSet(object sender, EventArgs e)
		{
			try
			{
				string p_value;
				if (this.textBoxOSCCAL.Text.Substring(0, 2) == "0x")
				{
					p_value = this.textBoxOSCCAL.Text;
				}
				else if (this.textBoxOSCCAL.Text.Substring(0, 1) == "x")
				{
					p_value = "0" + this.textBoxOSCCAL.Text;
				}
				else
				{
					p_value = "0x" + this.textBoxOSCCAL.Text;
				}
				int osccal = Utilities.Convert_Value_To_Int(p_value);
				PICkitFunctions.DeviceBuffers.OSCCAL = (uint)osccal;
				FormPICkit2.setOSCCALValue = true;
				base.Close();
			}
			catch
			{
				this.textBoxOSCCAL.Text = string.Format("{0:X4}", PICkitFunctions.DeviceBuffers.OSCCAL);
			}
		}

		// Token: 0x06000202 RID: 514 RVA: 0x0003C800 File Offset: 0x0003B800
		private void clickCancel(object sender, EventArgs e)
		{
			FormPICkit2.setOSCCALValue = false;
			base.Close();
		}
	}
}
