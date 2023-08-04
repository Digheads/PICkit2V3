using System;
using System.Threading;
using System.Windows.Forms;

namespace PICkit2V3
{
	public partial class DialogCalibrate : Form
	{
		public DialogCalibrate()
		{
			InitializeComponent();
			PICkitFunctions.VddOff();
			PICkitFunctions.ForcePICkitPowered();
			SetupClearButtons();
		}

		private void ButtonCancel_Click(object sender, EventArgs e)
		{
			PICkitFunctions.VddOff();
			if (unitIDChanged)
			{
				PICkitFunctions.ResetPICkit2();
				Thread.Sleep(1000);
				MessageBox.Show("Resetting PICkit 2.\n\nPlease wait for USB enumeration\nto complete before clicking OK...", "Reset PICkit 2");
				Thread.Sleep(1000);
			}
			Close();
		}

		private void SetupClearButtons()
		{
			if (PICkitFunctions.IsCalibrated())
			{
				buttonClearCal.Enabled = true;
				buttonClearCal.Text = "Clear Calibration";
			}
			else
			{
				buttonClearCal.Enabled = false;
				buttonClearCal.Text = "Unit Not Calibrated";
			}
			if (PICkitFunctions.UnitIDRead().Length > 0)
			{
				buttonClearUnitID.Enabled = true;
				buttonClearUnitID.Text = "Clear Unit ID";
				return;
			}
			buttonClearUnitID.Enabled = false;
			buttonClearUnitID.Text = "No Assigned ID";
		}

		private void ButtonNext_Click(object sender, EventArgs e)
		{
			if (panelIntro.Visible)
			{
				panelIntro.Visible = false;
				panelSetup.Visible = true;
				buttonBack.Enabled = true;
				return;
			}
			if (panelSetup.Visible)
			{
				panelSetup.Visible = false;
				panelCal.Visible = true;
				buttonCalibrate.Enabled = true;
				labelGoodCal.Visible = false;
				labelBadCal.Visible = false;
				textBoxVDD.Text = "4.000";
				textBoxVDD.Focus();
				textBoxVDD.SelectAll();
				PICkitFunctions.SetVoltageCals(256, 0, 128);
				PICkitFunctions.SetVddVoltage(4f, 3.4f);
				PICkitFunctions.VddOn();
				return;
			}
			if (panelCal.Visible)
			{
				panelCal.Visible = false;
				panelUnitID.Visible = true;
				buttonSetUnitID.Enabled = true;
				labelAssignedID.Visible = false;
				textBoxUnitID.Text = PICkitFunctions.UnitIDRead();
				textBoxUnitID.Focus();
				textBoxVDD.SelectAll();
				buttonNext.Enabled = false;
				buttonCancel.Text = "Finished";
				PICkitFunctions.VddOff();
			}
		}

		private void ButtonBack_Click(object sender, EventArgs e)
		{
			if (panelSetup.Visible)
			{
				panelIntro.Visible = true;
				panelSetup.Visible = false;
				buttonBack.Enabled = false;
				SetupClearButtons();
				return;
			}
			if (panelCal.Visible)
			{
				PICkitFunctions.VddOff();
				panelSetup.Visible = true;
				panelCal.Visible = false;
				return;
			}
			if (panelUnitID.Visible)
			{
				panelUnitID.Visible = false;
				panelCal.Visible = true;
				buttonCalibrate.Enabled = false;
				labelGoodCal.Visible = false;
				labelBadCal.Visible = false;
				textBoxVDD.Text = "-";
				buttonNext.Enabled = true;
				buttonCancel.Text = "Cancel";
			}
		}

		private void ButtonCalibrate_Click(object sender, EventArgs e)
		{
			float num = 0f;
			float num2 = 0f;
			bool flag = true;
			float num3;
			try
			{
				num3 = float.Parse(textBoxVDD.Text);
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
			PICkitFunctions.SetVddVoltage(3f, 2f);
			Thread.Sleep(150);
			PICkitFunctions.ReadPICkitVoltages(ref num, ref num2);
			float num5 = num;
			PICkitFunctions.SetVddVoltage(4f, 2.7f);
			Thread.Sleep(150);
			PICkitFunctions.ReadPICkitVoltages(ref num, ref num2);
			float num6 = (3f - 4f * num5 / num) * (PICkitFunctions.CalculateVddCPP(4f) >> 6);
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
				labelGoodCal.Visible = true;
				labelBadCal.Visible = false;
				PICkitFunctions.SetVoltageCals((ushort)num4, (byte)num6, (byte)((double)num7 + 0.5));
			}
			else
			{
				labelGoodCal.Visible = false;
				labelBadCal.Visible = true;
				PICkitFunctions.SetVoltageCals(256, 0, 128);
			}
			buttonCalibrate.Enabled = false;
			PICkitFunctions.VddOff();
		}

		private void TextBoxUnitID_TextChanged(object sender, EventArgs e)
		{
			if (textBoxUnitID.Text.Length > 14)
			{
				textBoxUnitID.Text = textBoxUnitID.Text.Substring(0, 14);
				textBoxUnitID.SelectionStart = 14;
			}
		}

		private void ButtonSetUnitID_Click(object sender, EventArgs e)
		{
			if (PICkitFunctions.UnitIDWrite(textBoxUnitID.Text))
			{
				labelAssignedID.Visible = true;
				buttonSetUnitID.Enabled = false;
				unitIDChanged = true;
			}
		}

		private void ButtonClearCal_Click(object sender, EventArgs e)
		{
			PICkitFunctions.SetVoltageCals(256, 0, 128);
			buttonClearCal.Enabled = false;
			buttonClearCal.Text = "Unit Not Calibrated";
		}

		private void ButtonClearUnitID_Click(object sender, EventArgs e)
		{
			PICkitFunctions.UnitIDWrite("");
			buttonClearUnitID.Enabled = false;
			buttonClearUnitID.Text = "No Assigned ID";
			unitIDChanged = true;
		}

		private bool unitIDChanged;
	}
}