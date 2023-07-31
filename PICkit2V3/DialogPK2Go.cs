using System;
using System.Drawing;
using System.Windows.Forms;

namespace PICkit2V3
{
	public partial class DialogPK2Go : Form
	{
		public DialogPK2Go()
		{
			InitializeComponent();
		}

		public void SetPTGMemory(byte value)
		{
			ptgMemory = value;
			if (ptgMemory > 0 && ptgMemory <= 5)
				label256K.Visible = true;
			if (ptgMemory == 1)
			{
				label256K.Text = "256K PICkit 2 upgrade support enabled.\r\n";
				return;
			}
			if (ptgMemory == 2)
			{
				label256K.Text = "512K SPI memory support enabled.\r\n";
				return;
			}
			if (ptgMemory == 3)
			{
				label256K.Text = "1M SPI memory support enabled.\r\n";
				return;
			}
			if (ptgMemory == 4)
			{
				label256K.Text = "2M SPI memory support enabled.\r\n";
				return;
			}
			if (ptgMemory == 5)
				label256K.Text = "4M SPI memory support enabled.\r\n";
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
				FillSettings(true);
				return;
			}
			if (panelSettings.Visible)
			{
				if (CheckEraseVoltage())
				{
					panelSettings.Visible = false;
					buttonNext.Text = "Download";
					FillDownload();
					return;
				}
			}
			else
			{
				if (panelDownload.Visible)
				{
					DownloadGO();
					return;
				}
				if (panelDownloadDone.Visible)
				{
					buttonNext.Enabled = false;
					panelDownloadDone.Visible = false;
					panelErrors.Visible = true;
					timerBlink.Interval = 84;
				}
			}
		}

		private void ButtonBack_Click(object sender, EventArgs e)
		{
			if (panelSettings.Visible)
			{
				panelSettings.Visible = false;
				panelIntro.Visible = true;
				buttonBack.Enabled = false;
				return;
			}
			if (panelDownload.Visible)
			{
				panelDownload.Visible = false;
				buttonNext.Text = "Next >";
				FillSettings(false);
			}
		}

