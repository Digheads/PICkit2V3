using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Pipes;
using System.Media;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace PICkit2V3
{
    public partial class FormPICkit2 : Form
	{
		[DllImport("user32.dll")]
		private static extern short FlashWindowEx(ref FLASHWINFO pwfi);

		public FormPICkit2(string filename = "")
		{
			InitializeComponent();
			float num = loadINI();
			if (mainWinOwnsMem)
			{
				AddOwnedForm(programMemMultiWin);
				AddOwnedForm(eepromDataMultiWin);
			}
			initializeGUI();
			if (!loadDeviceFile())
			{
				return;
			}
			if (toolStripMenuItemManualSelect.Checked)
			{
				ManualAutoSelectToggle(false);
			}
			buildDeviceMenu();
			if (!allowDataEdits)
			{
				dataGridProgramMemory.ReadOnly = true;
				dataGridViewEEPROM.ReadOnly = true;
			}
			PICkitFunctions.ResetBuffers();
			PIC32MXFunctions.UpdateStatusWinText = new DelegateStatusWin(StatusWinWr);
			PIC32MXFunctions.ResetStatusBar = new DelegateResetStatusBar(ResetStatusBar);
			PIC32MXFunctions.StepStatusBar = new DelegateStepStatusBar(StepStatusBar);
			dsPIC33_PE.UpdateStatusWinText = new DelegateStatusWin(StatusWinWr);
			dsPIC33_PE.ResetStatusBar = new DelegateResetStatusBar(ResetStatusBar);
			dsPIC33_PE.StepStatusBar = new DelegateStepStatusBar(StepStatusBar);
			PIC24F_PE.UpdateStatusWinText = new DelegateStatusWin(StatusWinWr);
			PIC24F_PE.ResetStatusBar = new DelegateResetStatusBar(ResetStatusBar);
			PIC24F_PE.StepStatusBar = new DelegateStepStatusBar(StepStatusBar);
			uartWindow.VddCallback = new DelegateVddCallback(SetVddState);
			logicWindow.VddCallback = new DelegateVddCallback(SetVddState);
			if (!detectPICkit2(true, true))
			{
				if (bootLoad)
				{
					return;
				}
				if (oldFirmware)
				{
					TestMemoryOpen = false;
					timerDLFW.Enabled = true;
					return;
				}
				Thread.Sleep(3000);
				if (!detectPICkit2(true, true))
				{
					return;
				}
			}
			partialEnableGUIControls();
			PICkitFunctions.ExitUARTMode();
			PICkitFunctions.VddOff();
			PICkitFunctions.SetVDDVoltage(3.3f, 0.85f);
			if (autoDetectToolStripMenuItem.Checked)
			{
				lookForPoweredTarget(false);
			}
			if (searchOnStartup && PICkitFunctions.DetectDevice(16777215, true, chkBoxVddOn.Checked))
			{
				setGUIVoltageLimits(true);
				PICkitFunctions.SetVDDVoltage((float)numUpDnVDD.Value, 0.85f);
				displayStatusWindow.Text += "\nPIC Device Found.";
				fullEnableGUIControls();
			}
			else
			{
				for (int i = 0; i < PICkitFunctions.DevFile.Info.NumberFamilies; i++)
				{
					if (PICkitFunctions.DevFile.Families[i].FamilyName == lastFamily)
					{
						PICkitFunctions.SetActiveFamily(i);
						if (!PICkitFunctions.DevFile.Families[i].PartDetect)
						{
							buildDeviceSelectDropDown(i);
							comboBoxSelectPart.Visible = true;
							comboBoxSelectPart.SelectedIndex = 0;
							displayDevice.Visible = true;
						}
					}
				}
				for (int j = 1; j < PICkitFunctions.DevFile.Info.NumberParts; j++)
				{
					if (PICkitFunctions.DevFile.PartsList[j].Family == PICkitFunctions.GetActiveFamily())
					{
						PICkitFunctions.DevFile.PartsList[0].VddMax = PICkitFunctions.DevFile.PartsList[j].VddMax;
						PICkitFunctions.DevFile.PartsList[0].VddMin = PICkitFunctions.DevFile.PartsList[j].VddMin;
						break;
					}
				}
				setGUIVoltageLimits(true);
			}
			if (num != 0f && PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].FamilyName == lastFamily && !selfPoweredTarget)
			{
				if (num > (float)numUpDnVDD.Maximum)
				{
					num = (float)numUpDnVDD.Maximum;
				}
				if (num < (float)numUpDnVDD.Minimum)
				{
					num = (float)numUpDnVDD.Minimum;
				}
				numUpDnVDD.Value = (decimal)num;
				PICkitFunctions.SetVDDVoltage((float)numUpDnVDD.Value, 0.85f);
			}
			checkForPowerErrors();
			if (TestMemoryEnabled)
			{
				toolStripMenuItemTestMemory.Visible = true;
				if (TestMemoryOpen)
				{
					openTestMemory();
				}
			}
			if (!PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].PartDetect)
			{
				disableGUIControls();
			}
			if (multiWindow)
			{
				saveMultWinPMemOpen = multiWinPMemOpen;
				toolStripMenuItemShowProgramMemory.Checked = false;
				multiWinPMemOpen = false;
				saveMultiWinEEMemOpen = multiWinEEMemOpen;
				toolStripMenuItemShowEEPROMData.Checked = false;
				multiWinEEMemOpen = false;
			}
			updateGUI(true);
			if (multiWindow)
			{
				timerInitalUpdate.Enabled = true;
			}

            if (!string.IsNullOrEmpty(filename) && File.Exists(filename))
                HexImportFromHistory(filename);

            var t = new Thread(ServerThread)
            {
                IsBackground = true
            };
            t.Start();
        }

		private void ServerThread()
		{
			NamedPipeServerStream pipeServer = new NamedPipeServerStream("PICkit2V3_Pipe", PipeDirection.InOut);
			StreamString ss = new StreamString(pipeServer);
			pipeServer.WaitForConnection();

			try
			{
				ss.WriteString("<RC%C6=?z76ek>*5");
				string filename = ss.ReadString();
                HexImportFromHistory(filename);
				WindowState = FormWindowState.Minimized;
				WindowState = FormWindowState.Normal;
            }
			finally
			{
				pipeServer.Close();
                var t = new Thread(ServerThread)
                {
                    IsBackground = true
                };
                t.Start();
            }
		}

        public void ExtCallUpdateGUI()
		{
			updateGUI(true);
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00025B23 File Offset: 0x00024B23
		public bool ExtCallVerify()
		{
			return this.deviceVerify(false, 0, false);
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00025B30 File Offset: 0x00024B30
		public bool ExtCallWrite(bool verify)
		{
			bool @checked = this.verifyOnWriteToolStripMenuItem.Checked;
			if (verify)
			{
				this.verifyOnWriteToolStripMenuItem.Checked = true;
			}
			else
			{
				this.verifyOnWriteToolStripMenuItem.Checked = false;
			}
			bool result = this.deviceWrite();
			this.verifyOnWriteToolStripMenuItem.Checked = @checked;
			return result;
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00025B7A File Offset: 0x00024B7A
		public void ExtCallRead()
		{
			this.deviceRead();
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00025B82 File Offset: 0x00024B82
		public void ExtCallErase(bool writeOSCCAL)
		{
			this.eraseDeviceAll(writeOSCCAL, new uint[0]);
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00025B91 File Offset: 0x00024B91
		public void ExtCallCalEraseWrite(uint[] calwords)
		{
			this.eraseDeviceAll(false, calwords);
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00025B9B File Offset: 0x00024B9B
		public bool ExtCallBlank()
		{
			return this.blankCheckDevice();
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00025BA3 File Offset: 0x00024BA3
		public void MultiWinProgMemClosed()
		{
			this.multiWinPMemOpen = false;
			this.toolStripMenuItemShowProgramMemory.Checked = false;
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00025BB8 File Offset: 0x00024BB8
		public void MultiWinEEMemClosed()
		{
			this.multiWinEEMemOpen = false;
			this.toolStripMenuItemShowEEPROMData.Checked = false;
		}

		public void ShowMemEdited()
		{
			displayDataSource.Text = "Edited.";
			checkImportFile = false;
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00025BE6 File Offset: 0x00024BE6
		public void StatusWinWr(string dispText)
		{
			this.displayStatusWindow.Text = dispText;
			base.Update();
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00025BFA File Offset: 0x00024BFA
		public void ResetStatusBar(int maxValue)
		{
			this.progressBar1.Value = 0;
			this.progressBar1.Maximum = maxValue;
			base.Update();
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00025C1A File Offset: 0x00024C1A
		public void StepStatusBar()
		{
			this.progressBar1.PerformStep();
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00025C28 File Offset: 0x00024C28
		public void SetVddState(bool force, bool forceState)
		{
			this.vddControl(force, forceState);
			this.uartWindow.SetVddBox(this.numUpDnVDD.Enabled, this.chkBoxVddOn.Checked);
			this.logicWindow.SetVddBox(this.numUpDnVDD.Enabled, this.chkBoxVddOn.Checked);
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00025C80 File Offset: 0x00024C80
		private bool checkForPowerErrors()
		{
			Thread.Sleep(100);
			Constants.PICkit2PWR pickit2PWR = PICkitFunctions.PowerStatus();
			if (pickit2PWR == Constants.PICkit2PWR.vdderror)
			{
				if (!this.timerAutoImportWrite.Enabled)
				{
					MessageBox.Show("PICkit 2 VDD voltage level error.\nCheck target & retry operation.", "PICkit 2 Error");
				}
			}
			else if (pickit2PWR == Constants.PICkit2PWR.vpperror)
			{
				if (!this.timerAutoImportWrite.Enabled)
				{
					MessageBox.Show("PICkit 2 VPP voltage level error.\nCheck target & retry operation.", "PICkit 2 Error");
				}
			}
			else if (pickit2PWR == Constants.PICkit2PWR.vddvpperrors)
			{
				if (!this.timerAutoImportWrite.Enabled)
				{
					MessageBox.Show("PICkit 2 VDD and VPP voltage level errors.\nCheck target & retry operation.", "PICkit 2 Error");
				}
			}
			else
			{
				if (pickit2PWR == Constants.PICkit2PWR.vdd_on)
				{
					this.chkBoxVddOn.Checked = true;
					return false;
				}
				if (pickit2PWR == Constants.PICkit2PWR.vdd_off)
				{
					this.chkBoxVddOn.Checked = false;
					return false;
				}
			}
			this.chkBoxVddOn.Checked = false;
			return true;
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00025D34 File Offset: 0x00024D34
		private bool lookForPoweredTarget(bool showMessageBox)
		{
			float num = 0f;
			float num2 = 0f;
			if (this.fastProgrammingToolStripMenuItem.Checked)
			{
				PICkitFunctions.SetProgrammingSpeed(0);
			}
			else
			{
				PICkitFunctions.SetProgrammingSpeed(FormPICkit2.slowSpeedICSP);
			}
			if (this.autoDetectToolStripMenuItem.Checked)
			{
				if (PICkitFunctions.CheckTargetPower(ref num, ref num2) == Constants.PICkit2PWR.selfpowered)
				{
					this.chkBoxVddOn.Checked = false;
					if (!FormPICkit2.selfPoweredTarget)
					{
						FormPICkit2.selfPoweredTarget = true;
						this.chkBoxVddOn.Enabled = true;
						this.chkBoxVddOn.Text = "Check";
						this.numUpDnVDD.Enabled = false;
						this.groupBoxVDD.Text = "VDD Target";
						if (showMessageBox)
						{
							MessageBox.Show("Powered target detected.\n VDD source set to target.", "Target VDD");
						}
					}
					this.numUpDnVDD.Maximum = (decimal)num;
					this.numUpDnVDD.Value = (decimal)num;
					this.numUpDnVDD.Update();
					return true;
				}
				if (FormPICkit2.selfPoweredTarget)
				{
					FormPICkit2.selfPoweredTarget = false;
					this.chkBoxVddOn.Enabled = true;
					this.chkBoxVddOn.Text = "On";
					this.numUpDnVDD.Enabled = true;
					this.setGUIVoltageLimits(true);
					this.groupBoxVDD.Text = "VDD PICkit 2";
					if (showMessageBox)
					{
						MessageBox.Show("Unpowered target detected.\n VDD source set to PICkit 2.", "Target VDD");
					}
				}
				return false;
			}
			else
			{
				if (this.forcePICkit2ToolStripMenuItem.Checked)
				{
					if (FormPICkit2.selfPoweredTarget)
					{
						PICkitFunctions.ForcePICkitPowered();
						FormPICkit2.selfPoweredTarget = false;
						this.chkBoxVddOn.Enabled = true;
						this.chkBoxVddOn.Text = "On";
						this.numUpDnVDD.Enabled = true;
						this.setGUIVoltageLimits(true);
						this.groupBoxVDD.Text = "VDD PICkit 2";
					}
					return false;
				}
				PICkitFunctions.CheckTargetPower(ref num, ref num2);
				PICkitFunctions.ForceTargetPowered();
				this.chkBoxVddOn.Checked = false;
				if (!FormPICkit2.selfPoweredTarget)
				{
					FormPICkit2.selfPoweredTarget = true;
					this.chkBoxVddOn.Enabled = true;
					this.chkBoxVddOn.Text = "Check";
					this.numUpDnVDD.Enabled = false;
					this.groupBoxVDD.Text = "VDD Target";
				}
				this.numUpDnVDD.Maximum = (decimal)num;
				this.numUpDnVDD.Value = (decimal)num;
				this.numUpDnVDD.Update();
				return true;
			}
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00025F74 File Offset: 0x00024F74
		private void setGUIVoltageLimits(bool setValue)
		{
			if (this.numUpDnVDD.Enabled)
			{
				this.numUpDnVDD.Maximum = (decimal)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].VddMax;
				this.numUpDnVDD.Minimum = (decimal)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].VddMin;
				if (PICkitFunctions.ActivePart != 0)
				{
					PICkitFunctions.DevFile.PartsList[0].VddMax = PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].VddMax;
					PICkitFunctions.DevFile.PartsList[0].VddMin = PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].VddMin;
				}
				if (setValue)
				{
					if ((double)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].VddMax <= 4.0 && (double)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].VddMax >= 3.3)
					{
						this.numUpDnVDD.Value = 3.3m;
						return;
					}
					if ((double)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].VddMax == 5.0)
					{
						this.numUpDnVDD.Value = 5.0m;
						return;
					}
					this.numUpDnVDD.Value = (decimal)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].VddMax;
				}
			}
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00026118 File Offset: 0x00025118
		private void initializeGUI()
		{
			FormPICkit2.ScalefactW = (float)this.dataGridProgramMemory.Size.Width / 502f;
			FormPICkit2.ScalefactH = (float)this.dataGridProgramMemory.Size.Height / 208f;
			this.dataGridConfigWords.BackgroundColor = SystemColors.Control;
			this.dataGridConfigWords.ColumnCount = 4;
			this.dataGridConfigWords.RowCount = 2;
			this.dataGridConfigWords.DefaultCellStyle.BackColor = SystemColors.Control;
			this.dataGridConfigWords[0, 0].Selected = true;
			this.dataGridConfigWords[0, 0].Selected = false;
			int width = (int)(40f * FormPICkit2.ScalefactW);
			for (int i = 0; i < 4; i++)
			{
				this.dataGridConfigWords.Columns[i].Width = width;
			}
			this.dataGridConfigWords.Rows[0].Height = (int)(17f * FormPICkit2.ScalefactH);
			this.dataGridConfigWords.Rows[1].Height = (int)(17f * FormPICkit2.ScalefactH);
			this.progressBar1.Step = 1;
			if (this.comboBoxProgMemView.SelectedIndex < 0)
			{
				this.comboBoxProgMemView.SelectedIndex = 0;
			}
			this.dataGridProgramMemory.DefaultCellStyle.Font = new Font("Courier New", 9f);
			this.dataGridProgramMemory.ColumnCount = 9;
			this.dataGridProgramMemory.RowCount = 512;
			this.dataGridProgramMemory[0, 0].Selected = true;
			this.dataGridProgramMemory[0, 0].Selected = false;
			width = (int)(59f * FormPICkit2.ScalefactW);
			this.dataGridProgramMemory.Columns[0].Width = width;
			this.dataGridProgramMemory.Columns[0].ReadOnly = true;
			width = (int)(53f * FormPICkit2.ScalefactW);
			for (int j = 1; j < 9; j++)
			{
				this.dataGridProgramMemory.Columns[j].Width = width;
			}
			for (int k = 0; k < 32; k++)
			{
				this.dataGridProgramMemory[0, k].Style.BackColor = SystemColors.ControlLight;
				this.dataGridProgramMemory[0, k].Value = string.Format("{0:X5}", k * 8);
			}
			if (this.comboBoxEE.SelectedIndex < 0)
			{
				this.comboBoxEE.SelectedIndex = 0;
			}
			this.dataGridViewEEPROM.DefaultCellStyle.Font = new Font("Courier New", 9f);
			this.dataGridViewEEPROM.ColumnCount = 9;
			this.dataGridViewEEPROM.RowCount = 16;
			width = (int)(40f * FormPICkit2.ScalefactW);
			this.dataGridViewEEPROM.Columns[0].Width = width;
			this.dataGridViewEEPROM.Columns[0].ReadOnly = true;
			width = (int)(41f * FormPICkit2.ScalefactW);
			for (int l = 1; l < 9; l++)
			{
				this.dataGridViewEEPROM.Columns[l].Width = width;
			}
			this.dataGridViewEEPROM[0, 0].Selected = true;
			this.dataGridViewEEPROM[0, 0].Selected = false;
			this.dataGridViewEEPROM.Visible = false;
			this.updateAlertSoundCheck();
			this.programMemMultiWin.TellMainFormProgMemClosed = new DelegateMultiProgMemClosed(this.MultiWinProgMemClosed);
			this.programMemMultiWin.TellMainFormProgMemEdited = new DelegateMemEdited(this.ShowMemEdited);
			this.programMemMultiWin.TellMainFormUpdateGUI = new DelegateUpdateGUI(this.ExtCallUpdateGUI);
			this.eepromDataMultiWin.TellMainFormEEMemClosed = new DelegateMultiEEMemClosed(this.MultiWinEEMemClosed);
			this.eepromDataMultiWin.TellMainFormProgMemEdited = new DelegateMemEdited(this.ShowMemEdited);
			this.eepromDataMultiWin.TellMainFormUpdateGUI = new DelegateUpdateGUI(this.ExtCallUpdateGUI);
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00026504 File Offset: 0x00025504
		private bool loadDeviceFile()
		{
			if (this.selectDeviceFile)
			{
				DialogDevFile dialogDevFile = new DialogDevFile();
				dialogDevFile.ShowDialog();
			}
			if (!PICkitFunctions.ReadDeviceFile(DeviceFileName))
			{
				displayStatusWindow.Text = "Device file " + DeviceFileName + " not found.\nMust be in same directory as executable.";
				checkCommunicationToolStripMenuItem.Enabled = false;
				return false;
			}
			if (PICkitFunctions.DevFile.Info.Compatibility < 0)
			{
				this.displayStatusWindow.Text = "Older device file is not compatible with this PICkit 2\nversion.  Visit www.microchip.com for updates.";
				this.checkCommunicationToolStripMenuItem.Enabled = false;
				return false;
			}
			if (PICkitFunctions.DevFile.Info.Compatibility > 6)
			{
				this.displayStatusWindow.Text = "The device file requires a newer version of PICkit 2.\nVisit www.microchip.com for updates.";
				this.checkCommunicationToolStripMenuItem.Enabled = false;
				return false;
			}
			return true;
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x000265C0 File Offset: 0x000255C0
		private bool detectPICkit2(bool showFound, bool detectMult)
		{
			Constants.PICkit2USB pickit2USB;
			if (detectMult)
			{
				FormPICkit2.pk2number = 0;
				pickit2USB = PICkitFunctions.DetectPICkit2Device(0, false);
				if (pickit2USB != Constants.PICkit2USB.notFound)
				{
					Constants.PICkit2USB pickit2USB2 = PICkitFunctions.DetectPICkit2Device(1, false);
					if (pickit2USB2 != Constants.PICkit2USB.notFound)
					{
						DialogUnitSelect dialogUnitSelect = new DialogUnitSelect();
						dialogUnitSelect.ShowDialog();
					}
				}
			}
			pickit2USB = PICkitFunctions.DetectPICkit2Device(FormPICkit2.pk2number, true);
			if (pickit2USB == Constants.PICkit2USB.found)
			{
				this.downloadPICkit2FirmwareToolStripMenuItem.Enabled = true;
				this.chkBoxVddOn.Enabled = true;
				if (!FormPICkit2.selfPoweredTarget)
				{
					this.numUpDnVDD.Enabled = true;
				}
				this.deviceToolStripMenuItem.Enabled = true;
				if (showFound)
				{
					string serialUnitID = PICkitFunctions.GetSerialUnitID();
					if (serialUnitID[0] == '-')
					{
						this.displayStatusWindow.Text = "PICkit 2 found and connected.";
						this.Text = "PICkit 2 Programmer";
					}
					else
					{
						this.displayStatusWindow.Text = "PICkit 2 connected.  ID = " + serialUnitID;
						this.Text = "PICkit 2 Programmer - " + serialUnitID;
					}
				}
				return true;
			}
			this.downloadPICkit2FirmwareToolStripMenuItem.Enabled = false;
			this.chkBoxVddOn.Enabled = false;
			this.numUpDnVDD.Enabled = false;
			this.deviceToolStripMenuItem.Enabled = false;
			this.disableGUIControls();
			if (pickit2USB == Constants.PICkit2USB.firmwareInvalid)
			{
				this.displayStatusWindow.BackColor = Color.Yellow;
				this.downloadPICkit2FirmwareToolStripMenuItem.Enabled = true;
				this.displayStatusWindow.Text = "The PICkit 2 OS v" + PICkitFunctions.FirmwareVersion + " must be updated.\nUse the Tools menu to download a new OS.";
				this.oldFirmware = true;
				return false;
			}
			if (pickit2USB == Constants.PICkit2USB.bootloader)
			{
				this.displayStatusWindow.BackColor = Color.Yellow;
				this.downloadPICkit2FirmwareToolStripMenuItem.Enabled = true;
				this.displayStatusWindow.Text = "The PICkit 2 has no Operating System.\nUse the Tools menu to download an OS.";
				this.bootLoad = true;
				return false;
			}
			this.displayStatusWindow.BackColor = Color.Salmon;
			this.displayStatusWindow.Text = "PICkit 2 not found.  Check USB connections and \nuse Tools->Check Communication to retry.";
			return false;
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00026778 File Offset: 0x00025778
		private void disableGUIControls()
		{
			this.importFileToolStripMenuItem.Enabled = false;
			this.exportFileToolStripMenuItem.Enabled = false;
			this.readDeviceToolStripMenuItem.Enabled = false;
			this.writeDeviceToolStripMenuItem.Enabled = false;
			this.verifyToolStripMenuItem.Enabled = false;
			this.eraseToolStripMenuItem.Enabled = false;
			this.blankCheckToolStripMenuItem.Enabled = false;
			this.writeOnPICkitButtonToolStripMenuItem.Enabled = false;
			this.pICkit2GoToolStripMenuItem.Enabled = false;
			this.setOSCCALToolStripMenuItem.Enabled = false;
			this.buttonRead.Enabled = false;
			this.buttonWrite.Enabled = false;
			this.buttonVerify.Enabled = false;
			this.buttonErase.Enabled = false;
			this.buttonBlankCheck.Enabled = false;
			this.checkBoxProgMemEnabled.Enabled = false;
			this.checkBoxProgMemEnabledAlt.Enabled = false;
			this.comboBoxProgMemView.Enabled = false;
			this.dataGridProgramMemory.ForeColor = SystemColors.GrayText;
			this.dataGridProgramMemory.Enabled = false;
			this.dataGridViewEEPROM.Visible = false;
			this.comboBoxEE.Enabled = false;
			this.checkBoxEEMem.Enabled = false;
			this.checkBoxEEDATAMemoryEnabledAlt.Enabled = false;
			this.buttonExportHex.Enabled = false;
			this.checkBoxAutoImportWrite.Enabled = false;
			this.troubleshhotToolStripMenuItem.Enabled = false;
			this.calibrateToolStripMenuItem.Enabled = false;
			this.programMemMultiWin.DisplayDisable();
			this.eepromDataMultiWin.DisplayDisable();
			this.UARTtoolStripMenuItem.Enabled = false;
			this.toolStripMenuItemLogicTool.Enabled = false;
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00026908 File Offset: 0x00025908
		private void partialEnableGUIControls()
		{
			this.importFileToolStripMenuItem.Enabled = true;
			this.exportFileToolStripMenuItem.Enabled = false;
			this.readDeviceToolStripMenuItem.Enabled = true;
			this.writeDeviceToolStripMenuItem.Enabled = true;
			this.verifyToolStripMenuItem.Enabled = true;
			this.eraseToolStripMenuItem.Enabled = true;
			this.blankCheckToolStripMenuItem.Enabled = true;
			this.writeOnPICkitButtonToolStripMenuItem.Enabled = true;
			this.pICkit2GoToolStripMenuItem.Enabled = true;
			this.setOSCCALToolStripMenuItem.Enabled = false;
			this.writeDeviceToolStripMenuItem.Enabled = false;
			this.verifyToolStripMenuItem.Enabled = false;
			this.buttonRead.Enabled = true;
			this.buttonWrite.Enabled = false;
			this.buttonVerify.Enabled = false;
			this.buttonErase.Enabled = true;
			this.buttonBlankCheck.Enabled = true;
			this.checkBoxProgMemEnabled.Enabled = false;
			this.checkBoxProgMemEnabledAlt.Enabled = false;
			this.comboBoxProgMemView.Enabled = false;
			this.dataGridProgramMemory.ForeColor = SystemColors.GrayText;
			this.dataGridProgramMemory.Enabled = false;
			this.dataGridViewEEPROM.Visible = false;
			this.comboBoxEE.Enabled = false;
			this.checkBoxEEMem.Enabled = false;
			this.checkBoxEEDATAMemoryEnabledAlt.Enabled = false;
			this.buttonExportHex.Enabled = false;
			this.checkBoxAutoImportWrite.Enabled = false;
			this.troubleshhotToolStripMenuItem.Enabled = true;
			this.calibrateToolStripMenuItem.Enabled = true;
			this.programMemMultiWin.DisplayDisable();
			this.eepromDataMultiWin.DisplayDisable();
			this.UARTtoolStripMenuItem.Enabled = true;
			this.toolStripMenuItemLogicTool.Enabled = true;
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00026AB0 File Offset: 0x00025AB0
		private void semiEnableGUIControls()
		{
			this.importFileToolStripMenuItem.Enabled = true;
			this.exportFileToolStripMenuItem.Enabled = false;
			this.readDeviceToolStripMenuItem.Enabled = true;
			this.writeDeviceToolStripMenuItem.Enabled = true;
			this.verifyToolStripMenuItem.Enabled = true;
			this.eraseToolStripMenuItem.Enabled = true;
			this.blankCheckToolStripMenuItem.Enabled = true;
			this.writeOnPICkitButtonToolStripMenuItem.Enabled = true;
			this.pICkit2GoToolStripMenuItem.Enabled = true;
			this.writeDeviceToolStripMenuItem.Enabled = true;
			this.verifyToolStripMenuItem.Enabled = true;
			this.setOSCCALToolStripMenuItem.Enabled = false;
			this.buttonRead.Enabled = true;
			this.buttonWrite.Enabled = true;
			this.buttonVerify.Enabled = true;
			this.buttonErase.Enabled = true;
			this.buttonBlankCheck.Enabled = true;
			this.checkBoxProgMemEnabled.Enabled = false;
			this.checkBoxProgMemEnabledAlt.Enabled = false;
			this.comboBoxProgMemView.Enabled = false;
			this.dataGridProgramMemory.ForeColor = SystemColors.GrayText;
			this.dataGridProgramMemory.Enabled = false;
			this.dataGridViewEEPROM.Visible = true;
			this.dataGridViewEEPROM.ForeColor = SystemColors.GrayText;
			this.comboBoxEE.Enabled = false;
			this.checkBoxEEMem.Enabled = false;
			this.checkBoxEEDATAMemoryEnabledAlt.Enabled = false;
			this.buttonExportHex.Enabled = false;
			this.checkBoxAutoImportWrite.Enabled = true;
			this.troubleshhotToolStripMenuItem.Enabled = true;
			this.calibrateToolStripMenuItem.Enabled = true;
			this.UARTtoolStripMenuItem.Enabled = true;
			this.toolStripMenuItemLogicTool.Enabled = true;
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00026C54 File Offset: 0x00025C54
		private void fullEnableGUIControls()
		{
			this.importFileToolStripMenuItem.Enabled = true;
			this.exportFileToolStripMenuItem.Enabled = true;
			this.readDeviceToolStripMenuItem.Enabled = true;
			this.writeDeviceToolStripMenuItem.Enabled = true;
			this.verifyToolStripMenuItem.Enabled = true;
			this.eraseToolStripMenuItem.Enabled = true;
			this.blankCheckToolStripMenuItem.Enabled = true;
			this.writeOnPICkitButtonToolStripMenuItem.Enabled = true;
			this.pICkit2GoToolStripMenuItem.Enabled = true;
			this.writeDeviceToolStripMenuItem.Enabled = true;
			this.verifyToolStripMenuItem.Enabled = true;
			this.buttonRead.Enabled = true;
			this.buttonWrite.Enabled = true;
			this.buttonVerify.Enabled = true;
			this.buttonErase.Enabled = true;
			this.buttonBlankCheck.Enabled = true;
			this.checkBoxProgMemEnabled.Enabled = true;
			this.checkBoxProgMemEnabledAlt.Enabled = true;
			this.comboBoxProgMemView.Enabled = true;
			this.dataGridProgramMemory.Enabled = true;
			this.dataGridProgramMemory.ForeColor = SystemColors.WindowText;
			this.dataGridViewEEPROM.ForeColor = SystemColors.WindowText;
			this.buttonExportHex.Enabled = true;
			this.checkBoxAutoImportWrite.Enabled = true;
			this.troubleshhotToolStripMenuItem.Enabled = true;
			this.calibrateToolStripMenuItem.Enabled = true;
			this.programMemMultiWin.DisplayEnable();
			this.eepromDataMultiWin.DisplayEnable();
			this.UARTtoolStripMenuItem.Enabled = true;
			this.toolStripMenuItemLogicTool.Enabled = true;
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00026DD0 File Offset: 0x00025DD0
		private void updateGUIView()
		{
			if (this.multiWindow)
			{
				this.toolStripMenuItemSingleWindow.Checked = false;
				this.toolStripMenuItemMultiWindow.Checked = true;
				this.groupBoxProgMem.Location = new Point((int)(12f * FormPICkit2.ScalefactW), (int)(300f * FormPICkit2.ScalefactH));
				base.Size = new Size((int)(544f * FormPICkit2.ScalefactW), (int)(320f * FormPICkit2.ScalefactH));
				this.pictureBox1.Location = new Point((int)(423f * FormPICkit2.ScalefactW), (int)(238f * FormPICkit2.ScalefactH));
				this.buttonExportHex.Location = new Point((int)(311f * FormPICkit2.ScalefactW), (int)(240f * FormPICkit2.ScalefactH));
				this.checkBoxAutoImportWrite.Location = new Point((int)(201f * FormPICkit2.ScalefactW), (int)(240f * FormPICkit2.ScalefactH));
				this.checkBoxProgMemEnabledAlt.Visible = true;
				this.checkBoxEEDATAMemoryEnabledAlt.Visible = true;
				this.toolStripMenuItemShowProgramMemory.Enabled = true;
				this.toolStripMenuItemShowEEPROMData.Enabled = true;
				this.mainWindowAlwaysInFrontToolStripMenuItem.Enabled = true;
				if (this.mainWinOwnsMem)
				{
					this.mainWindowAlwaysInFrontToolStripMenuItem.Checked = true;
				}
				Point right = new Point(0, 0);
				if (this.programMemMultiWin.Location == right && this.eepromDataMultiWin.Location == right)
				{
					this.programMemMultiWin.Location = new Point(base.Location.X, base.Location.Y + (int)(321f * FormPICkit2.ScalefactH));
					this.eepromDataMultiWin.Location = new Point(base.Location.X, base.Location.Y + (int)(530f * FormPICkit2.ScalefactH));
				}
				if (this.multiWinPMemOpen)
				{
					this.programMemMultiWin.Show();
					this.toolStripMenuItemShowProgramMemory.Checked = true;
				}
				if (this.multiWinEEMemOpen)
				{
					this.toolStripMenuItemShowEEPROMData.Checked = true;
					if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].EEMem > 0)
					{
						this.toolStripMenuItemShowEEPROMData.Enabled = true;
						this.eepromDataMultiWin.Show();
					}
					else
					{
						this.toolStripMenuItemShowEEPROMData.Enabled = false;
					}
				}
			}
			else
			{
				this.programMemMultiWin.Hide();
				this.eepromDataMultiWin.Hide();
				this.toolStripMenuItemSingleWindow.Checked = true;
				this.toolStripMenuItemMultiWindow.Checked = false;
				this.groupBoxProgMem.Location = new Point((int)(12f * FormPICkit2.ScalefactW), (int)(236f * FormPICkit2.ScalefactH));
				base.Size = new Size((int)(544f * FormPICkit2.ScalefactW), (int)(670f * FormPICkit2.ScalefactH));
				this.pictureBox1.Location = new Point((int)(423f * FormPICkit2.ScalefactW), (int)(586f * FormPICkit2.ScalefactH));
				this.buttonExportHex.Location = new Point((int)(423f * FormPICkit2.ScalefactW), (int)(545f * FormPICkit2.ScalefactH));
				this.checkBoxAutoImportWrite.Location = new Point((int)(423f * FormPICkit2.ScalefactW), (int)(505f * FormPICkit2.ScalefactH));
				this.checkBoxProgMemEnabledAlt.Visible = false;
				this.checkBoxEEDATAMemoryEnabledAlt.Visible = false;
				this.toolStripMenuItemShowProgramMemory.Enabled = false;
				this.toolStripMenuItemShowEEPROMData.Enabled = false;
				this.mainWindowAlwaysInFrontToolStripMenuItem.Enabled = false;
				this.toolStripMenuItemShowProgramMemory.Checked = false;
				this.toolStripMenuItemShowEEPROMData.Checked = false;
				this.mainWindowAlwaysInFrontToolStripMenuItem.Checked = false;
			}
			base.Focus();
		}

		private void updateGUI(bool updateMemories)
		{
			if (viewChanged)
			{
				updateGUIView();
				viewChanged = false;
			}
			statusGroupBox.Text = PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].FamilyName + " Configuration";
			if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].ProgEntryVPPScript > 0)
			{
				VppFirstToolStripMenuItem.Enabled = true;
			}
			else
			{
				VppFirstToolStripMenuItem.Checked = false;
				VppFirstToolStripMenuItem.Enabled = false;
			}
			if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].LVPScript > 0)
			{
				string text = PICkitFunctions.DevFile.Scripts[(int)(PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].LVPScript - 1)].ScriptName;
				text = text.Substring(text.Length - 2);
				if (text == "HV")
				{
					toolStripMenuItemLVPEnabled.Text = "Use &High Voltage Program Entry";
					labelLVP.Text = "HVP";
				}
				else
				{
					toolStripMenuItemLVPEnabled.Text = "Use &LVP Program Entry";
					labelLVP.Text = "LVP";
				}
				toolStripMenuItemLVPEnabled.Enabled = true;
				if (toolStripMenuItemLVPEnabled.Checked)
				{
					labelLVP.Visible = true;
				}
				else
				{
					labelLVP.Visible = false;
				}
			}
			else
			{
				toolStripMenuItemLVPEnabled.Text = "Use &LVP Program Entry";
				toolStripMenuItemLVPEnabled.Checked = false;
				toolStripMenuItemLVPEnabled.Enabled = false;
				labelLVP.Text = "LVP";
				labelLVP.Visible = false;
			}
			if (PICkitFunctions.FamilyIsEEPROM())
			{
				importFileToolStripMenuItem.Text = "&Import Hex/Bin";
				exportFileToolStripMenuItem.Text = "&Export Hex/Bin";
				toolStripMenuItemDisplayUnimplConfigAs.Enabled = false;
			}
			else
			{
				importFileToolStripMenuItem.Text = "&Import Hex";
				exportFileToolStripMenuItem.Text = "&Export Hex";
				toolStripMenuItemDisplayUnimplConfigAs.Enabled = true;
			}
			displayDevice.Text = PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].PartName;
			if (PICkitFunctions.ActivePart == 0)
			{
				if (PICkitFunctions.LastDeviceID == 0U)
				{
					displayDevice.Text = "No Device Found";
				}
				else
				{
					Label label = displayDevice;
					label.Text = label.Text + " (ID=" + string.Format("{0:X4}", PICkitFunctions.LastDeviceID) + ")";
				}
			}
			displayDevice.Update();
			displayRev.Text = " <" + string.Format("{0:X2}", PICkitFunctions.LastDeviceRev) + ">";
			if (updateMemories)
			{
				if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].UserIDWords > 0)
				{
					labelUserIDs.Enabled = true;
					if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].UserIDWords < 9)
					{
						displayUserIDs.Visible = true;
						buttonShowIDMem.Visible = false;
						string text2 = "";
						for (int i = 0; i < (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].UserIDWords; i++)
						{
							text2 += string.Format("{0:X2} ", 255U & PICkitFunctions.DeviceBuffers.UserIDs[i]);
						}
						displayUserIDs.Text = text2;
					}
					else
					{
						displayUserIDs.Visible = false;
						buttonShowIDMem.Visible = true;
						if (DialogUserIDs.IDMemOpen)
						{
							dialogIDMemory.UpdateIDMemoryGrid();
						}
					}
				}
				else
				{
					labelUserIDs.Enabled = false;
					displayUserIDs.Text = "";
					displayUserIDs.Visible = false;
					buttonShowIDMem.Visible = false;
				}
			}
			if (checkBoxProgMemEnabled.Checked)
			{
				displayUserIDs.ForeColor = SystemColors.WindowText;
			}
			else
			{
				displayUserIDs.ForeColor = SystemColors.GrayText;
			}
			if (updateMemories)
			{
				displayChecksum.Text = string.Format("{0:X4}", PICkitFunctions.ComputeChecksum(enableCodeProtectToolStripMenuItem.Checked, enableDataProtectStripMenuItem.Checked));
			}
			if (updateMemories)
			{
				if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigWords == 0 || PICkitFunctions.ActivePart == 0 || !allowDataEdits)
				{
					labelConfig.Enabled = false;
				}
				else
				{
					labelConfig.Enabled = true;
				}
				int num = 0;
				for (int j = 0; j < 2; j++)
				{
					for (int k = 0; k < 4; k++)
					{
						if (num < PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigWords)
						{
							uint num2 = PICkitFunctions.DeviceBuffers.ConfigWords[num];
							if (as0BitValueToolStripMenuItem.Checked)
							{
								num2 &= PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[num];
							}
							else if (as1BitValueToolStripMenuItem.Checked)
							{
								num2 |= ~(uint)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[num];
							}
							num2 &= PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue & 65535U;
							if ((PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].CPConfig - 1) == num)
							{
								if (enableCodeProtectToolStripMenuItem.Checked && (PICkitFunctions.DeviceBuffers.ConfigWords[(PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].CPConfig - 1)] & PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].CPMask) == PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].CPMask)
								{
									num2 &= ~(uint)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].CPMask;
								}
								if (enableDataProtectStripMenuItem.Checked && (PICkitFunctions.DeviceBuffers.ConfigWords[(PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].CPConfig - 1)] & PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].DPMask) == PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].DPMask)
								{
									num2 &= ~(uint)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].DPMask;
								}
							}
							dataGridConfigWords[k, j].Value = string.Format("{0:X4}", num2);
							num++;
						}
						else
						{
							dataGridConfigWords[k, j].Value = " ";
						}
						if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigWords == 9)
						{
							uint num3 = PICkitFunctions.DeviceBuffers.ConfigWords[8];
							if (as0BitValueToolStripMenuItem.Checked)
							{
								num3 &= PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[8];
							}
							else if (as1BitValueToolStripMenuItem.Checked)
							{
								num3 |= ~(uint)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[8];
							}
							num3 &= PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue & 65535U;
							labelConfig9.Text = string.Format("{0:X4}", num3);
							labelConfig9.Visible = true;
						}
						else
						{
							labelConfig9.Visible = false;
						}
					}
				}
			}
			if (checkBoxProgMemEnabled.Checked)
			{
				dataGridConfigWords.ForeColor = SystemColors.WindowText;
			}
			else
			{
				dataGridConfigWords.ForeColor = SystemColors.GrayText;
			}
			if (PICkitFunctions.FamilyIsEEPROM() && PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[0] == 1)
			{
				checkBoxA0CS.Visible = true;
				checkBoxA1CS.Visible = true;
				checkBoxA2CS.Visible = true;
				if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[3] == 1)
				{
					checkBoxA0CS.Enabled = true;
					checkBoxA1CS.Enabled = false;
					checkBoxA1CS.Checked = false;
					checkBoxA2CS.Enabled = false;
					checkBoxA2CS.Checked = false;
				}
				else if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[3] == 2)
				{
					checkBoxA0CS.Enabled = true;
					checkBoxA1CS.Enabled = true;
					checkBoxA2CS.Enabled = false;
					checkBoxA2CS.Checked = false;
				}
				else if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[3] == 3)
				{
					checkBoxA0CS.Enabled = true;
					checkBoxA1CS.Enabled = true;
					checkBoxA2CS.Enabled = true;
				}
				else
				{
					checkBoxA0CS.Enabled = false;
					checkBoxA0CS.Checked = false;
					checkBoxA1CS.Enabled = false;
					checkBoxA1CS.Checked = false;
					checkBoxA2CS.Enabled = false;
					checkBoxA2CS.Checked = false;
				}
			}
			else
			{
				checkBoxA0CS.Visible = false;
				checkBoxA1CS.Visible = false;
				checkBoxA2CS.Visible = false;
			}
			if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].OSSCALSave)
			{
				setOSCCALToolStripMenuItem.Enabled = true;
				labelOSCCAL.Enabled = true;
				displayOSCCAL.Text = string.Format("{0:X4}", PICkitFunctions.DeviceBuffers.OSCCAL);
				if (PICkitFunctions.ValidateOSSCAL())
				{
					labelOSSCALInvalid.Visible = false;
					displayOSCCAL.ForeColor = SystemColors.ControlText;
				}
				else
				{
					labelOSSCALInvalid.Visible = true;
					displayOSCCAL.ForeColor = Color.Red;
				}
			}
			else
			{
				labelOSSCALInvalid.Visible = false;
				setOSCCALToolStripMenuItem.Enabled = false;
				labelOSCCAL.Enabled = false;
				displayOSCCAL.Text = "";
			}
			if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].BandGapMask > 0U)
			{
				labelBandGap.Enabled = true;
				if (PICkitFunctions.DeviceBuffers.BandGap == PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue)
				{
					displayBandGap.Text = "";
				}
				else
				{
					displayBandGap.Text = string.Format("{0:X4}", PICkitFunctions.DeviceBuffers.BandGap);
				}
			}
			else
			{
				labelBandGap.Enabled = false;
				displayBandGap.Text = "";
			}
			switch (statusWindowColor)
			{
			case Constants.StatusColor.green:
				displayStatusWindow.BackColor = Color.LimeGreen;
				if (PlaySuccessWav)
				{
					wavPlayer.SoundLocation = SuccessWavFile;
					wavPlayer.Play();
				}
				break;
			case Constants.StatusColor.yellow:
				displayStatusWindow.BackColor = Color.Yellow;
				if (PlayWarningWav)
				{
					wavPlayer.SoundLocation = WarningWavFile;
					wavPlayer.Play();
				}
				break;
			case Constants.StatusColor.red:
				displayStatusWindow.BackColor = Color.Salmon;
				if (PlayErrorWav)
				{
					wavPlayer.SoundLocation = ErrorWavFile;
					wavPlayer.Play();
				}
				break;
			default:
				displayStatusWindow.BackColor = SystemColors.Info;
				break;
			}
			statusWindowColor = Constants.StatusColor.normal;
			if (PICkitFunctions.FamilyIsEEPROM())
			{
				checkBoxMCLR.Checked = false;
				checkBoxMCLR.Enabled = false;
				MCLRtoolStripMenuItem.Checked = false;
				MCLRtoolStripMenuItem.Enabled = false;
				PICkitFunctions.HoldMCLR(false);
			}
			else
			{
				checkBoxMCLR.Enabled = true;
				MCLRtoolStripMenuItem.Enabled = true;
			}
			if (PICkitFunctions.FamilyIsPIC32())
			{
				fastProgrammingToolStripMenuItem.Checked = true;
				fastProgrammingToolStripMenuItem.Enabled = false;
			}
			else
			{
				fastProgrammingToolStripMenuItem.Enabled = true;
			}
			if (updateMemories)
			{
				if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].CPMask == 0)
				{
					enableCodeProtectToolStripMenuItem.Checked = false;
					enableCodeProtectToolStripMenuItem.Enabled = false;
				}
				else
				{
					enableCodeProtectToolStripMenuItem.Enabled = true;
				}
			}
			if (updateMemories && multiWindow)
			{
				if (!programMemMultiWin.InitDone)
				{
					programMemMultiWin.InitProgMemDisplay(comboBoxProgMemView.SelectedIndex);
				}
				programMemMultiWin.UpdateMultiWinProgMem(displayDataSource.Text);
			}
			if (updateMemories && !multiWindow)
			{
				if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue > 16777215U)
				{
					comboBoxProgMemView.SelectedIndex = 0;
					comboBoxProgMemView.Enabled = false;
				}
				else
				{
					comboBoxProgMemView.Enabled = true;
				}
				int num4;
				int num5;
				int width;
				if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue <= 4095U)
				{
					if (PICkitFunctions.FamilyIsEEPROM())
					{
						num4 = 17;
						dataGridProgramMemory.Columns[0].Width = (int)(51f * ScalefactW);
						num5 = 16;
						width = (int)(27f * ScalefactW);
					}
					else
					{
						num4 = 17;
						dataGridProgramMemory.Columns[0].Width = (int)(35f * ScalefactW);
						num5 = 16;
						width = (int)(28f * ScalefactW);
					}
				}
				else if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue > 16777215U)
				{
					num4 = 5;
					dataGridProgramMemory.Columns[0].Width = (int)(99f * ScalefactW);
					num5 = 4;
					width = (int)(96f * ScalefactW);
				}
				else
				{
					num4 = 9;
					dataGridProgramMemory.Columns[0].Width = (int)(59f * ScalefactW);
					num5 = 8;
					width = (int)(53f * ScalefactW);
				}
				if (dataGridProgramMemory.ColumnCount != num4)
				{
					dataGridProgramMemory.Rows.Clear();
					dataGridProgramMemory.ColumnCount = num4;
				}
				for (int l = 1; l < dataGridProgramMemory.ColumnCount; l++)
				{
					dataGridProgramMemory.Columns[l].Width = width;
				}
				int addressIncrement = PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].AddressIncrement;
				int num6;
				int num7;
				int num8;
				if (comboBoxProgMemView.SelectedIndex == 0)
				{
					num6 = num5;
					num7 = (int)(PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgramMem / (uint)num6);
					if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgramMem % (ulong)((long)num6) > 0UL)
					{
						num7++;
					}
					num8 = addressIncrement * num5;
				}
				else
				{
					num6 = num5 / 2;
					num7 = (int)(PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgramMem / (uint)num6);
					if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgramMem % (ulong)((long)num6) > 0UL)
					{
						num7++;
					}
					num8 = addressIncrement * (num5 / 2);
				}
				if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue > 16777215U)
				{
					num7 += 2;
				}
				if (dataGridProgramMemory.RowCount != num7)
				{
					dataGridProgramMemory.Rows.Clear();
					dataGridProgramMemory.RowCount = num7;
				}
				for (int m = 0; m < num6; m++)
				{
					dataGridProgramMemory.Columns[m + 1].ReadOnly = false;
				}
				int num9 = dataGridProgramMemory.RowCount * num8 - 1;
				string format = "{0:X3}";
				if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue > 16777215U)
				{
					format = "{0:X8}";
				}
				else if (num9 > 65535)
				{
					format = "{0:X5}";
				}
				else if (num9 > 4095)
				{
					format = "{0:X4}";
				}
				int num10 = (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgramMem;
				int bootFlash = (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].BootFlash;
				num10 -= bootFlash;
				num10 /= num6;
				if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue > 16777215U)
				{
					dataGridProgramMemory.ShowCellToolTips = false;
					dataGridProgramMemory[0, 0].Value = "Program Flash";
					for (int n = 0; n < dataGridProgramMemory.ColumnCount; n++)
					{
						dataGridProgramMemory[n, 0].Style.BackColor = SystemColors.ControlDark;
						dataGridProgramMemory[n, 0].ReadOnly = true;
					}
					int num11 = 1;
					int num12 = 486539264;
					while (num11 <= num10)
					{
						dataGridProgramMemory[0, num11].Value = string.Format(format, num12);
						dataGridProgramMemory[0, num11].Style.BackColor = SystemColors.ControlLight;
						num12 += num8;
						num11++;
					}
					dataGridProgramMemory[0, num10 + 1].Value = "Boot Flash";
					for (int num13 = 0; num13 < dataGridProgramMemory.ColumnCount; num13++)
					{
						dataGridProgramMemory[num13, num10 + 1].Style.BackColor = SystemColors.ControlDark;
						dataGridProgramMemory[num13, num10 + 1].ReadOnly = true;
					}
					int num14 = num10 + 2;
					int num15 = 532676608;
					while (num14 < dataGridProgramMemory.RowCount)
					{
						dataGridProgramMemory[0, num14].Value = string.Format(format, num15);
						dataGridProgramMemory[0, num14].Style.BackColor = SystemColors.ControlLight;
						num15 += num8;
						num14++;
					}
				}
				else
				{
					dataGridProgramMemory.ShowCellToolTips = true;
					int num16 = 0;
					int num17 = 0;
					while (num16 < dataGridProgramMemory.RowCount)
					{
						dataGridProgramMemory[0, num16].Value = string.Format(format, num17);
						dataGridProgramMemory[0, num16].Style.BackColor = SystemColors.ControlLight;
						num17 += num8;
						num16++;
					}
				}
				string format2 = "{0:X2}";
				int numBytes = 1;
				if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue > 255U)
				{
					format2 = "{0:X3}";
					numBytes = 2;
				}
				if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue > 4095U)
				{
					format2 = "{0:X4}";
					numBytes = 2;
				}
				if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue > 65535U)
				{
					format2 = "{0:X6}";
					numBytes = 3;
				}
				if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue > 16777215U)
				{
					format2 = "{0:X8}";
					numBytes = 4;
				}
				if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue > 16777215U)
				{
					int num18 = 0;
					for (int num19 = 1; num19 <= num10; num19++)
					{
						for (int num20 = 0; num20 < num6; num20++)
						{
							dataGridProgramMemory[num20 + 1, num19].Value = string.Format(format2, PICkitFunctions.DeviceBuffers.ProgramMemory[num18++]);
						}
					}
					for (int num21 = num10 + 2; num21 < dataGridProgramMemory.RowCount; num21++)
					{
						for (int num22 = 0; num22 < num6; num22++)
						{
							dataGridProgramMemory[num22 + 1, num21].Value = string.Format(format2, PICkitFunctions.DeviceBuffers.ProgramMemory[num18++]);
						}
					}
				}
				else
				{
					int num23 = 0;
					int num24 = 0;
					while (num23 < dataGridProgramMemory.RowCount - 1)
					{
						for (int num25 = 0; num25 < num6; num25++)
						{
							dataGridProgramMemory[num25 + 1, num23].ToolTipText = string.Format(format, num24 * addressIncrement);
							dataGridProgramMemory[num25 + 1, num23].Value = string.Format(format2, PICkitFunctions.DeviceBuffers.ProgramMemory[num24++]);
						}
						num23++;
					}
				}
				int num26 = dataGridProgramMemory.RowCount - 1;
				int num27 = num26 * num6;
				int num28 = (int)(PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgramMem % (uint)num6);
				if (num28 == 0)
				{
					num28 = num6;
				}
				if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue <= 16777215U)
				{
					for (int num29 = 0; num29 < num6; num29++)
					{
						if (num29 < num28)
						{
							dataGridProgramMemory[num29 + 1, num26].ToolTipText = string.Format(format, num27 * addressIncrement);
							dataGridProgramMemory[num29 + 1, num26].Value = string.Format(format2, PICkitFunctions.DeviceBuffers.ProgramMemory[num27++]);
						}
						else
						{
							dataGridProgramMemory[num29 + 1, num26].ReadOnly = true;
						}
					}
				}
				if (comboBoxProgMemView.SelectedIndex >= 1)
				{
					for (int num30 = 0; num30 < num6; num30++)
					{
						dataGridProgramMemory.Columns[num30 + num6 + 1].ReadOnly = true;
					}
					if (comboBoxProgMemView.SelectedIndex == 1)
					{
						int num31 = 0;
						int num32 = 0;
						while (num31 < dataGridProgramMemory.RowCount)
						{
							for (int num33 = 0; num33 < num6; num33++)
							{
								dataGridProgramMemory[num33 + num6 + 1, num31].ToolTipText = string.Format(format, num32 * addressIncrement);
								dataGridProgramMemory[num33 + num6 + 1, num31].Value = Utilities.ConvertIntASCII((int)PICkitFunctions.DeviceBuffers.ProgramMemory[num32++], numBytes);
							}
							num31++;
						}
					}
					else
					{
						int num34 = 0;
						int num35 = 0;
						while (num34 < dataGridProgramMemory.RowCount)
						{
							for (int num36 = 0; num36 < num6; num36++)
							{
								dataGridProgramMemory[num36 + num6 + 1, num34].ToolTipText = string.Format(format, num35 * addressIncrement);
								dataGridProgramMemory[num36 + num6 + 1, num34].Value = Utilities.ConvertIntASCIIReverse((int)PICkitFunctions.DeviceBuffers.ProgramMemory[num35++], numBytes);
							}
							num34++;
						}
					}
				}
				if (dataGridProgramMemory.FirstDisplayedCell != null && !progMemJustEdited)
				{
					int rowIndex = dataGridProgramMemory.FirstDisplayedCell.RowIndex;
					dataGridProgramMemory.MultiSelect = false;
					dataGridProgramMemory[0, rowIndex].Selected = true;
					dataGridProgramMemory[0, rowIndex].Selected = false;
					dataGridProgramMemory.MultiSelect = true;
				}
				else if (dataGridProgramMemory.FirstDisplayedCell == null)
				{
					dataGridProgramMemory.MultiSelect = false;
					dataGridProgramMemory[0, 0].Selected = true;
					dataGridProgramMemory[0, 0].Selected = false;
					dataGridProgramMemory.MultiSelect = true;
				}
				progMemJustEdited = false;
			}
			if (updateMemories && PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].EEMem > 0)
			{
				checkBoxProgMemEnabled.Enabled = true;
				comboBoxEE.Enabled = true;
				if (!checkBoxEEMem.Enabled)
				{
					checkBoxEEMem.Checked = true;
					checkBoxEEDATAMemoryEnabledAlt.Checked = true;
				}
				checkBoxEEMem.Enabled = true;
				enableDataProtectStripMenuItem.Enabled = true;
				checkBoxEEDATAMemoryEnabledAlt.Enabled = true;
				checkBoxProgMemEnabledAlt.Enabled = true;
				if (multiWindow)
				{
					if (!eepromDataMultiWin.InitDone)
					{
						eepromDataMultiWin.InitMemDisplay(comboBoxEE.SelectedIndex);
					}
					if (!toolStripMenuItemShowEEPROMData.Enabled)
					{
						toolStripMenuItemShowEEPROMData.Enabled = true;
						if (multiWinEEMemOpen)
						{
							eepromDataMultiWin.Show();
							Focus();
						}
					}
					eepromDataMultiWin.UpdateMultiWinMem();
				}
				else
				{
					dataGridViewEEPROM.Visible = true;
					int num37 = PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].EEMemAddressIncrement;
					int num38 = num37;
					int num39;
					int num40;
					int width2;
					if (num37 == 1 && PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue != 4095U)
					{
						num39 = 17;
						dataGridViewEEPROM.Columns[0].Width = (int)(32f * ScalefactW);
						num40 = 16;
						width2 = (int)(21f * ScalefactW);
					}
					else
					{
						num39 = 9;
						dataGridViewEEPROM.Columns[0].Width = (int)(40f * ScalefactW);
						num40 = 8;
						width2 = (int)(41f * ScalefactW);
					}
					if (dataGridViewEEPROM.ColumnCount != num39)
					{
						dataGridViewEEPROM.Rows.Clear();
						dataGridViewEEPROM.ColumnCount = num39;
					}
					dataGridViewEEPROM.Columns[0].ReadOnly = true;
					for (int num41 = 1; num41 < dataGridViewEEPROM.ColumnCount; num41++)
					{
						dataGridViewEEPROM.Columns[num41].Width = width2;
					}
					int num42;
					int num43;
					if (comboBoxEE.SelectedIndex == 0)
					{
						num42 = num40;
						num43 = PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].EEMem / num42;
						num37 *= num40;
						num42 = num40;
					}
					else
					{
						num42 = num40 / 2;
						num43 = PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].EEMem / num42;
						num37 *= num40 / 2;
					}
					if (dataGridViewEEPROM.RowCount != num43)
					{
						dataGridViewEEPROM.Rows.Clear();
						dataGridViewEEPROM.RowCount = num43;
					}
					int num44 = dataGridViewEEPROM.RowCount * num37 - 1;
					string format3 = "{0:X2}";
					if (num44 > 255)
					{
						format3 = "{0:X3}";
					}
					else if (num44 > 4095)
					{
						format3 = "{0:X4}";
					}
					int num45 = 0;
					int num46 = 0;
					while (num45 < dataGridViewEEPROM.RowCount)
					{
						dataGridViewEEPROM[0, num45].Value = string.Format(format3, num46);
						dataGridViewEEPROM[0, num45].Style.BackColor = SystemColors.ControlLight;
						num46 += num37;
						num45++;
					}
					string format4 = "{0:X2}";
					int numBytes2 = 1;
					if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].EEMemAddressIncrement > 1)
					{
						format4 = "{0:X4}";
						numBytes2 = 2;
					}
					if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue == 4095U)
					{
						format4 = "{0:X3}";
						numBytes2 = 2;
					}
					for (int num47 = 0; num47 < num42; num47++)
					{
						dataGridViewEEPROM.Columns[num47 + 1].ReadOnly = false;
					}
					int num48 = 0;
					int num49 = 0;
					while (num48 < dataGridViewEEPROM.RowCount)
					{
						for (int num50 = 0; num50 < num42; num50++)
						{
							dataGridViewEEPROM[num50 + 1, num48].ToolTipText = string.Format(format3, num49 * num38);
							dataGridViewEEPROM[num50 + 1, num48].Value = string.Format(format4, PICkitFunctions.DeviceBuffers.EEPromMemory[num49++]);
						}
						num48++;
					}
					if (comboBoxEE.SelectedIndex >= 1)
					{
						for (int num51 = 0; num51 < num42; num51++)
						{
							dataGridViewEEPROM.Columns[num51 + num42 + 1].ReadOnly = true;
						}
						if (comboBoxEE.SelectedIndex == 1)
						{
							int num52 = 0;
							int num53 = 0;
							while (num52 < dataGridViewEEPROM.RowCount)
							{
								for (int num54 = 0; num54 < num42; num54++)
								{
									dataGridViewEEPROM[num54 + num42 + 1, num52].ToolTipText = string.Format(format3, num53 * num38);
									dataGridViewEEPROM[num54 + num42 + 1, num52].Value = Utilities.ConvertIntASCII((int)PICkitFunctions.DeviceBuffers.EEPromMemory[num53++], numBytes2);
								}
								num52++;
							}
						}
						else
						{
							int num55 = 0;
							int num56 = 0;
							while (num55 < dataGridViewEEPROM.RowCount)
							{
								for (int num57 = 0; num57 < num42; num57++)
								{
									dataGridViewEEPROM[num57 + num42 + 1, num55].ToolTipText = string.Format(format3, num56 * num38);
									dataGridViewEEPROM[num57 + num42 + 1, num55].Value = Utilities.ConvertIntASCIIReverse((int)PICkitFunctions.DeviceBuffers.EEPromMemory[num56++], numBytes2);
								}
								num55++;
							}
						}
					}
					if (dataGridViewEEPROM.FirstDisplayedCell != null && !eeMemJustEdited)
					{
						int rowIndex2 = dataGridViewEEPROM.FirstDisplayedCell.RowIndex;
						dataGridViewEEPROM.MultiSelect = false;
						dataGridViewEEPROM[0, rowIndex2].Selected = true;
						dataGridViewEEPROM[0, rowIndex2].Selected = false;
						dataGridViewEEPROM.MultiSelect = true;
					}
					else if (dataGridViewEEPROM.FirstDisplayedCell == null)
					{
						dataGridViewEEPROM.MultiSelect = false;
						dataGridViewEEPROM[0, 0].Selected = true;
						dataGridViewEEPROM[0, 0].Selected = false;
						dataGridViewEEPROM.MultiSelect = true;
					}
					eeMemJustEdited = false;
				}
			}
			else if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].EEMem == 0)
			{
				dataGridViewEEPROM.Visible = false;
				comboBoxEE.Enabled = false;
				checkBoxEEMem.Checked = false;
				checkBoxEEDATAMemoryEnabledAlt.Checked = false;
				checkBoxEEMem.Enabled = false;
				checkBoxEEDATAMemoryEnabledAlt.Enabled = false;
				enableDataProtectStripMenuItem.Enabled = false;
				enableDataProtectStripMenuItem.Checked = false;
				checkBoxProgMemEnabled.Checked = true;
				checkBoxProgMemEnabledAlt.Checked = true;
				checkBoxProgMemEnabled.Enabled = false;
				checkBoxProgMemEnabledAlt.Enabled = false;
				eepromDataMultiWin.Hide();
				toolStripMenuItemShowEEPROMData.Enabled = false;
			}
			if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].CPMask != 0 && (PICkitFunctions.DeviceBuffers.ConfigWords[PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].CPConfig - 1] & PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].CPMask) != PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].CPMask)
			{
				enableCodeProtectToolStripMenuItem.Checked = true;
				enableCodeProtectToolStripMenuItem.Enabled = false;
				if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].EEMem > 0 && PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].DPMask == 0)
				{
					enableDataProtectStripMenuItem.Checked = true;
					enableDataProtectStripMenuItem.Enabled = false;
				}
			}
			if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].EEMem > 0 && (PICkitFunctions.DeviceBuffers.ConfigWords[PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].CPConfig - 1] & PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].DPMask) != PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].DPMask)
			{
				enableDataProtectStripMenuItem.Checked = true;
				enableDataProtectStripMenuItem.Enabled = false;
			}
			if (enableCodeProtectToolStripMenuItem.Checked || enableDataProtectStripMenuItem.Checked)
			{
				labelCodeProtect.Visible = true;
				if (enableCodeProtectToolStripMenuItem.Checked && enableDataProtectStripMenuItem.Checked)
				{
					labelCodeProtect.Text = "All Protect";
				}
				else if (enableCodeProtectToolStripMenuItem.Checked)
				{
					labelCodeProtect.Text = "Code Protect";
				}
				else
				{
					labelCodeProtect.Text = "Data Protect";
				}
			}
			else
			{
				labelCodeProtect.Visible = false;
			}
			if (!checkBoxProgMemEnabled.Checked)
			{
				displayEEProgInfo.Text = "Write and Read EEPROM data only.";
				displayEEProgInfo.Visible = true;
				eepromDataMultiWin.DisplayEETextOn("Write and Read EEPROM data only.");
			}
			else if (!checkBoxEEMem.Checked && checkBoxEEMem.Enabled)
			{
				if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgMemEraseScript != 0)
				{
					displayEEProgInfo.Text = "Preserve device EEPROM data on write.";
					eepromDataMultiWin.DisplayEETextOn("Preserve device EEPROM data on write.");
				}
				else
				{
					displayEEProgInfo.Text = "Read/Restore device EEPROM on write.";
					eepromDataMultiWin.DisplayEETextOn("Read/Restore device EEPROM on write.");
				}
				displayEEProgInfo.Visible = true;
			}
			else
			{
				displayEEProgInfo.Visible = false;
				eepromDataMultiWin.DisplayEETextOff();
			}
			if (TestMemoryEnabled && TestMemoryOpen)
			{
				formTestMem.UpdateTestMemForm();
				if (updateMemories)
				{
					formTestMem.UpdateTestMemoryGrid();
				}
			}
		}

		private void progMemViewChanged(object sender, EventArgs e)
		{
			updateGUI(true);
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x000294A4 File Offset: 0x000284A4
		private void buildDeviceMenu()
		{
			for (int i = 0; i < PICkitFunctions.DevFile.Families.Length; i++)
			{
				for (int j = 0; j < PICkitFunctions.DevFile.Families.Length; j++)
				{
					if ((int)PICkitFunctions.DevFile.Families[j].FamilyType == i)
					{
						string familyName = PICkitFunctions.DevFile.Families[j].FamilyName;
						int num = familyName.IndexOf("/");
						if (familyName[0] != '#')
						{
							if (num < 0)
							{
								this.deviceToolStripMenuItem.DropDown.Items.Add(familyName);
							}
							else
							{
								int count = this.deviceToolStripMenuItem.DropDownItems.Count;
								for (int k = 0; k < count; k++)
								{
									if (familyName.Substring(0, num) == this.deviceToolStripMenuItem.DropDown.Items[k].ToString())
									{
										ToolStripMenuItem toolStripMenuItem = (ToolStripMenuItem)this.deviceToolStripMenuItem.DropDownItems[k];
										toolStripMenuItem.DropDown.Items.Add(familyName.Substring(num + 1));
									}
									else if (k == count - 1)
									{
										this.deviceToolStripMenuItem.DropDown.Items.Add(familyName.Substring(0, num));
										ToolStripMenuItem toolStripMenuItem2 = (ToolStripMenuItem)this.deviceToolStripMenuItem.DropDownItems[k + 1];
										toolStripMenuItem2.DropDown.Items.Add(familyName.Substring(num + 1));
										toolStripMenuItem2.DropDownItemClicked += this.deviceFamilyClick;
									}
								}
							}
						}
					}
				}
			}
			this.deviceToolStripMenuItem.Enabled = true;
		}

		// Token: 0x060000CA RID: 202 RVA: 0x0002965B File Offset: 0x0002865B
		private void guiVddControl(object sender, EventArgs e)
		{
			this.vddControl(false, false);
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00029668 File Offset: 0x00028668
		private void vddControl(bool force, bool forceState)
		{
			if (force)
			{
				this.chkBoxVddOn.Checked = forceState;
			}
			bool @checked = this.chkBoxVddOn.Checked;
			if (this.detectPICkit2(false, false))
			{
				if (@checked)
				{
					if (this.lookForPoweredTarget(true))
					{
						this.checkForPowerErrors();
						PICkitFunctions.VddOff();
						return;
					}
					this.chkBoxVddOn.Checked = true;
					PICkitFunctions.SetVDDVoltage((float)this.numUpDnVDD.Value, 0.85f);
					PICkitFunctions.VddOn();
					if (this.checkForPowerErrors())
					{
						PICkitFunctions.VddOff();
						return;
					}
				}
				else
				{
					this.chkBoxVddOn.Checked = false;
					PICkitFunctions.VddOff();
				}
			}
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00029703 File Offset: 0x00028703
		private void guiChangeVDD(object sender, EventArgs e)
		{
			if (this.detectPICkit2(false, false))
			{
				PICkitFunctions.SetVDDVoltage((float)this.numUpDnVDD.Value, 0.85f);
			}
		}

		// Token: 0x060000CD RID: 205 RVA: 0x0002972B File Offset: 0x0002872B
		private void pickitFormClosing(object sender, FormClosingEventArgs e)
		{
			this.SaveINI();
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00029733 File Offset: 0x00028733
		private void fileMenuExit(object sender, EventArgs e)
		{
			base.Close();
		}

		private void MenuFileImportHex(object sender, EventArgs e)
		{
			if (PICkitFunctions.FamilyIsKeeloq())
			{
				openHexFileDialog.Filter = "HEX files|*.hex;*.num|All files|*.*";
			}
			else if (PICkitFunctions.FamilyIsEEPROM())
			{
				openHexFileDialog.Filter = "HEX or BIN files|*.hex;*.bin|All files|*.*";
			}
			else
			{
				openHexFileDialog.Filter = "HEX files|*.hex|All files|*.*";
			}
			openHexFileDialog.ShowDialog();
			updateGUI(true);
		}

		private void ImportHexFile(object sender, CancelEventArgs e)
		{
			ImportHexFileGo();
		}

		private bool ImportHexFileGo()
		{
			int activePart = PICkitFunctions.ActivePart;
			bool flag = deviceVerification;
			deviceVerification = false;
			if (!preProgrammingCheck(PICkitFunctions.GetActiveFamily()))
			{
				deviceVerification = flag;
				displayStatusWindow.Text = "Device Error - hex file not loaded.";
				statusWindowColor = Constants.StatusColor.red;
				displayDataSource.Text = "None.";
				importGo = false;
				return false;
			}
			deviceVerification = flag;
			if (activePart != PICkitFunctions.ActivePart || PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].EEMem == 0 || (checkBoxProgMemEnabled.Checked && checkBoxEEMem.Checked))
			{
				PICkitFunctions.ResetBuffers();
			}
			else
			{
				if (checkBoxProgMemEnabled.Checked)
				{
					PICkitFunctions.DeviceBuffers.ClearProgramMemory(PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue);
					PICkitFunctions.DeviceBuffers.ClearConfigWords(PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigBlank);
					PICkitFunctions.DeviceBuffers.ClearUserIDs((int)PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].UserIDBytes, PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue);
				}
				if (checkBoxEEMem.Checked)
				{
					PICkitFunctions.DeviceBuffers.ClearEEPromMemory((int)PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].EEMemAddressIncrement, PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue);
				}
			}
			if (TestMemoryEnabled && TestMemoryOpen && formTestMem.HexImportExportTM())
			{
				formTestMem.ClearTestMemory();
			}
			string text = "Hex";
			if (openHexFileDialog.FileName.Substring(openHexFileDialog.FileName.Length - 4).ToUpper() == ".BIN" && PICkitFunctions.FamilyIsEEPROM())
			{
				text = "Bin";
			}
			switch (ImportExportHex.ImportHexFile(openHexFileDialog.FileName, checkBoxProgMemEnabled.Checked, checkBoxEEMem.Checked))
			{
				case Constants.FileRead.success:
					displayStatusWindow.Text = text + " file sucessfully imported.";
					if (multiWindow)
					{
						displayDataSource.Text = openHexFileDialog.FileName;
					}
					else
					{
						displayDataSource.Text = shortenHex(openHexFileDialog.FileName);
					}
					checkImportFile = true;
					importGo = true;
                    break;
                case Constants.FileRead.noconfig:
					statusWindowColor = Constants.StatusColor.yellow;
					displayStatusWindow.Text = "Warning: No configuration words in hex file.\nIn MPLAB use File-Export to save hex with config.";
					if (multiWindow)
					{
						displayDataSource.Text = openHexFileDialog.FileName;
					}
					else
					{
						displayDataSource.Text = shortenHex(openHexFileDialog.FileName);
					}
					checkImportFile = true;
					importGo = true;
                    break;
                case Constants.FileRead.partialcfg:
					statusWindowColor = Constants.StatusColor.yellow;
					displayStatusWindow.Text = "Warning: Some configuration words not in hex file.\nEnsure default values above right are acceptable.";
					if (multiWindow)
					{
						displayDataSource.Text = openHexFileDialog.FileName;
					}
					else
					{
						displayDataSource.Text = shortenHex(openHexFileDialog.FileName);
					}
					checkImportFile = true;
					importGo = true;
                    break;
                case Constants.FileRead.largemem:
					statusWindowColor = Constants.StatusColor.yellow;
					displayStatusWindow.Text = "Warning: " + text + " File Loaded is larger than device.";
					if (multiWindow)
					{
						displayDataSource.Text = openHexFileDialog.FileName;
					}
					else
					{
						displayDataSource.Text = shortenHex(openHexFileDialog.FileName);
					}
					checkImportFile = true;
					importGo = true;
					break;
                case Constants.FileRead.failed:
					statusWindowColor = Constants.StatusColor.red;
					displayStatusWindow.Text = "Error reading " + text + " file.";
					displayDataSource.Text = "None (Empty/Erased)";
					checkImportFile = false;
					importGo = false;
					PICkitFunctions.ResetBuffers();
					break;
			}

			if (checkImportFile)
			{
				if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].OSSCALSave)
				{
					PICkitFunctions.SetMCLRTemp(true);
					PICkitFunctions.VddOn();
					PICkitFunctions.ReadOSSCAL();
					PICkitFunctions.DeviceBuffers.ProgramMemory[PICkitFunctions.DeviceBuffers.ProgramMemory.Length - 1] = PICkitFunctions.DeviceBuffers.OSCCAL;
				}
				if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].BandGapMask > 0U)
				{
					PICkitFunctions.SetMCLRTemp(true);
					PICkitFunctions.VddOn();
					PICkitFunctions.ReadBandGap();
				}
				conditionalVDDOff();
				bool flag2 = false;
				bool flag3 = false;
				do
				{
					if (openHexFileDialog.FileName == hex4 || flag2)
					{
						if (!hex4ToolStripMenuItem.Visible && hex3.Length > 3)
						{
							hex4ToolStripMenuItem.Visible = true;
						}
						hex4 = hex3;
						hex4ToolStripMenuItem.Text = "&4" + hex3ToolStripMenuItem.Text.Substring(2);
						flag2 = true;
						flag3 = true;
					}
					if (openHexFileDialog.FileName == hex3 || flag2)
					{
						if (!hex3ToolStripMenuItem.Visible && hex2.Length > 3)
						{
							hex3ToolStripMenuItem.Visible = true;
						}
						hex3 = hex2;
						hex3ToolStripMenuItem.Text = "&3" + hex2ToolStripMenuItem.Text.Substring(2);
						flag2 = true;
						flag3 = true;
					}
					if (openHexFileDialog.FileName == hex2 || flag2)
					{
						if (!hex2ToolStripMenuItem.Visible && hex1.Length > 3)
						{
							hex2ToolStripMenuItem.Visible = true;
						}
						hex2 = hex1;
						hex2ToolStripMenuItem.Text = "&2" + hex1ToolStripMenuItem.Text.Substring(2);
						flag3 = true;
					}
					flag2 = true;
					if (openHexFileDialog.FileName == hex1)
					{
						flag3 = true;
					}
				}
				while (!flag3);
				if (!hex1ToolStripMenuItem.Visible)
				{
					hex1ToolStripMenuItem.Visible = true;
					toolStripMenuItem5.Visible = true;
				}
				hex1 = openHexFileDialog.FileName;
				hex1ToolStripMenuItem.Text = "&1 " + shortenHex(openHexFileDialog.FileName);
			}
			return checkImportFile;
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00029E4C File Offset: 0x00028E4C
		private void menuFileExportHex(object sender, EventArgs e)
		{
			if (PICkitFunctions.FamilyIsKeeloq())
			{
				MessageBox.Show("Export not supported for\nthis part type.");
				return;
			}
			if (PICkitFunctions.FamilyIsEEPROM())
			{
				this.saveHexFileDialog.Filter = "Hex files|*.hex|Bin Files|*.bin|All files|*.*";
			}
			else
			{
				this.saveHexFileDialog.Filter = "Hex files|*.hex|All files|*.*";
			}
			this.saveHexFileDialog.ShowDialog();
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00029EA1 File Offset: 0x00028EA1
		private void exportHexFile(object sender, CancelEventArgs e)
		{
			ImportExportHex.ExportHexFile(this.saveHexFileDialog.FileName, this.checkBoxProgMemEnabled.Checked, this.checkBoxEEMem.Checked);
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00029ECC File Offset: 0x00028ECC
		private bool preProgrammingCheck(int family)
		{
			this.statusGroupBox.Update();
			if (PICkitFunctions.LearnMode)
			{
				PICkitFunctions.SetVDDVoltage((float)this.numUpDnVDD.Value, 0.85f);
				return true;
			}
			if (!this.detectPICkit2(false, false))
			{
				return false;
			}
			if (this.checkForPowerErrors())
			{
				this.updateGUI(false);
				return false;
			}
			this.lookForPoweredTarget(!this.timerAutoImportWrite.Enabled);
			if (PICkitFunctions.DevFile.Families[family].PartDetect)
			{
				if (!PICkitFunctions.DetectDevice(family, false, this.chkBoxVddOn.Checked))
				{
					this.semiEnableGUIControls();
					FormPICkit2.statusWindowColor = Constants.StatusColor.yellow;
					this.displayStatusWindow.Text = "No device detected.";
					if (PICkitFunctions.DevFile.Families[family].Vpp < 1f)
					{
						Label label = this.displayStatusWindow;
						label.Text += "\nEnsure proper capacitance on VDDCORE/VCAP pin.";
					}
					this.checkForPowerErrors();
					this.updateGUI(false);
					return false;
				}
				this.setGUIVoltageLimits(false);
				this.fullEnableGUIControls();
				this.updateGUI(false);
			}
			else if (PICkitFunctions.DevFile.Families[family].DeviceIDMask > 0U && this.deviceVerification)
			{
				if (!PICkitFunctions.VerifyDeviceID(false, this.chkBoxVddOn.Checked))
				{
					FormPICkit2.statusWindowColor = Constants.StatusColor.yellow;
					if (PICkitFunctions.LastDeviceID == 0U || PICkitFunctions.LastDeviceID == PICkitFunctions.DevFile.Families[family].DeviceIDMask)
					{
						this.displayStatusWindow.Text = "No device detected.";
					}
					else
					{
						this.displayStatusWindow.Text = "Selected device not detected.";
						for (int i = 0; i < PICkitFunctions.DevFile.PartsList.Length; i++)
						{
							if ((int)PICkitFunctions.DevFile.PartsList[i].Family == family && PICkitFunctions.DevFile.PartsList[i].DeviceID == PICkitFunctions.LastDeviceID)
							{
								Label label2 = this.displayStatusWindow;
								label2.Text = label2.Text + "\nDetected a " + PICkitFunctions.DevFile.PartsList[i].PartName + " instead.";
								break;
							}
						}
					}
					this.checkForPowerErrors();
					this.updateGUI(false);
					return false;
				}
			}
			else
			{
				PICkitFunctions.SetMCLRTemp(true);
				PICkitFunctions.SetVDDVoltage((float)this.numUpDnVDD.Value, 0.85f);
				PICkitFunctions.VddOn();
				PICkitFunctions.RunScript(0, 1);
				Thread.Sleep(300);
				PICkitFunctions.RunScript(1, 1);
				this.conditionalVDDOff();
				if (this.checkForPowerErrors())
				{
					this.updateGUI(false);
					return false;
				}
			}
			PICkitFunctions.SetVDDVoltage((float)this.numUpDnVDD.Value, 0.85f);
			if (!this.checkBoxEEMem.Enabled && PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].EEMem > 0)
			{
				this.checkBoxEEMem.Checked = true;
			}
			return true;
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x0002A1AD File Offset: 0x000291AD
		private void readDevice(object sender, EventArgs e)
		{
			this.deviceRead();
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x0002A1B8 File Offset: 0x000291B8
		private void deviceRead()
		{
			if (PICkitFunctions.FamilyIsKeeloq())
			{
				this.displayStatusWindow.Text = "Read not supported for this device type.";
				FormPICkit2.statusWindowColor = Constants.StatusColor.yellow;
				this.updateGUI(false);
				return;
			}
			if (!this.preProgrammingCheck(PICkitFunctions.GetActiveFamily()))
			{
				return;
			}
			if (PICkitFunctions.FamilyIsPIC32())
			{
				if (PIC32MXFunctions.PIC32Read())
				{
					FormPICkit2.statusWindowColor = Constants.StatusColor.normal;
				}
				else
				{
					FormPICkit2.statusWindowColor = Constants.StatusColor.red;
				}
				this.conditionalVDDOff();
				this.updateGUI(true);
				return;
			}
			this.displayStatusWindow.Text = "Reading device:\n";
			base.Update();
			byte[] array = new byte[128];
			PICkitFunctions.SetMCLRTemp(true);
			PICkitFunctions.VddOn();
			if (this.checkBoxProgMemEnabled.Checked)
			{
				Label label = this.displayStatusWindow;
				label.Text += "Program Memory... ";
				base.Update();
				if (this.useProgExec33())
				{
					if (!dsPIC33_PE.PE33Read(this.displayStatusWindow.Text))
					{
						FormPICkit2.statusWindowColor = Constants.StatusColor.red;
						this.conditionalVDDOff();
						this.updateGUI(true);
						return;
					}
				}
				else if (this.useProgExec24F())
				{
					if (!PIC24F_PE.PE24FRead(this.displayStatusWindow.Text))
					{
						FormPICkit2.statusWindowColor = Constants.StatusColor.red;
						this.conditionalVDDOff();
						this.updateGUI(true);
						return;
					}
				}
				else
				{
					PICkitFunctions.RunScript(0, 1);
					if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgMemAddrSetScript != 0 && PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgMemAddrBytes != 0)
					{
						if (PICkitFunctions.FamilyIsEEPROM())
						{
							PICkitFunctions.DownloadAddress3MSBFirst(this.eeprom24BitAddress(0, false));
							PICkitFunctions.RunScript(5, 1);
							if (this.eeprom_CheckBusErrors())
							{
								return;
							}
						}
						else
						{
							PICkitFunctions.DownloadAddress3(0);
							PICkitFunctions.RunScript(5, 1);
						}
					}
					int bytesPerLocation = (int)PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BytesPerLocation;
					int num = 128 / ((int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgMemRdWords * bytesPerLocation);
					int num2 = num * (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgMemRdWords;
					int num3 = 0;
					this.progressBar1.Value = 0;
					this.progressBar1.Maximum = (int)(PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgramMem / (uint)num2);
					do
					{
						if (PICkitFunctions.FamilyIsEEPROM())
						{
							if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[0] == 1 && num3 > (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[1] && num3 % (int)(PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[1] + 1) == 0)
							{
								PICkitFunctions.DownloadAddress3MSBFirst(this.eeprom24BitAddress(num3, false));
								PICkitFunctions.RunScript(5, 1);
							}
							PICkitFunctions.Download3Multiples(this.eeprom24BitAddress(num3, true), num, (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgMemRdWords);
						}
						PICkitFunctions.RunScriptUploadNoLen(3, num);
						Array.Copy(PICkitFunctions.Usb_read_array, 1L, array, 0L, 64L);
						PICkitFunctions.UploadDataNoLen();
						Array.Copy(PICkitFunctions.Usb_read_array, 1L, array, 64L, 64L);
						int num4 = 0;
						for (int i = 0; i < num2; i++)
						{
							int num5 = 0;
							uint num6 = (uint)array[num4 + num5++];
							if (num5 < bytesPerLocation)
							{
								num6 |= (uint)((uint)array[num4 + num5++] << 8);
							}
							if (num5 < bytesPerLocation)
							{
								num6 |= (uint)((uint)array[num4 + num5++] << 16);
							}
							if (num5 < bytesPerLocation)
							{
								num6 |= (uint)((uint)array[num4 + num5++] << 24);
							}
							num4 += num5;
							if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].ProgMemShift > 0)
							{
								num6 = (num6 >> 1 & PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue);
							}
							PICkitFunctions.DeviceBuffers.ProgramMemory[num3++] = num6;
							if ((long)num3 == (long)((ulong)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgramMem))
							{
								break;
							}
							if (num3 % 32768 == 0 && PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgMemAddrSetScript != 0 && PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgMemAddrBytes != 0 && PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue > 65535U)
							{
								PICkitFunctions.DownloadAddress3(65536 * (num3 / 32768));
								PICkitFunctions.RunScript(5, 1);
								break;
							}
						}
						this.progressBar1.PerformStep();
					}
					while ((long)num3 < (long)((ulong)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgramMem));
					PICkitFunctions.RunScript(1, 1);
					if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[0] == 3 && bytesPerLocation == 2 && PICkitFunctions.FamilyIsEEPROM())
					{
						for (int j = 0; j < PICkitFunctions.DeviceBuffers.ProgramMemory.Length; j++)
						{
							uint num7 = PICkitFunctions.DeviceBuffers.ProgramMemory[j] << 8 & 65280U;
							PICkitFunctions.DeviceBuffers.ProgramMemory[j] >>= 8;
							PICkitFunctions.DeviceBuffers.ProgramMemory[j] |= num7;
						}
					}
				}
			}
			if (this.checkBoxEEMem.Checked && PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].EEMem > 0)
			{
				this.readEEPROM();
			}
			if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].UserIDWords > 0 && this.checkBoxProgMemEnabled.Checked)
			{
				Label label2 = this.displayStatusWindow;
				label2.Text += "UserIDs... ";
				base.Update();
				PICkitFunctions.RunScript(0, 1);
				if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].UserIDRdPrepScript > 0)
				{
					PICkitFunctions.RunScript(16, 1);
				}
				int userIDBytes = (int)PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].UserIDBytes;
				int num8 = 0;
				int num9 = 0;
				PICkitFunctions.RunScriptUploadNoLen(17, 1);
				Array.Copy(PICkitFunctions.Usb_read_array, 1L, array, 0L, 64L);
				if ((long)((int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].UserIDWords * userIDBytes) > 64L)
				{
					PICkitFunctions.UploadDataNoLen();
					Array.Copy(PICkitFunctions.Usb_read_array, 1L, array, 64L, 64L);
				}
				PICkitFunctions.RunScript(1, 1);
				do
				{
					int num10 = 0;
					uint num11 = (uint)array[num9 + num10++];
					if (num10 < userIDBytes)
					{
						num11 |= (uint)((uint)array[num9 + num10++] << 8);
					}
					if (num10 < userIDBytes)
					{
						num11 |= (uint)((uint)array[num9 + num10++] << 16);
					}
					if (num10 < userIDBytes)
					{
						num11 |= (uint)((uint)array[num9 + num10++] << 24);
					}
					num9 += num10;
					if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].ProgMemShift > 0)
					{
						num11 = (num11 >> 1 & PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue);
					}
					PICkitFunctions.DeviceBuffers.UserIDs[num8++] = num11;
				}
				while (num8 < (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].UserIDWords);
			}
			int num12 = (int)(PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigAddr / (uint)PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].ProgMemHexBytes);
			int configWords = (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigWords;
			if (configWords > 0 && (long)num12 >= (long)((ulong)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgramMem) && this.checkBoxProgMemEnabled.Checked)
			{
				Label label3 = this.displayStatusWindow;
				label3.Text += "Config... ";
				base.Update();
				PICkitFunctions.ReadConfigOutsideProgMem();
				if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].BandGapMask > 0U)
				{
					PICkitFunctions.DeviceBuffers.BandGap = (PICkitFunctions.DeviceBuffers.ConfigWords[0] & PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].BandGapMask);
				}
			}
			else if (configWords > 0 && this.checkBoxProgMemEnabled.Checked)
			{
				Label label4 = this.displayStatusWindow;
				label4.Text += "Config... ";
				base.Update();
				for (int k = 0; k < configWords; k++)
				{
					PICkitFunctions.DeviceBuffers.ConfigWords[k] = PICkitFunctions.DeviceBuffers.ProgramMemory[num12 + k];
				}
			}
			if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].OSSCALSave)
			{
				PICkitFunctions.ReadOSSCAL();
			}
			if (FormPICkit2.TestMemoryEnabled && FormPICkit2.TestMemoryOpen)
			{
				FormPICkit2.formTestMem.ReadTestMemory();
			}
			this.conditionalVDDOff();
			Label label5 = this.displayStatusWindow;
			label5.Text += "Done.";
			this.displayDataSource.Text = "Read from " + PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].PartName;
			this.checkImportFile = false;
			this.updateGUI(true);
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x0002AAF0 File Offset: 0x00029AF0
		private void readEEPROM()
		{
			byte[] array = new byte[128];
			Label label = this.displayStatusWindow;
			label.Text += "EE... ";
			base.Update();
			PICkitFunctions.RunScript(0, 1);
			if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].EERdPrepScript > 0)
			{
				if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].EEMemHexBytes == 4)
				{
					PICkitFunctions.DownloadAddress3((int)(PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].EEAddr / 2U));
				}
				else
				{
					PICkitFunctions.DownloadAddress3(0);
				}
				PICkitFunctions.RunScript(8, 1);
			}
			int eememBytesPerWord = (int)PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].EEMemBytesPerWord;
			int num = 128 / ((int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].EERdLocations * eememBytesPerWord);
			int num2 = num * (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].EERdLocations;
			int num3 = 0;
			uint eeblank = this.getEEBlank();
			this.progressBar1.Value = 0;
			this.progressBar1.Maximum = (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].EEMem / num2;
			do
			{
				PICkitFunctions.RunScriptUploadNoLen(9, num);
				Array.Copy(PICkitFunctions.Usb_read_array, 1L, array, 0L, 64L);
				PICkitFunctions.UploadDataNoLen();
				Array.Copy(PICkitFunctions.Usb_read_array, 1L, array, 64L, 64L);
				int num4 = 0;
				for (int i = 0; i < num2; i++)
				{
					int num5 = 0;
					uint num6 = (uint)array[num4 + num5++];
					if (num5 < eememBytesPerWord)
					{
						num6 |= (uint)((uint)array[num4 + num5++] << 8);
					}
					num4 += num5;
					if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].ProgMemShift > 0)
					{
						num6 = (num6 >> 1 & eeblank);
					}
					PICkitFunctions.DeviceBuffers.EEPromMemory[num3++] = num6;
					if (num3 >= (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].EEMem)
					{
						break;
					}
				}
				this.progressBar1.PerformStep();
			}
			while (num3 < (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].EEMem);
			PICkitFunctions.RunScript(1, 1);
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x0002AD36 File Offset: 0x00029D36
		private void eraseDevice(object sender, EventArgs e)
		{
			this.eraseDeviceAll(false, new uint[0]);
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x0002AD48 File Offset: 0x00029D48
		private void eraseDeviceAll(bool forceOSSCAL, uint[] calWords)
		{
			if (PICkitFunctions.FamilyIsKeeloq() || PICkitFunctions.FamilyIsMCP())
			{
				this.displayStatusWindow.Text = "Erase not supported for this device type.";
				FormPICkit2.statusWindowColor = Constants.StatusColor.yellow;
				this.updateGUI(false);
				return;
			}
			if (!this.preProgrammingCheck(PICkitFunctions.GetActiveFamily()))
			{
				return;
			}
			DeviceData deviceData = PICkitFunctions.CloneBuffers(PICkitFunctions.DeviceBuffers);
			if (PICkitFunctions.FamilyIsEEPROM() && PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[0] != 3 && PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[0] != 4)
			{
				PICkitFunctions.ResetBuffers();
				this.checkImportFile = false;
				if (!this.eepromWrite(true))
				{
					if (!this.toolStripMenuItemClearBuffersErase.Checked)
					{
						PICkitFunctions.DeviceBuffers = deviceData;
					}
					return;
				}
				Label label = this.displayStatusWindow;
				label.Text += "Complete";
				if (!this.toolStripMenuItemClearBuffersErase.Checked)
				{
					PICkitFunctions.DeviceBuffers = deviceData;
				}
				else
				{
					this.displayDataSource.Text = "None (Empty/Erased)";
				}
				this.updateGUI(true);
				return;
			}
			else
			{
				if (!this.checkEraseVoltage(false))
				{
					return;
				}
				this.progressBar1.Value = 0;
				PICkitFunctions.SetMCLRTemp(true);
				PICkitFunctions.VddOn();
				if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].OSSCALSave && !forceOSSCAL)
				{
					PICkitFunctions.ReadOSSCAL();
					if (!this.verifyOSCCAL())
					{
						return;
					}
				}
				uint osccal = PICkitFunctions.DeviceBuffers.OSCCAL;
				if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].BandGapMask > 0U)
				{
					PICkitFunctions.ReadBandGap();
				}
				uint bandGap = PICkitFunctions.DeviceBuffers.BandGap;
				this.displayStatusWindow.Text = "Erasing device...";
				base.Update();
				if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMemEraseScript > 0 && PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].DebugRowEraseSize == 0)
				{
					PICkitFunctions.RunScript(0, 1);
					if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigWrPrepScript > 0)
					{
						PICkitFunctions.DownloadAddress3(0);
						PICkitFunctions.RunScript(14, 1);
					}
					PICkitFunctions.ExecuteScript((int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMemEraseScript);
					PICkitFunctions.RunScript(1, 1);
				}
				PICkitFunctions.RunScript(0, 1);
				if (FormPICkit2.TestMemoryEnabled && FormPICkit2.TestMemoryOpen && calWords.Length > 0)
				{
					byte[] array = new byte[2 * calWords.Length];
					for (int i = 0; i < calWords.Length; i++)
					{
						calWords[i] <<= 1;
						array[2 * i] = (byte)(calWords[i] & 255U);
						array[2 * i + 1] = (byte)(calWords[i] >> 8);
					}
					PICkitFunctions.DataClrAndDownload(array, 0);
					PICkitFunctions.RunScript(21, 1);
				}
				else
				{
					if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ChipErasePrepScript > 0)
					{
						PICkitFunctions.RunScript(4, 1);
					}
					PICkitFunctions.RunScript(22, 1);
				}
				PICkitFunctions.RunScript(1, 1);
				PICkitFunctions.ResetBuffers();
				if (FormPICkit2.TestMemoryEnabled && FormPICkit2.TestMemoryOpen && !this.toolStripMenuItemClearBuffersErase.Checked)
				{
					FormPICkit2.formTestMem.ClearTestMemory();
				}
				if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].OSSCALSave)
				{
					PICkitFunctions.DeviceBuffers.OSCCAL = osccal;
					deviceData.OSCCAL = osccal;
					PICkitFunctions.WriteOSSCAL();
					PICkitFunctions.DeviceBuffers.ProgramMemory[PICkitFunctions.DeviceBuffers.ProgramMemory.Length - 1] = PICkitFunctions.DeviceBuffers.OSCCAL;
					deviceData.ProgramMemory[deviceData.ProgramMemory.Length - 1] = PICkitFunctions.DeviceBuffers.OSCCAL;
				}
				if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].BandGapMask > 0U)
				{
					PICkitFunctions.DeviceBuffers.BandGap = bandGap;
					deviceData.BandGap = bandGap;
					PICkitFunctions.WriteConfigOutsideProgMem(false, false);
				}
				if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].WriteCfgOnErase)
				{
					int num = (int)(PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigAddr / (uint)PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].ProgMemHexBytes);
					int configWords = (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigWords;
					int num2 = PICkitFunctions.DeviceBuffers.ProgramMemory.Length;
					if ((long)num < (long)((ulong)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgramMem) && configWords > 0)
					{
						uint num3;
						if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue == 65535U)
						{
							num3 = 61440U;
						}
						else
						{
							num3 = 16711680U;
						}
						for (int j = configWords; j > 0; j--)
						{
							PICkitFunctions.DeviceBuffers.ProgramMemory[num2 - j] = ((uint)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigBlank[configWords - j] | num3);
						}
						this.writeConfigInsideProgramMem();
					}
					else
					{
						PICkitFunctions.WriteConfigOutsideProgMem(false, false);
					}
				}
				if (!this.toolStripMenuItemClearBuffersErase.Checked)
				{
					PICkitFunctions.DeviceBuffers = deviceData;
				}
				Label label2 = this.displayStatusWindow;
				label2.Text += "Complete";
				base.Update();
				if (this.toolStripMenuItemClearBuffersErase.Checked)
				{
					this.displayDataSource.Text = "None (Empty/Erased)";
				}
				this.checkImportFile = false;
				this.conditionalVDDOff();
				this.updateGUI(true);
				return;
			}
		}

		// Token: 0x060000DA RID: 218 RVA: 0x0002B298 File Offset: 0x0002A298
		private bool checkEraseVoltage(bool checkRowErase)
		{
			if ((float)(this.numUpDnVDD.Value + 0.05m) >= PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].VddErase || !FormPICkit2.ShowWriteEraseVDDDialog)
			{
				return true;
			}
			if (checkRowErase && PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].DebugRowEraseScript > 0)
			{
				return false;
			}
			this.dialogVddErase.UpdateText();
			bool enabled = this.timerAutoImportWrite.Enabled;
			this.timerAutoImportWrite.Enabled = false;
			this.dialogVddErase.ShowDialog();
			this.timerAutoImportWrite.Enabled = enabled;
			return FormPICkit2.ContinueWriteErase;
		}

		// Token: 0x060000DB RID: 219 RVA: 0x0002B34C File Offset: 0x0002A34C
		private bool verifyOSCCAL()
		{
			if (!PICkitFunctions.ValidateOSSCAL() && this.verifyOSCCALValue && MessageBox.Show("Invalid OSCCAL Value detected:\n\nTo abort, click 'Cancel'\nTo continue, click 'OK'", "Warning!", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
			{
				this.conditionalVDDOff();
				this.displayStatusWindow.Text = "Operation Aborted.\n";
				FormPICkit2.statusWindowColor = Constants.StatusColor.red;
				this.updateGUI(true);
				return false;
			}
			return true;
		}

		private void writeDevice(object sender, EventArgs e)
		{
			deviceWrite();
		}

		private bool deviceWrite()
		{
			uint num = 0U;
			if (PICkitFunctions.FamilyIsEEPROM())
			{
				return eepromWrite(false);
			}
			bool flag = false;
			if (!preProgrammingCheck(PICkitFunctions.GetActiveFamily()))
			{
				return false;
			}
			if (!checkEraseVoltage(true) && !PICkitFunctions.FamilyIsPIC32())
			{
				if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].DebugRowEraseScript <= 0)
				{
					return false;
				}
				flag = true;
			}
			updateGUI(false);
            Update();
			if (checkImportFile && !PICkitFunctions.LearnMode)
			{
				FileInfo fileInfo = new FileInfo(openHexFileDialog.FileName);
				if (ImportExportHex.LastWriteTime != fileInfo.LastWriteTime)
				{
					displayStatusWindow.Text = "Reloading Hex File\n";
					Update();
					Thread.Sleep(300);
					if (!ImportHexFileGo())
					{
						displayStatusWindow.Text = "Error Loading Hex File: Write aborted.\n";
						statusWindowColor = Constants.StatusColor.red;
						updateGUI(true);
						return false;
					}
				}
			}
			if (PICkitFunctions.FamilyIsPIC32())
			{
				if (PIC32MXFunctions.P32Write(verifyOnWriteToolStripMenuItem.Checked, enableCodeProtectToolStripMenuItem.Checked))
				{
					statusWindowColor = Constants.StatusColor.green;
					conditionalVDDOff();
					updateGUI(true);
					return true;
				}
				statusWindowColor = Constants.StatusColor.red;
				conditionalVDDOff();
				updateGUI(true);
				return true;
			}
			else
			{
				PICkitFunctions.SetMCLRTemp(true);
				PICkitFunctions.VddOn();
				if (PICkitFunctions.LearnMode && PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].DeviceIDMask > 0U)
				{
					PICkitFunctions.MetaCmd_CHECK_DEVICE_ID();
				}
				if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].OSSCALSave)
				{
					if (PICkitFunctions.LearnMode)
					{
						PICkitFunctions.DeviceBuffers.ProgramMemory[PICkitFunctions.DeviceBuffers.ProgramMemory.Length - 1] = PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue;
						PICkitFunctions.MetaCmd_READ_OSCCAL();
					}
					else
					{
						PICkitFunctions.ReadOSSCAL();
						PICkitFunctions.DeviceBuffers.ProgramMemory[PICkitFunctions.DeviceBuffers.ProgramMemory.Length - 1] = PICkitFunctions.DeviceBuffers.OSCCAL;
						if (!verifyOSCCAL())
						{
							return false;
						}
					}
				}

				if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].BandGapMask > 0U)
				{
					if (PICkitFunctions.LearnMode)
					{
						PICkitFunctions.MetaCmd_READ_BANDGAP();
					}
					else
					{
						PICkitFunctions.ReadBandGap();
					}
				}

                bool flag2 = false;
				if (checkBoxProgMemEnabled.Checked && (checkBoxEEMem.Checked || !checkBoxEEMem.Enabled))
				{
					if (flag)
					{
						displayStatusWindow.Text = "Erasing Part with Low Voltage Row Erase...\n";
						Update();
						PICkitFunctions.RowEraseDevice();
					}
					else
					{
						if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMemEraseScript > 0 && PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].DebugRowEraseSize == 0)
						{
							PICkitFunctions.RunScript(0, 1);
							if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigWrPrepScript > 0)
							{
								PICkitFunctions.DownloadAddress3(0);
								PICkitFunctions.RunScript(14, 1);
							}
							PICkitFunctions.ExecuteScript((int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMemEraseScript);
							PICkitFunctions.RunScript(1, 1);
						}
						PICkitFunctions.RunScript(0, 1);
						if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ChipErasePrepScript > 0)
						{
							PICkitFunctions.RunScript(4, 1);
						}
						PICkitFunctions.RunScript(22, 1);
						PICkitFunctions.RunScript(1, 1);
					}
				}
				else if (checkBoxProgMemEnabled.Checked && PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgMemEraseScript != 0)
				{
					if (flag)
					{
						displayStatusWindow.Text = "Erasing Part with Low Voltage Row Erase...\n";
						Update();
						PICkitFunctions.RowEraseDevice();
					}
					else
					{
						PICkitFunctions.RunScript(0, 1);
						PICkitFunctions.RunScript(23, 1);
						PICkitFunctions.RunScript(1, 1);
					}
				}
				else if (checkBoxEEMem.Checked && PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].EEMemEraseScript != 0)
				{
					PICkitFunctions.RunScript(0, 1);
					PICkitFunctions.RunScript(24, 1);
					PICkitFunctions.RunScript(1, 1);
				}
				else if (!checkBoxEEMem.Checked && checkBoxEEMem.Enabled && PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgMemEraseScript == 0)
				{
					displayStatusWindow.Text = "Reading device:\n";
					Update();
					readEEPROM();
					updateGUI(true);
					if (flag)
					{
						displayStatusWindow.Text = "Erasing Part with Low Voltage Row Erase...\n";
						Update();
						PICkitFunctions.RowEraseDevice();
					}
					else
					{
						if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMemEraseScript > 0 && PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].DebugRowEraseSize == 0)
						{
							PICkitFunctions.RunScript(0, 1);
							if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigWrPrepScript > 0)
							{
								PICkitFunctions.DownloadAddress3(0);
								PICkitFunctions.RunScript(14, 1);
							}
							PICkitFunctions.ExecuteScript((int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMemEraseScript);
							PICkitFunctions.RunScript(1, 1);
						}
						PICkitFunctions.RunScript(0, 1);
						if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ChipErasePrepScript > 0)
						{
							PICkitFunctions.RunScript(4, 1);
						}
						PICkitFunctions.RunScript(22, 1);
						PICkitFunctions.RunScript(1, 1);
						flag2 = true;
					}
				}
				displayStatusWindow.Text = "Writing device:\n";
				Update();
				bool flag3 = false;
				int num2 = (int)(PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigAddr / (uint)PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].ProgMemHexBytes);
				int configWords = (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigWords;
				int num3 = PICkitFunctions.DeviceBuffers.ProgramMemory.Length;
				uint[] array = new uint[configWords];
				if (num2 < (long)((ulong)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgramMem) && configWords > 0)
				{
					flag3 = true;
					for (int i = configWords; i > 0; i--)
					{
						array[i - 1] = PICkitFunctions.DeviceBuffers.ProgramMemory[num3 - i];
						PICkitFunctions.DeviceBuffers.ProgramMemory[num3 - i] = PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue;
					}
				}
				num3--;
				if (checkBoxProgMemEnabled.Checked)
				{
					Label label = displayStatusWindow;
					label.Text += "Program Memory... ";
					Update();
					progressBar1.Value = 0;
					PICkitFunctions.RunScript(0, 1);
					if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgMemWrPrepScript != 0)
					{
						PICkitFunctions.DownloadAddress3(0);
						PICkitFunctions.RunScript(6, 1);
					}
					if (PICkitFunctions.FamilyIsKeeloq())
					{
						PICkitFunctions.HCS360_361_VppSpecial();
					}
					int progMemWrWords = (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgMemWrWords;
					int bytesPerLocation = (int)PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BytesPerLocation;
					int num4 = 256 / (progMemWrWords * bytesPerLocation);
					int num5 = num4 * progMemWrWords;
					int num6 = 0;
					num3 = PICkitFunctions.FindLastUsedInBuffer(PICkitFunctions.DeviceBuffers.ProgramMemory, PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue, num3);
					if ((progMemWrWords == num3 + 1 || num5 > num3 + 1) && !PICkitFunctions.LearnMode)
					{
						num4 = 1;
						num5 = progMemWrWords;
					}
					int num7 = (num3 + 1) / num5;
					if ((num3 + 1) % num5 > 0)
					{
						num7++;
					}
					num3 = num7 * num5;
					progressBar1.Maximum = num3 / num5;
					if (useProgExec33())
					{
						if (!dsPIC33_PE.PE33Write(num3, displayStatusWindow.Text))
						{
							statusWindowColor = Constants.StatusColor.red;
							conditionalVDDOff();
							updateGUI(true);
							return false;
						}
					}
					else if (useProgExec24F())
					{
						if (!PIC24F_PE.PE24FWrite(num3, displayStatusWindow.Text, verifyOnWriteToolStripMenuItem.Checked))
						{
							statusWindowColor = Constants.StatusColor.red;
							conditionalVDDOff();
							updateGUI(true);
							return false;
						}
					}
					else
					{
						byte[] array2 = new byte[256];
						do
						{
							int num8 = 0;
							int num9 = 0;
							while (num9 < num5 && num6 != num3)
							{
								uint num10 = PICkitFunctions.DeviceBuffers.ProgramMemory[num6++];
								if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].ProgMemShift > 0)
								{
									num10 <<= 1;
								}
								array2[num8++] = (byte)(num10 & 255U);
								num += (uint)((byte)(num10 & 255U));
								for (int j = 1; j < bytesPerLocation; j++)
								{
									num10 >>= 8;
									array2[num8++] = (byte)(num10 & 255U);
									num += (uint)((byte)(num10 & 255U));
								}
								num9++;
							}
							if (PICkitFunctions.FamilyIsKeeloq())
							{
								processKeeloqData(ref array2, num6);
							}
							for (int k = PICkitFunctions.DataClrAndDownload(array2, 0); k < num8; k = PICkitFunctions.DataDownload(array2, k, num8))
							{
							}
							PICkitFunctions.RunScript(7, num4);
							if (num6 % 32768 == 0 && PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgMemWrPrepScript != 0)
							{
								PICkitFunctions.DownloadAddress3(65536 * (num6 / 32768));
								PICkitFunctions.RunScript(6, 1);
							}
							progressBar1.PerformStep();
						}
						while (num6 < num3);
						PICkitFunctions.RunScript(1, 1);
					}
				}
				int num11 = num3;
				if (flag3)
				{
					for (int l = configWords; l > 0; l--)
					{
						PICkitFunctions.DeviceBuffers.ProgramMemory[PICkitFunctions.DeviceBuffers.ProgramMemory.Length - l] = array[l - 1];
					}
				}
				if ((checkBoxEEMem.Checked || flag2) && PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].EEMem > 0)
				{
					Label label2 = displayStatusWindow;
					label2.Text += "EE... ";
					base.Update();
					PICkitFunctions.RunScript(0, 1);
					if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].EEWrPrepScript > 1)
					{
						if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].EEMemHexBytes == 4)
						{
							PICkitFunctions.DownloadAddress3((int)(PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].EEAddr / 2U));
						}
						else
						{
							PICkitFunctions.DownloadAddress3(0);
						}
						PICkitFunctions.RunScript(10, 1);
					}
					int eememBytesPerWord = (int)PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].EEMemBytesPerWord;
					uint eeblank = this.getEEBlank();
					int num12 = (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].EEWrLocations;
					if (num12 < 16)
					{
						num12 = 16;
					}
					if (this.checkBoxProgMemEnabled.Checked && !flag && !PICkitFunctions.LearnMode)
					{
						num3 = PICkitFunctions.FindLastUsedInBuffer(PICkitFunctions.DeviceBuffers.EEPromMemory, eeblank, PICkitFunctions.DeviceBuffers.EEPromMemory.Length - 1);
					}
					else
					{
						num3 = PICkitFunctions.DeviceBuffers.EEPromMemory.Length - 1;
					}
					int num13 = (num3 + 1) / num12;
					if ((num3 + 1) % num12 > 0)
					{
						num13++;
					}
					num3 = num13 * num12;
					byte[] array3 = new byte[num12 * eememBytesPerWord];
					int repetitions = num12 / (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].EEWrLocations;
					int num14 = 0;
					this.progressBar1.Value = 0;
					this.progressBar1.Maximum = num3 / num12;
					do
					{
						int num15 = 0;
						for (int m = 0; m < num12; m++)
						{
							uint num16 = PICkitFunctions.DeviceBuffers.EEPromMemory[num14++];
							if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].ProgMemShift > 0)
							{
								num16 <<= 1;
							}
							array3[num15++] = (byte)(num16 & 255U);
							num += (uint)((byte)(num16 & 255U));
							for (int n = 1; n < eememBytesPerWord; n++)
							{
								num16 >>= 8;
								array3[num15++] = (byte)(num16 & 255U);
								num += (uint)((byte)(num16 & 255U));
							}
						}
						PICkitFunctions.DataClrAndDownload(array3, 0);
						PICkitFunctions.RunScript(11, repetitions);
						this.progressBar1.PerformStep();
					}
					while (num14 < num3);
					PICkitFunctions.RunScript(1, 1);
				}
				if (this.checkBoxProgMemEnabled.Checked && PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].UserIDWords > 0)
				{
					Label label3 = this.displayStatusWindow;
					label3.Text += "UserIDs... ";
					base.Update();
					PICkitFunctions.RunScript(0, 1);
					if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].UserIDWrPrepScript > 0)
					{
						PICkitFunctions.RunScript(18, 1);
					}
					int userIDBytes = (int)PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].UserIDBytes;
					byte[] array4 = new byte[(int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].UserIDWords * userIDBytes];
					int num17 = 0;
					int num18 = 0;
					for (int num19 = 0; num19 < (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].UserIDWords; num19++)
					{
						uint num20 = PICkitFunctions.DeviceBuffers.UserIDs[num18++];
						if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].ProgMemShift > 0)
						{
							num20 <<= 1;
						}
						array4[num17++] = (byte)(num20 & 255U);
						num += (uint)((byte)(num20 & 255U));
						for (int num21 = 1; num21 < userIDBytes; num21++)
						{
							num20 >>= 8;
							array4[num17++] = (byte)(num20 & 255U);
							num += (uint)((byte)(num20 & 255U));
						}
					}
					for (int num22 = PICkitFunctions.DataClrAndDownload(array4, 0); num22 < num17; num22 = PICkitFunctions.DataDownload(array4, num22, num17))
					{
					}
					PICkitFunctions.RunScript(19, 1);
					PICkitFunctions.RunScript(1, 1);
				}
				bool flag4 = true;
				if (flag3)
				{
					if (PICkitFunctions.LearnMode)
					{
						if ((long)num11 == (long)((ulong)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgramMem))
						{
							if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue > 65535U)
							{
								num -= 128U;
							}
							else
							{
								num -= 8U;
							}
						}
					}
					else
					{
						num11 -= configWords;
					}
				}
				if (this.verifyOnWriteToolStripMenuItem.Checked)
				{
					if (PICkitFunctions.LearnMode)
					{
						PICkitFunctions.MetaCmd_START_CHECKSUM();
					}
					flag4 = this.deviceVerify(true, num11 - 1, flag2);
					if (PICkitFunctions.LearnMode)
					{
						PICkitFunctions.MetaCmd_VERIFY_CHECKSUM(num);
						num = 0U;
					}
				}
				if (PICkitFunctions.LearnMode && PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].OSSCALSave)
				{
					PICkitFunctions.MetaCmd_WRITE_OSCCAL();
					PICkitFunctions.DeviceBuffers.ProgramMemory[PICkitFunctions.DeviceBuffers.ProgramMemory.Length - 1] = PICkitFunctions.DeviceBuffers.OSCCAL;
				}
				if (flag4)
				{
					if (configWords > 0 && !flag3 && this.checkBoxProgMemEnabled.Checked)
					{
						if (!this.verifyOnWriteToolStripMenuItem.Checked)
						{
							Label label4 = this.displayStatusWindow;
							label4.Text += "Config... ";
							base.Update();
						}
						if ((PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].FamilyName == "PIC18F" || PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].FamilyName == "PIC18F_K_") && PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigWords > 5 && ((ulong)PICkitFunctions.DeviceBuffers.ConfigWords[5] & 18446744073709543423UL) == (ulong)PICkitFunctions.DeviceBuffers.ConfigWords[5])
						{
							uint num23 = PICkitFunctions.DeviceBuffers.ConfigWords[5];
							PICkitFunctions.DeviceBuffers.ConfigWords[5] = 65535U;
							PICkitFunctions.WriteConfigOutsideProgMem(false, false);
							PICkitFunctions.DeviceBuffers.ConfigWords[5] = num23;
						}
						num += PICkitFunctions.WriteConfigOutsideProgMem(this.enableCodeProtectToolStripMenuItem.Enabled && this.enableCodeProtectToolStripMenuItem.Checked, this.enableDataProtectStripMenuItem.Enabled && this.enableDataProtectStripMenuItem.Checked);
						if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue == 65535U)
						{
							num += (uint)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[7];
						}
						if (this.verifyOnWriteToolStripMenuItem.Checked && (!PICkitFunctions.LearnMode || PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].BandGapMask == 0U))
						{
							bool flag5 = this.verifyConfig(configWords, num2);
							if (flag5)
							{
								FormPICkit2.statusWindowColor = Constants.StatusColor.green;
								this.displayStatusWindow.Text = "Programming Successful.\n";
							}
							else if (!PICkitFunctions.LearnMode)
							{
								FormPICkit2.statusWindowColor = Constants.StatusColor.red;
								flag4 = false;
							}
							if (PICkitFunctions.LearnMode && PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].BandGapMask == 0U)
							{
								PICkitFunctions.MetaCmd_VERIFY_CHECKSUM(num);
							}
						}
					}
					else if (configWords > 0 && this.checkBoxProgMemEnabled.Checked)
					{
						if (!this.verifyOnWriteToolStripMenuItem.Checked)
						{
							Label label5 = this.displayStatusWindow;
							label5.Text += "Config... ";
							base.Update();
						}
						for (int num24 = 0; num24 < configWords; num24++)
						{
							if (num24 == (int)(PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].CPConfig - 1))
							{
								if (this.enableCodeProtectToolStripMenuItem.Enabled && this.enableCodeProtectToolStripMenuItem.Checked)
								{
									PICkitFunctions.DeviceBuffers.ProgramMemory[num2 + num24] &= (uint)(~(uint)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].CPMask);
								}
								if (this.enableDataProtectStripMenuItem.Enabled && this.enableDataProtectStripMenuItem.Checked)
								{
									PICkitFunctions.DeviceBuffers.ProgramMemory[num2 + num24] &= (uint)(~(uint)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].DPMask);
								}
							}
						}
						this.writeConfigInsideProgramMem();
						if (this.verifyOnWriteToolStripMenuItem.Checked)
						{
							FormPICkit2.statusWindowColor = Constants.StatusColor.green;
							this.displayStatusWindow.Text = "Programming Successful.\n";
						}
						else
						{
							flag4 = false;
						}
					}
					else if (!this.checkBoxProgMemEnabled.Checked)
					{
						FormPICkit2.statusWindowColor = Constants.StatusColor.green;
						this.displayStatusWindow.Text = "Programming Successful.\n";
					}
					else
					{
						FormPICkit2.statusWindowColor = Constants.StatusColor.green;
						this.displayStatusWindow.Text = "Programming Successful.\n";
					}
					this.conditionalVDDOff();
					if (!this.verifyOnWriteToolStripMenuItem.Checked)
					{
						Label label6 = this.displayStatusWindow;
						label6.Text += "Done.";
					}
					if (PICkitFunctions.LearnMode)
					{
						this.displayStatusWindow.Text = "Programmer-To-Go download complete.";
					}
					this.updateGUI(true);
					return flag4;
				}
				return false;
			}
		}

		// Token: 0x060000DE RID: 222 RVA: 0x0002C68C File Offset: 0x0002B68C
		private void writeConfigInsideProgramMem()
		{
			PICkitFunctions.RunScript(0, 1);
			int num = PICkitFunctions.DeviceBuffers.ProgramMemory.Length - (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgMemWrWords;
			if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgMemWrPrepScript != 0)
			{
				PICkitFunctions.DownloadAddress3(num * (int)PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].AddressIncrement);
				PICkitFunctions.RunScript(6, 1);
			}
			byte[] array = new byte[256];
			int num2 = 0;
			for (int i = 0; i < (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgMemWrWords; i++)
			{
				uint num3 = PICkitFunctions.DeviceBuffers.ProgramMemory[num++];
				if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].ProgMemShift > 0)
				{
					num3 <<= 1;
				}
				array[num2++] = (byte)(num3 & 255U);
				for (int j = 1; j < (int)PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BytesPerLocation; j++)
				{
					num3 >>= 8;
					array[num2++] = (byte)(num3 & 255U);
				}
			}
			for (int k = PICkitFunctions.DataClrAndDownload(array, 0); k < num2; k = PICkitFunctions.DataDownload(array, k, num2))
			{
			}
			PICkitFunctions.RunScript(7, 1);
			PICkitFunctions.RunScript(1, 1);
		}

		// Token: 0x060000DF RID: 223 RVA: 0x0002C7F0 File Offset: 0x0002B7F0
		private void processKeeloqData(ref byte[] downloadBuffer, int wordsWritten)
		{
			if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].DeviceID == 4294967094U)
			{
				for (int i = wordsWritten / 2; i > 0; i--)
				{
					downloadBuffer[i * 4 - 1] = (byte)~downloadBuffer[i * 2 - 1];
					downloadBuffer[i * 4 - 2] = (byte)~downloadBuffer[i * 2 - 2];
					downloadBuffer[i * 4 - 3] = downloadBuffer[i * 2 - 1];
					downloadBuffer[i * 4 - 4] = downloadBuffer[i * 2 - 2];
				}
				byte[] array = downloadBuffer;
				int num = 0;
				array[num] = (byte)(array[num] >> 1);
			}
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x0002C885 File Offset: 0x0002B885
		private void blankCheck(object sender, EventArgs e)
		{
			this.blankCheckDevice();
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x0002C890 File Offset: 0x0002B890
		private bool blankCheckDevice()
		{
			if (PICkitFunctions.FamilyIsKeeloq() || PICkitFunctions.FamilyIsMCP())
			{
				this.displayStatusWindow.Text = "Blank Check not supported for this device type.";
				FormPICkit2.statusWindowColor = Constants.StatusColor.yellow;
				this.updateGUI(false);
				return false;
			}
			if (!this.preProgrammingCheck(PICkitFunctions.GetActiveFamily()))
			{
				return false;
			}
			if (!PICkitFunctions.FamilyIsPIC32())
			{
				DeviceData deviceData = new DeviceData(PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgramMem, PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].EEMem, PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigWords, PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].UserIDWords, PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue, (int)PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].EEMemAddressIncrement, (int)PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].UserIDBytes, PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigBlank, (uint)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[7]);
				int num = (int)(PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigAddr / (uint)PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].ProgMemHexBytes);
				int configWords = (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigWords;
				if ((long)num < (long)((ulong)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgramMem))
				{
					for (int i = 0; i < configWords; i++)
					{
						uint num2 = deviceData.ProgramMemory[num + i] & 4294901760U;
						if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue == 65535U)
						{
							num2 |= 61440U;
						}
						deviceData.ProgramMemory[num + i] = (num2 | (uint)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigBlank[i]);
					}
				}
				this.displayStatusWindow.Text = "Checking if Device is blank:\n";
				base.Update();
				PICkitFunctions.SetMCLRTemp(true);
				PICkitFunctions.VddOn();
				byte[] array = new byte[128];
				Label label = this.displayStatusWindow;
				label.Text += "Program Memory... ";
				base.Update();
				if (this.useProgExec33())
				{
					if (!dsPIC33_PE.PE33BlankCheck(this.displayStatusWindow.Text))
					{
						this.conditionalVDDOff();
						this.displayStatusWindow.Text = "Program Memory is not blank.";
						FormPICkit2.statusWindowColor = Constants.StatusColor.red;
						this.updateGUI(true);
						return false;
					}
				}
				else if (this.useProgExec24F())
				{
					if (!PIC24F_PE.PE24FBlankCheck(this.displayStatusWindow.Text))
					{
						this.conditionalVDDOff();
						FormPICkit2.statusWindowColor = Constants.StatusColor.red;
						this.updateGUI(true);
						return false;
					}
				}
				else
				{
					PICkitFunctions.RunScript(0, 1);
					if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgMemAddrSetScript != 0 && PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgMemAddrBytes != 0)
					{
						if (PICkitFunctions.FamilyIsEEPROM())
						{
							PICkitFunctions.DownloadAddress3MSBFirst(this.eeprom24BitAddress(0, false));
							PICkitFunctions.RunScript(5, 1);
							if (this.eeprom_CheckBusErrors())
							{
								return false;
							}
						}
						else
						{
							PICkitFunctions.DownloadAddress3(0);
							PICkitFunctions.RunScript(5, 1);
						}
					}
					int bytesPerLocation = (int)PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BytesPerLocation;
					int num3 = 128 / ((int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgMemRdWords * bytesPerLocation);
					int num4 = num3 * (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgMemRdWords;
					int num5 = 0;
					this.progressBar1.Value = 0;
					this.progressBar1.Maximum = (int)(PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgramMem / (uint)num4);
					for (;;)
					{
						if (PICkitFunctions.FamilyIsEEPROM())
						{
							if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[0] == 1 && num5 > (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[1] && num5 % (int)(PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[1] + 1) == 0)
							{
								PICkitFunctions.DownloadAddress3MSBFirst(this.eeprom24BitAddress(num5, false));
								PICkitFunctions.RunScript(5, 1);
							}
							PICkitFunctions.Download3Multiples(this.eeprom24BitAddress(num5, true), num3, (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgMemRdWords);
						}
						PICkitFunctions.RunScriptUploadNoLen(3, num3);
						Array.Copy(PICkitFunctions.Usb_read_array, 1L, array, 0L, 64L);
						PICkitFunctions.UploadDataNoLen();
						Array.Copy(PICkitFunctions.Usb_read_array, 1L, array, 64L, 64L);
						int num6 = 0;
						for (int j = 0; j < num4; j++)
						{
							int num7 = 0;
							uint num8 = (uint)array[num6 + num7++];
							if (num7 < bytesPerLocation)
							{
								num8 |= (uint)((uint)array[num6 + num7++] << 8);
							}
							if (num7 < bytesPerLocation)
							{
								num8 |= (uint)((uint)array[num6 + num7++] << 16);
							}
							if (num7 < bytesPerLocation)
							{
								num8 |= (uint)((uint)array[num6 + num7++] << 24);
							}
							num6 += num7;
							if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].ProgMemShift > 0)
							{
								num8 = (num8 >> 1 & PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue);
							}
							if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].OSSCALSave && (long)num5 == (long)((ulong)(PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgramMem - 1U)))
							{
								num8 = PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue;
							}
							if (num8 != deviceData.ProgramMemory[num5++])
							{
								goto Block_25;
							}
							if ((long)num5 == (long)((ulong)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgramMem))
							{
								break;
							}
							if (num5 % 32768 == 0 && PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgMemAddrSetScript != 0 && PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgMemAddrBytes != 0 && PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue > 65535U)
							{
								PICkitFunctions.DownloadAddress3(65536 * (num5 / 32768));
								PICkitFunctions.RunScript(5, 1);
								break;
							}
						}
						this.progressBar1.PerformStep();
						if ((long)num5 >= (long)((ulong)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgramMem))
						{
							goto Block_32;
						}
					}
					Block_25:
					PICkitFunctions.RunScript(1, 1);
					this.conditionalVDDOff();
					if (PICkitFunctions.FamilyIsEEPROM())
					{
						this.displayStatusWindow.Text = "EEPROM is not blank starting at address\n";
					}
					else
					{
						this.displayStatusWindow.Text = "Program Memory is not blank starting at address\n";
					}
					Label label2 = this.displayStatusWindow;
					label2.Text += string.Format("0x{0:X6}", (num5 - 1) * (int)PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].AddressIncrement);
					FormPICkit2.statusWindowColor = Constants.StatusColor.red;
					this.updateGUI(true);
					return false;
					Block_32:
					PICkitFunctions.RunScript(1, 1);
				}
				if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].EEMem > 0)
				{
					Label label3 = this.displayStatusWindow;
					label3.Text += "EE... ";
					base.Update();
					PICkitFunctions.RunScript(0, 1);
					if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].EERdPrepScript > 0)
					{
						if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].EEMemHexBytes == 4)
						{
							PICkitFunctions.DownloadAddress3((int)(PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].EEAddr / 2U));
						}
						else
						{
							PICkitFunctions.DownloadAddress3(0);
						}
						PICkitFunctions.RunScript(8, 1);
					}
					int eememBytesPerWord = (int)PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].EEMemBytesPerWord;
					uint eeblank = this.getEEBlank();
					int num9 = 128 / ((int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].EERdLocations * eememBytesPerWord);
					int num10 = num9 * (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].EERdLocations;
					int num11 = 0;
					this.progressBar1.Value = 0;
					this.progressBar1.Maximum = (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].EEMem / num10;
					for (;;)
					{
						PICkitFunctions.RunScriptUploadNoLen(9, num9);
						Array.Copy(PICkitFunctions.Usb_read_array, 1L, array, 0L, 64L);
						PICkitFunctions.UploadDataNoLen();
						Array.Copy(PICkitFunctions.Usb_read_array, 1L, array, 64L, 64L);
						int num12 = 0;
						for (int k = 0; k < num10; k++)
						{
							int num13 = 0;
							uint num14 = (uint)array[num12 + num13++];
							if (num13 < eememBytesPerWord)
							{
								num14 |= (uint)((uint)array[num12 + num13++] << 8);
							}
							num12 += num13;
							if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].ProgMemShift > 0)
							{
								num14 = (num14 >> 1 & eeblank);
							}
							num11++;
							if (num14 != eeblank)
							{
								goto Block_38;
							}
							if (num11 >= (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].EEMem)
							{
								break;
							}
						}
						this.progressBar1.PerformStep();
						if (num11 >= (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].EEMem)
						{
							goto Block_41;
						}
					}
					Block_38:
					PICkitFunctions.RunScript(1, 1);
					this.conditionalVDDOff();
					this.displayStatusWindow.Text = "EE Data Memory is not blank starting at address\n";
					if (eeblank == 65535U)
					{
						Label label4 = this.displayStatusWindow;
						label4.Text += string.Format("0x{0:X4}", (num11 - 1) * 2);
					}
					else
					{
						Label label5 = this.displayStatusWindow;
						label5.Text += string.Format("0x{0:X4}", num11 - 1);
					}
					FormPICkit2.statusWindowColor = Constants.StatusColor.red;
					this.updateGUI(true);
					return false;
					Block_41:
					PICkitFunctions.RunScript(1, 1);
				}
				if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].UserIDWords > 0 && !PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].BlankCheckSkipUsrIDs)
				{
					Label label6 = this.displayStatusWindow;
					label6.Text += "UserIDs... ";
					base.Update();
					PICkitFunctions.RunScript(0, 1);
					if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].UserIDRdPrepScript > 0)
					{
						PICkitFunctions.RunScript(16, 1);
					}
					int userIDBytes = (int)PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].UserIDBytes;
					int num15 = 0;
					int num16 = 0;
					PICkitFunctions.RunScriptUploadNoLen(17, 1);
					Array.Copy(PICkitFunctions.Usb_read_array, 1L, array, 0L, 64L);
					if ((long)((int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].UserIDWords * userIDBytes) > 64L)
					{
						PICkitFunctions.UploadDataNoLen();
						Array.Copy(PICkitFunctions.Usb_read_array, 1L, array, 64L, 64L);
					}
					PICkitFunctions.RunScript(1, 1);
					for (;;)
					{
						int num17 = 0;
						uint num18 = (uint)array[num16 + num17++];
						if (num17 < userIDBytes)
						{
							num18 |= (uint)((uint)array[num16 + num17++] << 8);
						}
						if (num17 < userIDBytes)
						{
							num18 |= (uint)((uint)array[num16 + num17++] << 16);
						}
						if (num17 < userIDBytes)
						{
							num18 |= (uint)((uint)array[num16 + num17++] << 24);
						}
						num16 += num17;
						if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].ProgMemShift > 0)
						{
							num18 = (num18 >> 1 & PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue);
						}
						num15++;
						uint num19 = PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue;
						if (userIDBytes == 1)
						{
							num19 &= 255U;
						}
						if (num18 != num19)
						{
							break;
						}
						if (num15 >= (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].UserIDWords)
						{
							goto IL_C83;
						}
					}
					this.conditionalVDDOff();
					this.displayStatusWindow.Text = "User IDs are not blank.";
					FormPICkit2.statusWindowColor = Constants.StatusColor.red;
					this.updateGUI(true);
					return false;
				}
				IL_C83:
				if (configWords > 0 && (long)num > (long)((ulong)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgramMem))
				{
					Label label7 = this.displayStatusWindow;
					label7.Text += "Config... ";
					base.Update();
					PICkitFunctions.RunScript(0, 1);
					PICkitFunctions.RunScript(13, 1);
					PICkitFunctions.UploadData();
					PICkitFunctions.RunScript(1, 1);
					int num20 = 2;
					for (int l = 0; l < configWords; l++)
					{
						uint num21 = (uint)PICkitFunctions.Usb_read_array[num20++];
						num21 |= (uint)((uint)PICkitFunctions.Usb_read_array[num20++] << 8);
						if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].ProgMemShift > 0)
						{
							num21 = (num21 >> 1 & PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue);
						}
						num21 &= (uint)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[l];
						int num22 = (int)(PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[l] & PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigBlank[l]);
						if ((long)num22 != (long)((ulong)num21))
						{
							this.conditionalVDDOff();
							this.displayStatusWindow.Text = "Configuration is not blank.";
							FormPICkit2.statusWindowColor = Constants.StatusColor.red;
							this.updateGUI(true);
							return false;
						}
					}
				}
				PICkitFunctions.RunScript(1, 1);
				this.conditionalVDDOff();
				FormPICkit2.statusWindowColor = Constants.StatusColor.green;
				this.displayStatusWindow.Text = "Device is Blank.";
				this.updateGUI(true);
				return true;
			}
			if (PIC32MXFunctions.PIC32BlankCheck())
			{
				FormPICkit2.statusWindowColor = Constants.StatusColor.green;
				this.conditionalVDDOff();
				this.updateGUI(true);
				return true;
			}
			FormPICkit2.statusWindowColor = Constants.StatusColor.red;
			this.conditionalVDDOff();
			this.updateGUI(true);
			return true;
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x0002D6B8 File Offset: 0x0002C6B8
		private void progMemEdit(object sender, DataGridViewCellEventArgs e)
		{
			int rowIndex = e.RowIndex;
			int columnIndex = e.ColumnIndex;
			string p_value = "0x" + this.dataGridProgramMemory[columnIndex, rowIndex].FormattedValue.ToString();
			int num = 0;
			try
			{
				num = Utilities.Convert_Value_To_Int(p_value);
			}
			catch
			{
				num = 0;
			}
			int num2 = this.dataGridProgramMemory.ColumnCount - 1;
			if (this.comboBoxProgMemView.SelectedIndex >= 1)
			{
				num2 /= 2;
			}
			int num3 = rowIndex * num2 + columnIndex - 1;
			if (PICkitFunctions.FamilyIsPIC32())
			{
				int num4 = (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgramMem;
				int bootFlash = (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].BootFlash;
				num4 -= bootFlash;
				num3 -= num2;
				if (num3 > num4)
				{
					num3 -= num2;
				}
			}
			PICkitFunctions.DeviceBuffers.ProgramMemory[num3] = (uint)((long)num & (long)((ulong)PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue));
			this.displayDataSource.Text = "Edited.";
			this.checkImportFile = false;
			this.progMemJustEdited = true;
			this.updateGUI(true);
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x0002D7EC File Offset: 0x0002C7EC
		private void eEpromEdit(object sender, DataGridViewCellEventArgs e)
		{
			int rowIndex = e.RowIndex;
			int columnIndex = e.ColumnIndex;
			string p_value = "0x" + this.dataGridViewEEPROM[columnIndex, rowIndex].FormattedValue.ToString();
			int num = 0;
			try
			{
				num = Utilities.Convert_Value_To_Int(p_value);
			}
			catch
			{
				num = 0;
			}
			int num2 = this.dataGridViewEEPROM.ColumnCount - 1;
			if (this.comboBoxEE.SelectedIndex >= 1)
			{
				num2 /= 2;
			}
			PICkitFunctions.DeviceBuffers.EEPromMemory[rowIndex * num2 + columnIndex - 1] = (uint)((long)num & (long)((ulong)this.getEEBlank()));
			this.displayDataSource.Text = "Edited.";
			this.checkImportFile = false;
			this.eeMemJustEdited = true;
			this.updateGUI(true);
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x0002D8B0 File Offset: 0x0002C8B0
		private void checkCommunication(object sender, EventArgs e)
		{
			if (!this.detectPICkit2(true, true))
			{
				return;
			}
			this.partialEnableGUIControls();
			this.lookForPoweredTarget(false);
			if (!PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].PartDetect)
			{
				this.setGUIVoltageLimits(true);
				PICkitFunctions.SetVDDVoltage((float)this.numUpDnVDD.Value, 0.85f);
				this.displayStatusWindow.Text = this.displayStatusWindow.Text + "\n[Parts in this family are not auto-detect.]";
				this.fullEnableGUIControls();
			}
			else if (PICkitFunctions.DetectDevice(16777215, true, this.chkBoxVddOn.Checked))
			{
				this.setGUIVoltageLimits(true);
				PICkitFunctions.SetVDDVoltage((float)this.numUpDnVDD.Value, 0.85f);
				this.displayStatusWindow.Text = this.displayStatusWindow.Text + "\nPIC Device Found.";
				this.fullEnableGUIControls();
			}
			this.displayDataSource.Text = "None (Empty/Erased)";
			this.checkForPowerErrors();
			this.updateGUI(true);
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x0002D9BB File Offset: 0x0002C9BB
		private void verifyDevice(object sender, EventArgs e)
		{
			if (PICkitFunctions.FamilyIsKeeloq())
			{
				this.displayStatusWindow.Text = "Verify not supported for this device type.";
				FormPICkit2.statusWindowColor = Constants.StatusColor.yellow;
				this.updateGUI(false);
				return;
			}
			this.deviceVerify(false, 0, false);
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x0002D9EC File Offset: 0x0002C9EC
		private bool deviceVerify(bool writeVerify, int lastLocation, bool forceEEVerify)
		{
			if (!writeVerify && !this.preProgrammingCheck(PICkitFunctions.GetActiveFamily()))
			{
				return false;
			}
			if (PICkitFunctions.FamilyIsPIC32())
			{
				if (PIC32MXFunctions.P32Verify(writeVerify, this.enableCodeProtectToolStripMenuItem.Checked))
				{
					FormPICkit2.statusWindowColor = Constants.StatusColor.green;
					this.conditionalVDDOff();
					this.updateGUI(true);
					return true;
				}
				FormPICkit2.statusWindowColor = Constants.StatusColor.red;
				this.conditionalVDDOff();
				this.updateGUI(true);
				return true;
			}
			else
			{
				this.displayStatusWindow.Text = "Verifying Device:\n";
				base.Update();
				if (!PICkitFunctions.FamilyIsKeeloq() && (!writeVerify || !PICkitFunctions.FamilyIsPIC24FJ()))
				{
					PICkitFunctions.SetMCLRTemp(true);
				}
				PICkitFunctions.VddOn();
				byte[] array = new byte[128];
				int configLocation = (int)(PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigAddr / (uint)PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].ProgMemHexBytes);
				int configWords = (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigWords;
				int num = PICkitFunctions.DeviceBuffers.ProgramMemory.Length - 1;
				if (writeVerify)
				{
					num = lastLocation;
				}
				if (this.checkBoxProgMemEnabled.Checked)
				{
					Label label = this.displayStatusWindow;
					label.Text += "Program Memory... ";
					base.Update();
					if (this.useProgExec33())
					{
						if (!dsPIC33_PE.PE33VerifyCRC(this.displayStatusWindow.Text))
						{
							if (!writeVerify)
							{
								this.displayStatusWindow.Text = "Verification of Program Memory failed.\n";
							}
							else
							{
								this.displayStatusWindow.Text = "Programming of Program Memory failed.\n";
							}
							this.conditionalVDDOff();
							FormPICkit2.statusWindowColor = Constants.StatusColor.red;
							this.updateGUI(true);
							return false;
						}
					}
					else if (this.useProgExec24F())
					{
						if (!PIC24F_PE.PE24FVerify(this.displayStatusWindow.Text, writeVerify, lastLocation))
						{
							this.conditionalVDDOff();
							FormPICkit2.statusWindowColor = Constants.StatusColor.red;
							this.updateGUI(true);
							return false;
						}
					}
					else
					{
						PICkitFunctions.RunScript(0, 1);
						if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgMemAddrSetScript != 0 && PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgMemAddrBytes != 0)
						{
							if (PICkitFunctions.FamilyIsEEPROM())
							{
								PICkitFunctions.DownloadAddress3MSBFirst(this.eeprom24BitAddress(0, false));
								PICkitFunctions.RunScript(5, 1);
								if (!writeVerify && this.eeprom_CheckBusErrors())
								{
									return false;
								}
							}
							else
							{
								PICkitFunctions.DownloadAddress3(0);
								PICkitFunctions.RunScript(5, 1);
							}
						}
						int bytesPerLocation = (int)PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BytesPerLocation;
						int num2 = 128 / ((int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgMemRdWords * bytesPerLocation);
						int num3 = num2 * (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgMemRdWords;
						int num4 = 0;
						if ((int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgMemRdWords == num + 1)
						{
							num2 = 1;
							num3 = num + 1;
						}
						this.progressBar1.Value = 0;
						this.progressBar1.Maximum = num / num3;
						for (;;)
						{
							if (PICkitFunctions.FamilyIsEEPROM())
							{
								if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[0] == 1 && num4 > (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[1] && num4 % (int)(PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[1] + 1) == 0)
								{
									PICkitFunctions.DownloadAddress3MSBFirst(this.eeprom24BitAddress(num4, false));
									PICkitFunctions.RunScript(5, 1);
								}
								PICkitFunctions.Download3Multiples(this.eeprom24BitAddress(num4, true), num2, (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgMemRdWords);
							}
							PICkitFunctions.RunScriptUploadNoLen(3, num2);
							Array.Copy(PICkitFunctions.Usb_read_array, 1L, array, 0L, 64L);
							PICkitFunctions.UploadDataNoLen();
							Array.Copy(PICkitFunctions.Usb_read_array, 1L, array, 64L, 64L);
							int num5 = 0;
							for (int i = 0; i < num3; i++)
							{
								int num6 = 0;
								uint num7 = (uint)array[num5 + num6++];
								if (num6 < bytesPerLocation)
								{
									num7 |= (uint)((uint)array[num5 + num6++] << 8);
								}
								if (num6 < bytesPerLocation)
								{
									num7 |= (uint)((uint)array[num5 + num6++] << 16);
								}
								if (num6 < bytesPerLocation)
								{
									num7 |= (uint)((uint)array[num5 + num6++] << 24);
								}
								num5 += num6;
								if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].ProgMemShift > 0)
								{
									num7 = (num7 >> 1 & PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue);
								}
								if (bytesPerLocation == 2 && PICkitFunctions.FamilyIsEEPROM() && PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[0] == 3)
								{
									uint num8 = num7 << 8 & 65280U;
									num7 >>= 8;
									num7 |= num8;
								}
								if (num7 != PICkitFunctions.DeviceBuffers.ProgramMemory[num4++] && !PICkitFunctions.LearnMode)
								{
									goto Block_32;
								}
								if (num4 % 32768 == 0 && PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgMemAddrSetScript != 0 && PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgMemAddrBytes != 0 && PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue > 65535U)
								{
									PICkitFunctions.DownloadAddress3(65536 * (num4 / 32768));
									PICkitFunctions.RunScript(5, 1);
									break;
								}
								if (num4 > num)
								{
									break;
								}
							}
							this.progressBar1.PerformStep();
							if (num4 >= num)
							{
								goto Block_41;
							}
						}
						Block_32:
						PICkitFunctions.RunScript(1, 1);
						this.conditionalVDDOff();
						if (!writeVerify)
						{
							if (PICkitFunctions.FamilyIsEEPROM())
							{
								this.displayStatusWindow.Text = "Verification of EEPROM failed at address\n";
							}
							else
							{
								this.displayStatusWindow.Text = "Verification of Program Memory failed at address\n";
							}
						}
						else if (PICkitFunctions.FamilyIsEEPROM())
						{
							this.displayStatusWindow.Text = "Programming failed at EEPROM address\n";
						}
						else
						{
							this.displayStatusWindow.Text = "Programming failed at Program Memory address\n";
						}
						Label label2 = this.displayStatusWindow;
						label2.Text += string.Format("0x{0:X6}", (num4 - 1) * (int)PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].AddressIncrement);
						FormPICkit2.statusWindowColor = Constants.StatusColor.red;
						this.updateGUI(true);
						return false;
						Block_41:
						PICkitFunctions.RunScript(1, 1);
					}
				}
				if ((this.checkBoxEEMem.Checked || forceEEVerify) && PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].EEMem > 0)
				{
					if (PICkitFunctions.LearnMode && PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].ProgMemShift > 0)
					{
						PICkitFunctions.MetaCmd_CHANGE_CHKSM_FRMT(2);
					}
					Label label3 = this.displayStatusWindow;
					label3.Text += "EE... ";
					base.Update();
					PICkitFunctions.RunScript(0, 1);
					if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].EERdPrepScript > 0)
					{
						if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].EEMemHexBytes == 4)
						{
							PICkitFunctions.DownloadAddress3((int)(PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].EEAddr / 2U));
						}
						else
						{
							PICkitFunctions.DownloadAddress3(0);
						}
						PICkitFunctions.RunScript(8, 1);
					}
					int eememBytesPerWord = (int)PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].EEMemBytesPerWord;
					int num9 = 128 / ((int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].EERdLocations * eememBytesPerWord);
					int num10 = num9 * (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].EERdLocations;
					int num11 = 0;
					uint eeblank = this.getEEBlank();
					this.progressBar1.Value = 0;
					this.progressBar1.Maximum = (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].EEMem / num10;
					for (;;)
					{
						PICkitFunctions.RunScriptUploadNoLen(9, num9);
						Array.Copy(PICkitFunctions.Usb_read_array, 1L, array, 0L, 64L);
						PICkitFunctions.UploadDataNoLen();
						Array.Copy(PICkitFunctions.Usb_read_array, 1L, array, 64L, 64L);
						int num12 = 0;
						for (int j = 0; j < num10; j++)
						{
							int num13 = 0;
							uint num14 = (uint)array[num12 + num13++];
							if (num13 < eememBytesPerWord)
							{
								num14 |= (uint)((uint)array[num12 + num13++] << 8);
							}
							num12 += num13;
							if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].ProgMemShift > 0)
							{
								num14 = (num14 >> 1 & eeblank);
							}
							if (num14 != PICkitFunctions.DeviceBuffers.EEPromMemory[num11++] && !PICkitFunctions.LearnMode)
							{
								goto Block_51;
							}
							if (num11 >= (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].EEMem)
							{
								break;
							}
						}
						this.progressBar1.PerformStep();
						if (num11 >= (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].EEMem)
						{
							goto Block_55;
						}
					}
					Block_51:
					PICkitFunctions.RunScript(1, 1);
					this.conditionalVDDOff();
					if (!writeVerify)
					{
						this.displayStatusWindow.Text = "Verification of EE Data Memory failed at address\n";
					}
					else
					{
						this.displayStatusWindow.Text = "Programming failed at EE Data address\n";
					}
					if (eeblank == 65535U)
					{
						Label label4 = this.displayStatusWindow;
						label4.Text += string.Format("0x{0:X4}", (num11 - 1) * 2);
					}
					else
					{
						Label label5 = this.displayStatusWindow;
						label5.Text += string.Format("0x{0:X4}", num11 - 1);
					}
					FormPICkit2.statusWindowColor = Constants.StatusColor.red;
					this.updateGUI(true);
					return false;
					Block_55:
					PICkitFunctions.RunScript(1, 1);
					if (PICkitFunctions.LearnMode && PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].ProgMemShift > 0)
					{
						PICkitFunctions.MetaCmd_CHANGE_CHKSM_FRMT(1);
					}
				}
				if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].UserIDWords > 0 && this.checkBoxProgMemEnabled.Checked)
				{
					Label label6 = this.displayStatusWindow;
					label6.Text += "UserIDs... ";
					base.Update();
					PICkitFunctions.RunScript(0, 1);
					if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].UserIDRdPrepScript > 0)
					{
						PICkitFunctions.RunScript(16, 1);
					}
					int userIDBytes = (int)PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].UserIDBytes;
					int num15 = 0;
					int num16 = 0;
					PICkitFunctions.RunScriptUploadNoLen(17, 1);
					Array.Copy(PICkitFunctions.Usb_read_array, 1L, array, 0L, 64L);
					if ((long)((int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].UserIDWords * userIDBytes) > 64L)
					{
						PICkitFunctions.UploadDataNoLen();
						Array.Copy(PICkitFunctions.Usb_read_array, 1L, array, 64L, 64L);
					}
					PICkitFunctions.RunScript(1, 1);
					for (;;)
					{
						int num17 = 0;
						uint num18 = (uint)array[num16 + num17++];
						if (num17 < userIDBytes)
						{
							num18 |= (uint)((uint)array[num16 + num17++] << 8);
						}
						if (num17 < userIDBytes)
						{
							num18 |= (uint)((uint)array[num16 + num17++] << 16);
						}
						if (num17 < userIDBytes)
						{
							num18 |= (uint)((uint)array[num16 + num17++] << 24);
						}
						num16 += num17;
						if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].ProgMemShift > 0)
						{
							num18 = (num18 >> 1 & PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue);
						}
						if (num18 != PICkitFunctions.DeviceBuffers.UserIDs[num15++] && !PICkitFunctions.LearnMode)
						{
							break;
						}
						if (num15 >= (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].UserIDWords)
						{
							goto IL_BAC;
						}
					}
					this.conditionalVDDOff();
					if (!writeVerify)
					{
						this.displayStatusWindow.Text = "Verification of User IDs failed.";
					}
					else
					{
						this.displayStatusWindow.Text = "Programming failed at User IDs.";
					}
					FormPICkit2.statusWindowColor = Constants.StatusColor.red;
					this.updateGUI(true);
					return false;
				}
				IL_BAC:
				if (!writeVerify && !this.verifyConfig(configWords, configLocation))
				{
					return false;
				}
				PICkitFunctions.RunScript(1, 1);
				if (!writeVerify)
				{
					FormPICkit2.statusWindowColor = Constants.StatusColor.green;
					this.displayStatusWindow.Text = "Verification Successful.\n";
					this.conditionalVDDOff();
				}
				this.updateGUI(true);
				return true;
			}
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x0002E5E4 File Offset: 0x0002D5E4
		private bool verifyConfig(int configWords, int configLocation)
		{
			if (configWords > 0 && (long)configLocation > (long)((ulong)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgramMem) && this.checkBoxProgMemEnabled.Checked)
			{
				Label label = this.displayStatusWindow;
				label.Text += "Config... ";
				base.Update();
				PICkitFunctions.RunScript(0, 1);
				PICkitFunctions.RunScript(13, 1);
				PICkitFunctions.UploadData();
				PICkitFunctions.RunScript(1, 1);
				int num = 2;
				for (int i = 0; i < configWords; i++)
				{
					uint num2 = (uint)PICkitFunctions.Usb_read_array[num++];
					num2 |= (uint)((uint)PICkitFunctions.Usb_read_array[num++] << 8);
					if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].ProgMemShift > 0)
					{
						num2 = (num2 >> 1 & PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue);
					}
					num2 &= (uint)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[i];
					uint num3 = PICkitFunctions.DeviceBuffers.ConfigWords[i] & (uint)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[i];
					if (i == (int)(PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].CPConfig - 1))
					{
						if (this.enableCodeProtectToolStripMenuItem.Enabled && this.enableCodeProtectToolStripMenuItem.Checked)
						{
							num3 &= (uint)(~(uint)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].CPMask);
						}
						if (this.enableDataProtectStripMenuItem.Enabled && this.enableDataProtectStripMenuItem.Checked)
						{
							num3 &= (uint)(~(uint)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].DPMask);
						}
					}
					if (num3 != num2 && !PICkitFunctions.LearnMode)
					{
						this.conditionalVDDOff();
						this.displayStatusWindow.Text = "Verification of configuration failed.";
						FormPICkit2.statusWindowColor = Constants.StatusColor.red;
						this.updateGUI(true);
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x0002E7D7 File Offset: 0x0002D7D7
		private void downloadPk2Firmware(object sender, EventArgs e)
		{
			if (this.openFWFile.ShowDialog() == DialogResult.OK)
			{
				this.downloadNewFirmware();
			}
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x0002E7F0 File Offset: 0x0002D7F0
		private void downloadNewFirmware()
		{
			this.progressBar1.Value = 0;
			this.progressBar1.Maximum = 2;
			this.displayStatusWindow.Text = "Downloading New PICkit 2 Operating System.";
			this.displayStatusWindow.BackColor = Color.SteelBlue;
			base.Update();
			if (!Pk2BootLoader.ReadHexAndDownload(this.openFWFile.FileName, ref FormPICkit2.pk2number))
			{
				this.displayStatusWindow.Text = "Downloading failed.\nUse Tools > Check Communications to reconnect.";
				this.displayStatusWindow.BackColor = Color.Salmon;
				this.downloadPICkit2FirmwareToolStripMenuItem.Enabled = false;
				this.chkBoxVddOn.Enabled = false;
				this.numUpDnVDD.Enabled = false;
				this.deviceToolStripMenuItem.Enabled = false;
				this.disableGUIControls();
				return;
			}
			this.progressBar1.PerformStep();
			this.displayStatusWindow.Text = "Verifying PICkit 2 Operating System.";
			base.Update();
			if (!Pk2BootLoader.ReadHexAndVerify(this.openFWFile.FileName))
			{
				this.displayStatusWindow.Text = "Operating System verification failed.";
				this.displayStatusWindow.BackColor = Color.Salmon;
				return;
			}
			if (!PICkitFunctions.BL_WriteFWLoadedKey())
			{
				this.displayStatusWindow.Text = "Error loading Operating System.";
				this.displayStatusWindow.BackColor = Color.Salmon;
				return;
			}
			this.progressBar1.PerformStep();
			this.displayStatusWindow.Text = "Verification Successful!\nWaiting for PICkit 2 to reset....";
			this.displayStatusWindow.BackColor = Color.LimeGreen;
			base.Update();
			PICkitFunctions.BL_Reset();
			Thread.Sleep(5000);
			PICkitFunctions.ResetPk2Number();
			if (!this.detectPICkit2(true, true))
			{
				return;
			}
			PICkitFunctions.VddOff();
			this.lookForPoweredTarget(false);
			if (PICkitFunctions.DetectDevice(16777215, true, this.chkBoxVddOn.Checked))
			{
				this.setGUIVoltageLimits(true);
				this.displayStatusWindow.Text = this.displayStatusWindow.Text + "\nPIC Device Found.";
				this.fullEnableGUIControls();
			}
			else
			{
				this.partialEnableGUIControls();
			}
			this.checkForPowerErrors();
			this.updateGUI(true);
		}

		// Token: 0x060000EA RID: 234 RVA: 0x0002E9E0 File Offset: 0x0002D9E0
		private void programmingSpeed(object sender, EventArgs e)
		{
			if (this.fastProgrammingToolStripMenuItem.Checked)
			{
				PICkitFunctions.SetFastProgramming(true);
				this.displayStatusWindow.BackColor = SystemColors.Info;
				if (!PICkitFunctions.FamilyIsEEPROM())
				{
					this.displayStatusWindow.Text = "Fast programming On- Programming operations\n";
					Label label = this.displayStatusWindow;
					label.Text += "are faster, but less tolerant of loaded ICSP lines.";
					return;
				}
				if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[0] == 1)
				{
					this.displayStatusWindow.Text = "Fast programming On- 400kHz I2C\n";
					return;
				}
				if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[0] == 4)
				{
					this.displayStatusWindow.Text = "Fast programming On- FBUS = 100kHz\n";
					return;
				}
				this.displayStatusWindow.Text = "Fast programming On- 925kHz SCK\n";
				return;
			}
			else
			{
				PICkitFunctions.SetFastProgramming(false);
				this.displayStatusWindow.BackColor = SystemColors.Info;
				if (!PICkitFunctions.FamilyIsEEPROM())
				{
					this.displayStatusWindow.Text = "Fast programming Off - Programming operations\n";
					Label label2 = this.displayStatusWindow;
					label2.Text += "are slower, but more tolerant of loaded ICSP lines.";
					return;
				}
				if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[0] == 1)
				{
					this.displayStatusWindow.Text = "Fast programming Off - 100kHz I2C\n";
					return;
				}
				if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[0] == 4)
				{
					this.displayStatusWindow.Text = "Fast programming Off- FBUS = 25kHz\n";
					return;
				}
				this.displayStatusWindow.Text = "Fast programming Off - 245kHz SCK\n";
				return;
			}
		}

		// Token: 0x060000EB RID: 235 RVA: 0x0002EB6C File Offset: 0x0002DB6C
		private void clickAbout(object sender, EventArgs e)
		{
			DialogAbout dialogAbout = new DialogAbout();
			dialogAbout.ShowDialog();
		}

		// Token: 0x060000EC RID: 236 RVA: 0x0002EB88 File Offset: 0x0002DB88
		private void launchUsersGuide(object sender, EventArgs e)
		{
			try
			{
				Process.Start(FormPICkit2.HomeDirectory + "\\PICkit2 User Guide 51553E.pdf");
			}
			catch
			{
				MessageBox.Show("Unable to open User's Guide.");
			}
		}

		// Token: 0x060000ED RID: 237 RVA: 0x0002EBCC File Offset: 0x0002DBCC
		private void launchReadMe(object sender, EventArgs e)
		{
			try
			{
				Process.Start(FormPICkit2.HomeDirectory + "\\PICkit 2 Readme.txt");
			}
			catch
			{
				MessageBox.Show("Unable to open ReadMe file.");
			}
		}

		// Token: 0x060000EE RID: 238 RVA: 0x0002EC10 File Offset: 0x0002DC10
		private void codeProtect(object sender, EventArgs e)
		{
			if (this.enableDataProtectStripMenuItem.Enabled && PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].DPMask == 0)
			{
				this.enableDataProtectStripMenuItem.Checked = this.enableCodeProtectToolStripMenuItem.Checked;
			}
			this.updateGUI(true);
		}

		// Token: 0x060000EF RID: 239 RVA: 0x0002EC64 File Offset: 0x0002DC64
		private void dataProtect(object sender, EventArgs e)
		{
			if (this.enableDataProtectStripMenuItem.Enabled && PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].DPMask == 0)
			{
				this.enableCodeProtectToolStripMenuItem.Checked = this.enableDataProtectStripMenuItem.Checked;
			}
			this.updateGUI(true);
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x0002ECB6 File Offset: 0x0002DCB6
		private void writeOnButton(object sender, EventArgs e)
		{
			if (this.writeOnPICkitButtonToolStripMenuItem.Checked)
			{
				this.timerButton.Enabled = true;
				this.buttonLast = true;
				this.displayStatusWindow.Text = "Waiting for PICkit 2 button to be pressed....";
				return;
			}
			this.timerButton.Enabled = false;
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x0002ECF5 File Offset: 0x0002DCF5
		private void timerGoesOff(object sender, EventArgs e)
		{
			if (!PICkitFunctions.ButtonPressed())
			{
				this.buttonLast = false;
				return;
			}
			if (this.buttonLast)
			{
				return;
			}
			this.buttonLast = true;
			this.deviceWrite();
			this.checkForPowerErrors();
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x0002ED24 File Offset: 0x0002DD24
		private void conditionalVDDOff()
		{
			if (!this.chkBoxVddOn.Checked)
			{
				PICkitFunctions.VddOff();
			}
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x0002ED3C File Offset: 0x0002DD3C
		private void buttonReadExport(object sender, EventArgs e)
		{
			if (PICkitFunctions.FamilyIsKeeloq())
			{
				this.displayStatusWindow.Text = "Read not supported for this device type.";
				FormPICkit2.statusWindowColor = Constants.StatusColor.yellow;
				this.updateGUI(false);
				return;
			}
			this.deviceRead();
			this.updateGUI(true);
			this.Refresh();
			this.saveHexFileDialog.ShowDialog();
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x0002ED8D File Offset: 0x0002DD8D
		private void menuVDDAuto(object sender, EventArgs e)
		{
			this.vddAuto();
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x0002ED95 File Offset: 0x0002DD95
		private void vddAuto()
		{
			this.autoDetectToolStripMenuItem.Checked = true;
			this.forcePICkit2ToolStripMenuItem.Checked = false;
			this.forceTargetToolStripMenuItem.Checked = false;
			this.lookForPoweredTarget(false);
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x0002EDC3 File Offset: 0x0002DDC3
		private void menuVDDPk2(object sender, EventArgs e)
		{
			this.vddPk2();
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x0002EDCB File Offset: 0x0002DDCB
		private void vddPk2()
		{
			this.autoDetectToolStripMenuItem.Checked = false;
			this.forcePICkit2ToolStripMenuItem.Checked = true;
			this.forceTargetToolStripMenuItem.Checked = false;
			this.lookForPoweredTarget(false);
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x0002EDF9 File Offset: 0x0002DDF9
		private void menuVDDTarget(object sender, EventArgs e)
		{
			this.vddTarget();
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x0002EE01 File Offset: 0x0002DE01
		private void vddTarget()
		{
			this.autoDetectToolStripMenuItem.Checked = false;
			this.forcePICkit2ToolStripMenuItem.Checked = false;
			this.forceTargetToolStripMenuItem.Checked = true;
			this.lookForPoweredTarget(false);
		}

		// Token: 0x060000FA RID: 250 RVA: 0x0002EE30 File Offset: 0x0002DE30
		private void deviceFamilyClick(object sender, ToolStripItemClickedEventArgs e)
		{
			ToolStripMenuItem toolStripMenuItem = (ToolStripMenuItem)e.ClickedItem;
			if (toolStripMenuItem.HasDropDownItems)
			{
				return;
			}
			this.labelConfig.Enabled = false;
			string a;
			if (toolStripMenuItem.OwnerItem.Equals(this.deviceToolStripMenuItem))
			{
				a = toolStripMenuItem.Text;
			}
			else
			{
				a = toolStripMenuItem.OwnerItem.Text + "/" + toolStripMenuItem.Text;
			}
			int num = 0;
			while (num < PICkitFunctions.DevFile.Families.Length && !(a == PICkitFunctions.DevFile.Families[num].FamilyName))
			{
				num++;
			}
			for (int i = 1; i < PICkitFunctions.DevFile.Info.NumberParts; i++)
			{
				if ((int)PICkitFunctions.DevFile.PartsList[i].Family == num)
				{
					PICkitFunctions.DevFile.PartsList[0].VddMax = PICkitFunctions.DevFile.PartsList[i].VddMax;
					PICkitFunctions.DevFile.PartsList[0].VddMin = PICkitFunctions.DevFile.PartsList[i].VddMin;
					break;
				}
			}
			this.FamilySelectLogic(num, true);
		}

		// Token: 0x060000FB RID: 251 RVA: 0x0002EF64 File Offset: 0x0002DF64
		private void FamilySelectLogic(int family, bool updateGUI_OK)
		{
			if (family != PICkitFunctions.GetActiveFamily())
			{
				PICkitFunctions.ActivePart = 0;
				this.setGUIVoltageLimits(true);
			}
			else
			{
				this.setGUIVoltageLimits(false);
			}
			this.displayStatusWindow.Text = "";
			if (PICkitFunctions.DevFile.Families[family].PartDetect)
			{
				PICkitFunctions.SetActiveFamily(family);
				if (this.preProgrammingCheck(family))
				{
					this.displayStatusWindow.Text = PICkitFunctions.DevFile.Families[family].FamilyName + " device found.";
					this.setGUIVoltageLimits(false);
				}
				this.comboBoxSelectPart.Visible = false;
				this.displayDevice.Visible = true;
				if (updateGUI_OK)
				{
					this.updateGUI(true);
				}
			}
			else
			{
				this.buildDeviceSelectDropDown(family);
				this.comboBoxSelectPart.Visible = true;
				this.comboBoxSelectPart.SelectedIndex = 0;
				this.displayDevice.Visible = true;
				PICkitFunctions.SetActiveFamily(family);
				if (updateGUI_OK)
				{
					this.updateGUI(true);
				}
				this.disableGUIControls();
			}
			this.displayDataSource.Text = "None (Empty/Erased)";
		}

		// Token: 0x060000FC RID: 252 RVA: 0x0002F06C File Offset: 0x0002E06C
		private void buildDeviceSelectDropDown(int family)
		{
			this.comboBoxSelectPart.Items.Clear();
			this.comboBoxSelectPart.Items.Add("-Select Part-");
			for (int i = 1; i < PICkitFunctions.DevFile.Info.NumberParts; i++)
			{
				if ((int)PICkitFunctions.DevFile.PartsList[i].Family == family)
				{
					this.comboBoxSelectPart.Items.Add(PICkitFunctions.DevFile.PartsList[i].PartName);
				}
			}
		}

		// Token: 0x060000FD RID: 253 RVA: 0x0002F0F8 File Offset: 0x0002E0F8
		private void selectPart(object sender, EventArgs e)
		{
			if (this.comboBoxSelectPart.SelectedIndex == 0)
			{
				this.disableGUIControls();
			}
			else
			{
				string b = this.comboBoxSelectPart.SelectedItem.ToString();
				this.fullEnableGUIControls();
				for (int i = 0; i < PICkitFunctions.DevFile.Info.NumberParts; i++)
				{
					if (PICkitFunctions.DevFile.PartsList[i].PartName == b)
					{
						PICkitFunctions.ActivePart = i;
						break;
					}
				}
			}
			PICkitFunctions.PrepNewPart(true);
			this.setGUIVoltageLimits(true);
			this.displayDataSource.Text = "None (Empty/Erased)";
			if (this.useLVP && PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].LVPScript > 0)
			{
				this.toolStripMenuItemLVPEnabled.Checked = true;
			}
			this.useLVP = false;
			this.updateGUI(true);
		}

		// Token: 0x060000FE RID: 254 RVA: 0x0002F1CC File Offset: 0x0002E1CC
		private void autoDownloadFW(object sender, EventArgs e)
		{
			this.timerDLFW.Enabled = false;
			this.displayStatusWindow.Text = "The PICkit 2 Operating System v" + PICkitFunctions.FirmwareVersion + " must be updated.";
			DialogResult dialogResult = MessageBox.Show("PICkit 2 Operating System must be updated\nbefore it can be used with this software\nversion.\n\nClick OK to download a new Operating System.", "Update Operating System", MessageBoxButtons.OKCancel);
			if (dialogResult == DialogResult.OK)
			{
				this.openFWFile.FileName = "PK2V023200.hex";
				this.downloadNewFirmware();
				this.oldFirmware = false;
				return;
			}
			this.displayStatusWindow.Text = "The PICkit 2 OS v" + PICkitFunctions.FirmwareVersion + " must be updated.\nUse the Tools menu to download a new OS.";
		}

		// Token: 0x060000FF RID: 255 RVA: 0x0002F258 File Offset: 0x0002E258
		private void SaveINI()
		{
			StreamWriter streamWriter = File.CreateText(FormPICkit2.HomeDirectory + "\\PICkit2.ini");
			string text = ";PICkit 2 version 2.61.00 INI file.";
			streamWriter.WriteLine(text);
			DateTime dateTime = default(DateTime);
			dateTime = DateTime.Now;
			text = ";" + dateTime.Date.ToShortDateString() + " " + dateTime.ToShortTimeString();
			streamWriter.WriteLine(text);
			streamWriter.WriteLine("");
			text = "Y";
			if (this.toolStripMenuItemManualSelect.Checked)
			{
				text = "N";
				this.searchOnStartup = false;
			}
			else if (!this.autoDetectInINI)
			{
				this.searchOnStartup = true;
			}
			streamWriter.WriteLine("ADET: " + text);
			text = "N";
			if (this.searchOnStartup)
			{
				text = "Y";
			}
			streamWriter.WriteLine("PDET: " + text);
			if (PICkitFunctions.DevFile.Families == null)
			{
				text = this.lastFamily;
			}
			else
			{
				text = PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].FamilyName;
			}
			streamWriter.WriteLine("LFAM: " + text);
			text = "N";
			if (this.verifyOnWriteToolStripMenuItem.Checked)
			{
				text = "Y";
			}
			streamWriter.WriteLine("VRFW: " + text);
			text = "N";
			if (this.writeOnPICkitButtonToolStripMenuItem.Checked)
			{
				text = "Y";
			}
			streamWriter.WriteLine("WRBT: " + text);
			text = "N";
			if (this.MCLRtoolStripMenuItem.Checked)
			{
				text = "Y";
			}
			streamWriter.WriteLine("MCLR: " + text);
			if (this.VppFirstToolStripMenuItem.Checked)
			{
				this.restoreVddTarget();
			}
			text = "Auto";
			if (this.forcePICkit2ToolStripMenuItem.Checked)
			{
				text = "PICkit";
			}
			else if (this.forceTargetToolStripMenuItem.Checked)
			{
				text = "Target";
			}
			streamWriter.WriteLine("TVDD: " + text);
			text = "N";
			if (this.fastProgrammingToolStripMenuItem.Checked)
			{
				text = "Y";
			}
			streamWriter.WriteLine("FPRG: " + text);
			text = string.Format("PCLK: {0:G}", FormPICkit2.slowSpeedICSP);
			streamWriter.WriteLine(text);
			text = "N";
			if (this.multiWindow)
			{
				this.comboBoxProgMemView.SelectedIndex = this.programMemMultiWin.GetViewMode();
			}
			if (this.comboBoxProgMemView.SelectedIndex == 1)
			{
				text = "Y";
			}
			else if (this.comboBoxProgMemView.SelectedIndex == 2)
			{
				text = "B";
			}
			streamWriter.WriteLine("PASC: " + text);
			text = "N";
			if (this.multiWindow)
			{
				this.comboBoxProgMemView.SelectedIndex = this.eepromDataMultiWin.GetViewMode();
			}
			if (this.comboBoxEE.SelectedIndex == 1)
			{
				text = "Y";
			}
			else if (this.comboBoxEE.SelectedIndex == 2)
			{
				text = "B";
			}
			streamWriter.WriteLine("EASC: " + text);
			text = "N";
			if (this.allowDataEdits)
			{
				text = "Y";
			}
			streamWriter.WriteLine("EDIT: " + text);
			if (this.displayRev.Visible)
			{
				streamWriter.WriteLine("REVS: Y");
			}
			text = string.Format("SETV: {0:0.0}", this.numUpDnVDD.Value);
			streamWriter.WriteLine(text);
			text = "N";
			if (this.toolStripMenuItemClearBuffersErase.Checked)
			{
				text = "Y";
			}
			streamWriter.WriteLine("CLBF: " + text);
			text = "N";
			if (this.usePE33)
			{
				text = "Y";
			}
			streamWriter.WriteLine("PE33: " + text);
			text = "N";
			if (this.usePE24)
			{
				text = "Y";
			}
			streamWriter.WriteLine("PE24: " + text);
			text = "0";
			if (this.as1BitValueToolStripMenuItem.Checked)
			{
				text = "1";
			}
			else if (this.asReadOrImportedToolStripMenuItem.Checked)
			{
				text = "R";
			}
			streamWriter.WriteLine("CFGU: " + text);
			text = "N";
			if (this.toolStripMenuItemLVPEnabled.Checked)
			{
				text = "Y";
			}
			streamWriter.WriteLine("LVPE: " + text);
			text = "N";
			if (this.deviceVerification)
			{
				text = "Y";
			}
			streamWriter.WriteLine("DVER: " + text);
			streamWriter.WriteLine("HEX1: " + this.hex1);
			streamWriter.WriteLine("HEX2: " + this.hex2);
			streamWriter.WriteLine("HEX3: " + this.hex3);
			streamWriter.WriteLine("HEX4: " + this.hex4);
			if (this.selectDeviceFile)
			{
				streamWriter.WriteLine("SDAT: Y");
			}
			if (FormPICkit2.TestMemoryEnabled)
			{
				text = "N";
				if (FormPICkit2.TestMemoryOpen)
				{
					text = "Y";
				}
				streamWriter.WriteLine("TMEN: " + text);
				text = string.Format("TMWD: {0:G}", FormPICkit2.TestMemoryWords);
				streamWriter.WriteLine(text);
				text = "N";
				if (FormPICkit2.TestMemoryImportExport)
				{
					text = "Y";
				}
				streamWriter.WriteLine("TMIE: " + text);
			}
			text = "N";
			if (this.multiWindow)
			{
				text = "Y";
			}
			streamWriter.WriteLine("MWEN: " + text);
			text = string.Format("MWLX: {0:G}", base.Location.X);
			streamWriter.WriteLine(text);
			text = string.Format("MWLY: {0:G}", base.Location.Y);
			streamWriter.WriteLine(text);
			text = "N";
			if (this.mainWinOwnsMem)
			{
				text = "Y";
			}
			streamWriter.WriteLine("MWFR: " + text);
			text = "N";
			if (this.multiWinPMemOpen)
			{
				text = "Y";
			}
			streamWriter.WriteLine("PMEN: " + text);
			text = string.Format("PMLX: {0:G}", this.programMemMultiWin.Location.X);
			streamWriter.WriteLine(text);
			text = string.Format("PMLY: {0:G}", this.programMemMultiWin.Location.Y);
			streamWriter.WriteLine(text);
			text = string.Format("PMSX: {0:G}", this.programMemMultiWin.Size.Width);
			streamWriter.WriteLine(text);
			text = string.Format("PMSY: {0:G}", this.programMemMultiWin.Size.Height);
			streamWriter.WriteLine(text);
			text = "N";
			if (this.multiWinEEMemOpen)
			{
				text = "Y";
			}
			streamWriter.WriteLine("EEEN: " + text);
			text = string.Format("EELX: {0:G}", this.eepromDataMultiWin.Location.X);
			streamWriter.WriteLine(text);
			text = string.Format("EELY: {0:G}", this.eepromDataMultiWin.Location.Y);
			streamWriter.WriteLine(text);
			text = string.Format("EESX: {0:G}", this.eepromDataMultiWin.Size.Width);
			streamWriter.WriteLine(text);
			text = string.Format("EESY: {0:G}", this.eepromDataMultiWin.Size.Height);
			streamWriter.WriteLine(text);
			streamWriter.WriteLine("UABD: " + this.uartWindow.GetBaudRate());
			text = "N";
			if (this.uartWindow.IsHexMode())
			{
				text = "Y";
			}
			streamWriter.WriteLine("UAHX: " + text);
			streamWriter.WriteLine("UAS1: " + this.uartWindow.GetStringMacro(1));
			streamWriter.WriteLine("UAS2: " + this.uartWindow.GetStringMacro(2));
			streamWriter.WriteLine("UAS3: " + this.uartWindow.GetStringMacro(3));
			streamWriter.WriteLine("UAS4: " + this.uartWindow.GetStringMacro(4));
			text = "N";
			if (this.uartWindow.GetAppendCRLF())
			{
				text = "Y";
			}
			streamWriter.WriteLine("UACL: " + text);
			text = "N";
			if (this.uartWindow.GetWrap())
			{
				text = "Y";
			}
			streamWriter.WriteLine("UAWR: " + text);
			text = "N";
			if (this.uartWindow.GetEcho())
			{
				text = "Y";
			}
			streamWriter.WriteLine("UAEC: " + text);
			text = "N";
			if (this.logicWindow.getModeAnalyzer())
			{
				text = "Y";
			}
			streamWriter.WriteLine("LTAM: " + text);
			text = string.Format("LTZM: {0:G}", this.logicWindow.getZoom());
			streamWriter.WriteLine(text);
			text = string.Format("LTT1: {0:G}", this.logicWindow.getCh1Trigger());
			streamWriter.WriteLine(text);
			text = string.Format("LTT2: {0:G}", this.logicWindow.getCh2Trigger());
			streamWriter.WriteLine(text);
			text = string.Format("LTT3: {0:G}", this.logicWindow.getCh3Trigger());
			streamWriter.WriteLine(text);
			text = string.Format("LTTC: {0:G}", this.logicWindow.getTrigCount());
			streamWriter.WriteLine(text);
			text = string.Format("LTSR: {0:G}", this.logicWindow.getSampleRate());
			streamWriter.WriteLine(text);
			text = string.Format("LTTP: {0:G}", this.logicWindow.getTriggerPosition());
			streamWriter.WriteLine(text);
			text = "N";
			if (this.logicWindow.getCursorsEnabled())
			{
				text = "Y";
			}
			streamWriter.WriteLine("LTCE: " + text);
			text = string.Format("LTCX: {0:G}", this.logicWindow.getXCursorPos());
			streamWriter.WriteLine(text);
			text = string.Format("LTCY: {0:G}", this.logicWindow.getYCursorPos());
			streamWriter.WriteLine(text);
			text = "0";
			if (this.ptgMemory > 0 && this.ptgMemory <= 5)
			{
				if (this.ptgMemory == 1)
				{
					text = "1";
				}
				else if (this.ptgMemory == 2)
				{
					text = "2";
				}
				else if (this.ptgMemory == 3)
				{
					text = "3";
				}
				else if (this.ptgMemory == 4)
				{
					text = "4";
				}
				else if (this.ptgMemory == 5)
				{
					text = "5";
				}
			}
			streamWriter.WriteLine("PTGM: " + text);
			text = "N";
			if (FormPICkit2.PlaySuccessWav)
			{
				text = "Y";
			}
			streamWriter.WriteLine("SDSP: " + text);
			text = "N";
			if (FormPICkit2.PlayWarningWav)
			{
				text = "Y";
			}
			streamWriter.WriteLine("SDWP: " + text);
			text = "N";
			if (FormPICkit2.PlayErrorWav)
			{
				text = "Y";
			}
			streamWriter.WriteLine("SDEP: " + text);
			streamWriter.WriteLine("SDSF: " + FormPICkit2.SuccessWavFile);
			streamWriter.WriteLine("SDWF: " + FormPICkit2.WarningWavFile);
			streamWriter.WriteLine("SDEF: " + FormPICkit2.ErrorWavFile);
			streamWriter.Flush();
			streamWriter.Close();
		}

		// Token: 0x06000100 RID: 256 RVA: 0x0002FDA4 File Offset: 0x0002EDA4
		private float loadINI()
		{
			float num = 0f;
			try
			{
				int height = SystemInformation.VirtualScreen.Height;
				int width = SystemInformation.VirtualScreen.Width;
				FileInfo fileInfo = new FileInfo("PICkit2.ini");
				FormPICkit2.HomeDirectory = fileInfo.DirectoryName;
				FormPICkit2.SuccessWavFile = FormPICkit2.HomeDirectory + FormPICkit2.SuccessWavFile;
				FormPICkit2.WarningWavFile = FormPICkit2.HomeDirectory + FormPICkit2.WarningWavFile;
				FormPICkit2.ErrorWavFile = FormPICkit2.HomeDirectory + FormPICkit2.ErrorWavFile;
				TextReader textReader = fileInfo.OpenText();
				for (string text = textReader.ReadLine(); text != null; text = textReader.ReadLine())
				{
					if (text != "" && string.Compare(text.Substring(0, 1), ";") != 0 && string.Compare(text.Substring(0, 1), " ") != 0)
					{
						string text2 = text.Substring(0, 5);
						string key;
						switch (key = text2)
						{
						case "ADET:":
							if (string.Compare(text.Substring(6, 1), "N") == 0)
							{
								this.toolStripMenuItemManualSelect.Checked = true;
								this.autoDetectInINI = false;
								this.searchOnStartup = false;
							}
							break;
						case "PDET:":
							if (string.Compare(text.Substring(6, 1), "N") == 0)
							{
								this.searchOnStartup = false;
							}
							break;
						case "LFAM:":
							this.lastFamily = text.Substring(6);
							break;
						case "VRFW:":
							if (string.Compare(text.Substring(6, 1), "N") == 0)
							{
								this.verifyOnWriteToolStripMenuItem.Checked = false;
							}
							break;
						case "WRBT:":
							if (string.Compare(text.Substring(6, 1), "Y") == 0)
							{
								this.writeOnPICkitButtonToolStripMenuItem.Checked = true;
								this.timerButton.Enabled = true;
								this.buttonLast = true;
							}
							break;
						case "MCLR:":
							if (string.Compare(text.Substring(6, 1), "Y") == 0)
							{
								this.MCLRtoolStripMenuItem.Checked = true;
								this.checkBoxMCLR.Checked = true;
								PICkitFunctions.HoldMCLR(true);
							}
							break;
						case "TVDD:":
							if (string.Compare(text.Substring(6, 1), "P") == 0)
							{
								this.vddPk2();
							}
							else if (string.Compare(text.Substring(6, 1), "T") == 0)
							{
								this.vddTarget();
							}
							break;
						case "FPRG:":
							if (string.Compare(text.Substring(6, 1), "N") == 0)
							{
								this.fastProgrammingToolStripMenuItem.Checked = false;
								PICkitFunctions.SetFastProgramming(false);
							}
							break;
						case "PCLK:":
							if (text.Length == 7)
							{
								FormPICkit2.slowSpeedICSP = byte.Parse(text.Substring(6, 1));
							}
							else
							{
								FormPICkit2.slowSpeedICSP = byte.Parse(text.Substring(6, 2));
							}
							if (FormPICkit2.slowSpeedICSP < 2)
							{
								FormPICkit2.slowSpeedICSP = 2;
							}
							if (FormPICkit2.slowSpeedICSP > 16)
							{
								FormPICkit2.slowSpeedICSP = 16;
							}
							break;
						case "PASC:":
							if (string.Compare(text.Substring(6, 1), "Y") == 0)
							{
								this.comboBoxProgMemView.SelectedIndex = 1;
							}
							else if (string.Compare(text.Substring(6, 1), "B") == 0)
							{
								this.comboBoxProgMemView.SelectedIndex = 2;
							}
							break;
						case "EASC:":
							if (string.Compare(text.Substring(6, 1), "Y") == 0)
							{
								this.comboBoxEE.SelectedIndex = 1;
							}
							else if (string.Compare(text.Substring(6, 1), "B") == 0)
							{
								this.comboBoxEE.SelectedIndex = 2;
							}
							break;
						case "EDIT:":
							if (string.Compare(text.Substring(6, 1), "N") == 0)
							{
								this.allowDataEdits = false;
								this.calibrateToolStripMenuItem.Visible = false;
							}
							break;
						case "REVS:":
							this.displayRev.Visible = true;
							break;
						case "SETV:":
							if (text.Length == 9)
							{
								num = float.Parse(text.Substring(6, 3));
								if (num > 5f)
								{
									num = 5f;
								}
								if ((double)num < 2.5)
								{
									num = 2.5f;
								}
							}
							else
							{
								num = 0f;
							}
							break;
						case "CLBF:":
							if (string.Compare(text.Substring(6, 1), "N") == 0)
							{
								this.toolStripMenuItemClearBuffersErase.Checked = false;
							}
							break;
						case "PE33:":
							if (string.Compare(text.Substring(6, 1), "N") == 0)
							{
								this.usePE33 = false;
							}
							break;
						case "PE24:":
							if (string.Compare(text.Substring(6, 1), "N") == 0)
							{
								this.usePE24 = false;
							}
							break;
						case "CFGU:":
							if (string.Compare(text.Substring(6, 1), "1") == 0)
							{
								this.as0BitValueToolStripMenuItem.Checked = false;
								this.as1BitValueToolStripMenuItem.Checked = true;
							}
							else if (string.Compare(text.Substring(6, 1), "R") == 0)
							{
								this.as0BitValueToolStripMenuItem.Checked = false;
								this.asReadOrImportedToolStripMenuItem.Checked = true;
							}
							break;
						case "LVPE:":
							if (string.Compare(text.Substring(6, 1), "Y") == 0)
							{
								this.useLVP = true;
							}
							break;
						case "DVER:":
							if (string.Compare(text.Substring(6, 1), "N") == 0)
							{
								this.deviceVerification = false;
							}
							break;
						case "HEX1:":
							this.hex1 = text.Substring(6);
							if (this.hex1.Length > 3)
							{
								this.hex1ToolStripMenuItem.Visible = true;
								this.toolStripMenuItem5.Visible = true;
							}
							break;
						case "HEX2:":
							this.hex2 = text.Substring(6);
							if (this.hex2.Length > 3)
							{
								this.hex2ToolStripMenuItem.Visible = true;
							}
							break;
						case "HEX3:":
							this.hex3 = text.Substring(6);
							if (this.hex3.Length > 3)
							{
								this.hex3ToolStripMenuItem.Visible = true;
							}
							break;
						case "HEX4:":
							this.hex4 = text.Substring(6);
							if (this.hex4.Length > 3)
							{
								this.hex4ToolStripMenuItem.Visible = true;
							}
							break;
						case "SDAT:":
							if (string.Compare(text.Substring(6, 1), "Y") == 0)
							{
								this.selectDeviceFile = true;
							}
							break;
						case "TMEN:":
							FormPICkit2.TestMemoryEnabled = true;
							if (text.Length > 5 && string.Compare(text.Substring(6, 1), "Y") == 0)
							{
								FormPICkit2.TestMemoryOpen = true;
							}
							break;
						case "TMWD:":
							FormPICkit2.TestMemoryWords = int.Parse(text.Substring(6, text.Length - 6));
							if (FormPICkit2.TestMemoryWords < 16)
							{
								FormPICkit2.TestMemoryWords = 16;
							}
							if (FormPICkit2.TestMemoryWords > 1024)
							{
								FormPICkit2.TestMemoryWords = 1024;
							}
							break;
						case "TMIE:":
							if (string.Compare(text.Substring(6, 1), "Y") == 0)
							{
								FormPICkit2.TestMemoryImportExport = true;
							}
							break;
						case "MWEN:":
							if (string.Compare(text.Substring(6, 1), "Y") == 0)
							{
								this.multiWindow = true;
								this.viewChanged = true;
							}
							break;
						case "MWLX:":
						{
							int num3 = int.Parse(text.Substring(6, text.Length - 6));
							if (num3 < 0)
							{
								num3 = 0;
							}
							if (num3 > width)
							{
								num3 = width - 75;
							}
							base.Location = new Point(num3, base.Location.Y);
							break;
						}
						case "MWLY:":
						{
							int num3 = int.Parse(text.Substring(6, text.Length - 6));
							if (num3 < 0)
							{
								num3 = 0;
							}
							if (num3 > height)
							{
								num3 = height - 75;
							}
							base.Location = new Point(base.Location.X, num3);
							break;
						}
						case "MWFR:":
							if (string.Compare(text.Substring(6, 1), "N") == 0)
							{
								this.mainWinOwnsMem = false;
							}
							break;
						case "PMEN:":
							if (string.Compare(text.Substring(6, 1), "N") == 0)
							{
								this.multiWinPMemOpen = false;
							}
							break;
						case "PMLX:":
						{
							int num3 = int.Parse(text.Substring(6, text.Length - 6));
							if (num3 < 0)
							{
								num3 = 0;
							}
							if (num3 > width)
							{
								num3 = width - 75;
							}
							this.programMemMultiWin.Location = new Point(num3, this.programMemMultiWin.Location.Y);
							break;
						}
						case "PMLY:":
						{
							int num3 = int.Parse(text.Substring(6, text.Length - 6));
							if (num3 < 0)
							{
								num3 = 0;
							}
							if (num3 > height)
							{
								num3 = height - 75;
							}
							this.programMemMultiWin.Location = new Point(this.programMemMultiWin.Location.X, num3);
							break;
						}
						case "PMSX:":
						{
							int num3 = int.Parse(text.Substring(6, text.Length - 6));
							if (num3 < 50)
							{
								num3 = 50;
							}
							if (num3 > width)
							{
								num3 = width;
							}
							this.programMemMultiWin.Size = new Size(num3, this.programMemMultiWin.Size.Height);
							break;
						}
						case "PMSY:":
						{
							int num3 = int.Parse(text.Substring(6, text.Length - 6));
							if (num3 < 50)
							{
								num3 = 50;
							}
							if (num3 > height)
							{
								num3 = height;
							}
							this.programMemMultiWin.Size = new Size(this.programMemMultiWin.Size.Width, num3);
							break;
						}
						case "EEEN:":
							if (string.Compare(text.Substring(6, 1), "N") == 0)
							{
								this.multiWinEEMemOpen = false;
							}
							break;
						case "EELX:":
						{
							int num3 = int.Parse(text.Substring(6, text.Length - 6));
							if (num3 < 0)
							{
								num3 = 0;
							}
							if (num3 > width)
							{
								num3 = width - 75;
							}
							this.eepromDataMultiWin.Location = new Point(num3, this.eepromDataMultiWin.Location.Y);
							break;
						}
						case "EELY:":
						{
							int num3 = int.Parse(text.Substring(6, text.Length - 6));
							if (num3 < 0)
							{
								num3 = 0;
							}
							if (num3 > height)
							{
								num3 = height - 75;
							}
							this.eepromDataMultiWin.Location = new Point(this.eepromDataMultiWin.Location.X, num3);
							break;
						}
						case "EESX:":
						{
							int num3 = int.Parse(text.Substring(6, text.Length - 6));
							if (num3 < 50)
							{
								num3 = 50;
							}
							if (num3 > width)
							{
								num3 = width;
							}
							this.eepromDataMultiWin.Size = new Size(num3, this.eepromDataMultiWin.Size.Height);
							break;
						}
						case "EESY:":
						{
							int num3 = int.Parse(text.Substring(6, text.Length - 6));
							if (num3 < 50)
							{
								num3 = 50;
							}
							if (num3 > height)
							{
								num3 = height;
							}
							this.eepromDataMultiWin.Size = new Size(this.eepromDataMultiWin.Size.Width, num3);
							break;
						}
						case "UABD:":
							this.uartWindow.SetBaudRate(text.Substring(6));
							break;
						case "UAHX:":
							if (string.Compare(text.Substring(6, 1), "Y") == 0)
							{
								this.uartWindow.SetModeHex();
							}
							break;
						case "UAS1:":
							this.uartWindow.SetStringMacro(text.Substring(6), 1);
							break;
						case "UAS2:":
							this.uartWindow.SetStringMacro(text.Substring(6), 2);
							break;
						case "UAS3:":
							this.uartWindow.SetStringMacro(text.Substring(6), 3);
							break;
						case "UAS4:":
							this.uartWindow.SetStringMacro(text.Substring(6), 4);
							break;
						case "UACL:":
							if (string.Compare(text.Substring(6, 1), "N") == 0)
							{
								this.uartWindow.ClearAppendCRLF();
							}
							break;
						case "UAWR:":
							if (string.Compare(text.Substring(6, 1), "N") == 0)
							{
								this.uartWindow.ClearWrap();
							}
							break;
						case "UAEC:":
							if (string.Compare(text.Substring(6, 1), "N") == 0)
							{
								this.uartWindow.ClearEcho();
							}
							break;
						case "LTAM:":
							if (string.Compare(text.Substring(6, 1), "Y") == 0)
							{
								this.logicWindow.setModeAnalyzer();
							}
							break;
						case "LTZM:":
						{
							int num3 = int.Parse(text.Substring(6, text.Length - 6));
							if (num3 > 3)
							{
								num3 = 3;
							}
							this.logicWindow.setZoom(num3);
							break;
						}
						case "LTT1:":
						{
							int num3 = int.Parse(text.Substring(6, text.Length - 6));
							if (num3 > 5)
							{
								num3 = 5;
							}
							this.logicWindow.setCh1Trigger(num3);
							break;
						}
						case "LTT2:":
						{
							int num3 = int.Parse(text.Substring(6, text.Length - 6));
							if (num3 > 5)
							{
								num3 = 5;
							}
							this.logicWindow.setCh2Trigger(num3);
							break;
						}
						case "LTT3:":
						{
							int num3 = int.Parse(text.Substring(6, text.Length - 6));
							if (num3 > 5)
							{
								num3 = 5;
							}
							this.logicWindow.setCh3Trigger(num3);
							break;
						}
						case "LTTC:":
						{
							int num3 = int.Parse(text.Substring(6, text.Length - 6));
							if (num3 > 256)
							{
								num3 = 256;
							}
							if (num3 < 1)
							{
								num3 = 1;
							}
							this.logicWindow.setTrigCount(num3);
							break;
						}
						case "LTSR:":
						{
							int num3 = int.Parse(text.Substring(6, text.Length - 6));
							if (num3 > 7)
							{
								num3 = 7;
							}
							this.logicWindow.setSampleRate(num3);
							break;
						}
						case "LTTP:":
						{
							int num3 = int.Parse(text.Substring(6, text.Length - 6));
							if (num3 > 5)
							{
								num3 = 5;
							}
							this.logicWindow.setTriggerPosition(num3);
							break;
						}
						case "LTCE:":
							if (string.Compare(text.Substring(6, 1), "Y") == 0)
							{
								this.logicWindow.setCursorsEnabled(true);
							}
							break;
						case "LTCX:":
						{
							int num3 = int.Parse(text.Substring(6, text.Length - 6));
							if (num3 > 4095)
							{
								num3 = 4095;
							}
							this.logicWindow.setXCursorPos(num3);
							break;
						}
						case "LTCY:":
						{
							int num3 = int.Parse(text.Substring(6, text.Length - 6));
							if (num3 > 4095)
							{
								num3 = 4095;
							}
							this.logicWindow.setYCursorPos(num3);
							break;
						}
						case "PTGM:":
							if (string.Compare(text.Substring(6, 1), "1") == 0)
							{
								this.ptgMemory = 1;
							}
							else if (string.Compare(text.Substring(6, 1), "2") == 0)
							{
								this.ptgMemory = 2;
							}
							else if (string.Compare(text.Substring(6, 1), "3") == 0)
							{
								this.ptgMemory = 3;
							}
							else if (string.Compare(text.Substring(6, 1), "4") == 0)
							{
								this.ptgMemory = 4;
							}
							else if (string.Compare(text.Substring(6, 1), "5") == 0)
							{
								this.ptgMemory = 5;
							}
							break;
						case "SDSP:":
							if (string.Compare(text.Substring(6, 1), "Y") == 0)
							{
								FormPICkit2.PlaySuccessWav = true;
							}
							break;
						case "SDWP:":
							if (string.Compare(text.Substring(6, 1), "Y") == 0)
							{
								FormPICkit2.PlayWarningWav = true;
							}
							break;
						case "SDEP:":
							if (string.Compare(text.Substring(6, 1), "Y") == 0)
							{
								FormPICkit2.PlayErrorWav = true;
							}
							break;
						case "SDSF:":
							FormPICkit2.SuccessWavFile = text.Substring(6);
							break;
						case "SDWF:":
							FormPICkit2.WarningWavFile = text.Substring(6);
							break;
						case "SDEF:":
							FormPICkit2.ErrorWavFile = text.Substring(6);
							break;
						}
					}
				}
				textReader.Close();
			}
			catch
			{
				return 0f;
			}
			this.hex1ToolStripMenuItem.Text = "&1 " + this.shortenHex(this.hex1);
			this.hex2ToolStripMenuItem.Text = "&2 " + this.shortenHex(this.hex2);
			this.hex3ToolStripMenuItem.Text = "&3 " + this.shortenHex(this.hex3);
			this.hex4ToolStripMenuItem.Text = "&4 " + this.shortenHex(this.hex4);
			return num;
		}

		// Token: 0x06000101 RID: 257 RVA: 0x000311D4 File Offset: 0x000301D4
		private string shortenHex(string fullPath)
		{
			if (fullPath.Length > 42)
			{
				return fullPath.Substring(0, 3) + "..." + fullPath.Substring(fullPath.Length - 36);
			}
			return fullPath;
		}

		private void hex1Click(object sender, EventArgs e)
		{
			HexImportFromHistory(hex1);
		}

		private void hex2Click(object sender, EventArgs e)
		{
			HexImportFromHistory(hex2);
		}

		// Token: 0x06000104 RID: 260 RVA: 0x0003121F File Offset: 0x0003021F
		private void hex3Click(object sender, EventArgs e)
		{
			this.HexImportFromHistory(this.hex3);
		}

		// Token: 0x06000105 RID: 261 RVA: 0x0003122D File Offset: 0x0003022D
		private void hex4Click(object sender, EventArgs e)
		{
			this.HexImportFromHistory(this.hex4);
		}

		private void HexImportFromHistory(string filename)
		{
			if (importFileToolStripMenuItem.Enabled && filename.Length > 3)
			{
				openHexFileDialog.FileName = filename;
				ImportHexFileGo();
				updateGUI(true);
			}
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00031270 File Offset: 0x00030270
		private void launchLPCDemoGuide(object sender, EventArgs e)
		{
			try
			{
				Process.Start(FormPICkit2.HomeDirectory + "\\Low Pin Count User Guide 51556a.pdf");
			}
			catch
			{
				MessageBox.Show("Unable to open\nLPC Demo Board User's Guide.");
			}
		}

		// Token: 0x06000108 RID: 264 RVA: 0x000312B4 File Offset: 0x000302B4
		private void uG44pinToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				Process.Start(FormPICkit2.HomeDirectory + "\\44-Pin Demo Board User Guide 41296b.pdf");
			}
			catch
			{
				MessageBox.Show("Unable to open\n44-Pin Demo Board User's Guide.");
			}
		}

		// Token: 0x06000109 RID: 265 RVA: 0x000312F8 File Offset: 0x000302F8
		private void memorySelectVerify(object sender, EventArgs e)
		{
			if (sender.Equals(this.checkBoxProgMemEnabled))
			{
				this.checkBoxProgMemEnabledAlt.Checked = this.checkBoxProgMemEnabled.Checked;
			}
			if (sender.Equals(this.checkBoxProgMemEnabledAlt))
			{
				this.checkBoxProgMemEnabled.Checked = this.checkBoxProgMemEnabledAlt.Checked;
			}
			if (sender.Equals(this.checkBoxEEMem))
			{
				this.checkBoxEEDATAMemoryEnabledAlt.Checked = this.checkBoxEEMem.Checked;
			}
			if (sender.Equals(this.checkBoxEEDATAMemoryEnabledAlt))
			{
				this.checkBoxEEMem.Checked = this.checkBoxEEDATAMemoryEnabledAlt.Checked;
			}
			if (!this.checkBoxProgMemEnabled.Checked && !this.checkBoxEEMem.Checked)
			{
				MessageBox.Show("At least one memory region\nmust be selected.");
				if (sender.Equals(this.checkBoxProgMemEnabled) || sender.Equals(this.checkBoxProgMemEnabledAlt))
				{
					this.checkBoxProgMemEnabled.Checked = true;
					this.checkBoxProgMemEnabledAlt.Checked = true;
				}
				else
				{
					this.checkBoxEEMem.Checked = true;
					this.checkBoxEEDATAMemoryEnabledAlt.Checked = true;
				}
			}
			this.updateGUI(false);
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00031410 File Offset: 0x00030410
		private void setOSCCAL(object sender, EventArgs e)
		{
			if (this.setOSCCALToolStripMenuItem.Enabled)
			{
				SetOSCCAL setOSCCAL = new SetOSCCAL();
				setOSCCAL.ShowDialog();
				if (FormPICkit2.setOSCCALValue)
				{
					this.eraseDeviceAll(true, new uint[0]);
					Label label = this.displayStatusWindow;
					label.Text += "\nOSCCAL Set.";
				}
				FormPICkit2.setOSCCALValue = false;
				this.updateGUI(true);
			}
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00031474 File Offset: 0x00030474
		private void pickit2OnTheWeb(object sender, EventArgs e)
		{
			try
			{
				Process.Start("http://www.microchip.com/pickit2");
			}
			catch
			{
				MessageBox.Show("Unable to open link.");
			}
		}

		// Token: 0x0600010C RID: 268 RVA: 0x000314AC File Offset: 0x000304AC
		private void troubleshhotToolStripMenuItem_Click(object sender, EventArgs e)
		{
			DialogTroubleshoot dialogTroubleshoot = new DialogTroubleshoot();
			dialogTroubleshoot.ShowDialog();
			this.chkBoxVddOn.Checked = false;
			if (FormPICkit2.selfPoweredTarget)
			{
				PICkitFunctions.ForceTargetPowered();
			}
		}

		// Token: 0x0600010D RID: 269 RVA: 0x000314E0 File Offset: 0x000304E0
		private void MCLRtoolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (this.MCLRtoolStripMenuItem.Checked)
			{
				this.checkBoxMCLR.Checked = false;
				this.MCLRtoolStripMenuItem.Checked = false;
				PICkitFunctions.HoldMCLR(false);
				return;
			}
			this.checkBoxMCLR.Checked = true;
			this.MCLRtoolStripMenuItem.Checked = true;
			PICkitFunctions.HoldMCLR(true);
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00031539 File Offset: 0x00030539
		private void toolStripMenuItemTestMemory_Click(object sender, EventArgs e)
		{
			if (FormPICkit2.TestMemoryEnabled && !FormPICkit2.TestMemoryOpen)
			{
				this.openTestMemory();
			}
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00031550 File Offset: 0x00030550
		private void openTestMemory()
		{
			FormPICkit2.formTestMem = new FormTestMemory();
			FormPICkit2.formTestMem.UpdateMainFormGUI = new DelegateUpdateGUI(this.ExtCallUpdateGUI);
			FormPICkit2.formTestMem.CallMainFormEraseWrCal = new DelegateWriteCal(this.ExtCallCalEraseWrite);
			FormPICkit2.formTestMem.Show();
		}

		// Token: 0x06000110 RID: 272 RVA: 0x0003159D File Offset: 0x0003059D
		private void buttonImportWrite(object sender, EventArgs e)
		{
		}

		// Token: 0x06000111 RID: 273 RVA: 0x000315A0 File Offset: 0x000305A0
		private void checkBoxAutoImportWrite_Click(object sender, EventArgs e)
		{
			if (!this.checkBoxAutoImportWrite.Checked)
			{
				this.displayStatusWindow.Text = "Exited Auto-Import-Write mode.";
			}
			if (this.checkBoxAutoImportWrite.Checked)
			{
				this.importGo = false;
				if (this.hex1.Length > 3)
				{
					this.openHexFileDialog.FileName = this.hex1;
				}
				this.openHexFileDialog.ShowDialog();
				if (this.importGo)
				{
					this.updateGUI(true);
					this.Refresh();
					if (this.deviceWrite())
					{
						this.importFileToolStripMenuItem.Enabled = false;
						this.exportFileToolStripMenuItem.Enabled = false;
						this.readDeviceToolStripMenuItem.Enabled = false;
						this.writeDeviceToolStripMenuItem.Enabled = false;
						this.verifyToolStripMenuItem.Enabled = false;
						this.eraseToolStripMenuItem.Enabled = false;
						this.blankCheckToolStripMenuItem.Enabled = false;
						this.writeOnPICkitButtonToolStripMenuItem.Enabled = false;
						this.pICkit2GoToolStripMenuItem.Enabled = false;
						this.setOSCCALToolStripMenuItem.Enabled = false;
						this.buttonRead.Enabled = false;
						this.buttonWrite.Enabled = false;
						this.buttonVerify.Enabled = false;
						this.buttonErase.Enabled = false;
						this.buttonBlankCheck.Enabled = false;
						this.dataGridProgramMemory.Enabled = false;
						this.dataGridViewEEPROM.Enabled = false;
						this.buttonExportHex.Enabled = false;
						this.deviceToolStripMenuItem.Enabled = false;
						this.checkCommunicationToolStripMenuItem.Enabled = false;
						this.troubleshhotToolStripMenuItem.Enabled = false;
						this.downloadPICkit2FirmwareToolStripMenuItem.Enabled = false;
						Label label = this.displayStatusWindow;
						label.Text += "Waiting for file update...  (Click button again to exit)";
						this.timerAutoImportWrite.Enabled = true;
					}
					else
					{
						this.importGo = false;
					}
				}
				else
				{
					this.updateGUI(true);
				}
				if (!this.importGo)
				{
					this.checkBoxAutoImportWrite.Checked = false;
				}
			}
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00031784 File Offset: 0x00030784
		private void checkBoxAutoImportWrite_Changed(object sender, EventArgs e)
		{
			if (!this.checkBoxAutoImportWrite.Checked || !this.importGo)
			{
				this.importFileToolStripMenuItem.Enabled = true;
				this.exportFileToolStripMenuItem.Enabled = true;
				this.readDeviceToolStripMenuItem.Enabled = true;
				this.writeDeviceToolStripMenuItem.Enabled = true;
				this.verifyToolStripMenuItem.Enabled = true;
				this.eraseToolStripMenuItem.Enabled = true;
				this.blankCheckToolStripMenuItem.Enabled = true;
				this.writeOnPICkitButtonToolStripMenuItem.Enabled = true;
				this.pICkit2GoToolStripMenuItem.Enabled = true;
				this.setOSCCALToolStripMenuItem.Enabled = true;
				this.buttonRead.Enabled = true;
				this.buttonWrite.Enabled = true;
				this.buttonVerify.Enabled = true;
				this.buttonErase.Enabled = true;
				this.buttonBlankCheck.Enabled = true;
				this.dataGridProgramMemory.Enabled = true;
				this.dataGridViewEEPROM.Enabled = true;
				this.buttonExportHex.Enabled = true;
				this.deviceToolStripMenuItem.Enabled = true;
				this.checkCommunicationToolStripMenuItem.Enabled = true;
				this.troubleshhotToolStripMenuItem.Enabled = true;
				this.downloadPICkit2FirmwareToolStripMenuItem.Enabled = true;
				this.timerAutoImportWrite.Enabled = false;
				FormPICkit2.FLASHWINFO flashwinfo = default(FormPICkit2.FLASHWINFO);
				flashwinfo.cbSize = (ushort)Marshal.SizeOf(flashwinfo);
				flashwinfo.hwnd = base.Handle;
				flashwinfo.dwFlags = 14U;
				flashwinfo.uCount = ushort.MaxValue;
				flashwinfo.dwTimeout = 0U;
				FormPICkit2.FlashWindowEx(ref flashwinfo);
				if (base.WindowState == FormWindowState.Minimized)
				{
					base.WindowState = FormWindowState.Normal;
				}
			}
		}

		// Token: 0x06000113 RID: 275 RVA: 0x0003191C File Offset: 0x0003091C
		private void timerAutoImportWrite_Tick(object sender, EventArgs e)
		{
			FileInfo fileInfo = new FileInfo(this.openHexFileDialog.FileName);
			if (ImportExportHex.LastWriteTime != fileInfo.LastWriteTime)
			{
				if (this.deviceWrite())
				{
					this.importFileToolStripMenuItem.Enabled = false;
					this.exportFileToolStripMenuItem.Enabled = false;
					this.readDeviceToolStripMenuItem.Enabled = false;
					this.writeDeviceToolStripMenuItem.Enabled = false;
					this.verifyToolStripMenuItem.Enabled = false;
					this.eraseToolStripMenuItem.Enabled = false;
					this.blankCheckToolStripMenuItem.Enabled = false;
					this.writeOnPICkitButtonToolStripMenuItem.Enabled = false;
					this.pICkit2GoToolStripMenuItem.Enabled = false;
					this.setOSCCALToolStripMenuItem.Enabled = false;
					this.buttonRead.Enabled = false;
					this.buttonWrite.Enabled = false;
					this.buttonVerify.Enabled = false;
					this.buttonErase.Enabled = false;
					this.buttonBlankCheck.Enabled = false;
					this.dataGridProgramMemory.Enabled = false;
					this.dataGridViewEEPROM.Enabled = false;
					this.buttonExportHex.Enabled = false;
					this.deviceToolStripMenuItem.Enabled = false;
					this.checkCommunicationToolStripMenuItem.Enabled = false;
					this.troubleshhotToolStripMenuItem.Enabled = false;
					this.downloadPICkit2FirmwareToolStripMenuItem.Enabled = false;
					Label label = this.displayStatusWindow;
					label.Text += "Waiting for file update...  (Click button again to exit)";
					return;
				}
				this.timerAutoImportWrite.Enabled = false;
				this.checkBoxAutoImportWrite.Checked = false;
			}
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00031C04 File Offset: 0x00030C04
		private void buttonShowIDMem_Click(object sender, EventArgs e)
		{
			if (!DialogUserIDs.IDMemOpen)
			{
				this.dialogIDMemory = new DialogUserIDs();
				this.dialogIDMemory.Show();
			}
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00031C24 File Offset: 0x00030C24
		private uint getEEBlank()
		{
			uint result = 255U;
			if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].EEMemAddressIncrement > 1)
			{
				result = 65535U;
			}
			if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue == 4095U)
			{
				result = 4095U;
			}
			return result;
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00031C80 File Offset: 0x00030C80
		private void restoreVddTarget()
		{
			if (this.VddTargetSave == Constants.VddTargetSelect.auto)
			{
				this.vddAuto();
				return;
			}
			if (this.VddTargetSave == Constants.VddTargetSelect.pickit2)
			{
				this.vddPk2();
				return;
			}
			this.vddTarget();
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00031CA8 File Offset: 0x00030CA8
		private void VppFirstToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (!this.VppFirstToolStripMenuItem.Checked)
			{
				PICkitFunctions.ClearVppFirstProgramEntry();
				if (this.toolStripMenuItemManualSelect.Checked)
				{
					PICkitFunctions.PrepNewPart(false);
				}
				if (!this.toolStripMenuItemLVPEnabled.Checked)
				{
					this.displayStatusWindow.Text = "Normal programming mode entry.";
				}
				this.targetPowerToolStripMenuItem.Enabled = true;
				this.restoreVddTarget();
				return;
			}
			if (this.toolStripMenuItemLVPEnabled.Checked)
			{
				string text = PICkitFunctions.DevFile.Scripts[(int)(PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].LVPScript - 1)].ScriptName;
				text = text.Substring(text.Length - 2);
				if (text == "HV")
				{
					MessageBox.Show("'Use High Voltage Program Entry' is enabled.\n\nVPP First Program Entry may not be used\nwhile that option is enabled.", "Use VPP First Program Entry");
				}
				else
				{
					MessageBox.Show("'Use LVP Program Entry' is enabled.\n\nVPP First Program Entry may not be used\nwhile that option is enabled.", "Use VPP First Program Entry");
				}
				this.VppFirstToolStripMenuItem.Checked = false;
				return;
			}
			PICkitFunctions.SetVPPFirstProgramEntry();
			this.displayStatusWindow.Text = "VPP First programming mode entry set.\nTo use, PICkit 2 MUST supply VDD to target.";
			if (this.toolStripMenuItemManualSelect.Checked)
			{
				PICkitFunctions.PrepNewPart(false);
			}
			if (this.autoDetectToolStripMenuItem.Checked)
			{
				this.VddTargetSave = Constants.VddTargetSelect.auto;
			}
			else if (this.forcePICkit2ToolStripMenuItem.Checked)
			{
				this.VddTargetSave = Constants.VddTargetSelect.pickit2;
			}
			else
			{
				this.VddTargetSave = Constants.VddTargetSelect.target;
			}
			this.vddPk2();
			this.targetPowerToolStripMenuItem.Enabled = false;
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00031E04 File Offset: 0x00030E04
		private bool eepromWrite(bool eraseWrite)
		{
			if (!this.preProgrammingCheck(PICkitFunctions.GetActiveFamily()))
			{
				return false;
			}
			this.updateGUI(false);
			base.Update();
			if (this.checkImportFile && !eraseWrite)
			{
				FileInfo fileInfo = new FileInfo(this.openHexFileDialog.FileName);
				if (ImportExportHex.LastWriteTime != fileInfo.LastWriteTime)
				{
					this.displayStatusWindow.Text = "Reloading Hex File\n";
					base.Update();
					Thread.Sleep(300);
					if (!this.ImportHexFileGo())
					{
						this.displayStatusWindow.Text = "Error Loading Hex File: Write aborted.\n";
						FormPICkit2.statusWindowColor = Constants.StatusColor.red;
						this.updateGUI(true);
						return false;
					}
				}
			}
			PICkitFunctions.VddOn();
			if (eraseWrite)
			{
				this.displayStatusWindow.Text = "Erasing device:\n";
			}
			else
			{
				this.displayStatusWindow.Text = "Writing device:\n";
			}
			base.Update();
			int programMem = (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgramMem;
			if (this.checkBoxProgMemEnabled.Checked)
			{
				PICkitFunctions.RunScript(0, 1);
				Label label = this.displayStatusWindow;
				label.Text += "EEPROM... ";
				base.Update();
				this.progressBar1.Value = 0;
				int num = 3;
				if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[0] == 4)
				{
					num = 4;
				}
				int progMemWrWords = (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgMemWrWords;
				int bytesPerLocation = (int)PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BytesPerLocation;
				int num2 = 256;
				if (programMem < num2)
				{
					num2 = programMem + programMem / (progMemWrWords * bytesPerLocation) * num;
				}
				if (num2 > 256)
				{
					num2 = 256;
				}
				int num3 = num2 / (progMemWrWords * bytesPerLocation + num);
				int num4 = num3 * progMemWrWords;
				int num5 = 0;
				this.progressBar1.Maximum = programMem / num4;
				byte[] array = new byte[256];
				if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgMemWrPrepScript != 0)
				{
					PICkitFunctions.RunScript(6, 1);
				}
				for (;;)
				{
					int num6 = 0;
					for (int i = 0; i < num4; i++)
					{
						if (num5 == programMem)
						{
							num3 = num6 / (progMemWrWords * bytesPerLocation + num);
							break;
						}
						if (num5 % progMemWrWords == 0)
						{
							int num7 = this.eeprom24BitAddress(num5, false);
							if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[0] == 4)
							{
								array[num6++] = 150;
							}
							array[num6++] = (byte)(num7 >> 16 & 255);
							array[num6++] = (byte)(num7 >> 8 & 255);
							array[num6++] = (byte)(num7 & 255);
						}
						uint num8 = PICkitFunctions.DeviceBuffers.ProgramMemory[num5++];
						array[num6++] = (byte)(num8 & 255U);
						for (int j = 1; j < bytesPerLocation; j++)
						{
							num8 >>= 8;
							array[num6++] = (byte)(num8 & 255U);
						}
						if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[0] == 3 && bytesPerLocation == 2)
						{
							byte b = array[num6 - 2];
							array[num6 - 2] = array[num6 - 1];
							array[num6 - 1] = b;
						}
					}
					for (int k = PICkitFunctions.DataClrAndDownload(array, 0); k < num6; k = PICkitFunctions.DataDownload(array, k, num6))
					{
					}
					PICkitFunctions.RunScript(7, num3);
					if ((PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[0] == 1 || PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[0] == 4) && this.eeprom_CheckBusErrors())
					{
						break;
					}
					this.progressBar1.PerformStep();
					if (num5 >= programMem)
					{
						goto IL_3B2;
					}
				}
				return false;
			}
			IL_3B2:
			PICkitFunctions.RunScript(1, 1);
			bool flag = true;
			if (this.verifyOnWriteToolStripMenuItem.Checked && !eraseWrite)
			{
				if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[0] == 4)
				{
					this.conditionalVDDOff();
				}
				flag = this.deviceVerify(true, programMem - 1, false);
			}
			this.conditionalVDDOff();
			if (flag && !eraseWrite)
			{
				FormPICkit2.statusWindowColor = Constants.StatusColor.green;
				this.displayStatusWindow.Text = "Programming Successful.\n";
				this.updateGUI(true);
				return true;
			}
			return flag;
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00032240 File Offset: 0x00031240
		private int eeprom24BitAddress(int wordsWritten, bool setReadBit)
		{
			if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[0] == 1)
			{
				int num = (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[3];
				int num2 = wordsWritten & (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[1] & 65535;
				int num3 = wordsWritten >> (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[2];
				num3 <<= 17 + num;
				if (num > 0)
				{
					if (this.checkBoxA0CS.Checked)
					{
						num3 |= 131072;
					}
					if (this.checkBoxA1CS.Checked)
					{
						num3 |= 262144;
					}
					if (this.checkBoxA2CS.Checked)
					{
						num3 |= 524288;
					}
				}
				num2 += (num3 & 917504) + 10485760;
				if (setReadBit)
				{
					num2 |= 65536;
				}
				return num2;
			}
			if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[0] == 2)
			{
				int num4;
				if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgramMem <= 65536U)
				{
					num4 = (wordsWritten & (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[1] & 65535);
					int num5 = wordsWritten >> (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[2];
					num5 <<= 19;
					num4 += (num5 & 524288) + 131072;
					if (setReadBit)
					{
						num4 |= 65536;
					}
				}
				else
				{
					num4 = wordsWritten;
				}
				return num4;
			}
			if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[0] == 3)
			{
				int num6 = 5;
				int num7 = wordsWritten & (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[1] & 65535;
				if (setReadBit)
				{
					num6 = 6;
				}
				num6 <<= (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[2];
				return num7 | num6;
			}
			if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[0] == 4)
			{
				int num8 = wordsWritten & (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[1] & 65535;
				if (setReadBit)
				{
					num8 |= 196608;
				}
				else
				{
					num8 |= 7077888;
				}
				return num8;
			}
			return 0;
		}

		// Token: 0x0600011D RID: 285 RVA: 0x000324D4 File Offset: 0x000314D4
		private bool eeprom_CheckBusErrors()
		{
			if (PICkitFunctions.BusErrorCheck())
			{
				PICkitFunctions.RunScript(1, 1);
				this.conditionalVDDOff();
				if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[0] == 4)
				{
					this.displayStatusWindow.Text = "UNI/O Bus Error (NoSAK) - Aborted.\n";
				}
				else
				{
					this.displayStatusWindow.Text = "I2C Bus Error (No Acknowledge) - Aborted.\n";
				}
				FormPICkit2.statusWindowColor = Constants.StatusColor.yellow;
				this.updateGUI(true);
				return true;
			}
			return false;
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00032548 File Offset: 0x00031548
		private void calibrateToolStripMenuItem_Click(object sender, EventArgs e)
		{
			DialogCalibrate dialogCalibrate = new DialogCalibrate();
			dialogCalibrate.ShowDialog();
			this.chkBoxVddOn.Checked = false;
			if (FormPICkit2.selfPoweredTarget)
			{
				PICkitFunctions.ForceTargetPowered();
			}
			this.detectPICkit2(true, true);
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00032584 File Offset: 0x00031584
		private void UARTtoolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.timerButton.Enabled = false;
			this.MCLRtoolStripMenuItem.Checked = false;
			this.checkBoxMCLR.Checked = false;
			this.uartWindow.SetVddBox(this.numUpDnVDD.Enabled, this.chkBoxVddOn.Checked);
			if (this.multiWindow)
			{
				this.programMemMultiWin.Hide();
				this.eepromDataMultiWin.Hide();
			}
			base.Hide();
			this.uartWindow.ShowDialog();
			base.Show();
			if (this.multiWindow)
			{
				if (this.multiWinPMemOpen)
				{
					this.programMemMultiWin.Show();
				}
				if (this.multiWinEEMemOpen && PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].EEMem > 0)
				{
					this.eepromDataMultiWin.Show();
				}
				base.Focus();
			}
			if (!FormPICkit2.selfPoweredTarget)
			{
				PICkitFunctions.ForcePICkitPowered();
			}
			if (this.writeOnPICkitButtonToolStripMenuItem.Checked)
			{
				this.buttonLast = true;
				this.timerButton.Enabled = true;
			}
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00032689 File Offset: 0x00031689
		private void toolStripMenuItemSingleWindow_Click(object sender, EventArgs e)
		{
			if (this.multiWindow)
			{
				this.viewChanged = true;
			}
			this.multiWindow = false;
			this.updateGUI(true);
		}

		// Token: 0x06000121 RID: 289 RVA: 0x000326A8 File Offset: 0x000316A8
		private void toolStripMenuItemMultiWindow_Click(object sender, EventArgs e)
		{
			if (!this.multiWindow)
			{
				this.viewChanged = true;
			}
			this.multiWindow = true;
			this.updateGUI(true);
		}

		// Token: 0x06000122 RID: 290 RVA: 0x000326C8 File Offset: 0x000316C8
		private void toolStripMenuItemShowProgramMemory_Click(object sender, EventArgs e)
		{
			if (this.multiWinPMemOpen)
			{
				this.multiWinPMemOpen = false;
				this.toolStripMenuItemShowProgramMemory.Checked = false;
				this.programMemMultiWin.Hide();
			}
			else
			{
				this.multiWinPMemOpen = true;
				this.toolStripMenuItemShowProgramMemory.Checked = true;
				this.programMemMultiWin.Show();
			}
			base.Focus();
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00032724 File Offset: 0x00031724
		private void toolStripMenuItemShowEEPROMData_Click(object sender, EventArgs e)
		{
			if (this.multiWinEEMemOpen)
			{
				this.multiWinEEMemOpen = false;
				this.toolStripMenuItemShowEEPROMData.Checked = false;
				this.eepromDataMultiWin.Hide();
			}
			else
			{
				this.multiWinEEMemOpen = true;
				this.toolStripMenuItemShowEEPROMData.Checked = true;
				this.eepromDataMultiWin.Show();
			}
			base.Focus();
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00032780 File Offset: 0x00031780
		private void FormPICkit2_Move(object sender, EventArgs e)
		{
			if (base.WindowState != FormWindowState.Minimized)
			{
				if (this.multiWindow && this.mainWinOwnsMem)
				{
					int num = base.Location.X - this.lastLoc.X;
					int num2 = base.Location.Y - this.lastLoc.Y;
					int height = SystemInformation.VirtualScreen.Height;
					int width = SystemInformation.VirtualScreen.Width;
					int num3 = this.programMemMultiWin.Location.X + num;
					int num4 = this.programMemMultiWin.Location.Y + num2;
					if (num3 + 75 > width)
					{
						num3 = width - 75;
					}
					if (num3 < 0)
					{
						num3 = 0;
					}
					if (num4 + 75 > height)
					{
						num4 = height - 75;
					}
					if (num4 < 0)
					{
						num4 = 0;
					}
					if (this.programMemMultiWin.WindowState != FormWindowState.Maximized && this.programMemMultiWin.WindowState != FormWindowState.Minimized)
					{
						this.programMemMultiWin.Location = new Point(num3, num4);
					}
					num3 = this.eepromDataMultiWin.Location.X + num;
					num4 = this.eepromDataMultiWin.Location.Y + num2;
					if (num3 + 75 > width)
					{
						num3 = width - 75;
					}
					if (num3 < 0)
					{
						num3 = 0;
					}
					if (num4 + 75 > height)
					{
						num4 = height - 75;
					}
					if (num4 < 0)
					{
						num4 = 0;
					}
					if (this.eepromDataMultiWin.WindowState != FormWindowState.Maximized && this.eepromDataMultiWin.WindowState != FormWindowState.Minimized)
					{
						this.eepromDataMultiWin.Location = new Point(num3, num4);
					}
				}
				this.lastLoc = base.Location;
			}
		}

		// Token: 0x06000125 RID: 293 RVA: 0x0003292C File Offset: 0x0003192C
		private void pICkit2GoToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (PICkitFunctions.FamilyIsEEPROM() || PICkitFunctions.FamilyIsKeeloq() || PICkitFunctions.FamilyIsPIC32() || PICkitFunctions.FamilyIsMCP())
			{
				MessageBox.Show("PICkit 2 Programmer-To-Go does not support\n" + PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].FamilyName + " family devices.", "Unsupported Device Family");
				return;
			}
			if (PICkitFunctions.ActivePart == 0)
			{
				MessageBox.Show("No device selected!", "Programmer-To-Go");
				return;
			}
			if (!this.checkBoxEEMem.Checked && this.checkBoxEEMem.Enabled && PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgMemEraseScript == 0)
			{
				MessageBox.Show("PICkit 2 Programmer-To-Go does not support\npreserving EEPROM on devices requiring a\nRead/Restore operation.\n\nThe entire device must be programmed.", "Programmer-To-Go");
				return;
			}
			if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue > 16383U)
			{
				int bytesPerLocation = (int)PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BytesPerLocation;
				int num = (int)(PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigAddr / (uint)PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].ProgMemHexBytes);
				int configWords = (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigWords;
				int num2 = PICkitFunctions.DeviceBuffers.ProgramMemory.Length;
				if ((long)num < (long)((ulong)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgramMem) && configWords > 0)
				{
					num2 -= configWords + 1;
				}
				int progMemWrWords = (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgMemWrWords;
				int num3 = 256 / (progMemWrWords * bytesPerLocation);
				int num4 = num3 * progMemWrWords;
				num2 = PICkitFunctions.FindLastUsedInBuffer(PICkitFunctions.DeviceBuffers.ProgramMemory, PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue, num2 - 1);
				int num5 = (num2 + 1) / num4;
				if ((num2 + 1) % num4 > 0)
				{
					num5++;
				}
				num2 = num5 * num4;
				if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue > 65535U)
				{
					float num6 = 1.23f;
					if (PICkitFunctions.FamilyIsdsPIC30())
					{
						num6 = 1.26f;
					}
					num2 = (int)((float)num2 * num6);
				}
				else
				{
					float num7 = 1.22f;
					if (PICkitFunctions.FamilyIsPIC18J())
					{
						num7 = 1.17f;
					}
					num2 = (int)((float)num2 * num7);
				}
				num2 *= bytesPerLocation;
				int num8 = 131072;
				if (this.ptgMemory >= 1)
				{
					num8 = 131072 * (2 << (int)(this.ptgMemory - 1));
				}
				if (num2 > num8)
				{
					if (this.ptgMemory > 0)
					{
						MessageBox.Show("The data in the buffer is too large\nto be downloaded to PICkit 2.\n\nIt cannot be used with Programmer-To-Go.", "Programmer-To-Go");
						return;
					}
					MessageBox.Show("The data in the buffer is too large\nto be downloaded to PICkit 2.\n\nSee section 3.1 of the Programmer-To-Go\nUser Guide for information on increasing\nthe PICkit 2 memory.", "Programmer-To-Go");
					return;
				}
			}
			if (this.VppFirstToolStripMenuItem.Checked && this.VppFirstToolStripMenuItem.Enabled && (float)this.numUpDnVDD.Value < PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].VddErase && PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].DebugRowEraseScript == 0)
			{
				MessageBox.Show("VPP First Program Entry selected:\nPICkit 2 must power target.\n\nHowever, VDD box setpoint is below the\nminimum Erase voltage for this part.", "Programmer-To-Go");
				return;
			}
			this.timerButton.Enabled = false;
			DialogPK2Go dialogPK2Go = new DialogPK2Go();
			dialogPK2Go.VDDVolts = (float)this.numUpDnVDD.Value;
			if (this.multiWindow)
			{
				dialogPK2Go.dataSource = this.shortenHex(this.displayDataSource.Text);
			}
			else
			{
				dialogPK2Go.dataSource = this.displayDataSource.Text;
			}
			if (this.enableCodeProtectToolStripMenuItem.Checked || this.enableDataProtectStripMenuItem.Checked)
			{
				if (this.enableDataProtectStripMenuItem.Checked)
				{
					dialogPK2Go.dataProtect = true;
				}
				if (this.enableCodeProtectToolStripMenuItem.Checked)
				{
					dialogPK2Go.codeProtect = true;
				}
			}
			dialogPK2Go.writeProgMem = this.checkBoxProgMemEnabled.Checked;
			dialogPK2Go.writeEEPROM = this.checkBoxEEMem.Checked;
			if (this.verifyOnWriteToolStripMenuItem.Checked)
			{
				dialogPK2Go.verifyDevice = true;
			}
			if (this.VppFirstToolStripMenuItem.Enabled && this.VppFirstToolStripMenuItem.Checked)
			{
				dialogPK2Go.vppFirst = true;
			}
			if (this.fastProgrammingToolStripMenuItem.Enabled && !this.fastProgrammingToolStripMenuItem.Checked)
			{
				dialogPK2Go.fastProgramming = false;
			}
			dialogPK2Go.icspSpeedSlow = FormPICkit2.slowSpeedICSP;
			if (this.MCLRtoolStripMenuItem.Enabled && this.MCLRtoolStripMenuItem.Checked)
			{
				dialogPK2Go.holdMCLR = true;
			}
			dialogPK2Go.SetPTGMemory(this.ptgMemory);
			dialogPK2Go.PICkit2WriteGo = new DelegateWrite(this.ExtCallWrite);
			dialogPK2Go.OpenProgToGoGuide = new DelegateOpenProgToGoGuide(this.OpenProgToGoUserGuide);
			bool flag = this.usePE33;
			this.usePE33 = false;
			bool flag2 = this.usePE24;
			this.usePE24 = false;
			dialogPK2Go.ShowDialog();
			this.usePE33 = flag;
			this.usePE24 = flag2;
			if (!FormPICkit2.selfPoweredTarget)
			{
				PICkitFunctions.ForcePICkitPowered();
			}
			else
			{
				PICkitFunctions.ForcePICkitPowered();
			}
			if (this.writeOnPICkitButtonToolStripMenuItem.Checked)
			{
				this.buttonLast = true;
				this.timerButton.Enabled = true;
			}
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00032E2D File Offset: 0x00031E2D
		private void toolStripMenuItemManualSelect_Click(object sender, EventArgs e)
		{
			this.ManualAutoSelectToggle(true);
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00032E38 File Offset: 0x00031E38
		private void ManualAutoSelectToggle(bool updateGUI_OK)
		{
			if (this.toolStripMenuItemManualSelect.Checked)
			{
				for (int i = 0; i < PICkitFunctions.DevFile.Families.Length; i++)
				{
					PICkitFunctions.DevFile.Families[i].PartDetect = false;
				}
			}
			else
			{
				for (int j = 0; j < PICkitFunctions.DevFile.Families.Length; j++)
				{
					if (PICkitFunctions.DevFile.Families[j].DeviceIDMask > 0U)
					{
						PICkitFunctions.DevFile.Families[j].PartDetect = true;
					}
				}
				this.toolStripMenuItemLVPEnabled.Checked = false;
			}
			this.FamilySelectLogic(PICkitFunctions.GetActiveFamily(), updateGUI_OK);
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00032EDE File Offset: 0x00031EDE
		private void toolStripMenuItemProgToGo_Click(object sender, EventArgs e)
		{
			this.OpenProgToGoUserGuide();
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00032EE8 File Offset: 0x00031EE8
		public void OpenProgToGoUserGuide()
		{
			try
			{
				Process.Start(FormPICkit2.HomeDirectory + "\\Programmer-To-Go User Guide.pdf");
			}
			catch
			{
				MessageBox.Show("Unable to open Programmer-To-Go Guide.");
			}
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00032F2C File Offset: 0x00031F2C
		private void toolStripMenuItemLogicTool_Click(object sender, EventArgs e)
		{
			this.timerButton.Enabled = false;
			this.MCLRtoolStripMenuItem.Checked = false;
			this.checkBoxMCLR.Checked = false;
			this.logicWindow.SetVddBox(this.numUpDnVDD.Enabled, this.chkBoxVddOn.Checked);
			if (this.multiWindow)
			{
				this.programMemMultiWin.Hide();
				this.eepromDataMultiWin.Hide();
			}
			base.Hide();
			this.logicWindow.ShowDialog();
			base.Show();
			if (this.multiWindow)
			{
				if (this.multiWinPMemOpen)
				{
					this.programMemMultiWin.Show();
				}
				if (this.multiWinEEMemOpen && PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].EEMem > 0)
				{
					this.eepromDataMultiWin.Show();
				}
				base.Focus();
			}
			if (this.writeOnPICkitButtonToolStripMenuItem.Checked)
			{
				this.buttonLast = true;
				this.timerButton.Enabled = true;
			}
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00033028 File Offset: 0x00032028
		private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
		{
			if (e.ClickedItem.Text == "Select All")
			{
				if (this.dataGridProgramMemory.ContainsFocus)
				{
					this.dataGridProgramMemory.SelectAll();
					return;
				}
				if (this.dataGridViewEEPROM.ContainsFocus)
				{
					this.dataGridViewEEPROM.SelectAll();
					return;
				}
			}
			else if (e.ClickedItem.Text == "Copy")
			{
				if (this.dataGridProgramMemory.ContainsFocus)
				{
					Clipboard.SetDataObject(this.dataGridProgramMemory.GetClipboardContent());
					return;
				}
				if (this.dataGridViewEEPROM.ContainsFocus)
				{
					Clipboard.SetDataObject(this.dataGridViewEEPROM.GetClipboardContent());
				}
			}
		}

		// Token: 0x0600012C RID: 300 RVA: 0x000330D0 File Offset: 0x000320D0
		private void dataGridProgramMemory_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				this.dataGridProgramMemory.Focus();
			}
		}

		// Token: 0x0600012D RID: 301 RVA: 0x000330EB File Offset: 0x000320EB
		private void dataGridViewEEPROM_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				this.dataGridViewEEPROM.Focus();
			}
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00033108 File Offset: 0x00032108
		private void toolStripMenuItemLogicToolUG_Click(object sender, EventArgs e)
		{
			try
			{
				Process.Start(FormPICkit2.HomeDirectory + "\\Logic Tool User Guide.pdf");
			}
			catch
			{
				MessageBox.Show("Unable to open Logic Tool User Guide.");
			}
		}

		// Token: 0x0600012F RID: 303 RVA: 0x0003314C File Offset: 0x0003214C
		private void calAutoRegenerateToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (this.setOSCCALToolStripMenuItem.Enabled)
			{
				if (MessageBox.Show("Regenerating the OSCCAL value\nwill completely erase this\npart.\n\nAre you sure you wish to\ncontinue?", "Regenerate OSCCAL", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
				{
					return;
				}
				if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue == 4095U)
				{
					short num = 0;
					float num2 = 0f;
					this.verifyOSCCALValue = false;
					for (int i = 0; i < 5; i++)
					{
						float num3 = (1f - 400f / num2) / 0.0057f + 0.5f;
						num += (short)num3;
						if (num < -64 || num > 63)
						{
							this.conditionalVDDOff();
							this.eraseDeviceAll(false, new uint[0]);
							this.verifyOSCCALValue = true;
							this.updateGUI(true);
							MessageBox.Show("Regenerating OSCCAL Failed!\n\nCalibration out of range.", "Regenerate OSCCAL");
							return;
						}
						PICkitFunctions.ResetBuffers();
						PICkitFunctions.DeviceBuffers.ProgramMemory[0] = (Constants.BASELINE_CAL[0] | (uint)((int)num << 1 & 255));
						PICkitFunctions.DeviceBuffers.ConfigWords[0] = (uint)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[6];
						for (int j = 1; j < Constants.BASELINE_CAL.Length; j++)
						{
							PICkitFunctions.DeviceBuffers.ProgramMemory[j] = Constants.BASELINE_CAL[j];
						}
						if (!this.deviceWrite())
						{
							PICkitFunctions.ResetBuffers();
							this.verifyOSCCALValue = true;
							this.updateGUI(true);
							MessageBox.Show("Regenerating OSCCAL Failed!\n\nUnable to program part.", "Regenerate OSCCAL");
							return;
						}
						PICkitFunctions.VddOn();
						for (int k = 0; k < 3; k++)
						{
							num2 = PICkitFunctions.MeasurePGDPulse();
							if (num2 < 695f && num2 > 10f)
							{
								break;
							}
							if (k == 2)
							{
								this.conditionalVDDOff();
								this.eraseDeviceAll(false, new uint[0]);
								this.verifyOSCCALValue = true;
								MessageBox.Show("Regenerating OSCCAL Failed!\n\nUnable to connect to\ncalibration executive.", "Regenerate OSCCAL");
								this.updateGUI(true);
								return;
							}
						}
						this.conditionalVDDOff();
						float num4 = 404f;
						if (i == 4)
						{
							num4 = 406f;
						}
						float num5 = 396f;
						if (i == 4)
						{
							num4 = 394f;
						}
						if (num2 > num5 && num2 < num4)
						{
							PICkitFunctions.DeviceBuffers.OSCCAL = PICkitFunctions.DeviceBuffers.ProgramMemory[0];
							this.eraseDeviceAll(true, new uint[0]);
							this.verifyOSCCALValue = true;
							MessageBox.Show("Success!\n\nOSSCAL Regenerated and\nwritten to device.", "Regenerate OSCCAL");
							this.updateGUI(true);
							return;
						}
					}
				}
				else
				{
					short num6 = 32;
					float num7 = 0f;
					this.verifyOSCCALValue = false;
					for (int l = 0; l < 5; l++)
					{
						float num8 = (1f - 400f / num7) / 0.007f + 0.5f;
						num6 += (short)num8;
						if (num6 < 0 || num6 > 63)
						{
							this.conditionalVDDOff();
							this.eraseDeviceAll(false, new uint[0]);
							this.verifyOSCCALValue = true;
							this.updateGUI(true);
							MessageBox.Show("Regenerating OSCCAL Failed!\n\nCalibration out of range.", "Regenerate OSCCAL");
							return;
						}
						PICkitFunctions.ResetBuffers();
						PICkitFunctions.DeviceBuffers.ProgramMemory[0] = (Constants.MR16F676FAM_CAL[0] | (uint)((int)num6 << 2 & 255));
						PICkitFunctions.DeviceBuffers.ConfigWords[0] = (uint)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[6];
						for (int m = 1; m < Constants.MR16F676FAM_CAL.Length; m++)
						{
							PICkitFunctions.DeviceBuffers.ProgramMemory[m] = Constants.MR16F676FAM_CAL[m];
						}
						if (!this.deviceWrite())
						{
							PICkitFunctions.ResetBuffers();
							this.verifyOSCCALValue = true;
							this.updateGUI(true);
							MessageBox.Show("Regenerating OSCCAL Failed!\n\nUnable to program part.", "Regenerate OSCCAL");
							return;
						}
						PICkitFunctions.VddOn();
						for (int n = 0; n < 3; n++)
						{
							num7 = PICkitFunctions.MeasurePGDPulse();
							if (num7 < 695f && num7 > 10f)
							{
								break;
							}
							if (n == 2)
							{
								this.conditionalVDDOff();
								this.eraseDeviceAll(false, new uint[0]);
								this.verifyOSCCALValue = true;
								MessageBox.Show("Regenerating OSCCAL Failed!\n\nUnable to connect to\ncalibration executive.", "Regenerate OSCCAL");
								this.updateGUI(true);
								return;
							}
						}
						this.conditionalVDDOff();
						float num9 = 404f;
						if (l == 4)
						{
							num9 = 406f;
						}
						float num10 = 396f;
						if (l == 4)
						{
							num9 = 394f;
						}
						if (num7 > num10 && num7 < num9)
						{
							PICkitFunctions.DeviceBuffers.OSCCAL = ((uint)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[7] | (PICkitFunctions.DeviceBuffers.ProgramMemory[0] & 255U));
							this.eraseDeviceAll(true, new uint[0]);
							this.verifyOSCCALValue = true;
							MessageBox.Show("Success!\n\nOSSCAL Regenerated and\nwritten to device.", "Regenerate OSCCAL");
							this.updateGUI(true);
							return;
						}
					}
				}
				this.eraseDeviceAll(false, new uint[0]);
				this.verifyOSCCALValue = true;
				this.updateGUI(true);
				MessageBox.Show("Regenerating OSCCAL Failed!\n\nUnable to calibrate.", "Regenerate OSCCAL");
			}
		}

		// Token: 0x06000130 RID: 304 RVA: 0x000335F4 File Offset: 0x000325F4
		private void timerInitalUpdate_Tick(object sender, EventArgs e)
		{
			this.timerInitalUpdate.Enabled = false;
			this.toolStripMenuItemShowProgramMemory.Checked = this.saveMultWinPMemOpen;
			this.multiWinPMemOpen = this.saveMultWinPMemOpen;
			if (this.multiWinPMemOpen)
			{
				this.programMemMultiWin.Show();
			}
			this.toolStripMenuItemShowEEPROMData.Checked = this.saveMultiWinEEMemOpen;
			this.multiWinEEMemOpen = this.saveMultiWinEEMemOpen;
			if (this.multiWinEEMemOpen && PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].EEMem > 0)
			{
				this.eepromDataMultiWin.Show();
			}
			base.Focus();
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00033690 File Offset: 0x00032690
		private void mainWindowAlwaysInFrontToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (this.mainWindowAlwaysInFrontToolStripMenuItem.Checked)
			{
				this.mainWinOwnsMem = true;
				base.AddOwnedForm(this.programMemMultiWin);
				base.AddOwnedForm(this.eepromDataMultiWin);
				return;
			}
			this.mainWinOwnsMem = false;
			base.RemoveOwnedForm(this.programMemMultiWin);
			base.RemoveOwnedForm(this.eepromDataMultiWin);
		}

		// Token: 0x06000132 RID: 306 RVA: 0x000336E9 File Offset: 0x000326E9
		private bool useProgExec33()
		{
			return (PICkitFunctions.FamilyIsdsPIC33F() || PICkitFunctions.FamilyIsPIC24H()) && PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgramMem >= 4096U && this.usePE33;
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00033721 File Offset: 0x00032721
		private bool useProgExec24F()
		{
			return PICkitFunctions.FamilyIsPIC24FJ() && this.usePE24;
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00033732 File Offset: 0x00032732
		private void updateAlertSoundCheck()
		{
			this.toolStripMenuItemSounds.Checked = (FormPICkit2.PlayErrorWav || FormPICkit2.PlaySuccessWav || FormPICkit2.PlayWarningWav);
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00033758 File Offset: 0x00032758
		private void toolStripMenuItemSounds_Click(object sender, EventArgs e)
		{
			dialogSounds dialogSounds = new dialogSounds();
			dialogSounds.ShowDialog();
			this.updateAlertSoundCheck();
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00033778 File Offset: 0x00032778
		private void as0BitValueToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.as0BitValueToolStripMenuItem.Checked = true;
			this.as1BitValueToolStripMenuItem.Checked = false;
			this.asReadOrImportedToolStripMenuItem.Checked = false;
			this.updateGUI(true);
		}

		// Token: 0x06000137 RID: 311 RVA: 0x000337A5 File Offset: 0x000327A5
		private void as1BitValueToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.as0BitValueToolStripMenuItem.Checked = false;
			this.as1BitValueToolStripMenuItem.Checked = true;
			this.asReadOrImportedToolStripMenuItem.Checked = false;
			this.updateGUI(true);
		}

		// Token: 0x06000138 RID: 312 RVA: 0x000337D2 File Offset: 0x000327D2
		private void asReadOrImportedToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.as0BitValueToolStripMenuItem.Checked = false;
			this.as1BitValueToolStripMenuItem.Checked = false;
			this.asReadOrImportedToolStripMenuItem.Checked = true;
			this.updateGUI(true);
		}

		private void labelConfig_Click(object sender, EventArgs e)
		{
			DialogConfigEdit dialogConfigEdit = new DialogConfigEdit();
			dialogConfigEdit.ScalefactW = ScalefactW;
			dialogConfigEdit.ScalefactH = ScalefactH;
			if (as0BitValueToolStripMenuItem.Checked)
			{
				dialogConfigEdit.SetDisplayMask(0);
			}
			else if (as1BitValueToolStripMenuItem.Checked)
			{
				dialogConfigEdit.SetDisplayMask(1);
			}
			else
			{
				dialogConfigEdit.SetDisplayMask(2);
			}
			dialogConfigEdit.ShowDialog();
			if (ConfigsEdited)
			{
				displayDataSource.Text = "Edited.";
				checkImportFile = false;
				ConfigsEdited = false;
			}
			updateGUI(true);
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00033890 File Offset: 0x00032890
		private void toolStripMenuItemLVPEnabled_CheckedChanged(object sender, EventArgs e)
		{
			if (this.toolStripMenuItemLVPEnabled.Checked)
			{
				if (!this.toolStripMenuItemManualSelect.Checked)
				{
					string text = PICkitFunctions.DevFile.Scripts[(int)(PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].LVPScript - 1)].ScriptName;
					text = text.Substring(text.Length - 2);
					if (text == "HV")
					{
						MessageBox.Show("High Voltage Program entry may not be used\nwhen auto-detecting parts.\n\nSelect 'Programmer > Manual Device Select'\nto allow HVP to be used.", "Use HVP Program Entry");
					}
					else
					{
						MessageBox.Show("Low Voltage Program entry may not be used\nwhen auto-detecting parts.\n\nSelect 'Programmer > Manual Device Select'\nto allow LVP to be used.", "Use LVP Program Entry");
					}
					this.toolStripMenuItemLVPEnabled.Checked = false;
				}
				else if (this.VppFirstToolStripMenuItem.Checked)
				{
					MessageBox.Show("'Use VPP First Program Entry' is enabled.\n\nLVP Program Entry may not be used while\nthat option is enabled.", "Use LVP Program Entry");
					this.toolStripMenuItemLVPEnabled.Checked = false;
				}
				else
				{
					PICkitFunctions.SetLVPProgramEntry();
					string text2 = PICkitFunctions.DevFile.Scripts[(int)(PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].LVPScript - 1)].ScriptName;
					if (text2.Substring(text2.Length - 2) == "HV")
					{
						this.displayStatusWindow.Text = "High Voltage Programming (HVP) entry set.";
					}
					else
					{
						this.displayStatusWindow.Text = "Low Voltage Programming (LVP) entry set.";
					}
					text2 = text2.Substring(text2.Length - 3);
					if (text2 == "PGM")
					{
						Label label = this.displayStatusWindow;
						label.Text += "\nConnect PICkit 2 AUX to device PGM pin.";
					}
					PICkitFunctions.PrepNewPart(false);
				}
			}
			else
			{
				PICkitFunctions.ClearLVPProgramEntry();
				if (!this.VppFirstToolStripMenuItem.Checked)
				{
					this.displayStatusWindow.Text = "Normal programming mode entry.";
				}
				if (this.toolStripMenuItemManualSelect.Checked)
				{
					PICkitFunctions.PrepNewPart(false);
				}
			}
			this.updateGUI(false);
		}

		// Token: 0x040002C4 RID: 708
		public static bool ShowWriteEraseVDDDialog = true;

		// Token: 0x040002C5 RID: 709
		public static bool ContinueWriteErase = false;

		// Token: 0x040002C6 RID: 710
		public static bool setOSCCALValue = false;

		// Token: 0x040002C7 RID: 711
		public static bool ConfigsEdited = false;

		// Token: 0x040002C8 RID: 712
		public static bool TestMemoryOpen = false;

		// Token: 0x040002C9 RID: 713
		public static bool TestMemoryEnabled = false;

		// Token: 0x040002CA RID: 714
		public static int TestMemoryWords = 64;

		// Token: 0x040002CB RID: 715
		public static ushort pk2number = 0;

		// Token: 0x040002CC RID: 716
		public static bool TestMemoryImportExport = false;

		// Token: 0x040002CD RID: 717
		public static FormTestMemory formTestMem;

		// Token: 0x040002CE RID: 718
		public static string DeviceFileName = "PK2DeviceFile.dat";

		// Token: 0x040002CF RID: 719
		public static float ScalefactW = 1f;

		// Token: 0x040002D0 RID: 720
		public static float ScalefactH = 1f;

		// Token: 0x040002D1 RID: 721
		public static string HomeDirectory;

		// Token: 0x040002D2 RID: 722
		public static byte slowSpeedICSP = 4;

		// Token: 0x040002D3 RID: 723
		public static bool PlaySuccessWav = false;

		// Token: 0x040002D4 RID: 724
		public static string SuccessWavFile = "\\Sounds\\success.wav";

		// Token: 0x040002D5 RID: 725
		public static bool PlayWarningWav = false;

		// Token: 0x040002D6 RID: 726
		public static string WarningWavFile = "\\Sounds\\warning.wav";

		// Token: 0x040002D7 RID: 727
		public static bool PlayErrorWav = false;

		// Token: 0x040002D8 RID: 728
		public static string ErrorWavFile = "\\Sounds\\error.wav";

		// Token: 0x040002D9 RID: 729
		private static bool selfPoweredTarget;

		// Token: 0x040002DA RID: 730
		private static Constants.StatusColor statusWindowColor = Constants.StatusColor.normal;

		// Token: 0x040002DB RID: 731
		private DialogVDDErase dialogVddErase = new DialogVDDErase();

		// Token: 0x040002DC RID: 732
		private DialogUserIDs dialogIDMemory;

		// Token: 0x040002DD RID: 733
		private Constants.VddTargetSelect VddTargetSave;

		// Token: 0x040002DE RID: 734
		private DialogUART uartWindow = new DialogUART();

		// Token: 0x040002DF RID: 735
		private DialogLogic logicWindow = new DialogLogic();

		// Token: 0x040002E0 RID: 736
		private FormMultiWinProgMem programMemMultiWin = new FormMultiWinProgMem();

		// Token: 0x040002E1 RID: 737
		private FormMultiWinEEData eepromDataMultiWin = new FormMultiWinEEData();

		// Token: 0x040002E2 RID: 738
		private Point lastLoc = new Point(0, 0);

		// Token: 0x040002E3 RID: 739
		private bool buttonLast = true;

		// Token: 0x040002E4 RID: 740
		private bool checkImportFile;

		// Token: 0x040002E5 RID: 741
		private bool oldFirmware;

		// Token: 0x040002E6 RID: 742
		private bool bootLoad;

		// Token: 0x040002E7 RID: 743
		private bool importGo;

		// Token: 0x040002E8 RID: 744
		private bool allowDataEdits = true;

		// Token: 0x040002E9 RID: 745
		private bool progMemJustEdited;

		// Token: 0x040002EA RID: 746
		private bool eeMemJustEdited;

		// Token: 0x040002EB RID: 747
		private bool testConnected;

		// Token: 0x040002EC RID: 748
		private bool searchOnStartup = true;

		// Token: 0x040002ED RID: 749
		private bool autoDetectInINI = true;

		// Token: 0x040002EE RID: 750
		private bool selectDeviceFile;

		// Token: 0x040002EF RID: 751
		private bool viewChanged;

		// Token: 0x040002F0 RID: 752
		private bool multiWindow;

		// Token: 0x040002F1 RID: 753
		private bool multiWinPMemOpen = true;

		// Token: 0x040002F2 RID: 754
		private bool multiWinEEMemOpen = true;

		// Token: 0x040002F3 RID: 755
		private bool saveMultWinPMemOpen = true;

		// Token: 0x040002F4 RID: 756
		private bool saveMultiWinEEMemOpen = true;

		// Token: 0x040002F5 RID: 757
		private bool verifyOSCCALValue = true;

		// Token: 0x040002F6 RID: 758
		private bool mainWinOwnsMem = true;

		// Token: 0x040002F7 RID: 759
		private bool usePE33 = true;

		// Token: 0x040002F8 RID: 760
		private bool usePE24 = true;

		// Token: 0x040002F9 RID: 761
		private bool useLVP;

		// Token: 0x040002FA RID: 762
		private bool deviceVerification = true;

		// Token: 0x040002FB RID: 763
		private byte ptgMemory;

		// Token: 0x040002FC RID: 764
		private string lastFamily = "Midrange";

		// Token: 0x040002FD RID: 765
		private string hex1 = "";

		// Token: 0x040002FE RID: 766
		private string hex2 = "";

		// Token: 0x040002FF RID: 767
		private string hex3 = "";

		// Token: 0x04000300 RID: 768
		private string hex4 = "";

		// Token: 0x04000301 RID: 769
		private SoundPlayer wavPlayer = new SoundPlayer();

		// Token: 0x02000011 RID: 17
		public struct FLASHWINFO
		{
			// Token: 0x04000302 RID: 770
			public ushort cbSize;

			// Token: 0x04000303 RID: 771
			public IntPtr hwnd;

			// Token: 0x04000304 RID: 772
			public uint dwFlags;

			// Token: 0x04000305 RID: 773
			public ushort uCount;

			// Token: 0x04000306 RID: 774
			public uint dwTimeout;
		}
	}
}
