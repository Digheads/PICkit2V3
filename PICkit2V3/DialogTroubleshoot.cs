using System;
using System.Threading;
using System.Windows.Forms;

namespace PICkit2V3
{
	public partial class DialogTroubleshoot : Form
	{
		public DialogTroubleshoot()
		{
			InitializeComponent();
			PICkitFunctions.VddOff();
			PICkitFunctions.SendScript(new byte[]
			{
				243,
				3
			});
		}

		private void ButtonCancel_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void ButtonNext_Click(object sender, EventArgs e)
		{
			if (panelIntro.Visible)
			{
				panelIntro.Visible = false;
				buttonBack.Enabled = true;
				TestVDD();
				return;
			}

			if (panelStep1VDDTest.Visible)
			{
				PICkitFunctions.VddOff();
				panelStep1VDDTest.Visible = false;
				panelCautionVDD.Visible = true;
				return;
			}

			if (panelStep1VDDExt.Visible)
			{
				PICkitFunctions.VddOff();
				panelStep1VDDExt.Visible = false;
				panelStep2VPP.Visible = true;
				TestVPP_Enter();
				return;
			}

			if (panelCautionVDD.Visible)
			{
				panelCautionVDD.Visible = false;
				panelStep2VPP.Visible = true;
				TestVPP_Enter();
				return;
			}

			if (panelStep2VPP.Visible)
			{
				panelStep2VPP.Visible = false;
				panelPGCPGD.Visible = true;
				buttonNext.Enabled = false;
				TestPGCPGDEnter();
			}
		}

		private void ButtonBack_Click(object sender, EventArgs e)
		{
			if (panelStep1VDDExt.Visible || panelStep1VDDTest.Visible)
			{
				PICkitFunctions.VddOff();
				panelIntro.Visible = true;
				buttonBack.Enabled = false;
				panelStep1VDDTest.Visible = false;
				panelStep1VDDExt.Visible = false;
				return;
			}

			if (panelCautionVDD.Visible || panelStep2VPP.Visible)
			{
				panelCautionVDD.Visible = false;
				panelStep2VPP.Visible = false;
				TestVDD();
				return;
			}

			if (panelPGCPGD.Visible)
			{
				panelPGCPGD.Visible = false;
				panelStep2VPP.Visible = true;
				buttonNext.Enabled = true;
				TestVPP_Enter();
			}
		}

		private void TestVDD()
		{
			float num = 0f;
			float num2 = 0f;
			PICkitFunctions.SendScript(new byte[]
			{
				250,
				248,
				254,
				253
			});
			Thread.Sleep(250);

			if (PICkitFunctions.CheckTargetPower(ref num, ref num2) == Constants.PICkit2PWR.selfpowered)
			{
				panelStep1VDDExt.Visible = true;
				labelVoltageOnVDD.Text = "An external voltage was detected\non the VDD pin at " + string.Format("{0:0.0} Volts.", num);
				return;
			}
			panelStep1VDDExt.Visible = false;
			panelStep1VDDTest.Visible = true;
			labelGood.Visible = false;
			labelVDDShort.Visible = false;
			labelVDDLow.Visible = false;
			labelReadVDD.Text = "";
			numericUpDown1.Maximum = (decimal)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].VddMax;
			numericUpDown1.Minimum = (decimal)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].VddMin;