		private bool CheckEraseVoltage()
		{
			if (radioButtonSelfPower.Checked)
				return true;

			if (vddVolts < PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].VddErase && PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].DebugRowEraseScript == 0)
			{
				DialogResult dialogResult = MessageBox.Show("The selected PICkit 2 VDD voltage is below\nthe minimum required to Bulk Erase this part.\n\nContinue anyway?", labelPartNumber.Text + " VDD Error", MessageBoxButtons.OKCancel);
				return dialogResult == DialogResult.OK;
			}
			return true;
		}

		private void FillSettings(bool changePower)
		{
			labelPartNumber.Text = PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].PartName;
			if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].OSSCALSave)
			{
				labelOSCCAL_BandGap.Visible = true;
				if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].BandGapMask > 0)
					labelOSCCAL_BandGap.Text = "OSCCAL && BandGap will be preserved.";
			}

			if (dataSource == "Edited.")
				labelDataSource.Text = "Edited Buffer.";
			else
				labelDataSource.Text = dataSource;

			if (!writeProgMem)
			{
				labelCodeProtect.Text = "N/A";
				labelDataProtect.Text = "N/A";
			}
			else
			{
				if (codeProtect)
					labelCodeProtect.Text = "ON";
				else
					labelCodeProtect.Text = "OFF";

				if (dataProtect)
					labelDataProtect.Text = "ON";
				else if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].EEMem > 0)
					labelDataProtect.Text = "OFF";
				else
					labelDataProtect.Text = "N/A";
			}

			if (!writeProgMem)
				labelMemRegions.Text = "Write EEPROM data only.";
			else if (!writeEEPROM && PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].EEMem > 0)
				labelMemRegions.Text = "Preserve EEPROM on write.";
			else
				labelMemRegions.Text = "Write entire device.";

			if (verifyDevice)
				labelVerify.Text = "Yes";
			else
				labelVerify.Text = "No - device will NOT be verified";

			if (changePower)
			{
				radioButtonPK2Power.Text = string.Format("Power target from PICkit 2 at {0:0.0} Volts.", vddVolts);
				if (vppFirst)
				{
					radioButtonSelfPower.Enabled = false;
					radioButtonSelfPower.Text = "Use VPP First - must power from PICkit 2";
					checkBoxRowErase.Enabled = false;
					radioButtonPK2Power.Checked = true;
					Pickit2PowerRowErase();
				}
				else
				{
					radioButtonSelfPower.Checked = true;
					if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].DebugRowEraseScript > 0)
					{
						checkBoxRowErase.Text = string.Format("VDD < {0:0.0}V: Use low voltage row erase", PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].VddErase);
						checkBoxRowErase.Enabled = true;
					}
					else
					{
						checkBoxRowErase.Visible = false;
						checkBoxRowErase.Enabled = false;
						labelVDDMin.Text = string.Format("VDD must be >= {0:0.0} Volts.", PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].VddErase);
						labelVDDMin.Visible = true;
					}
				}
			}
			panelSettings.Visible = true;
		}

		private bool Pickit2PowerRowErase()
		{
			if (vddVolts < PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].VddErase)
			{
				if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].DebugRowEraseScript <= 0)
				{
					MessageBox.Show(string.Format("PICkit 2 cannot program this device\nat the selected VDD voltage.\n\n{0:0.0}V is below the minimum for erase, {0:0.0}V", vddVolts, PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].VddErase), "Programmer-To-Go");
					return false;
				}
				labelRowErase.Text = "Row Erase used: Will NOT program Code Protected parts!";
				labelRowErase.Visible = true;
			}
			else
				labelRowErase.Visible = false;
			return true;
		}

		private void FillDownload()
		{
			labelPNsmmry.Text = labelPartNumber.Text;
			labelSourceSmmry.Text = labelDataSource.Text;
			if (radioButtonSelfPower.Checked)
			{
				if (checkBoxRowErase.Enabled && checkBoxRowErase.Checked)
					labelTargetPowerSmmry.Text = "Target is Powered (Use Low Voltage Row Erase)";
				else
					labelTargetPowerSmmry.Text = string.Format("Target is Powered (Min VDD = {0:0.0} Volts)", PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].VddErase);
			}
			else
				labelTargetPowerSmmry.Text = string.Format("Power target from PICkit 2 at {0:0.0} Volts", vddVolts);

			labelMemRegionsSmmry.Text = labelMemRegions.Text;
			if (writeProgMem)
			{
				if (codeProtect)
				{
					Label label = labelMemRegionsSmmry;
					label.Text += " -CP";
				}
				if (dataProtect)
				{
					Label label2 = labelMemRegionsSmmry;
					label2.Text += " -DP";
				}
			}

			if (vppFirst)
				labelVPP1stSmmry.Text = "Use VPP 1st Program Entry";
			else
				labelVPP1stSmmry.Text = "";

			if (verifyDevice)
				labelVerifySmmry.Text = "Device will be verified";
			else
				labelVerifySmmry.Text = "Device will NOT be verified";

			if (fastProgramming)
				labelFastProgSmmry.Text = "Fast Programming is ON";
			else
				labelFastProgSmmry.Text = "Fast Programming is OFF";

			if (holdMCLR)
				labelMCLRHoldSmmry.Text = "MCLR kept asserted during && after programming";
			else
				labelMCLRHoldSmmry.Text = "MCLR released after programming";
			panelDownload.Visible = true;
		}

		private void DownloadGO()
		{
			panelDownload.Visible = false;
			panelDownloading.Visible = true;
			buttonHelp.Enabled = false;
			buttonBack.Enabled = false;
			buttonNext.Enabled = false;
			buttonCancel.Enabled = false;
			buttonCancel.Text = "Exit";
			Update();

			if (radioButtonSelfPower.Checked)
				PICkitFunctions.ForceTargetPowered();
			else
				PICkitFunctions.ForcePICkitPowered();

			if (ptgMemory <= 5)
				PICkitFunctions.EnterLearnMode(ptgMemory);
			else
				PICkitFunctions.EnterLearnMode(0);

			if (fastProgramming)
				PICkitFunctions.SetProgrammingSpeed(0);
			else
				PICkitFunctions.SetProgrammingSpeed(icspSpeedSlow);

			PICkit2WriteGo(true);
			PICkitFunctions.ExitLearnMode();

			if (ptgMemory <= 5)
				PICkitFunctions.EnablePK2GoMode(ptgMemory);
			else
				PICkitFunctions.EnablePK2GoMode(0);

			PICkitFunctions.DisconnectPICkit2Unit();
			panelDownloading.Visible = false;
			panelDownloadDone.Visible = true;
			buttonHelp.Enabled = true;
			buttonNext.Enabled = true;
			buttonNext.Text = "Next >";
			buttonCancel.Enabled = true;
			timerBlink.Enabled = true;
		}

		private void RadioButtonPK2Power_Click(object sender, EventArgs e)
		{
			RadiobuttonPower();
		}

		private void RadioButtonSelfPower_Click(object sender, EventArgs e)
		{
			RadiobuttonPower();
		}

		private void RadiobuttonPower()
		{
			if (radioButtonPK2Power.Checked)
			{
				checkBoxRowErase.Enabled = false;
				if (!Pickit2PowerRowErase())
				{
					radioButtonPK2Power.Checked = false;
					radioButtonSelfPower.Checked = true;
					return;
				}
			}
			else
			{
				if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].DebugRowEraseScript > 0)
					checkBoxRowErase.Enabled = true;
				else
					checkBoxRowErase.Enabled = false;

				if (checkBoxRowErase.Enabled && checkBoxRowErase.Checked)
				{
					labelRowErase.Text = "Row Erase used: Will NOT program Code Protected parts!";
					labelRowErase.Visible = true;
					return;
				}
				labelRowErase.Visible = false;
			}
		}

		private void CheckBoxRowErase_CheckedChanged(object sender, EventArgs e)
		{
			if (checkBoxRowErase.Enabled && checkBoxRowErase.Checked)
			{
				labelRowErase.Text = "Row Erase used: Will NOT program Code Protected parts!";
				labelRowErase.Visible = true;
				return;
			}
			labelRowErase.Visible = false;
		}

		private void TimerBlink_Tick(object sender, EventArgs e)
		{
			if (panelDownloadDone.Visible)
			{
				blinkCount++;
				if (blinkCount > 5)
					blinkCount = 0;

				if (blinkCount < 4)
				{
					if ((blinkCount & 1) == 0)
					{
						pictureBoxTarget.BackColor = Color.Yellow;
						return;
					}
					pictureBoxTarget.BackColor = SystemColors.ControlText;
					return;
				}
			}
			else if (radioButtonVErr.Checked)
			{
				blinkCount++;
				if ((blinkCount & 1) == 0)
				{
					pictureBoxBusy.BackColor = Color.Red;
					return;
				}
				pictureBoxBusy.BackColor = SystemColors.ControlText;
				return;
			}
			else
			{
				int num = 4;
				if (radioButton3Blinks.Checked)
					num = 6;
				else if (radioButton4Blinks.Checked)
					num = 8;

				if (blinkCount++ <= num)
				{
					if ((blinkCount & 1) == 0)
					{
						pictureBoxBusy.BackColor = Color.Red;
						return;
					}
					pictureBoxBusy.BackColor = SystemColors.ControlText;
					return;
				}
				else
					blinkCount = 0;
			}
		}

		private void DialogPK2Go_FormClosing(object sender, FormClosingEventArgs e)
		{
			PICkitFunctions.ExitLearnMode();
		}

		private void RadioButtonVErr_Click(object sender, EventArgs e)
		{
			if (radioButtonVErr.Checked)
			{
				timerBlink.Interval = 84;
				return;
			}
			timerBlink.Interval = 200;
		}

		private void ButtonHelp_Click(object sender, EventArgs e)
		{
			OpenProgToGoGuide();
		}

        public float vddVolts;
        public string dataSource = "--";
        public bool codeProtect;
        public bool dataProtect;
        public bool verifyDevice;
        public bool vppFirst;
        public bool writeProgMem = true;
        public bool writeEEPROM = true;
        public bool fastProgramming = true;
        public bool holdMCLR;
        public byte icspSpeedSlow = 4;
        private byte ptgMemory;
        private int blinkCount;
        public DelegateWrite PICkit2WriteGo;
        public DelegateOpenProgToGoGuide OpenProgToGoGuide;
    }
}
