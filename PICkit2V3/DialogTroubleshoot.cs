using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace PICkit2V3
{
	// Token: 0x02000009 RID: 9
	public partial class DialogTroubleshoot : Form
	{
		// Token: 0x0600005C RID: 92 RVA: 0x000082B0 File Offset: 0x000072B0
		public DialogTroubleshoot()
		{
			this.InitializeComponent();
			PICkitFunctions.VddOff();
			PICkitFunctions.SendScript(new byte[]
			{
				243,
				3
			});
		}

		// Token: 0x0600005D RID: 93 RVA: 0x000082E9 File Offset: 0x000072E9
		private void buttonCancel_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		// Token: 0x0600005E RID: 94 RVA: 0x000082F4 File Offset: 0x000072F4
		private void buttonNext_Click(object sender, EventArgs e)
		{
			if (this.panelIntro.Visible)
			{
				this.panelIntro.Visible = false;
				this.buttonBack.Enabled = true;
				this.testVDD();
				return;
			}
			if (this.panelStep1VDDTest.Visible)
			{
				PICkitFunctions.VddOff();
				this.panelStep1VDDTest.Visible = false;
				this.panelCautionVDD.Visible = true;
				return;
			}
			if (this.panelStep1VDDExt.Visible)
			{
				PICkitFunctions.VddOff();
				this.panelStep1VDDExt.Visible = false;
				this.panelStep2VPP.Visible = true;
				this.testVPP_Enter();
				return;
			}
			if (this.panelCautionVDD.Visible)
			{
				this.panelCautionVDD.Visible = false;
				this.panelStep2VPP.Visible = true;
				this.testVPP_Enter();
				return;
			}
			if (this.panelStep2VPP.Visible)
			{
				this.panelStep2VPP.Visible = false;
				this.panelPGCPGD.Visible = true;
				this.buttonNext.Enabled = false;
				this.testPGCPGDEnter();
			}
		}

		// Token: 0x0600005F RID: 95 RVA: 0x000083F0 File Offset: 0x000073F0
		private void buttonBack_Click(object sender, EventArgs e)
		{
			if (this.panelStep1VDDExt.Visible || this.panelStep1VDDTest.Visible)
			{
				PICkitFunctions.VddOff();
				this.panelIntro.Visible = true;
				this.buttonBack.Enabled = false;
				this.panelStep1VDDTest.Visible = false;
				this.panelStep1VDDExt.Visible = false;
				return;
			}
			if (this.panelCautionVDD.Visible || this.panelStep2VPP.Visible)
			{
				this.panelCautionVDD.Visible = false;
				this.panelStep2VPP.Visible = false;
				this.testVDD();
				return;
			}
			if (this.panelPGCPGD.Visible)
			{
				this.panelPGCPGD.Visible = false;
				this.panelStep2VPP.Visible = true;
				this.buttonNext.Enabled = true;
				this.testVPP_Enter();
			}
		}

		// Token: 0x06000060 RID: 96 RVA: 0x000084C0 File Offset: 0x000074C0
		private void testVDD()
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
				this.panelStep1VDDExt.Visible = true;
				this.labelVoltageOnVDD.Text = "An external voltage was detected\non the VDD pin at " + string.Format("{0:0.0} Volts.", num);
				return;
			}
			this.panelStep1VDDExt.Visible = false;
			this.panelStep1VDDTest.Visible = true;
			this.labelGood.Visible = false;
			this.labelVDDShort.Visible = false;
			this.labelVDDLow.Visible = false;
			this.labelReadVDD.Text = "";
			this.numericUpDown1.Maximum = (decimal)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].VddMax;
			this.numericUpDown1.Minimum = (decimal)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].VddMin;
			if ((float)this.numericUpDown1.Maximum > 4.5f)
			{
				this.numericUpDown1.Value = 4.5m;
				return;
			}
			this.numericUpDown1.Value = this.numericUpDown1.Maximum;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00008634 File Offset: 0x00007634
		private void buttonStep1Recheck_Click(object sender, EventArgs e)
		{
			this.testVDD();
		}

		// Token: 0x06000062 RID: 98 RVA: 0x0000863C File Offset: 0x0000763C
		private void buttonVDDOn_Click(object sender, EventArgs e)
		{
			float num = 0f;
			float num2 = 0f;
			this.labelGood.Visible = false;
			this.labelVDDShort.Visible = false;
			this.labelVDDLow.Visible = false;
			this.labelReadVDD.Text = "";
			float voltage = (float)this.numericUpDown1.Value;
			if (PICkitFunctions.SetVddVoltage(voltage, 0.45f))
			{
				PICkitFunctions.ForcePICkitPowered();
				if (PICkitFunctions.VddOn())
				{
					if (PICkitFunctions.PowerStatus() != Constants.PICkit2PWR.vdd_on)
					{
						this.labelVDDShort.Visible = true;
						this.labelReadVDD.Text = "Short!";
						return;
					}
					if (PICkitFunctions.ReadPICkitVoltages(ref num, ref num2))
					{
						this.labelReadVDD.Text = string.Format("{0:0.0} V", num);
						float num3 = (float)this.numericUpDown1.Value;
						if (num3 > 4.6f)
						{
							num3 = 4.6f;
						}
						if (num3 - num > 0.2f)
						{
							this.labelVDDLow.Visible = true;
							return;
						}
						this.labelGood.Visible = true;
					}
				}
			}
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00008748 File Offset: 0x00007748
		private void testVPP_Enter()
		{
			PICkitFunctions.VddOff();
			PICkitFunctions.SendScript(new byte[]
			{
				243,
				3
			});
			this.timerPGxToggle.Enabled = false;
			this.buttonCancel.Text = "Cancel";
			if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].Vpp < 1f)
			{
				this.labelStep2FamilyVPP.Text = "1) VPP for this family: " + string.Format("{0:0.0}V (=VDD)", this.numericUpDown1.Value);
			}
			else
			{
				this.labelStep2FamilyVPP.Text = "1) VPP for this family: " + string.Format("{0:0.0} Volts.", PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].Vpp);
			}
			this.labelReadVPP.Text = "";
			this.labelVPPLow.Visible = false;
			this.labelVPPMCLR.Visible = false;
			this.labelVPPMCLROff.Visible = false;
			this.labelVPPPass.Visible = false;
			this.labelVPPShort.Visible = false;
			this.labelVPPVDDShort.Visible = false;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00008878 File Offset: 0x00007878
		private void buttonTestVPP_Click(object sender, EventArgs e)
		{
			float num = 0f;
			float num2 = 0f;
			this.labelVPPLow.Visible = false;
			this.labelVPPMCLR.Visible = false;
			this.labelVPPMCLROff.Visible = false;
			this.labelVPPPass.Visible = false;
			this.labelVPPShort.Visible = false;
			this.labelVPPVDDShort.Visible = false;
			this.labelReadVPP.Text = "";
			Thread.Sleep(250);
			if (PICkitFunctions.CheckTargetPower(ref num, ref num2) == Constants.PICkit2PWR.selfpowered)
			{
				PICkitFunctions.VddOff();
			}
			else
			{
				PICkitFunctions.SetVddVoltage((float)this.numericUpDown1.Value, 0.85f);
				PICkitFunctions.VddOn();
			}
			float num3;
			if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].Vpp > 1f)
			{
				num3 = PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].Vpp;
			}
			else
			{
				num3 = (float)this.numericUpDown1.Value;
			}
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
				this.labelVPPVDDShort.Visible = true;
				return;
			}
			if (pickit2PWR == Constants.PICkit2PWR.vpperror)
			{
				this.labelVPPShort.Visible = true;
				this.labelReadVPP.Text = "Short!";
				return;
			}
			if (pickit2PWR != Constants.PICkit2PWR.no_response && PICkitFunctions.ReadPICkitVoltages(ref num, ref num2))
			{
				this.labelReadVPP.Text = string.Format("{0:0.0} V", num2);
				if (num3 - num2 > 0.3f)
				{
					this.labelVPPLow.Visible = true;
					return;
				}
				this.labelVPPPass.Visible = true;
			}
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00008A58 File Offset: 0x00007A58
		private void buttonMCLR_Click(object sender, EventArgs e)
		{
			this.labelVPPLow.Visible = false;
			this.labelVPPMCLR.Visible = true;
			this.labelVPPMCLROff.Visible = false;
			this.labelVPPPass.Visible = false;
			this.labelVPPShort.Visible = false;
			this.labelVPPVDDShort.Visible = false;
			this.labelReadVPP.Text = "/MCLR On";
			PICkitFunctions.SendScript(new byte[]
			{
				250,
				248,
				247
			});
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00008AE4 File Offset: 0x00007AE4
		private void buttonMCLROff_Click(object sender, EventArgs e)
		{
			this.labelVPPLow.Visible = false;
			this.labelVPPMCLR.Visible = false;
			this.labelVPPMCLROff.Visible = true;
			this.labelVPPPass.Visible = false;
			this.labelVPPShort.Visible = false;
			this.labelVPPVDDShort.Visible = false;
			this.labelReadVPP.Text = "/MCLR Off";
			PICkitFunctions.SendScript(new byte[]
			{
				250,
				248,
				246
			});
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00008B70 File Offset: 0x00007B70
		private void testPGCPGDEnter()
		{
			float num = 0f;
			float num2 = 0f;
			byte[] array = new byte[]
			{
				250,
				248,
				247
			};
			PICkitFunctions.SendScript(array);
			PICkitFunctions.VddOff();
			this.buttonCancel.Text = "Finished";
			Thread.Sleep(200);
			if (PICkitFunctions.CheckTargetPower(ref num, ref num2) == Constants.PICkit2PWR.selfpowered)
			{
				PICkitFunctions.VddOff();
			}
			else
			{
				PICkitFunctions.SetVddVoltage((float)this.numericUpDown1.Value, 0.85f);
				PICkitFunctions.VddOn();
				Thread.Sleep(50);
			}
			Constants.PICkit2PWR pickit2PWR = PICkitFunctions.PowerStatus();
			if (pickit2PWR == Constants.PICkit2PWR.vdderror || pickit2PWR == Constants.PICkit2PWR.vddvpperrors)
			{
				this.radioButtonPGCHigh.Enabled = false;
				this.radioButtonPGCLow.Enabled = false;
				this.radioButtonPGDHigh.Enabled = false;
				this.radioButtonPGDLow.Enabled = false;
				this.radioButtonPGCToggle.Enabled = false;
				this.radioButtonPGDToggle.Enabled = false;
				this.labelPGxOScope.Visible = false;
				this.labelPGxVDDShort.Visible = true;
				return;
			}
			if (pickit2PWR == Constants.PICkit2PWR.vpperror)
			{
				this.radioButtonPGCHigh.Enabled = false;
				this.radioButtonPGCLow.Enabled = false;
				this.radioButtonPGDHigh.Enabled = false;
				this.radioButtonPGDLow.Enabled = false;
				this.radioButtonPGCToggle.Enabled = false;
				this.radioButtonPGDToggle.Enabled = false;
				this.labelPGxOScope.Visible = false;
				this.labelPGxVDDShort.Visible = true;
				return;
			}
			if (pickit2PWR != Constants.PICkit2PWR.no_response)
			{
				this.radioButtonPGCHigh.Enabled = true;
				this.radioButtonPGCLow.Enabled = true;
				this.radioButtonPGDHigh.Enabled = true;
				this.radioButtonPGDLow.Enabled = true;
				this.radioButtonPGCToggle.Enabled = true;
				this.radioButtonPGDToggle.Enabled = true;
				this.labelPGxOScope.Visible = true;
				this.labelPGxVDDShort.Visible = false;
				array[0] = 243;
				array[1] = 0;
				array[2] = 244;
				PICkitFunctions.SendScript(array);
				this.radioButtonPGDToggle.Checked = false;
				this.radioButtonPGCToggle.Checked = false;
				this.radioButtonPGCHigh.Checked = false;
				this.radioButtonPGCLow.Checked = true;
				this.radioButtonPGDHigh.Checked = false;
				this.radioButtonPGDLow.Checked = true;
			}
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00008DAC File Offset: 0x00007DAC
		private void radioButtonPGCHigh_CheckedChanged(object sender, EventArgs e)
		{
			byte[] array = new byte[2];
			if (this.radioButtonPGDToggle.Checked || this.radioButtonPGCToggle.Checked)
			{
				return;
			}
			this.timerPGxToggle.Enabled = false;
			array[0] = 243;
			if (this.radioButtonPGCHigh.Checked && this.radioButtonPGDHigh.Checked)
			{
				array[1] = 12;
			}
			else if (this.radioButtonPGCHigh.Checked)
			{
				array[1] = 4;
			}
			else if (this.radioButtonPGDHigh.Checked)
			{
				array[1] = 8;
			}
			else
			{
				array[1] = 0;
			}
			PICkitFunctions.SendScript(array);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00008E41 File Offset: 0x00007E41
		private void radioButtonPGDToggle_Click(object sender, EventArgs e)
		{
			this.PGxToggle();
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00008E49 File Offset: 0x00007E49
		private void timerPGxToggle_Tick(object sender, EventArgs e)
		{
			this.PGxToggle();
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00008E54 File Offset: 0x00007E54
		private void PGxToggle()
		{
			this.timerPGxToggle.Enabled = false;
			byte b = 0;
			byte b2 = 0;
			if (this.radioButtonPGDToggle.Checked)
			{
				b |= 8;
			}
			if (this.radioButtonPGCToggle.Checked)
			{
				b |= 4;
			}
			if (this.radioButtonPGCHigh.Checked)
			{
				b |= 4;
				b2 |= 4;
			}
			if (this.radioButtonPGDHigh.Checked)
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
			this.timerPGxToggle.Enabled = true;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00008F54 File Offset: 0x00007F54
		private void trblshtingFormClosing(object sender, FormClosingEventArgs e)
		{
			this.timerPGxToggle.Enabled = false;
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