			if ((float)numericUpDown1.Maximum > 4.5f)
			{
				numericUpDown1.Value = 4.5m;
				return;
			}
			numericUpDown1.Value = numericUpDown1.Maximum;
		}

		private void ButtonStep1Recheck_Click(object sender, EventArgs e)
		{
			TestVDD();
		}

		private void ButtonVDDOn_Click(object sender, EventArgs e)
		{
			float num = 0f;
			float num2 = 0f;
			labelGood.Visible = false;
			labelVDDShort.Visible = false;
			labelVDDLow.Visible = false;
			labelReadVDD.Text = "";
			float voltage = (float)numericUpDown1.Value;
			if (PICkitFunctions.SetVddVoltage(voltage, 0.45f))
			{
				PICkitFunctions.ForcePICkitPowered();
				if (PICkitFunctions.VddOn())
				{
					if (PICkitFunctions.PowerStatus() != Constants.PICkit2PWR.vdd_on)
					{
						labelVDDShort.Visible = true;
						labelReadVDD.Text = "Short!";
						return;
					}

					if (PICkitFunctions.ReadPICkitVoltages(ref num, ref num2))
					{
						labelReadVDD.Text = string.Format("{0:0.0} V", num);
						float num3 = (float)numericUpDown1.Value;
						if (num3 > 4.6f)
						{
							num3 = 4.6f;
						}

						if (num3 - num > 0.2f)
						{
							labelVDDLow.Visible = true;
							return;
						}
						labelGood.Visible = true;
					}
				}
			}
		}

		private void TestVPP_Enter()
		{
			PICkitFunctions.VddOff();
			PICkitFunctions.SendScript(new byte[]
			{
				243,
				3
			});
			timerPGxToggle.Enabled = false;
			buttonCancel.Text = "Cancel";
			if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].Vpp < 1f)
				labelStep2FamilyVPP.Text = "1) VPP for this family: " + string.Format("{0:0.0}V (=VDD)", numericUpDown1.Value);
			else
				labelStep2FamilyVPP.Text = "1) VPP for this family: " + string.Format("{0:0.0} Volts.", PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].Vpp);
			
			labelReadVPP.Text = "";
			labelVPPLow.Visible = false;
			labelVPPMCLR.Visible = false;
			labelVPPMCLROff.Visible = false;
			labelVPPPass.Visible = false;
			labelVPPShort.Visible = false;
			labelVPPVDDShort.Visible = false;
		}

		private void ButtonTestVPP_Click(object sender, EventArgs e)
		{
			float num = 0f;
			float num2 = 0f;
			labelVPPLow.Visible = false;
			labelVPPMCLR.Visible = false;
			labelVPPMCLROff.Visible = false;
			labelVPPPass.Visible = false;
			labelVPPShort.Visible = false;
			labelVPPVDDShort.Visible = false;
			labelReadVPP.Text = "";
			Thread.Sleep(250);
			if (PICkitFunctions.CheckTargetPower(ref num, ref num2) == Constants.PICkit2PWR.selfpowered)
				PICkitFunctions.VddOff();
			else
			{
				PICkitFunctions.SetVddVoltage((float)numericUpDown1.Value, 0.85f);
				PICkitFunctions.VddOn();
			}

			float num3;
			if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].Vpp > 1)
				num3 = PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].Vpp;
			else
				num3 = (float)numericUpDown1.Value;

			PICkitFunctions.SetVppVoltage(num3, 0.5f);
			PICkitFunctions.SendScript(new byte[]
			{
				250,
				249,
				232,
				30,
				246,
				251,
				232,
				20
			});
			Constants.PICkit2PWR pickit2PWR = PICkitFunctions.PowerStatus();
			if (pickit2PWR == Constants.PICkit2PWR.vdderror || pickit2PWR == Constants.PICkit2PWR.vddvpperrors)
			{
				labelVPPVDDShort.Visible = true;
				return;
			}

			if (pickit2PWR == Constants.PICkit2PWR.vpperror)
			{
				labelVPPShort.Visible = true;
				labelReadVPP.Text = "Short!";
				return;
			}

			if (pickit2PWR != Constants.PICkit2PWR.no_response && PICkitFunctions.ReadPICkitVoltages(ref num, ref num2))
			{
				labelReadVPP.Text = string.Format("{0:0.0} V", num2);
				if (num3 - num2 > 0.3f)
				{
					labelVPPLow.Visible = true;
					return;
				}
				labelVPPPass.Visible = true;
			}
		}

		private void ButtonMCLR_Click(object sender, EventArgs e)
		{
			labelVPPLow.Visible = false;
			labelVPPMCLR.Visible = true;
			labelVPPMCLROff.Visible = false;
			labelVPPPass.Visible = false;
			labelVPPShort.Visible = false;
			labelVPPVDDShort.Visible = false;
			labelReadVPP.Text = "/MCLR On";
			PICkitFunctions.SendScript(new byte[]
			{
				250,
				248,
				247
			});
		}

		private void ButtonMCLROff_Click(object sender, EventArgs e)
		{
			labelVPPLow.Visible = false;
			labelVPPMCLR.Visible = false;
			labelVPPMCLROff.Visible = true;
			labelVPPPass.Visible = false;
			labelVPPShort.Visible = false;
			labelVPPVDDShort.Visible = false;
			labelReadVPP.Text = "/MCLR Off";
			PICkitFunctions.SendScript(new byte[]
			{
				250,
				248,
				246
			});
		}

		private void TestPGCPGDEnter()
		{
			float num = 0;
			float num2 = 0;
			byte[] array = new byte[]
			{
				250,
				248,
				247
			};
			PICkitFunctions.SendScript(array);
			PICkitFunctions.VddOff();
			buttonCancel.Text = "Finished";
			Thread.Sleep(200);
			if (PICkitFunctions.CheckTargetPower(ref num, ref num2) == Constants.PICkit2PWR.selfpowered)
				PICkitFunctions.VddOff();
			else
			{
				PICkitFunctions.SetVddVoltage((float)numericUpDown1.Value, 0.85f);
				PICkitFunctions.VddOn();
				Thread.Sleep(50);
			}

			Constants.PICkit2PWR pickit2PWR = PICkitFunctions.PowerStatus();
			if (pickit2PWR == Constants.PICkit2PWR.vdderror || pickit2PWR == Constants.PICkit2PWR.vddvpperrors)
			{
				radioButtonPGCHigh.Enabled = false;
				radioButtonPGCLow.Enabled = false;
				radioButtonPGDHigh.Enabled = false;
				radioButtonPGDLow.Enabled = false;
				radioButtonPGCToggle.Enabled = false;
				radioButtonPGDToggle.Enabled = false;
				labelPGxOScope.Visible = false;
				labelPGxVDDShort.Visible = true;
				return;
			}

			if (pickit2PWR == Constants.PICkit2PWR.vpperror)
			{
				radioButtonPGCHigh.Enabled = false;
				radioButtonPGCLow.Enabled = false;
				radioButtonPGDHigh.Enabled = false;
				radioButtonPGDLow.Enabled = false;
				radioButtonPGCToggle.Enabled = false;
				radioButtonPGDToggle.Enabled = false;
				labelPGxOScope.Visible = false;
				labelPGxVDDShort.Visible = true;
				return;
			}
			if (pickit2PWR != Constants.PICkit2PWR.no_response)
			{
				radioButtonPGCHigh.Enabled = true;
				radioButtonPGCLow.Enabled = true;
				radioButtonPGDHigh.Enabled = true;
				radioButtonPGDLow.Enabled = true;
				radioButtonPGCToggle.Enabled = true;
				radioButtonPGDToggle.Enabled = true;
				labelPGxOScope.Visible = true;
				labelPGxVDDShort.Visible = false;
				array[0] = 243;
				array[1] = 0;
				array[2] = 244;
				PICkitFunctions.SendScript(array);
				radioButtonPGDToggle.Checked = false;
				radioButtonPGCToggle.Checked = false;
				radioButtonPGCHigh.Checked = false;
				radioButtonPGCLow.Checked = true;
				radioButtonPGDHigh.Checked = false;
				radioButtonPGDLow.Checked = true;
			}
		}

		private void RadioButtonPGCHigh_CheckedChanged(object sender, EventArgs e)
		{
			byte[] array = new byte[2];
			if (radioButtonPGDToggle.Checked || radioButtonPGCToggle.Checked)
				return;

			timerPGxToggle.Enabled = false;
			array[0] = 243;
			if (radioButtonPGCHigh.Checked && radioButtonPGDHigh.Checked)
				array[1] = 12;
			else if (radioButtonPGCHigh.Checked)
				array[1] = 4;
			else if (radioButtonPGDHigh.Checked)
				array[1] = 8;
			else
				array[1] = 0;
			PICkitFunctions.SendScript(array);
		}

		private void RadioButtonPGDToggle_Click(object sender, EventArgs e)
		{
			PGxToggle();
		}

		private void TimerPGxToggle_Tick(object sender, EventArgs e)
		{
			PGxToggle();
		}

		private void PGxToggle()
		{
			timerPGxToggle.Enabled = false;
			byte b = 0;
			byte b2 = 0;
			if (radioButtonPGDToggle.Checked)
				b |= 8;
			if (radioButtonPGCToggle.Checked)
				b |= 4;
			if (radioButtonPGCHigh.Checked)
			{
				b |= 4;
				b2 |= 4;
			}
			if (radioButtonPGDHigh.Checked)
			{
				b |= 8;
				b2 |= 8;
			}
			PICkitFunctions.SendScript(new byte[]
			{
				210,
				59,
				210,
				0,
				243,
				b,
				245,
				245,
				243,
				b2,
				245,
				233,
				7,
				0,
				221,
				10,
				244
			});
			timerPGxToggle.Enabled = true;
		}

		private void TrblshtingFormClosing(object sender, FormClosingEventArgs e)
		{
			timerPGxToggle.Enabled = false;
			PICkitFunctions.SendScript(new byte[]
			{
				250,
				246,
				248,
				243,
				3
			});
			PICkitFunctions.VddOff();
		}
	}
}