using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace PICkit2V3
{
	// Token: 0x02000026 RID: 38
	public partial class DialogPK2Go : Form
	{
		// Token: 0x06000205 RID: 517 RVA: 0x0003F63C File Offset: 0x0003E63C
		public DialogPK2Go()
		{
			this.InitializeComponent();
		}

		// Token: 0x06000206 RID: 518 RVA: 0x0003F674 File Offset: 0x0003E674
		public void SetPTGMemory(byte value)
		{
			this.ptgMemory = value;
			if (this.ptgMemory > 0 && this.ptgMemory <= 5)
			{
				this.label256K.Visible = true;
			}
			if (this.ptgMemory == 1)
			{
				this.label256K.Text = "256K PICkit 2 upgrade support enabled.\r\n";
				return;
			}
			if (this.ptgMemory == 2)
			{
				this.label256K.Text = "512K SPI memory support enabled.\r\n";
				return;
			}
			if (this.ptgMemory == 3)
			{
				this.label256K.Text = "1M SPI memory support enabled.\r\n";
				return;
			}
			if (this.ptgMemory == 4)
			{
				this.label256K.Text = "2M SPI memory support enabled.\r\n";
				return;
			}
			if (this.ptgMemory == 5)
			{
				this.label256K.Text = "4M SPI memory support enabled.\r\n";
			}
		}

		// Token: 0x06000207 RID: 519 RVA: 0x0003F727 File Offset: 0x0003E727
		private void buttonCancel_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		// Token: 0x06000208 RID: 520 RVA: 0x0003F730 File Offset: 0x0003E730
		private void buttonNext_Click(object sender, EventArgs e)
		{
			if (this.panelIntro.Visible)
			{
				this.panelIntro.Visible = false;
				this.buttonBack.Enabled = true;
				this.fillSettings(true);
				return;
			}
			if (this.panelSettings.Visible)
			{
				if (this.checkEraseVoltage())
				{
					this.panelSettings.Visible = false;
					this.buttonNext.Text = "Download";
					this.fillDownload();
					return;
				}
			}
			else
			{
				if (this.panelDownload.Visible)
				{
					this.downloadGO();
					return;
				}
				if (this.panelDownloadDone.Visible)
				{
					this.buttonNext.Enabled = false;
					this.panelDownloadDone.Visible = false;
					this.panelErrors.Visible = true;
					this.timerBlink.Interval = 84;
				}
			}
		}

		// Token: 0x06000209 RID: 521 RVA: 0x0003F7F4 File Offset: 0x0003E7F4
		private void buttonBack_Click(object sender, EventArgs e)
		{
			if (this.panelSettings.Visible)
			{
				this.panelSettings.Visible = false;
				this.panelIntro.Visible = true;
				this.buttonBack.Enabled = false;
				return;
			}
			if (this.panelDownload.Visible)
			{
				this.panelDownload.Visible = false;
				this.buttonNext.Text = "Next >";
				this.fillSettings(false);
			}
		}

		// Token: 0x0600020A RID: 522 RVA: 0x0003F864 File Offset: 0x0003E864
		private bool checkEraseVoltage()
		{
			if (this.radioButtonSelfPower.Checked)
			{
				return true;
			}
			if (this.VDDVolts < PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].VddErase && PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].DebugRowEraseScript == 0)
			{
				DialogResult dialogResult = MessageBox.Show("The selected PICkit 2 VDD voltage is below\nthe minimum required to Bulk Erase this part.\n\nContinue anyway?", this.labelPartNumber.Text + " VDD Error", MessageBoxButtons.OKCancel);
				return dialogResult == DialogResult.OK;
			}
			return true;
		}

		// Token: 0x0600020B RID: 523 RVA: 0x0003F8E8 File Offset: 0x0003E8E8
		private void fillSettings(bool changePower)
		{
			this.labelPartNumber.Text = PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].PartName;
			if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].OSSCALSave)
			{
				this.labelOSCCAL_BandGap.Visible = true;
				if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].BandGapMask > 0U)
				{
					this.labelOSCCAL_BandGap.Text = "OSCCAL && BandGap will be preserved.";
				}
			}
			if (this.dataSource == "Edited.")
			{
				this.labelDataSource.Text = "Edited Buffer.";
			}
			else
			{
				this.labelDataSource.Text = this.dataSource;
			}
			if (!this.writeProgMem)
			{
				this.labelCodeProtect.Text = "N/A";
				this.labelDataProtect.Text = "N/A";
			}
			else
			{
				if (this.codeProtect)
				{
					this.labelCodeProtect.Text = "ON";
				}
				else
				{
					this.labelCodeProtect.Text = "OFF";
				}
				if (this.dataProtect)
				{
					this.labelDataProtect.Text = "ON";
				}
				else if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].EEMem > 0)
				{
					this.labelDataProtect.Text = "OFF";
				}
				else
				{
					this.labelDataProtect.Text = "N/A";
				}
			}
			if (!this.writeProgMem)
			{
				this.labelMemRegions.Text = "Write EEPROM data only.";
			}
			else if (!this.writeEEPROM && PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].EEMem > 0)
			{
				this.labelMemRegions.Text = "Preserve EEPROM on write.";
			}
			else
			{
				this.labelMemRegions.Text = "Write entire device.";
			}
			if (this.verifyDevice)
			{
				this.labelVerify.Text = "Yes";
			}
			else
			{
				this.labelVerify.Text = "No - device will NOT be verified";
			}
			if (changePower)
			{
				this.radioButtonPK2Power.Text = string.Format("Power target from PICkit 2 at {0:0.0} Volts.", this.VDDVolts);
				if (this.vppFirst)
				{
					this.radioButtonSelfPower.Enabled = false;
					this.radioButtonSelfPower.Text = "Use VPP First - must power from PICkit 2";
					this.checkBoxRowErase.Enabled = false;
					this.radioButtonPK2Power.Checked = true;
					this.pickit2PowerRowErase();
				}
				else
				{
					this.radioButtonSelfPower.Checked = true;
					if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].DebugRowEraseScript > 0)
					{
						this.checkBoxRowErase.Text = string.Format("VDD < {0:0.0}V: Use low voltage row erase", PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].VddErase);
						this.checkBoxRowErase.Enabled = true;
					}
					else
					{
						this.checkBoxRowErase.Visible = false;
						this.checkBoxRowErase.Enabled = false;
						this.labelVDDMin.Text = string.Format("VDD must be >= {0:0.0} Volts.", PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].VddErase);
						this.labelVDDMin.Visible = true;
					}
				}
			}
			this.panelSettings.Visible = true;
		}

		// Token: 0x0600020C RID: 524 RVA: 0x0003FC14 File Offset: 0x0003EC14
		private bool pickit2PowerRowErase()
		{
			if (this.VDDVolts < PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].VddErase)
			{
				if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].DebugRowEraseScript <= 0)
				{
					MessageBox.Show(string.Format("PICkit 2 cannot program this device\nat the selected VDD voltage.\n\n{0:0.0}V is below the minimum for erase, {0:0.0}V", this.VDDVolts, PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].VddErase), "Programmer-To-Go");
					return false;
				}
				this.labelRowErase.Text = "Row Erase used: Will NOT program Code Protected parts!";
				this.labelRowErase.Visible = true;
			}
			else
			{
				this.labelRowErase.Visible = false;
			}
			return true;
		}

		// Token: 0x0600020D RID: 525 RVA: 0x0003FCCC File Offset: 0x0003ECCC
		private void fillDownload()
		{
			this.labelPNsmmry.Text = this.labelPartNumber.Text;
			this.labelSourceSmmry.Text = this.labelDataSource.Text;
			if (this.radioButtonSelfPower.Checked)
			{
				if (this.checkBoxRowErase.Enabled && this.checkBoxRowErase.Checked)
				{
					this.labelTargetPowerSmmry.Text = "Target is Powered (Use Low Voltage Row Erase)";
				}
				else
				{
					this.labelTargetPowerSmmry.Text = string.Format("Target is Powered (Min VDD = {0:0.0} Volts)", PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].VddErase);
				}
			}
			else
			{
				this.labelTargetPowerSmmry.Text = string.Format("Power target from PICkit 2 at {0:0.0} Volts", this.VDDVolts);
			}
			this.labelMemRegionsSmmry.Text = this.labelMemRegions.Text;
			if (this.writeProgMem)
			{
				if (this.codeProtect)
				{
					Label label = this.labelMemRegionsSmmry;
					label.Text += " -CP";
				}
				if (this.dataProtect)
				{
					Label label2 = this.labelMemRegionsSmmry;
					label2.Text += " -DP";
				}
			}
			if (this.vppFirst)
			{
				this.labelVPP1stSmmry.Text = "Use VPP 1st Program Entry";
			}
			else
			{
				this.labelVPP1stSmmry.Text = "";
			}
			if (this.verifyDevice)
			{
				this.labelVerifySmmry.Text = "Device will be verified";
			}
			else
			{
				this.labelVerifySmmry.Text = "Device will NOT be verified";
			}
			if (this.fastProgramming)
			{
				this.labelFastProgSmmry.Text = "Fast Programming is ON";
			}
			else
			{
				this.labelFastProgSmmry.Text = "Fast Programming is OFF";
			}
			if (this.holdMCLR)
			{
				this.labelMCLRHoldSmmry.Text = "MCLR kept asserted during && after programming";
			}
			else
			{
				this.labelMCLRHoldSmmry.Text = "MCLR released after programming";
			}
			this.panelDownload.Visible = true;
		}

		// Token: 0x0600020E RID: 526 RVA: 0x0003FEAC File Offset: 0x0003EEAC
		private void downloadGO()
		{
			this.panelDownload.Visible = false;
			this.panelDownloading.Visible = true;
			this.buttonHelp.Enabled = false;
			this.buttonBack.Enabled = false;
			this.buttonNext.Enabled = false;
			this.buttonCancel.Enabled = false;
			this.buttonCancel.Text = "Exit";
			base.Update();
			if (this.radioButtonSelfPower.Checked)
			{
				PICkitFunctions.ForceTargetPowered();
			}
			else
			{
				PICkitFunctions.ForcePICkitPowered();
			}
			if (this.ptgMemory <= 5)
			{
				PICkitFunctions.EnterLearnMode(this.ptgMemory);
			}
			else
			{
				PICkitFunctions.EnterLearnMode(0);
			}
			if (this.fastProgramming)
			{
				PICkitFunctions.SetProgrammingSpeed(0);
			}
			else
			{
				PICkitFunctions.SetProgrammingSpeed(this.icspSpeedSlow);
			}
			this.PICkit2WriteGo(true);
			PICkitFunctions.ExitLearnMode();
			if (this.ptgMemory <= 5)
			{
				PICkitFunctions.EnablePK2GoMode(this.ptgMemory);
			}
			else
			{
				PICkitFunctions.EnablePK2GoMode(0);
			}
			PICkitFunctions.DisconnectPICkit2Unit();
			this.panelDownloading.Visible = false;
			this.panelDownloadDone.Visible = true;
			this.buttonHelp.Enabled = true;
			this.buttonNext.Enabled = true;
			this.buttonNext.Text = "Next >";
			this.buttonCancel.Enabled = true;
			this.timerBlink.Enabled = true;
		}

		// Token: 0x0600020F RID: 527 RVA: 0x0003FFF9 File Offset: 0x0003EFF9
		private void radioButtonPK2Power_Click(object sender, EventArgs e)
		{
			this.radiobuttonPower();
		}

		// Token: 0x06000210 RID: 528 RVA: 0x00040001 File Offset: 0x0003F001
		private void radioButtonSelfPower_Click(object sender, EventArgs e)
		{
			this.radiobuttonPower();
		}

		// Token: 0x06000211 RID: 529 RVA: 0x0004000C File Offset: 0x0003F00C
		private void radiobuttonPower()
		{
			if (this.radioButtonPK2Power.Checked)
			{
				this.checkBoxRowErase.Enabled = false;
				if (!this.pickit2PowerRowErase())
				{
					this.radioButtonPK2Power.Checked = false;
					this.radioButtonSelfPower.Checked = true;
					return;
				}
			}
			else
			{
				if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].DebugRowEraseScript > 0)
				{
					this.checkBoxRowErase.Enabled = true;
				}
				else
				{
					this.checkBoxRowErase.Enabled = false;
				}
				if (this.checkBoxRowErase.Enabled && this.checkBoxRowErase.Checked)
				{
					this.labelRowErase.Text = "Row Erase used: Will NOT program Code Protected parts!";
					this.labelRowErase.Visible = true;
					return;
				}
				this.labelRowErase.Visible = false;
			}
		}

		// Token: 0x06000212 RID: 530 RVA: 0x000400D0 File Offset: 0x0003F0D0
		private void checkBoxRowErase_CheckedChanged(object sender, EventArgs e)
		{
			if (this.checkBoxRowErase.Enabled && this.checkBoxRowErase.Checked)
			{
				this.labelRowErase.Text = "Row Erase used: Will NOT program Code Protected parts!";
				this.labelRowErase.Visible = true;
				return;
			}
			this.labelRowErase.Visible = false;
		}

		// Token: 0x06000213 RID: 531 RVA: 0x00040120 File Offset: 0x0003F120
		private void timerBlink_Tick(object sender, EventArgs e)
		{
			if (this.panelDownloadDone.Visible)
			{
				this.blinkCount++;
				if (this.blinkCount > 5)
				{
					this.blinkCount = 0;
				}
				if (this.blinkCount < 4)
				{
					if ((this.blinkCount & 1) == 0)
					{
						this.pictureBoxTarget.BackColor = Color.Yellow;
						return;
					}
					this.pictureBoxTarget.BackColor = SystemColors.ControlText;
					return;
				}
			}
			else if (this.radioButtonVErr.Checked)
			{
				this.blinkCount++;
				if ((this.blinkCount & 1) == 0)
				{
					this.pictureBoxBusy.BackColor = Color.Red;
					return;
				}
				this.pictureBoxBusy.BackColor = SystemColors.ControlText;
				return;
			}
			else
			{
				int num = 4;
				if (this.radioButton3Blinks.Checked)
				{
					num = 6;
				}
				else if (this.radioButton4Blinks.Checked)
				{
					num = 8;
				}
				if (this.blinkCount++ <= num)
				{
					if ((this.blinkCount & 1) == 0)
					{
						this.pictureBoxBusy.BackColor = Color.Red;
						return;
					}
					this.pictureBoxBusy.BackColor = SystemColors.ControlText;
					return;
				}
				else
				{
					this.blinkCount = 0;
				}
			}
		}

		// Token: 0x06000214 RID: 532 RVA: 0x00040240 File Offset: 0x0003F240
		private void DialogPK2Go_FormClosing(object sender, FormClosingEventArgs e)
		{
			PICkitFunctions.ExitLearnMode();
		}

		// Token: 0x06000215 RID: 533 RVA: 0x00040248 File Offset: 0x0003F248
		private void radioButtonVErr_Click(object sender, EventArgs e)
		{
			if (this.radioButtonVErr.Checked)
			{
				this.timerBlink.Interval = 84;
				return;
			}
			this.timerBlink.Interval = 200;
		}

		// Token: 0x06000216 RID: 534 RVA: 0x00040275 File Offset: 0x0003F275
		private void buttonHelp_Click(object sender, EventArgs e)
		{
			this.OpenProgToGoGuide();
		}

		// Token: 0x0400038A RID: 906
		public float VDDVolts;

		// Token: 0x0400038B RID: 907
		public string dataSource = "--";

		// Token: 0x0400038C RID: 908
		public bool codeProtect;

		// Token: 0x0400038D RID: 909
		public bool dataProtect;

		// Token: 0x0400038E RID: 910
		public bool verifyDevice;

		// Token: 0x0400038F RID: 911
		public bool vppFirst;

		// Token: 0x04000390 RID: 912
		public bool writeProgMem = true;

		// Token: 0x04000391 RID: 913
		public bool writeEEPROM = true;

		// Token: 0x04000392 RID: 914
		public bool fastProgramming = true;

		// Token: 0x04000393 RID: 915
		public bool holdMCLR;

		// Token: 0x04000394 RID: 916
		public byte icspSpeedSlow = 4;

		// Token: 0x04000395 RID: 917
		private byte ptgMemory;

		// Token: 0x04000396 RID: 918
		private int blinkCount;

		// Token: 0x04000397 RID: 919
		public DelegateWrite PICkit2WriteGo;

		// Token: 0x04000398 RID: 920
		public DelegateOpenProgToGoGuide OpenProgToGoGuide;
	}
}
