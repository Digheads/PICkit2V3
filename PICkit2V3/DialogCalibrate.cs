using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace PICkit2V3
{
	// Token: 0x02000008 RID: 8
	public partial class DialogCalibrate : Form
	{
		// Token: 0x06000052 RID: 82 RVA: 0x00007CAD File Offset: 0x00006CAD
		public DialogCalibrate()
		{
			this.InitializeComponent();
			PICkitFunctions.VddOff();
			PICkitFunctions.ForcePICkitPowered();
			this.setupClearButtons();
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00007CCC File Offset: 0x00006CCC
		private void buttonCancel_Click(object sender, EventArgs e)
		{
			PICkitFunctions.VddOff();
			if (this.unitIDChanged)
			{
				PICkitFunctions.ResetPICkit2();
				Thread.Sleep(1000);
				MessageBox.Show("Resetting PICkit 2.\n\nPlease wait for USB enumeration\nto complete before clicking OK...", "Reset PICkit 2");
				Thread.Sleep(1000);
			}
			base.Close();
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00007D0C File Offset: 0x00006D0C
		private void setupClearButtons()
		{
			if (PICkitFunctions.isCalibrated())
			{
				this.buttonClearCal.Enabled = true;
				this.buttonClearCal.Text = "Clear Calibration";
			}
			else
			{
				this.buttonClearCal.Enabled = false;
				this.buttonClearCal.Text = "Unit Not Calibrated";
			}
			if (PICkitFunctions.UnitIDRead().Length > 0)
			{
				this.buttonClearUnitID.Enabled = true;
				this.buttonClearUnitID.Text = "Clear Unit ID";
				return;
			}
			this.buttonClearUnitID.Enabled = false;
			this.buttonClearUnitID.Text = "No Assigned ID";
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00007DA0 File Offset: 0x00006DA0
		private void buttonNext_Click(object sender, EventArgs e)
		{
			if (this.panelIntro.Visible)
			{
				this.panelIntro.Visible = false;
				this.panelSetup.Visible = true;
				this.buttonBack.Enabled = true;
				return;
			}
			if (this.panelSetup.Visible)
			{
				this.panelSetup.Visible = false;
				this.panelCal.Visible = true;
				this.buttonCalibrate.Enabled = true;
				this.labelGoodCal.Visible = false;
				this.labelBadCal.Visible = false;
				this.textBoxVDD.Text = "4.000";
				this.textBoxVDD.Focus();
				this.textBoxVDD.SelectAll();
				PICkitFunctions.SetVoltageCals(256, 0, 128);
				PICkitFunctions.SetVDDVoltage(4f, 3.4f);
				PICkitFunctions.VddOn();
				return;
			}
			if (this.panelCal.Visible)
			{
				this.panelCal.Visible = false;
				this.panelUnitID.Visible = true;
				this.buttonSetUnitID.Enabled = true;
				this.labelAssignedID.Visible = false;
				this.textBoxUnitID.Text = PICkitFunctions.UnitIDRead();
				this.textBoxUnitID.Focus();
				this.textBoxVDD.SelectAll();
				this.buttonNext.Enabled = false;
				this.buttonCancel.Text = "Finished";
				PICkitFunctions.VddOff();
			}
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00007F00 File Offset: 0x00006F00
		private void buttonBack_Click(object sender, EventArgs e)
		{
			if (this.panelSetup.Visible)
			{
				this.panelIntro.Visible = true;
				this.panelSetup.Visible = false;
				this.buttonBack.Enabled = false;
				this.setupClearButtons();
				return;
			}
			if (this.panelCal.Visible)
			{
				PICkitFunctions.VddOff();
				this.panelSetup.Visible = true;
				this.panelCal.Visible = false;
				return;
			}
			if (this.panelUnitID.Visible)
			{
				this.panelUnitID.Visible = false;
				this.panelCal.Visible = true;
				this.buttonCalibrate.Enabled = false;
				this.labelGoodCal.Visible = false;
				this.labelBadCal.Visible = false;
				this.textBoxVDD.Text = "-";
				this.buttonNext.Enabled = true;
				this.buttonCancel.Text = "Cancel";
			}
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00007FE8 File Offset: 0x00006FE8
		private void buttonCalibrate_Click(object sender, EventArgs e)
		{
			float num = 0f;
			float num2 = 0f;
			float num3 = 0f;
			bool flag = true;
			try
			{
				num3 = float.Parse(this.textBoxVDD.Text);
			}
			catch
			{
				MessageBox.Show("Invalid 'volts measured' value.");
				return;
			}
			PICkitFunctions.ReadPICkitVoltages(ref num, ref num2);
			num3 /= num;
			if (num3 > 1.25f)
			{
				num3 = 1.25f;
				flag = false;
			}
			if (num3 < 0.75f)
			{
				num3 = 0.75f;
				flag = false;
			}
			float num4 = 256f * num3;
			PICkitFunctions.SetVoltageCals((ushort)num4, 0, 128);
			PICkitFunctions.SetVDDVoltage(3f, 2f);
			Thread.Sleep(150);
			PICkitFunctions.ReadPICkitVoltages(ref num, ref num2);
			float num5 = num;
			PICkitFunctions.SetVDDVoltage(4f, 2.7f);
			Thread.Sleep(150);
			PICkitFunctions.ReadPICkitVoltages(ref num, ref num2);
			float num6 = (3f - 4f * num5 / num) * (float)(PICkitFunctions.CalculateVddCPP(4f) >> 6);
			if (num6 > 127f)
			{
				num6 = 127f;
				flag = false;
			}
			if (num6 < -128f)
			{
				num6 = -128f;
				flag = false;
			}
			float num7 = 1f / (num - num5) * 128f;
			if (num7 > 173f)
			{
				num7 = 173f;
				flag = false;
			}
			if (num7 < 83f)
			{
				num7 = 83f;
				flag = false;
			}
			if (flag)
			{
				this.labelGoodCal.Visible = true;
				this.labelBadCal.Visible = false;
				PICkitFunctions.SetVoltageCals((ushort)num4, (byte)num6, (byte)((double)num7 + 0.5));
			}
			else
			{
				this.labelGoodCal.Visible = false;
				this.labelBadCal.Visible = true;
				PICkitFunctions.SetVoltageCals(256, 0, 128);
			}
			this.buttonCalibrate.Enabled = false;
			PICkitFunctions.VddOff();
		}

		// Token: 0x06000058 RID: 88 RVA: 0x000081D0 File Offset: 0x000071D0
		private void textBoxUnitID_TextChanged(object sender, EventArgs e)
		{
			if (this.textBoxUnitID.Text.Length > 14)
			{
				this.textBoxUnitID.Text = this.textBoxUnitID.Text.Substring(0, 14);
				this.textBoxUnitID.SelectionStart = 14;
			}
		}

		// Token: 0x06000059 RID: 89 RVA: 0x0000821C File Offset: 0x0000721C
		private void buttonSetUnitID_Click(object sender, EventArgs e)
		{
			if (PICkitFunctions.UnitIDWrite(this.textBoxUnitID.Text))
			{
				this.labelAssignedID.Visible = true;
				this.buttonSetUnitID.Enabled = false;
				this.unitIDChanged = true;
			}
		}

		// Token: 0x0600005A RID: 90 RVA: 0x0000824F File Offset: 0x0000724F
		private void buttonClearCal_Click(object sender, EventArgs e)
		{
			PICkitFunctions.SetVoltageCals(256, 0, 128);
			this.buttonClearCal.Enabled = false;
			this.buttonClearCal.Text = "Unit Not Calibrated";
		}

		// Token: 0x0600005B RID: 91 RVA: 0x0000827E File Offset: 0x0000727E
		private void buttonClearUnitID_Click(object sender, EventArgs e)
		{
			PICkitFunctions.UnitIDWrite("");
			this.buttonClearUnitID.Enabled = false;
			this.buttonClearUnitID.Text = "No Assigned ID";
			this.unitIDChanged = true;
		}

		// Token: 0x0400006A RID: 106
		private bool unitIDChanged;
	}
}
