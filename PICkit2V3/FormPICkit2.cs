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
			float num = LoadINI();
			if (mainWinOwnsMem)
			{
				AddOwnedForm(programMemMultiWin);
				AddOwnedForm(eepromDataMultiWin);
			}
			InitializeGUI();
			if (!LoadDeviceFile())
				return;
			if (toolStripMenuItemManualSelect.Checked)
				ManualAutoSelectToggle(false);
			BuildDeviceMenu();
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
			if (!DetectPICkit2(true, true))
			{
				if (bootLoad)
					return;
				if (oldFirmware)
				{
					testMemoryOpen = false;
					timerDLFW.Enabled = true;
					return;
				}
				Thread.Sleep(3000);
				if (!DetectPICkit2(true, true))
					return;
			}
			PartialEnableGUIControls();
			PICkitFunctions.ExitUARTMode();
			PICkitFunctions.VddOff();
			PICkitFunctions.SetVddVoltage(3.3f, 0.85f);
			if (autoDetectToolStripMenuItem.Checked)
				LookForPoweredTarget(false);
			if (searchOnStartup && PICkitFunctions.DetectDevice(16777215, true, chkBoxVddOn.Checked))
			{
				SetGUIVoltageLimits(true);
				PICkitFunctions.SetVddVoltage((float)numUpDnVdd.Value, 0.85f);
				displayStatusWindow.Text += "\nPIC Device Found.";
				FullEnableGUIControls();
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
							BuildDeviceSelectDropDown(i);
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
				SetGUIVoltageLimits(true);
			}
			if (num != 0f && PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].FamilyName == lastFamily && !selfPoweredTarget)
			{
				if (num > (float)numUpDnVdd.Maximum)
					num = (float)numUpDnVdd.Maximum;
				if (num < (float)numUpDnVdd.Minimum)
					num = (float)numUpDnVdd.Minimum;
				numUpDnVdd.Value = (decimal)num;
				PICkitFunctions.SetVddVoltage((float)numUpDnVdd.Value, 0.85f);
			}
			CheckForPowerErrors();
			if (testMemoryEnabled)
			{
				toolStripMenuItemTestMemory.Visible = true;
				if (testMemoryOpen)
					OpenTestMemory();
			}
			if (!PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].PartDetect)
				DisableGUIControls();
			if (multiWindow)
			{
				saveMultWinPMemOpen = multiWinPMemOpen;
				toolStripMenuItemShowProgramMemory.Checked = false;
				multiWinPMemOpen = false;
				saveMultiWinEEMemOpen = multiWinEEMemOpen;
				toolStripMenuItemShowEEPROMData.Checked = false;
				multiWinEEMemOpen = false;
			}
			UpdateGUI(true);
			if (multiWindow)
				timerInitalUpdate.Enabled = true;

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
			UpdateGUI(true);
		}

		public bool ExtCallVerify()
		{
			return DeviceVerify(false, 0, false);
		}

		public bool ExtCallWrite(bool verify)
		{
			bool isChecked = verifyOnWriteToolStripMenuItem.Checked;
			if (verify)
				verifyOnWriteToolStripMenuItem.Checked = true;
			else
				verifyOnWriteToolStripMenuItem.Checked = false;
			bool result = DeviceWrite();
			verifyOnWriteToolStripMenuItem.Checked = isChecked;
			return result;
		}

		public void ExtCallRead()
		{
			DeviceRead();
		}

		public void ExtCallErase(bool writeOSCCAL)
		{
			EraseDeviceAll(writeOSCCAL, new uint[0]);
		}

		public void ExtCallCalEraseWrite(uint[] calwords)
		{
			EraseDeviceAll(false, calwords);
		}

		public bool ExtCallBlank()
		{
			return BlankCheckDevice();
		}

		public void MultiWinProgMemClosed()
		{
			multiWinPMemOpen = false;
			toolStripMenuItemShowProgramMemory.Checked = false;
		}

		public void MultiWinEEMemClosed()
		{
			multiWinEEMemOpen = false;
			toolStripMenuItemShowEEPROMData.Checked = false;
		}

		public void ShowMemEdited()
		{
			displayDataSource.Text = "Edited.";
			checkImportFile = false;
		}

		public void StatusWinWr(string dispText)
		{
			displayStatusWindow.Text = dispText;
			Update();
		}

		public void ResetStatusBar(int maxValue)
		{
			progressBar1.Value = 0;
			progressBar1.Maximum = maxValue;
			Update();
		}

		public void StepStatusBar()
		{
			progressBar1.PerformStep();
		}

		public void SetVddState(bool force, bool forceState)
		{
			VddControl(force, forceState);
			uartWindow.SetVddBox(numUpDnVdd.Enabled, chkBoxVddOn.Checked);
			logicWindow.SetVddBox(numUpDnVdd.Enabled, chkBoxVddOn.Checked);
		}

		private bool CheckForPowerErrors()
		{
			Thread.Sleep(100);
			Constants.PICkit2PWR pickit2PWR = PICkitFunctions.PowerStatus();
			if (pickit2PWR == Constants.PICkit2PWR.vdderror)
			{
				if (!timerAutoImportWrite.Enabled)
					MessageBox.Show("PICkit 2 VDD voltage level error.\nCheck target & retry operation.", "PICkit 2 Error");
			}
			else if (pickit2PWR == Constants.PICkit2PWR.vpperror)
			{
				if (!timerAutoImportWrite.Enabled)
					MessageBox.Show("PICkit 2 VPP voltage level error.\nCheck target & retry operation.", "PICkit 2 Error");
			}
			else if (pickit2PWR == Constants.PICkit2PWR.vddvpperrors)
			{
				if (!timerAutoImportWrite.Enabled)
					MessageBox.Show("PICkit 2 VDD and VPP voltage level errors.\nCheck target & retry operation.", "PICkit 2 Error");
			}
			else
			{
				if (pickit2PWR == Constants.PICkit2PWR.vdd_on)
				{
					chkBoxVddOn.Checked = true;
					return false;
				}
				if (pickit2PWR == Constants.PICkit2PWR.vdd_off)
				{
					chkBoxVddOn.Checked = false;
					return false;
				}
			}
			chkBoxVddOn.Checked = false;
			return true;
		}

		private bool LookForPoweredTarget(bool showMessageBox)
		{
			float num = 0f;
			float num2 = 0f;
			if (fastProgrammingToolStripMenuItem.Checked)
				PICkitFunctions.SetProgrammingSpeed(0);
			else
				PICkitFunctions.SetProgrammingSpeed(slowSpeedICSP);
			if (autoDetectToolStripMenuItem.Checked)
			{
				if (PICkitFunctions.CheckTargetPower(ref num, ref num2) == Constants.PICkit2PWR.selfpowered)
				{
					chkBoxVddOn.Checked = false;
					if (!selfPoweredTarget)
					{
						selfPoweredTarget = true;
						chkBoxVddOn.Enabled = true;
						chkBoxVddOn.Text = "Check";
						numUpDnVdd.Enabled = false;
						groupBoxVdd.Text = "VDD Target";
						if (showMessageBox)
							MessageBox.Show("Powered target detected.\n VDD source set to target.", "Target VDD");
					}
					numUpDnVdd.Maximum = (decimal)num;
					numUpDnVdd.Value = (decimal)num;
					numUpDnVdd.Update();
					return true;
				}
				if (selfPoweredTarget)
				{
					selfPoweredTarget = false;
					chkBoxVddOn.Enabled = true;
					chkBoxVddOn.Text = "On";
					numUpDnVdd.Enabled = true;
					SetGUIVoltageLimits(true);
					groupBoxVdd.Text = "VDD PICkit 2";
					if (showMessageBox)
						MessageBox.Show("Unpowered target detected.\n VDD source set to PICkit 2.", "Target VDD");
				}
				return false;
			}
			else
			{
				if (forcePICkit2ToolStripMenuItem.Checked)
				{
					if (selfPoweredTarget)
					{
						PICkitFunctions.ForcePICkitPowered();
						selfPoweredTarget = false;
						chkBoxVddOn.Enabled = true;
						chkBoxVddOn.Text = "On";
						numUpDnVdd.Enabled = true;
						SetGUIVoltageLimits(true);
						groupBoxVdd.Text = "VDD PICkit 2";
					}
					return false;
				}
				PICkitFunctions.CheckTargetPower(ref num, ref num2);
				PICkitFunctions.ForceTargetPowered();
				chkBoxVddOn.Checked = false;
				if (!selfPoweredTarget)
				{
					selfPoweredTarget = true;
					chkBoxVddOn.Enabled = true;
					chkBoxVddOn.Text = "Check";
					numUpDnVdd.Enabled = false;
					groupBoxVdd.Text = "VDD Target";
				}
				numUpDnVdd.Maximum = (decimal)num;
				numUpDnVdd.Value = (decimal)num;
				numUpDnVdd.Update();
				return true;
			}
		}

		private void SetGUIVoltageLimits(bool setValue)
		{
			if (numUpDnVdd.Enabled)
			{
				numUpDnVdd.Maximum = (decimal)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].VddMax;
				numUpDnVdd.Minimum = (decimal)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].VddMin;
				if (PICkitFunctions.ActivePart != 0)
				{
					PICkitFunctions.DevFile.PartsList[0].VddMax = PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].VddMax;
					PICkitFunctions.DevFile.PartsList[0].VddMin = PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].VddMin;
				}
				if (setValue)
				{
					if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].VddMax <= 4.0 && PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].VddMax >= 3.3)
					{
						numUpDnVdd.Value = 3.3m;
						return;
					}
					if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].VddMax == 5.0)
					{
						numUpDnVdd.Value = 5.0m;
						return;
					}
					numUpDnVdd.Value = (decimal)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].VddMax;
				}
			}
		}

		private void InitializeGUI()
		{
			scalefactW = dataGridProgramMemory.Size.Width / 502f;
			scalefactH = dataGridProgramMemory.Size.Height / 208f;
			dataGridConfigWords.BackgroundColor = SystemColors.Control;
			dataGridConfigWords.ColumnCount = 4;
			dataGridConfigWords.RowCount = 2;
			dataGridConfigWords.DefaultCellStyle.BackColor = SystemColors.Control;
			dataGridConfigWords[0, 0].Selected = true;
			dataGridConfigWords[0, 0].Selected = false;
			int width = (int)(40f * scalefactW);
			for (int i = 0; i < 4; i++)
				dataGridConfigWords.Columns[i].Width = width;
			dataGridConfigWords.Rows[0].Height = (int)(17 * scalefactH);
			dataGridConfigWords.Rows[1].Height = (int)(17 * scalefactH);
			progressBar1.Step = 1;
			if (comboBoxProgMemView.SelectedIndex < 0)
				comboBoxProgMemView.SelectedIndex = 0;
			dataGridProgramMemory.DefaultCellStyle.Font = new Font("Courier New", 9);
			dataGridProgramMemory.ColumnCount = 9;
			dataGridProgramMemory.RowCount = 512;
			dataGridProgramMemory[0, 0].Selected = true;
			dataGridProgramMemory[0, 0].Selected = false;
			width = (int)(59 * scalefactW);
			dataGridProgramMemory.Columns[0].Width = width;
			dataGridProgramMemory.Columns[0].ReadOnly = true;
			width = (int)(53 * scalefactW);
			for (int j = 1; j < 9; j++)
				dataGridProgramMemory.Columns[j].Width = width;
			for (int k = 0; k < 32; k++)
			{
				dataGridProgramMemory[0, k].Style.BackColor = SystemColors.ControlLight;
				dataGridProgramMemory[0, k].Value = string.Format("{0:X5}", k * 8);
			}
			if (comboBoxEE.SelectedIndex < 0)
				comboBoxEE.SelectedIndex = 0;
			dataGridViewEEPROM.DefaultCellStyle.Font = new Font("Courier New", 9);
			dataGridViewEEPROM.ColumnCount = 9;
			dataGridViewEEPROM.RowCount = 16;
			width = (int)(40 * scalefactW);
			dataGridViewEEPROM.Columns[0].Width = width;
			dataGridViewEEPROM.Columns[0].ReadOnly = true;
			width = (int)(41 * scalefactW);
			for (int l = 1; l < 9; l++)
				dataGridViewEEPROM.Columns[l].Width = width;
			dataGridViewEEPROM[0, 0].Selected = true;
			dataGridViewEEPROM[0, 0].Selected = false;
			dataGridViewEEPROM.Visible = false;
			UpdateAlertSoundCheck();
			programMemMultiWin.TellMainFormProgMemClosed = new DelegateMultiProgMemClosed(MultiWinProgMemClosed);
			programMemMultiWin.TellMainFormProgMemEdited = new DelegateMemEdited(ShowMemEdited);
			programMemMultiWin.TellMainFormUpdateGUI = new DelegateUpdateGUI(ExtCallUpdateGUI);
			eepromDataMultiWin.TellMainFormEEMemClosed = new DelegateMultiEEMemClosed(MultiWinEEMemClosed);
			eepromDataMultiWin.TellMainFormProgMemEdited = new DelegateMemEdited(ShowMemEdited);
			eepromDataMultiWin.TellMainFormUpdateGUI = new DelegateUpdateGUI(ExtCallUpdateGUI);
		}

		private bool LoadDeviceFile()
		{
			if (selectDeviceFile)
			{
				DialogDevFile dialogDevFile = new DialogDevFile();
				dialogDevFile.ShowDialog();
			}
			if (!PICkitFunctions.ReadDeviceFile(deviceFileName))
			{
				displayStatusWindow.Text = "Device file " + deviceFileName + " not found.\nMust be in same directory as executable.";
				checkCommunicationToolStripMenuItem.Enabled = false;
				return false;
			}
			if (PICkitFunctions.DevFile.Info.Compatibility < 0)
			{
				displayStatusWindow.Text = "Older device file is not compatible with this PICkit 2\nversion.  Visit www.microchip.com for updates.";
				checkCommunicationToolStripMenuItem.Enabled = false;
				return false;
			}
			if (PICkitFunctions.DevFile.Info.Compatibility > 6)
			{
				displayStatusWindow.Text = "The device file requires a newer version of PICkit 2.\nVisit www.microchip.com for updates.";
				checkCommunicationToolStripMenuItem.Enabled = false;
				return false;
			}
			return true;
		}

		private bool DetectPICkit2(bool showFound, bool detectMult)
		{
			Constants.PICkit2USB pickit2USB;
			if (detectMult)
			{
				pk2number = 0;
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
			pickit2USB = PICkitFunctions.DetectPICkit2Device(pk2number, true);
			if (pickit2USB == Constants.PICkit2USB.found)
			{
				downloadPICkit2FirmwareToolStripMenuItem.Enabled = true;
				chkBoxVddOn.Enabled = true;
				if (!selfPoweredTarget)
					numUpDnVdd.Enabled = true;
				deviceToolStripMenuItem.Enabled = true;
				if (showFound)
				{
					string serialUnitID = PICkitFunctions.GetSerialUnitID();
					if (serialUnitID[0] == '-')
					{
						displayStatusWindow.Text = "PICkit 2 found and connected.";
						Text = "PICkit 2 Programmer";
					}
					else
					{
						displayStatusWindow.Text = "PICkit 2 connected.  ID = " + serialUnitID;
						Text = "PICkit 2 Programmer - " + serialUnitID;
					}
				}
				return true;
			}
			downloadPICkit2FirmwareToolStripMenuItem.Enabled = false;
			chkBoxVddOn.Enabled = false;
			numUpDnVdd.Enabled = false;
			deviceToolStripMenuItem.Enabled = false;
			DisableGUIControls();
			if (pickit2USB == Constants.PICkit2USB.firmwareInvalid)
			{
				displayStatusWindow.BackColor = Color.Yellow;
				downloadPICkit2FirmwareToolStripMenuItem.Enabled = true;
				displayStatusWindow.Text = "The PICkit 2 OS v" + PICkitFunctions.FirmwareVersion + " must be updated.\nUse the Tools menu to download a new OS.";
				oldFirmware = true;
				return false;
			}
			if (pickit2USB == Constants.PICkit2USB.bootloader)
			{
				displayStatusWindow.BackColor = Color.Yellow;
				downloadPICkit2FirmwareToolStripMenuItem.Enabled = true;
				displayStatusWindow.Text = "The PICkit 2 has no Operating System.\nUse the Tools menu to download an OS.";
				bootLoad = true;
				return false;
			}
			displayStatusWindow.BackColor = Color.Salmon;
			displayStatusWindow.Text = "PICkit 2 not found.  Check USB connections and \nuse Tools->Check Communication to retry.";
			return false;
		}

		private void DisableGUIControls()
		{
			importFileToolStripMenuItem.Enabled = false;
			exportFileToolStripMenuItem.Enabled = false;
			readDeviceToolStripMenuItem.Enabled = false;
			writeDeviceToolStripMenuItem.Enabled = false;
			verifyToolStripMenuItem.Enabled = false;
			eraseToolStripMenuItem.Enabled = false;
			blankCheckToolStripMenuItem.Enabled = false;
			writeOnPICkitButtonToolStripMenuItem.Enabled = false;
			picKit2GoToolStripMenuItem.Enabled = false;
			setOSCCALToolStripMenuItem.Enabled = false;
			buttonRead.Enabled = false;
			buttonWrite.Enabled = false;
			buttonVerify.Enabled = false;
			buttonErase.Enabled = false;
			buttonBlankCheck.Enabled = false;
			checkBoxProgMemEnabled.Enabled = false;
			checkBoxProgMemEnabledAlt.Enabled = false;
			comboBoxProgMemView.Enabled = false;
			dataGridProgramMemory.ForeColor = SystemColors.GrayText;
			dataGridProgramMemory.Enabled = false;
			dataGridViewEEPROM.Visible = false;
			comboBoxEE.Enabled = false;
			checkBoxEEMem.Enabled = false;
			checkBoxEEDATAMemoryEnabledAlt.Enabled = false;
			buttonExportHex.Enabled = false;
			checkBoxAutoImportWrite.Enabled = false;
			troubleshhotToolStripMenuItem.Enabled = false;
			calibrateToolStripMenuItem.Enabled = false;
			programMemMultiWin.DisplayDisable();
			eepromDataMultiWin.DisplayDisable();
			UARTtoolStripMenuItem.Enabled = false;
			toolStripMenuItemLogicTool.Enabled = false;
		}

		private void PartialEnableGUIControls()
		{
			importFileToolStripMenuItem.Enabled = true;
			exportFileToolStripMenuItem.Enabled = false;
			readDeviceToolStripMenuItem.Enabled = true;
			writeDeviceToolStripMenuItem.Enabled = true;
			verifyToolStripMenuItem.Enabled = true;
			eraseToolStripMenuItem.Enabled = true;
			blankCheckToolStripMenuItem.Enabled = true;
			writeOnPICkitButtonToolStripMenuItem.Enabled = true;
			picKit2GoToolStripMenuItem.Enabled = true;
			setOSCCALToolStripMenuItem.Enabled = false;
			writeDeviceToolStripMenuItem.Enabled = false;
			verifyToolStripMenuItem.Enabled = false;
			buttonRead.Enabled = true;
			buttonWrite.Enabled = false;
			buttonVerify.Enabled = false;
			buttonErase.Enabled = true;
			buttonBlankCheck.Enabled = true;
			checkBoxProgMemEnabled.Enabled = false;
			checkBoxProgMemEnabledAlt.Enabled = false;
			comboBoxProgMemView.Enabled = false;
			dataGridProgramMemory.ForeColor = SystemColors.GrayText;
			dataGridProgramMemory.Enabled = false;
			dataGridViewEEPROM.Visible = false;
			comboBoxEE.Enabled = false;
			checkBoxEEMem.Enabled = false;
			checkBoxEEDATAMemoryEnabledAlt.Enabled = false;
			buttonExportHex.Enabled = false;
			checkBoxAutoImportWrite.Enabled = false;
			troubleshhotToolStripMenuItem.Enabled = true;
			calibrateToolStripMenuItem.Enabled = true;
			programMemMultiWin.DisplayDisable();
			eepromDataMultiWin.DisplayDisable();
			UARTtoolStripMenuItem.Enabled = true;
			toolStripMenuItemLogicTool.Enabled = true;
		}

		private void SemiEnableGUIControls()
		{
			importFileToolStripMenuItem.Enabled = true;
			exportFileToolStripMenuItem.Enabled = false;
			readDeviceToolStripMenuItem.Enabled = true;
			writeDeviceToolStripMenuItem.Enabled = true;
			verifyToolStripMenuItem.Enabled = true;
			eraseToolStripMenuItem.Enabled = true;
			blankCheckToolStripMenuItem.Enabled = true;
			writeOnPICkitButtonToolStripMenuItem.Enabled = true;
			picKit2GoToolStripMenuItem.Enabled = true;
			writeDeviceToolStripMenuItem.Enabled = true;
			verifyToolStripMenuItem.Enabled = true;
			setOSCCALToolStripMenuItem.Enabled = false;
			buttonRead.Enabled = true;
			buttonWrite.Enabled = true;
			buttonVerify.Enabled = true;
			buttonErase.Enabled = true;
			buttonBlankCheck.Enabled = true;
			checkBoxProgMemEnabled.Enabled = false;
			checkBoxProgMemEnabledAlt.Enabled = false;
			comboBoxProgMemView.Enabled = false;
			dataGridProgramMemory.ForeColor = SystemColors.GrayText;
			dataGridProgramMemory.Enabled = false;
			dataGridViewEEPROM.Visible = true;
			dataGridViewEEPROM.ForeColor = SystemColors.GrayText;
			comboBoxEE.Enabled = false;
			checkBoxEEMem.Enabled = false;
			checkBoxEEDATAMemoryEnabledAlt.Enabled = false;
			buttonExportHex.Enabled = false;
			checkBoxAutoImportWrite.Enabled = true;
			troubleshhotToolStripMenuItem.Enabled = true;
			calibrateToolStripMenuItem.Enabled = true;
			UARTtoolStripMenuItem.Enabled = true;
			toolStripMenuItemLogicTool.Enabled = true;
		}

		private void FullEnableGUIControls()
		{
			importFileToolStripMenuItem.Enabled = true;
			exportFileToolStripMenuItem.Enabled = true;
			readDeviceToolStripMenuItem.Enabled = true;
			writeDeviceToolStripMenuItem.Enabled = true;
			verifyToolStripMenuItem.Enabled = true;
			eraseToolStripMenuItem.Enabled = true;
			blankCheckToolStripMenuItem.Enabled = true;
			writeOnPICkitButtonToolStripMenuItem.Enabled = true;
			picKit2GoToolStripMenuItem.Enabled = true;
			writeDeviceToolStripMenuItem.Enabled = true;
			verifyToolStripMenuItem.Enabled = true;
			buttonRead.Enabled = true;
			buttonWrite.Enabled = true;
			buttonVerify.Enabled = true;
			buttonErase.Enabled = true;
			buttonBlankCheck.Enabled = true;
			checkBoxProgMemEnabled.Enabled = true;
			checkBoxProgMemEnabledAlt.Enabled = true;
			comboBoxProgMemView.Enabled = true;
			dataGridProgramMemory.Enabled = true;
			dataGridProgramMemory.ForeColor = SystemColors.WindowText;
			dataGridViewEEPROM.ForeColor = SystemColors.WindowText;
			buttonExportHex.Enabled = true;
			checkBoxAutoImportWrite.Enabled = true;
			troubleshhotToolStripMenuItem.Enabled = true;
			calibrateToolStripMenuItem.Enabled = true;
			programMemMultiWin.DisplayEnable();
			eepromDataMultiWin.DisplayEnable();
			UARTtoolStripMenuItem.Enabled = true;
			toolStripMenuItemLogicTool.Enabled = true;
		}

		private void UpdateGUIView()
		{
			if (multiWindow)
			{
				toolStripMenuItemSingleWindow.Checked = false;
				toolStripMenuItemMultiWindow.Checked = true;
				groupBoxProgMem.Location = new Point((int)(12 * scalefactW), (int)(300 * scalefactH));
				Size = new Size((int)(544f * scalefactW), (int)(320 * scalefactH));
				pictureBox1.Location = new Point((int)(423 * scalefactW), (int)(238 * scalefactH));
				buttonExportHex.Location = new Point((int)(311 * scalefactW), (int)(240 * scalefactH));
				checkBoxAutoImportWrite.Location = new Point((int)(201 * scalefactW), (int)(240 * scalefactH));
				checkBoxProgMemEnabledAlt.Visible = true;
				checkBoxEEDATAMemoryEnabledAlt.Visible = true;
				toolStripMenuItemShowProgramMemory.Enabled = true;
				toolStripMenuItemShowEEPROMData.Enabled = true;
				mainWindowAlwaysInFrontToolStripMenuItem.Enabled = true;
				if (mainWinOwnsMem)
					mainWindowAlwaysInFrontToolStripMenuItem.Checked = true;
				Point right = new Point(0, 0);
				if (programMemMultiWin.Location == right && eepromDataMultiWin.Location == right)
				{
					programMemMultiWin.Location = new Point(Location.X, Location.Y + (int)(321 * scalefactH));
					eepromDataMultiWin.Location = new Point(Location.X, Location.Y + (int)(530 * scalefactH));
				}
				if (multiWinPMemOpen)
				{
					programMemMultiWin.Show();
					toolStripMenuItemShowProgramMemory.Checked = true;
				}
				if (multiWinEEMemOpen)
				{
					toolStripMenuItemShowEEPROMData.Checked = true;
					if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].EEMem > 0)
					{
						toolStripMenuItemShowEEPROMData.Enabled = true;
						eepromDataMultiWin.Show();
					}
					else
						toolStripMenuItemShowEEPROMData.Enabled = false;
				}
			}
			else
			{
				programMemMultiWin.Hide();
				eepromDataMultiWin.Hide();
				toolStripMenuItemSingleWindow.Checked = true;
				toolStripMenuItemMultiWindow.Checked = false;
				groupBoxProgMem.Location = new Point((int)(12 * scalefactW), (int)(236 * scalefactH));
				Size = new Size((int)(544 * scalefactW), (int)(670 * scalefactH));
				pictureBox1.Location = new Point((int)(423 * scalefactW), (int)(586 * scalefactH));
				buttonExportHex.Location = new Point((int)(423 * scalefactW), (int)(545 * scalefactH));
				checkBoxAutoImportWrite.Location = new Point((int)(423 * scalefactW), (int)(505 * scalefactH));
				checkBoxProgMemEnabledAlt.Visible = false;
				checkBoxEEDATAMemoryEnabledAlt.Visible = false;
				toolStripMenuItemShowProgramMemory.Enabled = false;
				toolStripMenuItemShowEEPROMData.Enabled = false;
				mainWindowAlwaysInFrontToolStripMenuItem.Enabled = false;
				toolStripMenuItemShowProgramMemory.Checked = false;
				toolStripMenuItemShowEEPROMData.Checked = false;
				mainWindowAlwaysInFrontToolStripMenuItem.Checked = false;
			}
			Focus();
		}

		private void UpdateGUI(bool updateMemories)
		{
			if (viewChanged)
			{
				UpdateGUIView();
				viewChanged = false;
			}
			statusGroupBox.Text = PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].FamilyName + " Configuration";
			if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].ProgEntryVPPScript > 0)
				VppFirstToolStripMenuItem.Enabled = true;
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
					labelLVP.Visible = true;
				else
					labelLVP.Visible = false;
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
					displayDevice.Text = "No Device Found";
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
							text2 += string.Format("{0:X2} ", 255U & PICkitFunctions.DeviceBuffers.UserIDs[i]);
						displayUserIDs.Text = text2;
					}
					else
					{
						displayUserIDs.Visible = false;
						buttonShowIDMem.Visible = true;
						if (DialogUserIDs.IDMemOpen)
							dialogIDMemory.UpdateIDMemoryGrid();
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
				displayUserIDs.ForeColor = SystemColors.WindowText;
			else
				displayUserIDs.ForeColor = SystemColors.GrayText;
			if (updateMemories)
				displayChecksum.Text = string.Format("{0:X4}", PICkitFunctions.ComputeChecksum(enableCodeProtectToolStripMenuItem.Checked, enableDataProtectStripMenuItem.Checked));
			if (updateMemories)
			{
				if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigWords == 0 || PICkitFunctions.ActivePart == 0 || !allowDataEdits)
					labelConfig.Enabled = false;
				else
					labelConfig.Enabled = true;
				int num = 0;
				for (int j = 0; j < 2; j++)
				{
					for (int k = 0; k < 4; k++)
					{
						if (num < PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigWords)
						{
							uint num2 = PICkitFunctions.DeviceBuffers.ConfigWords[num];
							if (as0BitValueToolStripMenuItem.Checked)
								num2 &= PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[num];
							else if (as1BitValueToolStripMenuItem.Checked)
								num2 |= ~(uint)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[num];
							num2 &= PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue & 65535U;
							if ((PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].CPConfig - 1) == num)
							{
								if (enableCodeProtectToolStripMenuItem.Checked && (PICkitFunctions.DeviceBuffers.ConfigWords[PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].CPConfig - 1] & PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].CPMask) == PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].CPMask)
									num2 &= ~(uint)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].CPMask;
								if (enableDataProtectStripMenuItem.Checked && (PICkitFunctions.DeviceBuffers.ConfigWords[PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].CPConfig - 1] & PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].DPMask) == PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].DPMask)
									num2 &= ~(uint)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].DPMask;
							}
							dataGridConfigWords[k, j].Value = string.Format("{0:X4}", num2);
							num++;
						}
						else
							dataGridConfigWords[k, j].Value = " ";
						if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigWords == 9)
						{
							uint num3 = PICkitFunctions.DeviceBuffers.ConfigWords[8];
							if (as0BitValueToolStripMenuItem.Checked)
								num3 &= PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[8];
							else if (as1BitValueToolStripMenuItem.Checked)
								num3 |= ~(uint)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[8];
							num3 &= PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue & 65535U;
							labelConfig9.Text = string.Format("{0:X4}", num3);
							labelConfig9.Visible = true;
						}
						else
							labelConfig9.Visible = false;
					}
				}
			}
			if (checkBoxProgMemEnabled.Checked)
				dataGridConfigWords.ForeColor = SystemColors.WindowText;
			else
				dataGridConfigWords.ForeColor = SystemColors.GrayText;
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
			if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].BandGapMask > 0)
			{
				labelBandGap.Enabled = true;
				if (PICkitFunctions.DeviceBuffers.BandGap == PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue)
					displayBandGap.Text = "";
				else
					displayBandGap.Text = string.Format("{0:X4}", PICkitFunctions.DeviceBuffers.BandGap);
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
				if (playSuccessWav)
				{
					wavPlayer.SoundLocation = successWavFile;
					wavPlayer.Play();
				}
				break;
			case Constants.StatusColor.yellow:
				displayStatusWindow.BackColor = Color.Yellow;
				if (playWarningWav)
				{
					wavPlayer.SoundLocation = warningWavFile;
					wavPlayer.Play();
				}
				break;
			case Constants.StatusColor.red:
				displayStatusWindow.BackColor = Color.Salmon;
				if (playErrorWav)
				{
					wavPlayer.SoundLocation = errorWavFile;
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
					enableCodeProtectToolStripMenuItem.Enabled = true;
			}
			if (updateMemories && multiWindow)
			{
				if (!programMemMultiWin.InitDone)
					programMemMultiWin.InitProgMemDisplay(comboBoxProgMemView.SelectedIndex);
				programMemMultiWin.UpdateMultiWinProgMem(displayDataSource.Text);
			}
			if (updateMemories && !multiWindow)
			{
				if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue > 16777215)
				{
					comboBoxProgMemView.SelectedIndex = 0;
					comboBoxProgMemView.Enabled = false;
				}
				else
					comboBoxProgMemView.Enabled = true;
				int num4;
				int num5;
				int width;
				if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue <= 4095)
				{
					if (PICkitFunctions.FamilyIsEEPROM())
					{
						num4 = 17;
						dataGridProgramMemory.Columns[0].Width = (int)(51f * scalefactW);
						num5 = 16;
						width = (int)(27 * scalefactW);
					}
					else
					{
						num4 = 17;
						dataGridProgramMemory.Columns[0].Width = (int)(35f * scalefactW);
						num5 = 16;
						width = (int)(28 * scalefactW);
					}
				}
				else if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue > 16777215)
				{
					num4 = 5;
					dataGridProgramMemory.Columns[0].Width = (int)(99 * scalefactW);
					num5 = 4;
					width = (int)(96 * scalefactW);
				}
				else
				{
					num4 = 9;
					dataGridProgramMemory.Columns[0].Width = (int)(59 * scalefactW);
					num5 = 8;
					width = (int)(53 * scalefactW);
				}
				if (dataGridProgramMemory.ColumnCount != num4)
				{
					dataGridProgramMemory.Rows.Clear();
					dataGridProgramMemory.ColumnCount = num4;
				}
				for (int l = 1; l < dataGridProgramMemory.ColumnCount; l++)
					dataGridProgramMemory.Columns[l].Width = width;
				int addressIncrement = PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].AddressIncrement;
				int num6;
				int num7;
				int num8;
				if (comboBoxProgMemView.SelectedIndex == 0)
				{
					num6 = num5;
					num7 = (int)(PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgramMem / (uint)num6);
					if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgramMem % num6 > 0)
						num7++;
					num8 = addressIncrement * num5;
				}
				else
				{
					num6 = num5 / 2;
					num7 = (int)(PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgramMem / (uint)num6);
					if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgramMem % num6 > 0U)
						num7++;
					num8 = addressIncrement * (num5 / 2);
				}
				if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue > 16777215)
					num7 += 2;
				if (dataGridProgramMemory.RowCount != num7)
				{
					dataGridProgramMemory.Rows.Clear();
					dataGridProgramMemory.RowCount = num7;
				}
				for (int m = 0; m < num6; m++)
					dataGridProgramMemory.Columns[m + 1].ReadOnly = false;
				int num9 = dataGridProgramMemory.RowCount * num8 - 1;
				string format = "{0:X3}";
				if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue > 16777215)
					format = "{0:X8}";
				else if (num9 > 65535)
					format = "{0:X5}";
				else if (num9 > 4095)
					format = "{0:X4}";
				int num10 = (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgramMem;
				int bootFlash = (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].BootFlash;
				num10 -= bootFlash;
				num10 /= num6;
				if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue > 16777215)
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
				if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue > 255)
				{
					format2 = "{0:X3}";
					numBytes = 2;
				}
				if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue > 4095)
				{
					format2 = "{0:X4}";
					numBytes = 2;
				}
				if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue > 65535)
				{
					format2 = "{0:X6}";
					numBytes = 3;
				}
				if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue > 16777215)
				{
					format2 = "{0:X8}";
					numBytes = 4;
				}
				if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue > 16777215)
				{
					int num18 = 0;
					for (int num19 = 1; num19 <= num10; num19++)
					{
						for (int num20 = 0; num20 < num6; num20++)
							dataGridProgramMemory[num20 + 1, num19].Value = string.Format(format2, PICkitFunctions.DeviceBuffers.ProgramMemory[num18++]);
					}
					for (int num21 = num10 + 2; num21 < dataGridProgramMemory.RowCount; num21++)
					{
						for (int num22 = 0; num22 < num6; num22++)
							dataGridProgramMemory[num22 + 1, num21].Value = string.Format(format2, PICkitFunctions.DeviceBuffers.ProgramMemory[num18++]);
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
					num28 = num6;
				if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue <= 16777215)
				{
					for (int num29 = 0; num29 < num6; num29++)
					{
						if (num29 < num28)
						{
							dataGridProgramMemory[num29 + 1, num26].ToolTipText = string.Format(format, num27 * addressIncrement);
							dataGridProgramMemory[num29 + 1, num26].Value = string.Format(format2, PICkitFunctions.DeviceBuffers.ProgramMemory[num27++]);
						}
						else
							dataGridProgramMemory[num29 + 1, num26].ReadOnly = true;
					}
				}
				if (comboBoxProgMemView.SelectedIndex >= 1)
				{
					for (int num30 = 0; num30 < num6; num30++)
						dataGridProgramMemory.Columns[num30 + num6 + 1].ReadOnly = true;
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
						eepromDataMultiWin.InitMemDisplay(comboBoxEE.SelectedIndex);
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
					if (num37 == 1 && PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue != 4095)
					{
						num39 = 17;
						dataGridViewEEPROM.Columns[0].Width = (int)(32 * scalefactW);
						num40 = 16;
						width2 = (int)(21 * scalefactW);
					}
					else
					{
						num39 = 9;
						dataGridViewEEPROM.Columns[0].Width = (int)(40 * scalefactW);
						num40 = 8;
						width2 = (int)(41 * scalefactW);
					}
					if (dataGridViewEEPROM.ColumnCount != num39)
					{
						dataGridViewEEPROM.Rows.Clear();
						dataGridViewEEPROM.ColumnCount = num39;
					}
					dataGridViewEEPROM.Columns[0].ReadOnly = true;
					for (int num41 = 1; num41 < dataGridViewEEPROM.ColumnCount; num41++)
						dataGridViewEEPROM.Columns[num41].Width = width2;
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
						format3 = "{0:X3}";
					else if (num44 > 4095)
						format3 = "{0:X4}";
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
					if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue == 4095)
					{
						format4 = "{0:X3}";
						numBytes2 = 2;
					}
					for (int num47 = 0; num47 < num42; num47++)
						dataGridViewEEPROM.Columns[num47 + 1].ReadOnly = false;
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
							dataGridViewEEPROM.Columns[num51 + num42 + 1].ReadOnly = true;
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
					labelCodeProtect.Text = "All Protect";
				else if (enableCodeProtectToolStripMenuItem.Checked)
					labelCodeProtect.Text = "Code Protect";
				else
					labelCodeProtect.Text = "Data Protect";
			}
			else
				labelCodeProtect.Visible = false;
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
			if (testMemoryEnabled && testMemoryOpen)
			{
				formTestMem.UpdateTestMemForm();
				if (updateMemories)
					formTestMem.UpdateTestMemoryGrid();
			}
		}

		private void ProgMemViewChanged(object sender, EventArgs e)
		{
			UpdateGUI(true);
		}

		private void BuildDeviceMenu()
		{
			for (int i = 0; i < PICkitFunctions.DevFile.Families.Length; i++)
			{
				for (int j = 0; j < PICkitFunctions.DevFile.Families.Length; j++)
				{
					if (PICkitFunctions.DevFile.Families[j].FamilyType == i)
					{
						string familyName = PICkitFunctions.DevFile.Families[j].FamilyName;
						int num = familyName.IndexOf("/");
						if (familyName[0] != '#')
						{
							if (num < 0)
								deviceToolStripMenuItem.DropDown.Items.Add(familyName);
							else
							{
								int count = deviceToolStripMenuItem.DropDownItems.Count;
								for (int k = 0; k < count; k++)
								{
									if (familyName.Substring(0, num) == deviceToolStripMenuItem.DropDown.Items[k].ToString())
									{
										ToolStripMenuItem toolStripMenuItem = (ToolStripMenuItem)deviceToolStripMenuItem.DropDownItems[k];
										toolStripMenuItem.DropDown.Items.Add(familyName.Substring(num + 1));
									}
									else if (k == count - 1)
									{
										deviceToolStripMenuItem.DropDown.Items.Add(familyName.Substring(0, num));
										ToolStripMenuItem toolStripMenuItem2 = (ToolStripMenuItem)deviceToolStripMenuItem.DropDownItems[k + 1];
										toolStripMenuItem2.DropDown.Items.Add(familyName.Substring(num + 1));
										toolStripMenuItem2.DropDownItemClicked += DeviceFamilyClick;
									}
								}
							}
						}
					}
				}
			}
			deviceToolStripMenuItem.Enabled = true;
		}

		private void GUIVddControl(object sender, EventArgs e)
		{
			VddControl(false, false);
		}

		private void VddControl(bool force, bool forceState)
		{
			if (force)
				chkBoxVddOn.Checked = forceState;
			bool @checked = chkBoxVddOn.Checked;
			if (DetectPICkit2(false, false))
			{
				if (@checked)
				{
					if (LookForPoweredTarget(true))
					{
						CheckForPowerErrors();
						PICkitFunctions.VddOff();
						return;
					}
					chkBoxVddOn.Checked = true;
					PICkitFunctions.SetVddVoltage((float)numUpDnVdd.Value, 0.85f);
					PICkitFunctions.VddOn();
					if (CheckForPowerErrors())
					{
						PICkitFunctions.VddOff();
						return;
					}
				}
				else
				{
					chkBoxVddOn.Checked = false;
					PICkitFunctions.VddOff();
				}
			}
		}

		private void GUIChangeVdd(object sender, EventArgs e)
		{
			if (DetectPICkit2(false, false))
				PICkitFunctions.SetVddVoltage((float)numUpDnVdd.Value, 0.85f);
		}

		private void PICkitFormClosing(object sender, FormClosingEventArgs e)
		{
			SaveINI();
		}

		private void FileMenuExit(object sender, EventArgs e)
		{
			Close();
		}

		private void MenuFileImportHex(object sender, EventArgs e)
		{
			if (PICkitFunctions.FamilyIsKeeloq())
				openHexFileDialog.Filter = "HEX files|*.hex;*.num|All files|*.*";
			else if (PICkitFunctions.FamilyIsEEPROM())
				openHexFileDialog.Filter = "HEX or BIN files|*.hex;*.bin|All files|*.*";
			else
				openHexFileDialog.Filter = "HEX files|*.hex|All files|*.*";
			openHexFileDialog.ShowDialog();
			UpdateGUI(true);
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
			if (!PreProgrammingCheck(PICkitFunctions.GetActiveFamily()))
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
				PICkitFunctions.ResetBuffers();
			else
			{
				if (checkBoxProgMemEnabled.Checked)
				{
					PICkitFunctions.DeviceBuffers.ClearProgramMemory(PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue);
					PICkitFunctions.DeviceBuffers.ClearConfigWords(PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigBlank);
					PICkitFunctions.DeviceBuffers.ClearUserIDs((int)PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].UserIDBytes, PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue);
				}
				if (checkBoxEEMem.Checked)
					PICkitFunctions.DeviceBuffers.ClearEEPromMemory((int)PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].EEMemAddressIncrement, PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue);
			}
			if (testMemoryEnabled && testMemoryOpen && formTestMem.HexImportExportTM())
				formTestMem.ClearTestMemory();
			string text = "Hex";
			if (openHexFileDialog.FileName.Substring(openHexFileDialog.FileName.Length - 4).ToUpper() == ".BIN" && PICkitFunctions.FamilyIsEEPROM())
				text = "Bin";
			switch (ImportExportHex.ImportHexFile(openHexFileDialog.FileName, checkBoxProgMemEnabled.Checked, checkBoxEEMem.Checked))
			{
				case Constants.FileRead.success:
					displayStatusWindow.Text = text + " file sucessfully imported.";
					if (multiWindow)
						displayDataSource.Text = openHexFileDialog.FileName;
					else
						displayDataSource.Text = ShortenHex(openHexFileDialog.FileName);
					checkImportFile = true;
					importGo = true;
                    break;
                case Constants.FileRead.noconfig:
					statusWindowColor = Constants.StatusColor.yellow;
					displayStatusWindow.Text = "Warning: No configuration words in hex file.\nIn MPLAB use File-Export to save hex with config.";
					if (multiWindow)
						displayDataSource.Text = openHexFileDialog.FileName;
					else
						displayDataSource.Text = ShortenHex(openHexFileDialog.FileName);
					checkImportFile = true;
					importGo = true;
                    break;
                case Constants.FileRead.partialcfg:
					statusWindowColor = Constants.StatusColor.yellow;
					displayStatusWindow.Text = "Warning: Some configuration words not in hex file.\nEnsure default values above right are acceptable.";
					if (multiWindow)
						displayDataSource.Text = openHexFileDialog.FileName;
					else
						displayDataSource.Text = ShortenHex(openHexFileDialog.FileName);
					checkImportFile = true;
					importGo = true;
                    break;
                case Constants.FileRead.largemem:
					statusWindowColor = Constants.StatusColor.yellow;
					displayStatusWindow.Text = "Warning: " + text + " File Loaded is larger than device.";
					if (multiWindow)
						displayDataSource.Text = openHexFileDialog.FileName;
					else
						displayDataSource.Text = ShortenHex(openHexFileDialog.FileName);
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
				ConditionalVddOff();
				bool flag2 = false;
				bool flag3 = false;
				do
				{
					if (openHexFileDialog.FileName == hex4 || flag2)
					{
						if (!hex4ToolStripMenuItem.Visible && hex3.Length > 3)
							hex4ToolStripMenuItem.Visible = true;
						hex4 = hex3;
						hex4ToolStripMenuItem.Text = "&4" + hex3ToolStripMenuItem.Text.Substring(2);
						flag2 = true;
						flag3 = true;
					}
					if (openHexFileDialog.FileName == hex3 || flag2)
					{
						if (!hex3ToolStripMenuItem.Visible && hex2.Length > 3)
							hex3ToolStripMenuItem.Visible = true;
						hex3 = hex2;
						hex3ToolStripMenuItem.Text = "&3" + hex2ToolStripMenuItem.Text.Substring(2);
						flag2 = true;
						flag3 = true;
					}
					if (openHexFileDialog.FileName == hex2 || flag2)
					{
						if (!hex2ToolStripMenuItem.Visible && hex1.Length > 3)
							hex2ToolStripMenuItem.Visible = true;
						hex2 = hex1;
						hex2ToolStripMenuItem.Text = "&2" + hex1ToolStripMenuItem.Text.Substring(2);
						flag3 = true;
					}
					flag2 = true;
					if (openHexFileDialog.FileName == hex1)
						flag3 = true;
				}
				while (!flag3);
				if (!hex1ToolStripMenuItem.Visible)
				{
					hex1ToolStripMenuItem.Visible = true;
					toolStripMenuItem5.Visible = true;
				}
				hex1 = openHexFileDialog.FileName;
				hex1ToolStripMenuItem.Text = "&1 " + ShortenHex(openHexFileDialog.FileName);
			}
			return checkImportFile;
		}

		private void MenuFileExportHex(object sender, EventArgs e)
		{
			if (PICkitFunctions.FamilyIsKeeloq())
			{
				MessageBox.Show("Export not supported for\nthis part type.");
				return;
			}
			if (PICkitFunctions.FamilyIsEEPROM())
				saveHexFileDialog.Filter = "Hex files|*.hex|Bin Files|*.bin|All files|*.*";
			else
				saveHexFileDialog.Filter = "Hex files|*.hex|All files|*.*";
			saveHexFileDialog.ShowDialog();
		}

		private void ExportHexFile(object sender, CancelEventArgs e)
		{
			ImportExportHex.ExportHexFile(saveHexFileDialog.FileName, checkBoxProgMemEnabled.Checked, checkBoxEEMem.Checked);
		}

		private bool PreProgrammingCheck(int family)
		{
			statusGroupBox.Update();
			if (PICkitFunctions.LearnMode)
			{
				PICkitFunctions.SetVddVoltage((float)numUpDnVdd.Value, 0.85f);
				return true;
			}
			if (!DetectPICkit2(false, false))
			{
				return false;
			}
			if (CheckForPowerErrors())
			{
				UpdateGUI(false);
				return false;
			}
			LookForPoweredTarget(!timerAutoImportWrite.Enabled);
			if (PICkitFunctions.DevFile.Families[family].PartDetect)
			{
				if (!PICkitFunctions.DetectDevice(family, false, chkBoxVddOn.Checked))
				{
					SemiEnableGUIControls();
					statusWindowColor = Constants.StatusColor.yellow;
					displayStatusWindow.Text = "No device detected.";
					if (PICkitFunctions.DevFile.Families[family].Vpp < 1)
					{
						Label label = displayStatusWindow;
						label.Text += "\nEnsure proper capacitance on VDDCORE/VCAP pin.";
					}
					CheckForPowerErrors();
					UpdateGUI(false);
					return false;
				}
				SetGUIVoltageLimits(false);
				FullEnableGUIControls();
				UpdateGUI(false);
			}
			else if (PICkitFunctions.DevFile.Families[family].DeviceIDMask > 0 && deviceVerification)
			{
				if (!PICkitFunctions.VerifyDeviceID(false, chkBoxVddOn.Checked))
				{
					statusWindowColor = Constants.StatusColor.yellow;
					if (PICkitFunctions.LastDeviceID == 0 || PICkitFunctions.LastDeviceID == PICkitFunctions.DevFile.Families[family].DeviceIDMask)
					{
						displayStatusWindow.Text = "No device detected.";
					}
					else
					{
						displayStatusWindow.Text = "Selected device not detected.";
						for (int i = 0; i < PICkitFunctions.DevFile.PartsList.Length; i++)
						{
							if (PICkitFunctions.DevFile.PartsList[i].Family == family && PICkitFunctions.DevFile.PartsList[i].DeviceID == PICkitFunctions.LastDeviceID)
							{
								Label label2 = displayStatusWindow;
								label2.Text = label2.Text + "\nDetected a " + PICkitFunctions.DevFile.PartsList[i].PartName + " instead.";
								break;
							}
						}
					}
					CheckForPowerErrors();
					UpdateGUI(false);
					return false;
				}
			}
			else
			{
				PICkitFunctions.SetMCLRTemp(true);
				PICkitFunctions.SetVddVoltage((float)numUpDnVdd.Value, 0.85f);
				PICkitFunctions.VddOn();
				PICkitFunctions.RunScript(0, 1);
				Thread.Sleep(300);
				PICkitFunctions.RunScript(1, 1);
				ConditionalVddOff();
				if (CheckForPowerErrors())
				{
					UpdateGUI(false);
					return false;
				}
			}
			PICkitFunctions.SetVddVoltage((float)numUpDnVdd.Value, 0.85f);
			if (!checkBoxEEMem.Enabled && PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].EEMem > 0)
				checkBoxEEMem.Checked = true;
			return true;
		}

		private void ReadDevice(object sender, EventArgs e)
		{
			DeviceRead();
		}

		private void DeviceRead()
		{
			if (PICkitFunctions.FamilyIsKeeloq())
			{
				displayStatusWindow.Text = "Read not supported for this device type.";
				statusWindowColor = Constants.StatusColor.yellow;
				UpdateGUI(false);
				return;
			}
			if (!PreProgrammingCheck(PICkitFunctions.GetActiveFamily()))
				return;
			if (PICkitFunctions.FamilyIsPIC32())
			{
				if (PIC32MXFunctions.PIC32Read())
					statusWindowColor = Constants.StatusColor.normal;
				else
					statusWindowColor = Constants.StatusColor.red;
				ConditionalVddOff();
				UpdateGUI(true);
				return;
			}
			displayStatusWindow.Text = "Reading device:\n";
			Update();
			byte[] array = new byte[128];
			PICkitFunctions.SetMCLRTemp(true);
			PICkitFunctions.VddOn();
			if (checkBoxProgMemEnabled.Checked)
			{
				Label label = displayStatusWindow;
				label.Text += "Program Memory... ";
				Update();
				if (UseProgExec33())
				{
					if (!dsPIC33_PE.PE33Read(displayStatusWindow.Text))
					{
						statusWindowColor = Constants.StatusColor.red;
						ConditionalVddOff();
						UpdateGUI(true);
						return;
					}
				}
				else if (UseProgExec24F())
				{
					if (!PIC24F_PE.PE24FRead(displayStatusWindow.Text))
					{
						statusWindowColor = Constants.StatusColor.red;
						ConditionalVddOff();
						UpdateGUI(true);
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
							PICkitFunctions.DownloadAddress3MSBFirst(EEPROM24BitAddress(0, false));
							PICkitFunctions.RunScript(5, 1);
							if (EEPROM_CheckBusErrors())
								return;
						}
						else
						{
							PICkitFunctions.DownloadAddress3(0);
							PICkitFunctions.RunScript(5, 1);
						}
					}
					int bytesPerLocation = PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BytesPerLocation;
					int num = 128 / (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgMemRdWords * bytesPerLocation);
					int num2 = num * PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgMemRdWords;
					int num3 = 0;
					progressBar1.Value = 0;
					progressBar1.Maximum = (int)(PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgramMem / (uint)num2);
					do
					{
						if (PICkitFunctions.FamilyIsEEPROM())
						{
							if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[0] == 1 && num3 > PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[1] && num3 % (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[1] + 1) == 0)
							{
								PICkitFunctions.DownloadAddress3MSBFirst(EEPROM24BitAddress(num3, false));
								PICkitFunctions.RunScript(5, 1);
							}
							PICkitFunctions.Download3Multiples(EEPROM24BitAddress(num3, true), num, (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgMemRdWords);
						}
						PICkitFunctions.RunScriptUploadNoLen(3, num);
						Array.Copy(PICkitFunctions.Usb_read_array, 1L, array, 0, 64);
						PICkitFunctions.UploadDataNoLen();
						Array.Copy(PICkitFunctions.Usb_read_array, 1L, array, 64, 64);
						int num4 = 0;
						for (int i = 0; i < num2; i++)
						{
							int num5 = 0;
							uint num6 = array[num4 + num5++];
							if (num5 < bytesPerLocation)
								num6 |= (uint)array[num4 + num5++] << 8;
							if (num5 < bytesPerLocation)
								num6 |= (uint)array[num4 + num5++] << 16;
							if (num5 < bytesPerLocation)
								num6 |= (uint)array[num4 + num5++] << 24;
							num4 += num5;
							if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].ProgMemShift > 0)
								num6 = num6 >> 1 & PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue;
							PICkitFunctions.DeviceBuffers.ProgramMemory[num3++] = num6;
							if (num3 == PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgramMem)
								break;
							if (num3 % 32768 == 0 && PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgMemAddrSetScript != 0 && PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgMemAddrBytes != 0 && PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue > 65535)
							{
								PICkitFunctions.DownloadAddress3(65536 * (num3 / 32768));
								PICkitFunctions.RunScript(5, 1);
								break;
							}
						}
						progressBar1.PerformStep();
					}
					while (num3 < PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgramMem);
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
			if (checkBoxEEMem.Checked && PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].EEMem > 0)
				ReadEEPROM();
			if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].UserIDWords > 0 && checkBoxProgMemEnabled.Checked)
			{
				Label label2 = displayStatusWindow;
				label2.Text += "UserIDs... ";
				Update();
				PICkitFunctions.RunScript(0, 1);
				if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].UserIDRdPrepScript > 0)
					PICkitFunctions.RunScript(16, 1);
				int userIDBytes = PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].UserIDBytes;
				int num8 = 0;
				int num9 = 0;
				PICkitFunctions.RunScriptUploadNoLen(17, 1);
				Array.Copy(PICkitFunctions.Usb_read_array, 1L, array, 0, 64);
				if ((PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].UserIDWords * userIDBytes) > 64)
				{
					PICkitFunctions.UploadDataNoLen();
					Array.Copy(PICkitFunctions.Usb_read_array, 1L, array, 64L, 64L);
				}
				PICkitFunctions.RunScript(1, 1);
				do
				{
					int num10 = 0;
					uint num11 = array[num9 + num10++];
					if (num10 < userIDBytes)
						num11 |=(uint)array[num9 + num10++] << 8;
					if (num10 < userIDBytes)
						num11 |= (uint)array[num9 + num10++] << 16;
					if (num10 < userIDBytes)
						num11 |= (uint)array[num9 + num10++] << 24;
					num9 += num10;
					if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].ProgMemShift > 0)
						num11 = (num11 >> 1 & PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue);
					PICkitFunctions.DeviceBuffers.UserIDs[num8++] = num11;
				}
				while (num8 < PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].UserIDWords);
			}
			int num12 = (int)(PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigAddr / PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].ProgMemHexBytes);
			int configWords = PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigWords;
			if (configWords > 0 && num12 >= PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgramMem && checkBoxProgMemEnabled.Checked)
			{
				Label label3 = displayStatusWindow;
				label3.Text += "Config... ";
				Update();
				PICkitFunctions.ReadConfigOutsideProgMem();
				if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].BandGapMask > 0)
					PICkitFunctions.DeviceBuffers.BandGap = PICkitFunctions.DeviceBuffers.ConfigWords[0] & PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].BandGapMask;
			}
			else if (configWords > 0 && checkBoxProgMemEnabled.Checked)
			{
				Label label4 = displayStatusWindow;
				label4.Text += "Config... ";
				Update();
				for (int k = 0; k < configWords; k++)
					PICkitFunctions.DeviceBuffers.ConfigWords[k] = PICkitFunctions.DeviceBuffers.ProgramMemory[num12 + k];
			}
			if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].OSSCALSave)
				PICkitFunctions.ReadOSSCAL();
			if (testMemoryEnabled && testMemoryOpen)
			{
				formTestMem.ReadTestMemory();
			}
			ConditionalVddOff();
			Label label5 = displayStatusWindow;
			label5.Text += "Done.";
			displayDataSource.Text = "Read from " + PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].PartName;
			checkImportFile = false;
			UpdateGUI(true);
		}

		private void ReadEEPROM()
		{
			byte[] array = new byte[128];
			Label label = displayStatusWindow;
			label.Text += "EE... ";
			Update();
			PICkitFunctions.RunScript(0, 1);
			if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].EERdPrepScript > 0)
			{
				if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].EEMemHexBytes == 4)
					PICkitFunctions.DownloadAddress3((int)(PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].EEAddr / 2));
				else
					PICkitFunctions.DownloadAddress3(0);
				PICkitFunctions.RunScript(8, 1);
			}
			int eememBytesPerWord = PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].EEMemBytesPerWord;
			int num = 128 / (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].EERdLocations * eememBytesPerWord);
			int num2 = num * PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].EERdLocations;
			int num3 = 0;
			uint eeblank = GetEEBlank();
			progressBar1.Value = 0;
			progressBar1.Maximum = PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].EEMem / num2;
			do
			{
				PICkitFunctions.RunScriptUploadNoLen(9, num);
				Array.Copy(PICkitFunctions.Usb_read_array, 1, array, 0, 64);
				PICkitFunctions.UploadDataNoLen();
				Array.Copy(PICkitFunctions.Usb_read_array, 1, array, 64, 64);
				int num4 = 0;
				for (int i = 0; i < num2; i++)
				{
					int num5 = 0;
					uint num6 = array[num4 + num5++];
					if (num5 < eememBytesPerWord)
						num6 |= (uint)array[num4 + num5++] << 8;
					num4 += num5;
					if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].ProgMemShift > 0)
						num6 = num6 >> 1 & eeblank;
					PICkitFunctions.DeviceBuffers.EEPromMemory[num3++] = num6;
					if (num3 >= PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].EEMem)
						break;
				}
				progressBar1.PerformStep();
			}
			while (num3 < PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].EEMem);
			PICkitFunctions.RunScript(1, 1);
		}

		private void EraseDevice(object sender, EventArgs e)
		{
			EraseDeviceAll(false, new uint[0]);
		}

		private void EraseDeviceAll(bool forceOSSCAL, uint[] calWords)
		{
			if (PICkitFunctions.FamilyIsKeeloq() || PICkitFunctions.FamilyIsMCP())
			{
				displayStatusWindow.Text = "Erase not supported for this device type.";
				statusWindowColor = Constants.StatusColor.yellow;
				UpdateGUI(false);
				return;
			}
			if (!PreProgrammingCheck(PICkitFunctions.GetActiveFamily()))
				return;
			DeviceData deviceData = PICkitFunctions.CloneBuffers(PICkitFunctions.DeviceBuffers);
			if (PICkitFunctions.FamilyIsEEPROM() && PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[0] != 3 && PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[0] != 4)
			{
				PICkitFunctions.ResetBuffers();
				checkImportFile = false;
				if (!EEPROMWrite(true))
				{
					if (!toolStripMenuItemClearBuffersErase.Checked)
						PICkitFunctions.DeviceBuffers = deviceData;
					return;
				}
				Label label = displayStatusWindow;
				label.Text += "Complete";
				if (!toolStripMenuItemClearBuffersErase.Checked)
					PICkitFunctions.DeviceBuffers = deviceData;
				else
					displayDataSource.Text = "None (Empty/Erased)";
				UpdateGUI(true);
				return;
			}
			else
			{
				if (!CheckEraseVoltage(false))
					return;
				progressBar1.Value = 0;
				PICkitFunctions.SetMCLRTemp(true);
				PICkitFunctions.VddOn();
				if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].OSSCALSave && !forceOSSCAL)
				{
					PICkitFunctions.ReadOSSCAL();
					if (!VerifyOSCCAL())
						return;
				}
				uint osccal = PICkitFunctions.DeviceBuffers.OSCCAL;
				if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].BandGapMask > 0U)
					PICkitFunctions.ReadBandGap();
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
					PICkitFunctions.ExecuteScript(PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMemEraseScript);
					PICkitFunctions.RunScript(1, 1);
				}
				PICkitFunctions.RunScript(0, 1);
				if (testMemoryEnabled && testMemoryOpen && calWords.Length > 0)
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
						PICkitFunctions.RunScript(4, 1);
					PICkitFunctions.RunScript(22, 1);
				}
				PICkitFunctions.RunScript(1, 1);
				PICkitFunctions.ResetBuffers();
				if (testMemoryEnabled && testMemoryOpen && !toolStripMenuItemClearBuffersErase.Checked)
					formTestMem.ClearTestMemory();
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
					int num = (int)(PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigAddr / PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].ProgMemHexBytes);
					int configWords = PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigWords;
					int num2 = PICkitFunctions.DeviceBuffers.ProgramMemory.Length;
					if (num < (long)((ulong)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgramMem) && configWords > 0)
					{
						uint num3;
						if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue == 65535)
							num3 = 61440;
						else
							num3 = 16711680;
						for (int j = configWords; j > 0; j--)
							PICkitFunctions.DeviceBuffers.ProgramMemory[num2 - j] = PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigBlank[configWords - j] | num3;
						WriteConfigInsideProgramMem();
					}
					else
						PICkitFunctions.WriteConfigOutsideProgMem(false, false);
				}
				if (!toolStripMenuItemClearBuffersErase.Checked)
					PICkitFunctions.DeviceBuffers = deviceData;
				Label label2 = displayStatusWindow;
				label2.Text += "Complete";
				Update();
				if (toolStripMenuItemClearBuffersErase.Checked)
					displayDataSource.Text = "None (Empty/Erased)";
				checkImportFile = false;
				ConditionalVddOff();
				UpdateGUI(true);
				return;
			}
		}

		private bool CheckEraseVoltage(bool checkRowErase)
		{
			if ((float)(numUpDnVdd.Value + 0.05m) >= PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].VddErase || !showWriteEraseVddDialog)
				return true;
			if (checkRowErase && PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].DebugRowEraseScript > 0)
				return false;
			dialogVddErase.UpdateText();
			bool enabled = timerAutoImportWrite.Enabled;
			timerAutoImportWrite.Enabled = false;
			dialogVddErase.ShowDialog();
			timerAutoImportWrite.Enabled = enabled;
			return continueWriteErase;
		}

		private bool VerifyOSCCAL()
		{
			if (!PICkitFunctions.ValidateOSSCAL() && verifyOSCCALValue && MessageBox.Show("Invalid OSCCAL Value detected:\n\nTo abort, click 'Cancel'\nTo continue, click 'OK'", "Warning!", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
			{
				ConditionalVddOff();
				displayStatusWindow.Text = "Operation Aborted.\n";
				statusWindowColor = Constants.StatusColor.red;
				UpdateGUI(true);
				return false;
			}
			return true;
		}

		private void WriteDevice(object sender, EventArgs e)
		{
			DeviceWrite();
		}

		private bool DeviceWrite()
		{
			uint num = 0;
			if (PICkitFunctions.FamilyIsEEPROM())
				return EEPROMWrite(false);
			bool flag = false;
			if (!PreProgrammingCheck(PICkitFunctions.GetActiveFamily()))
				return false;
			if (!CheckEraseVoltage(true) && !PICkitFunctions.FamilyIsPIC32())
			{
				if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].DebugRowEraseScript <= 0)
					return false;
				flag = true;
			}
			UpdateGUI(false);
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
						UpdateGUI(true);
						return false;
					}
				}
			}
			if (PICkitFunctions.FamilyIsPIC32())
			{
				if (PIC32MXFunctions.P32Write(verifyOnWriteToolStripMenuItem.Checked, enableCodeProtectToolStripMenuItem.Checked))
				{
					statusWindowColor = Constants.StatusColor.green;
					ConditionalVddOff();
					UpdateGUI(true);
					return true;
				}
				statusWindowColor = Constants.StatusColor.red;
				ConditionalVddOff();
				UpdateGUI(true);
				return true;
			}
			else
			{
				PICkitFunctions.SetMCLRTemp(true);
				PICkitFunctions.VddOn();
				if (PICkitFunctions.LearnMode && PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].DeviceIDMask > 0)
					PICkitFunctions.MetaCmd_CHECK_DEVICE_ID();
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
						if (!VerifyOSCCAL())
							return false;
					}
				}

				if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].BandGapMask > 0)
				{
					if (PICkitFunctions.LearnMode)
						PICkitFunctions.MetaCmd_READ_BANDGAP();
					else
						PICkitFunctions.ReadBandGap();
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
							PICkitFunctions.ExecuteScript(PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMemEraseScript);
							PICkitFunctions.RunScript(1, 1);
						}
						PICkitFunctions.RunScript(0, 1);
						if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ChipErasePrepScript > 0)
							PICkitFunctions.RunScript(4, 1);
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
					ReadEEPROM();
					UpdateGUI(true);
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
							PICkitFunctions.ExecuteScript(PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMemEraseScript);
							PICkitFunctions.RunScript(1, 1);
						}
						PICkitFunctions.RunScript(0, 1);
						if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ChipErasePrepScript > 0)
							PICkitFunctions.RunScript(4, 1);
						PICkitFunctions.RunScript(22, 1);
						PICkitFunctions.RunScript(1, 1);
						flag2 = true;
					}
				}
				displayStatusWindow.Text = "Writing device:\n";
				Update();
				bool flag3 = false;
				int num2 = (int)(PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigAddr / PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].ProgMemHexBytes);
				int configWords = PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigWords;
				int num3 = PICkitFunctions.DeviceBuffers.ProgramMemory.Length;
				uint[] array = new uint[configWords];
				if (num2 < PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgramMem && configWords > 0)
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
						PICkitFunctions.HCS360_361_VppSpecial();
					int progMemWrWords = PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgMemWrWords;
					int bytesPerLocation = PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BytesPerLocation;
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
						num7++;
					num3 = num7 * num5;
					progressBar1.Maximum = num3 / num5;
					if (UseProgExec33())
					{
						if (!dsPIC33_PE.PE33Write(num3, displayStatusWindow.Text))
						{
							statusWindowColor = Constants.StatusColor.red;
							ConditionalVddOff();
							UpdateGUI(true);
							return false;
						}
					}
					else if (UseProgExec24F())
					{
						if (!PIC24F_PE.PE24FWrite(num3, displayStatusWindow.Text, verifyOnWriteToolStripMenuItem.Checked))
						{
							statusWindowColor = Constants.StatusColor.red;
							ConditionalVddOff();
							UpdateGUI(true);
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
									num10 <<= 1;
								array2[num8++] = (byte)(num10 & 255U);
								num += (byte)(num10 & 255U);
								for (int j = 1; j < bytesPerLocation; j++)
								{
									num10 >>= 8;
									array2[num8++] = (byte)(num10 & 255U);
									num += (byte)(num10 & 255U);
								}
								num9++;
							}
							if (PICkitFunctions.FamilyIsKeeloq())
								ProcessKeeloqData(ref array2, num6);
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
						PICkitFunctions.DeviceBuffers.ProgramMemory[PICkitFunctions.DeviceBuffers.ProgramMemory.Length - l] = array[l - 1];
				}
				if ((checkBoxEEMem.Checked || flag2) && PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].EEMem > 0)
				{
					Label label2 = displayStatusWindow;
					label2.Text += "EE... ";
					Update();
					PICkitFunctions.RunScript(0, 1);
					if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].EEWrPrepScript > 1)
					{
						if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].EEMemHexBytes == 4)
							PICkitFunctions.DownloadAddress3((int)(PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].EEAddr / 2));
						else
							PICkitFunctions.DownloadAddress3(0);
						PICkitFunctions.RunScript(10, 1);
					}
					int eememBytesPerWord = PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].EEMemBytesPerWord;
					uint eeblank = GetEEBlank();
					int num12 = PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].EEWrLocations;
					if (num12 < 16)
						num12 = 16;
					if (checkBoxProgMemEnabled.Checked && !flag && !PICkitFunctions.LearnMode)
						num3 = PICkitFunctions.FindLastUsedInBuffer(PICkitFunctions.DeviceBuffers.EEPromMemory, eeblank, PICkitFunctions.DeviceBuffers.EEPromMemory.Length - 1);
					else
						num3 = PICkitFunctions.DeviceBuffers.EEPromMemory.Length - 1;
					int num13 = (num3 + 1) / num12;
					if ((num3 + 1) % num12 > 0)
						num13++;
					num3 = num13 * num12;
					byte[] array3 = new byte[num12 * eememBytesPerWord];
					int repetitions = num12 / PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].EEWrLocations;
					int num14 = 0;
					progressBar1.Value = 0;
					progressBar1.Maximum = num3 / num12;
					do
					{
						int num15 = 0;
						for (int m = 0; m < num12; m++)
						{
							uint num16 = PICkitFunctions.DeviceBuffers.EEPromMemory[num14++];
							if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].ProgMemShift > 0)
								num16 <<= 1;
							array3[num15++] = (byte)(num16 & 255U);
							num += (byte)(num16 & 255U);
							for (int n = 1; n < eememBytesPerWord; n++)
							{
								num16 >>= 8;
								array3[num15++] = (byte)(num16 & 255U);
								num += (byte)(num16 & 255U);
							}
						}
						PICkitFunctions.DataClrAndDownload(array3, 0);
						PICkitFunctions.RunScript(11, repetitions);
						progressBar1.PerformStep();
					}
					while (num14 < num3);
					PICkitFunctions.RunScript(1, 1);
				}
				if (checkBoxProgMemEnabled.Checked && PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].UserIDWords > 0)
				{
					Label label3 = displayStatusWindow;
					label3.Text += "UserIDs... ";
					Update();
					PICkitFunctions.RunScript(0, 1);
					if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].UserIDWrPrepScript > 0)
						PICkitFunctions.RunScript(18, 1);
					int userIDBytes = PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].UserIDBytes;
					byte[] array4 = new byte[PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].UserIDWords * userIDBytes];
					int num17 = 0;
					int num18 = 0;
					for (int num19 = 0; num19 < PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].UserIDWords; num19++)
					{
						uint num20 = PICkitFunctions.DeviceBuffers.UserIDs[num18++];
						if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].ProgMemShift > 0)
							num20 <<= 1;
						array4[num17++] = (byte)(num20 & 255U);
						num += (byte)(num20 & 255U);
						for (int num21 = 1; num21 < userIDBytes; num21++)
						{
							num20 >>= 8;
							array4[num17++] = (byte)(num20 & 255U);
							num += (byte)(num20 & 255U);
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
						if (num11 == PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgramMem)
						{
							if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue > 65535U)
								num -= 128;
							else
								num -= 8;
						}
					}
					else
						num11 -= configWords;
				}
				if (verifyOnWriteToolStripMenuItem.Checked)
				{
					if (PICkitFunctions.LearnMode)
						PICkitFunctions.MetaCmd_START_CHECKSUM();
					flag4 = DeviceVerify(true, num11 - 1, flag2);
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
					if (configWords > 0 && !flag3 && checkBoxProgMemEnabled.Checked)
					{
						if (!verifyOnWriteToolStripMenuItem.Checked)
						{
							Label label4 = displayStatusWindow;
							label4.Text += "Config... ";
							Update();
						}
						if ((PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].FamilyName == "PIC18F" || PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].FamilyName == "PIC18F_K_") && PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigWords > 5 && (PICkitFunctions.DeviceBuffers.ConfigWords[5] & 18446744073709543423UL) == PICkitFunctions.DeviceBuffers.ConfigWords[5])
						{
							uint num23 = PICkitFunctions.DeviceBuffers.ConfigWords[5];
							PICkitFunctions.DeviceBuffers.ConfigWords[5] = 65535U;
							PICkitFunctions.WriteConfigOutsideProgMem(false, false);
							PICkitFunctions.DeviceBuffers.ConfigWords[5] = num23;
						}
						num += PICkitFunctions.WriteConfigOutsideProgMem(enableCodeProtectToolStripMenuItem.Enabled && enableCodeProtectToolStripMenuItem.Checked, enableDataProtectStripMenuItem.Enabled && enableDataProtectStripMenuItem.Checked);
						if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue == 65535)
							num += PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[7];
						if (verifyOnWriteToolStripMenuItem.Checked && (!PICkitFunctions.LearnMode || PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].BandGapMask == 0))
						{
							bool flag5 = VerifyConfig(configWords, num2);
							if (flag5)
							{
								statusWindowColor = Constants.StatusColor.green;
								displayStatusWindow.Text = "Programming Successful.\n";
							}
							else if (!PICkitFunctions.LearnMode)
							{
								statusWindowColor = Constants.StatusColor.red;
								flag4 = false;
							}
							if (PICkitFunctions.LearnMode && PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].BandGapMask == 0)
								PICkitFunctions.MetaCmd_VERIFY_CHECKSUM(num);
						}
					}
					else if (configWords > 0 && checkBoxProgMemEnabled.Checked)
					{
						if (!verifyOnWriteToolStripMenuItem.Checked)
						{
							Label label5 = displayStatusWindow;
							label5.Text += "Config... ";
							Update();
						}
						for (int num24 = 0; num24 < configWords; num24++)
						{
							if (num24 == (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].CPConfig - 1))
							{
								if (enableCodeProtectToolStripMenuItem.Enabled && enableCodeProtectToolStripMenuItem.Checked)
									PICkitFunctions.DeviceBuffers.ProgramMemory[num2 + num24] &= ~(uint)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].CPMask;
								if (enableDataProtectStripMenuItem.Enabled && enableDataProtectStripMenuItem.Checked)
									PICkitFunctions.DeviceBuffers.ProgramMemory[num2 + num24] &= ~(uint)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].DPMask;
							}
						}
						WriteConfigInsideProgramMem();
						if (verifyOnWriteToolStripMenuItem.Checked)
						{
							statusWindowColor = Constants.StatusColor.green;
							displayStatusWindow.Text = "Programming Successful.\n";
						}
						else
							flag4 = false;
					}
					else if (!checkBoxProgMemEnabled.Checked)
					{
						statusWindowColor = Constants.StatusColor.green;
						displayStatusWindow.Text = "Programming Successful.\n";
					}
					else
					{
						statusWindowColor = Constants.StatusColor.green;
						displayStatusWindow.Text = "Programming Successful.\n";
					}
					ConditionalVddOff();
					if (!verifyOnWriteToolStripMenuItem.Checked)
					{
						Label label6 = displayStatusWindow;
						label6.Text += "Done.";
					}
					if (PICkitFunctions.LearnMode)
						displayStatusWindow.Text = "Programmer-To-Go download complete.";
					UpdateGUI(true);
					return flag4;
				}
				return false;
			}
		}

		private void WriteConfigInsideProgramMem()
		{
			PICkitFunctions.RunScript(0, 1);
			int num = PICkitFunctions.DeviceBuffers.ProgramMemory.Length - PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgMemWrWords;
			if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgMemWrPrepScript != 0)
			{
				PICkitFunctions.DownloadAddress3(num * PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].AddressIncrement);
				PICkitFunctions.RunScript(6, 1);
			}
			byte[] array = new byte[256];
			int num2 = 0;
			for (int i = 0; i < PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgMemWrWords; i++)
			{
				uint num3 = PICkitFunctions.DeviceBuffers.ProgramMemory[num++];
				if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].ProgMemShift > 0)
					num3 <<= 1;
				array[num2++] = (byte)(num3 & 255U);
				for (int j = 1; j < PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BytesPerLocation; j++)
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

		private void ProcessKeeloqData(ref byte[] downloadBuffer, int wordsWritten)
		{
			if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].DeviceID == 4294967094)
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

		private void BlankCheck(object sender, EventArgs e)
		{
			BlankCheckDevice();
		}

		private bool BlankCheckDevice()
		{
			if (PICkitFunctions.FamilyIsKeeloq() || PICkitFunctions.FamilyIsMCP())
			{
				displayStatusWindow.Text = "Blank Check not supported for this device type.";
				statusWindowColor = Constants.StatusColor.yellow;
				UpdateGUI(false);
				return false;
			}
			if (!PreProgrammingCheck(PICkitFunctions.GetActiveFamily()))
				return false;
			if (!PICkitFunctions.FamilyIsPIC32())
			{
				DeviceData deviceData = new DeviceData(PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgramMem, PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].EEMem, PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigWords, PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].UserIDWords, PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue, PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].EEMemAddressIncrement, PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].UserIDBytes, PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigBlank, PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[7]);
				int num = (int)(PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigAddr / PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].ProgMemHexBytes);
				int configWords = PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigWords;
				if (num < PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgramMem)
				{
					for (int i = 0; i < configWords; i++)
					{
						uint num2 = deviceData.ProgramMemory[num + i] & 4294901760U;
						if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue == 65535)
							num2 |= 61440U;
						deviceData.ProgramMemory[num + i] = num2 | PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigBlank[i];
					}
				}
				displayStatusWindow.Text = "Checking if Device is blank:\n";
				Update();
				PICkitFunctions.SetMCLRTemp(true);
				PICkitFunctions.VddOn();
				byte[] array = new byte[128];
				Label label = displayStatusWindow;
				label.Text += "Program Memory... ";
				Update();
				if (UseProgExec33())
				{
					if (!dsPIC33_PE.PE33BlankCheck(displayStatusWindow.Text))
					{
						ConditionalVddOff();
						displayStatusWindow.Text = "Program Memory is not blank.";
						statusWindowColor = Constants.StatusColor.red;
						UpdateGUI(true);
						return false;
					}
				}
				else if (UseProgExec24F())
				{
					if (!PIC24F_PE.PE24FBlankCheck(displayStatusWindow.Text))
					{
						ConditionalVddOff();
						statusWindowColor = Constants.StatusColor.red;
						UpdateGUI(true);
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
							PICkitFunctions.DownloadAddress3MSBFirst(EEPROM24BitAddress(0, false));
							PICkitFunctions.RunScript(5, 1);
							if (EEPROM_CheckBusErrors())
								return false;
						}
						else
						{
							PICkitFunctions.DownloadAddress3(0);
							PICkitFunctions.RunScript(5, 1);
						}
					}
					int bytesPerLocation = PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BytesPerLocation;
					int num3 = 128 / (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgMemRdWords * bytesPerLocation);
					int num4 = num3 * PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgMemRdWords;
					int num5 = 0;
					progressBar1.Value = 0;
					progressBar1.Maximum = (int)(PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgramMem / (uint)num4);
					for (;;)
					{
						if (PICkitFunctions.FamilyIsEEPROM())
						{
							if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[0] == 1 && num5 > PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[1] && num5 % (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[1] + 1) == 0)
							{
								PICkitFunctions.DownloadAddress3MSBFirst(EEPROM24BitAddress(num5, false));
								PICkitFunctions.RunScript(5, 1);
							}
							PICkitFunctions.Download3Multiples(EEPROM24BitAddress(num5, true), num3, PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgMemRdWords);
						}
						PICkitFunctions.RunScriptUploadNoLen(3, num3);
						Array.Copy(PICkitFunctions.Usb_read_array, 1, array, 0, 64);
						PICkitFunctions.UploadDataNoLen();
						Array.Copy(PICkitFunctions.Usb_read_array, 1, array, 64, 64);
						int num6 = 0;
						for (int j = 0; j < num4; j++)
						{
							int num7 = 0;
							uint num8 = array[num6 + num7++];
							if (num7 < bytesPerLocation)
								num8 |= (uint)array[num6 + num7++] << 8;
							if (num7 < bytesPerLocation)
								num8 |= (uint)array[num6 + num7++] << 16;
							if (num7 < bytesPerLocation)
								num8 |= (uint)array[num6 + num7++] << 24;
							num6 += num7;
							if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].ProgMemShift > 0)
								num8 = num8 >> 1 & PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue;
							if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].OSSCALSave && num5 == (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgramMem - 1))
								num8 = PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue;
							if (num8 != deviceData.ProgramMemory[num5++])
								goto Block_25;
							if (num5 == PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgramMem)
								break;
							if (num5 % 32768 == 0 && PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgMemAddrSetScript != 0 && PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgMemAddrBytes != 0 && PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue > 65535)
							{
								PICkitFunctions.DownloadAddress3(65536 * (num5 / 32768));
								PICkitFunctions.RunScript(5, 1);
								break;
							}
						}
						progressBar1.PerformStep();
						if (num5 >= PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgramMem)
							goto Block_32;
					}
					Block_25:
					PICkitFunctions.RunScript(1, 1);
					ConditionalVddOff();
					if (PICkitFunctions.FamilyIsEEPROM())
						displayStatusWindow.Text = "EEPROM is not blank starting at address\n";
					else
						displayStatusWindow.Text = "Program Memory is not blank starting at address\n";
					Label label2 = displayStatusWindow;
					label2.Text += string.Format("0x{0:X6}", (num5 - 1) * PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].AddressIncrement);
					statusWindowColor = Constants.StatusColor.red;
					UpdateGUI(true);
					return false;
					Block_32:
					PICkitFunctions.RunScript(1, 1);
				}
				if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].EEMem > 0)
				{
					Label label3 = displayStatusWindow;
					label3.Text += "EE... ";
					Update();
					PICkitFunctions.RunScript(0, 1);
					if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].EERdPrepScript > 0)
					{
						if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].EEMemHexBytes == 4)
							PICkitFunctions.DownloadAddress3((int)(PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].EEAddr / 2));
						else
							PICkitFunctions.DownloadAddress3(0);
						PICkitFunctions.RunScript(8, 1);
					}
					int eememBytesPerWord = PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].EEMemBytesPerWord;
					uint eeblank = GetEEBlank();
					int num9 = 128 / (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].EERdLocations * eememBytesPerWord);
					int num10 = num9 * PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].EERdLocations;
					int num11 = 0;
					progressBar1.Value = 0;
					progressBar1.Maximum = PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].EEMem / num10;
					while(true)
					{
						PICkitFunctions.RunScriptUploadNoLen(9, num9);
						Array.Copy(PICkitFunctions.Usb_read_array, 1, array, 0, 64);
						PICkitFunctions.UploadDataNoLen();
						Array.Copy(PICkitFunctions.Usb_read_array, 1, array, 64, 64);
						int num12 = 0;
						for (int k = 0; k < num10; k++)
						{
							int num13 = 0;
							uint num14 = array[num12 + num13++];
							if (num13 < eememBytesPerWord)
								num14 |= (uint)array[num12 + num13++] << 8;
							num12 += num13;
							if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].ProgMemShift > 0)
								num14 = num14 >> 1 & eeblank;
							num11++;
							if (num14 != eeblank)
								goto Block_38;
							if (num11 >= PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].EEMem)
								break;
						}
						progressBar1.PerformStep();
						if (num11 >= PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].EEMem)
						{
							goto Block_41;
						}
					}
					Block_38:
					PICkitFunctions.RunScript(1, 1);
					ConditionalVddOff();
					displayStatusWindow.Text = "EE Data Memory is not blank starting at address\n";
					if (eeblank == 65535)
					{
						Label label4 = displayStatusWindow;
						label4.Text += string.Format("0x{0:X4}", (num11 - 1) * 2);
					}
					else
					{
						Label label5 = displayStatusWindow;
						label5.Text += string.Format("0x{0:X4}", num11 - 1);
					}
					statusWindowColor = Constants.StatusColor.red;
					UpdateGUI(true);
					return false;
					Block_41:
					PICkitFunctions.RunScript(1, 1);
				}
				if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].UserIDWords > 0 && !PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].BlankCheckSkipUsrIDs)
				{
					Label label6 = displayStatusWindow;
					label6.Text += "UserIDs... ";
					Update();
					PICkitFunctions.RunScript(0, 1);
					if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].UserIDRdPrepScript > 0)
						PICkitFunctions.RunScript(16, 1);
					int userIDBytes = PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].UserIDBytes;
					int num15 = 0;
					int num16 = 0;
					PICkitFunctions.RunScriptUploadNoLen(17, 1);
					Array.Copy(PICkitFunctions.Usb_read_array, 1, array, 0, 64);
					if ((long)(PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].UserIDWords * userIDBytes) > 64)
					{
						PICkitFunctions.UploadDataNoLen();
						Array.Copy(PICkitFunctions.Usb_read_array, 1, array, 64, 64);
					}
					PICkitFunctions.RunScript(1, 1);
					while(true)
					{
						int num17 = 0;
						uint num18 = array[num16 + num17++];
						if (num17 < userIDBytes)
							num18 |= (uint)array[num16 + num17++] << 8;
						if (num17 < userIDBytes)
							num18 |= (uint)array[num16 + num17++] << 16;
						if (num17 < userIDBytes)
							num18 |= (uint)array[num16 + num17++] << 24;
						num16 += num17;
						if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].ProgMemShift > 0)
							num18 = num18 >> 1 & PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue;
						num15++;
						uint num19 = PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue;
						if (userIDBytes == 1)
							num19 &= 255U;
						if (num18 != num19)
							break;
						if (num15 >= PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].UserIDWords)
							goto IL_C83;
					}
					ConditionalVddOff();
					displayStatusWindow.Text = "User IDs are not blank.";
					statusWindowColor = Constants.StatusColor.red;
					UpdateGUI(true);
					return false;
				}
				IL_C83:
				if (configWords > 0 && num > PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgramMem)
				{
					Label label7 = displayStatusWindow;
					label7.Text += "Config... ";
					Update();
					PICkitFunctions.RunScript(0, 1);
					PICkitFunctions.RunScript(13, 1);
					PICkitFunctions.UploadData();
					PICkitFunctions.RunScript(1, 1);
					int num20 = 2;
					for (int l = 0; l < configWords; l++)
					{
						uint num21 = PICkitFunctions.Usb_read_array[num20++];
						num21 |= (uint)PICkitFunctions.Usb_read_array[num20++] << 8;
						if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].ProgMemShift > 0)
							num21 = num21 >> 1 & PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue;
						num21 &= PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[l];
						int num22 = PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[l] & PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigBlank[l];
						if (num22 != num21)
						{
							ConditionalVddOff();
							displayStatusWindow.Text = "Configuration is not blank.";
							statusWindowColor = Constants.StatusColor.red;
							UpdateGUI(true);
							return false;
						}
					}
				}
				PICkitFunctions.RunScript(1, 1);
				ConditionalVddOff();
				statusWindowColor = Constants.StatusColor.green;
				displayStatusWindow.Text = "Device is Blank.";
				UpdateGUI(true);
				return true;
			}
			if (PIC32MXFunctions.PIC32BlankCheck())
			{
				statusWindowColor = Constants.StatusColor.green;
				ConditionalVddOff();
				UpdateGUI(true);
				return true;
			}
			statusWindowColor = Constants.StatusColor.red;
			ConditionalVddOff();
			UpdateGUI(true);
			return true;
		}

		private void ProgMemEdit(object sender, DataGridViewCellEventArgs e)
		{
			int rowIndex = e.RowIndex;
			int columnIndex = e.ColumnIndex;
			string p_value = "0x" + dataGridProgramMemory[columnIndex, rowIndex].FormattedValue.ToString();
            int num;
            try
			{
				num = Utilities.Convert_Value_To_Int(p_value);
			}
			catch
			{
				num = 0;
			}
			int num2 = dataGridProgramMemory.ColumnCount - 1;
			if (comboBoxProgMemView.SelectedIndex >= 1)
				num2 /= 2;
			int num3 = rowIndex * num2 + columnIndex - 1;
			if (PICkitFunctions.FamilyIsPIC32())
			{
				int num4 = (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgramMem;
				int bootFlash = (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].BootFlash;
				num4 -= bootFlash;
				num3 -= num2;
				if (num3 > num4)
					num3 -= num2;
			}
			PICkitFunctions.DeviceBuffers.ProgramMemory[num3] = (uint)(num & PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue);
			displayDataSource.Text = "Edited.";
			checkImportFile = false;
			progMemJustEdited = true;
			UpdateGUI(true);
		}

		private void EEPROMEdit(object sender, DataGridViewCellEventArgs e)
		{
			int rowIndex = e.RowIndex;
			int columnIndex = e.ColumnIndex;
			string p_value = "0x" + dataGridViewEEPROM[columnIndex, rowIndex].FormattedValue.ToString();
			int num;
			try
			{
				num = Utilities.Convert_Value_To_Int(p_value);
			}
			catch
			{
				num = 0;
			}
			int num2 = dataGridViewEEPROM.ColumnCount - 1;
			if (comboBoxEE.SelectedIndex >= 1)
				num2 /= 2;
			PICkitFunctions.DeviceBuffers.EEPromMemory[rowIndex * num2 + columnIndex - 1] = (uint)(num & GetEEBlank());
			displayDataSource.Text = "Edited.";
			checkImportFile = false;
			eeMemJustEdited = true;
			UpdateGUI(true);
		}

		private void CheckCommunication(object sender, EventArgs e)
		{
			if (!DetectPICkit2(true, true))
				return;
			PartialEnableGUIControls();
			LookForPoweredTarget(false);
			if (!PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].PartDetect)
			{
				SetGUIVoltageLimits(true);
				PICkitFunctions.SetVddVoltage((float)numUpDnVdd.Value, 0.85f);
				displayStatusWindow.Text += "\n[Parts in this family are not auto-detect.]";
				FullEnableGUIControls();
			}
			else if (PICkitFunctions.DetectDevice(16777215, true, chkBoxVddOn.Checked))
			{
				SetGUIVoltageLimits(true);
				PICkitFunctions.SetVddVoltage((float)numUpDnVdd.Value, 0.85f);
				displayStatusWindow.Text += "\nPIC Device Found.";
				FullEnableGUIControls();
			}
			displayDataSource.Text = "None (Empty/Erased)";
			CheckForPowerErrors();
			UpdateGUI(true);
		}

		private void VerifyDevice(object sender, EventArgs e)
		{
			if (PICkitFunctions.FamilyIsKeeloq())
			{
				displayStatusWindow.Text = "Verify not supported for this device type.";
				statusWindowColor = Constants.StatusColor.yellow;
				UpdateGUI(false);
				return;
			}
			DeviceVerify(false, 0, false);
		}

		private bool DeviceVerify(bool writeVerify, int lastLocation, bool forceEEVerify)
		{
			if (!writeVerify && !PreProgrammingCheck(PICkitFunctions.GetActiveFamily()))
				return false;
			if (PICkitFunctions.FamilyIsPIC32())
			{
				if (PIC32MXFunctions.P32Verify(writeVerify, enableCodeProtectToolStripMenuItem.Checked))
				{
					statusWindowColor = Constants.StatusColor.green;
					ConditionalVddOff();
					UpdateGUI(true);
					return true;
				}
				statusWindowColor = Constants.StatusColor.red;
				ConditionalVddOff();
				UpdateGUI(true);
				return true;
			}
			else
			{
				displayStatusWindow.Text = "Verifying Device:\n";
				Update();
				if (!PICkitFunctions.FamilyIsKeeloq() && (!writeVerify || !PICkitFunctions.FamilyIsPIC24FJ()))
					PICkitFunctions.SetMCLRTemp(true);
				PICkitFunctions.VddOn();
				byte[] array = new byte[128];
				int configLocation = (int)(PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigAddr / PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].ProgMemHexBytes);
				int configWords = PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigWords;
				int num = PICkitFunctions.DeviceBuffers.ProgramMemory.Length - 1;
				if (writeVerify)
					num = lastLocation;
				if (checkBoxProgMemEnabled.Checked)
				{
					Label label = displayStatusWindow;
					label.Text += "Program Memory... ";
					Update();
					if (UseProgExec33())
					{
						if (!dsPIC33_PE.PE33VerifyCRC(displayStatusWindow.Text))
						{
							if (!writeVerify)
								displayStatusWindow.Text = "Verification of Program Memory failed.\n";
							else
								displayStatusWindow.Text = "Programming of Program Memory failed.\n";
							ConditionalVddOff();
							statusWindowColor = Constants.StatusColor.red;
							UpdateGUI(true);
							return false;
						}
					}
					else if (UseProgExec24F())
					{
						if (!PIC24F_PE.PE24FVerify(displayStatusWindow.Text, writeVerify, lastLocation))
						{
							ConditionalVddOff();
							statusWindowColor = Constants.StatusColor.red;
							UpdateGUI(true);
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
								PICkitFunctions.DownloadAddress3MSBFirst(EEPROM24BitAddress(0, false));
								PICkitFunctions.RunScript(5, 1);
								if (!writeVerify && EEPROM_CheckBusErrors())
									return false;
							}
							else
							{
								PICkitFunctions.DownloadAddress3(0);
								PICkitFunctions.RunScript(5, 1);
							}
						}
						int bytesPerLocation = PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BytesPerLocation;
						int num2 = 128 / (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgMemRdWords * bytesPerLocation);
						int num3 = num2 * PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgMemRdWords;
						int num4 = 0;
						if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgMemRdWords == num + 1)
						{
							num2 = 1;
							num3 = num + 1;
						}
						progressBar1.Value = 0;
						progressBar1.Maximum = num / num3;
						while(true)
						{
							if (PICkitFunctions.FamilyIsEEPROM())
							{
								if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[0] == 1 && num4 > PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[1] && num4 % (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[1] + 1) == 0)
								{
									PICkitFunctions.DownloadAddress3MSBFirst(EEPROM24BitAddress(num4, false));
									PICkitFunctions.RunScript(5, 1);
								}
								PICkitFunctions.Download3Multiples(EEPROM24BitAddress(num4, true), num2, (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgMemRdWords);
							}
							PICkitFunctions.RunScriptUploadNoLen(3, num2);
							Array.Copy(PICkitFunctions.Usb_read_array, 1, array, 0, 64);
							PICkitFunctions.UploadDataNoLen();
							Array.Copy(PICkitFunctions.Usb_read_array, 1L, array, 64, 64);
							int num5 = 0;
							for (int i = 0; i < num3; i++)
							{
								int num6 = 0;
								uint num7 = array[num5 + num6++];
								if (num6 < bytesPerLocation)
									num7 |= (uint)array[num5 + num6++] << 8;
								if (num6 < bytesPerLocation)
									num7 |= (uint)array[num5 + num6++] << 16;
								if (num6 < bytesPerLocation)
									num7 |= (uint)array[num5 + num6++] << 24;
								num5 += num6;
								if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].ProgMemShift > 0)
									num7 = num7 >> 1 & PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue;
								if (bytesPerLocation == 2 && PICkitFunctions.FamilyIsEEPROM() && PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[0] == 3)
								{
									uint num8 = num7 << 8 & 65280U;
									num7 >>= 8;
									num7 |= num8;
								}
								if (num7 != PICkitFunctions.DeviceBuffers.ProgramMemory[num4++] && !PICkitFunctions.LearnMode)
									goto Block_32;
								if (num4 % 32768 == 0 && PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgMemAddrSetScript != 0 && PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgMemAddrBytes != 0 && PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue > 65535)
								{
									PICkitFunctions.DownloadAddress3(65536 * (num4 / 32768));
									PICkitFunctions.RunScript(5, 1);
									break;
								}
								if (num4 > num)
									break;
							}
							progressBar1.PerformStep();
							if (num4 >= num)
								goto Block_41;
						}
						Block_32:
						PICkitFunctions.RunScript(1, 1);
						ConditionalVddOff();
						if (!writeVerify)
						{
							if (PICkitFunctions.FamilyIsEEPROM())
							{
								displayStatusWindow.Text = "Verification of EEPROM failed at address\n";
							}
							else
							{
								displayStatusWindow.Text = "Verification of Program Memory failed at address\n";
							}
						}
						else if (PICkitFunctions.FamilyIsEEPROM())
						{
							displayStatusWindow.Text = "Programming failed at EEPROM address\n";
						}
						else
						{
							displayStatusWindow.Text = "Programming failed at Program Memory address\n";
						}
						Label label2 = displayStatusWindow;
						label2.Text += string.Format("0x{0:X6}", (num4 - 1) * PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].AddressIncrement);
						statusWindowColor = Constants.StatusColor.red;
						UpdateGUI(true);
						return false;
						Block_41:
						PICkitFunctions.RunScript(1, 1);
					}
				}
				if ((checkBoxEEMem.Checked || forceEEVerify) && PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].EEMem > 0)
				{
					if (PICkitFunctions.LearnMode && PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].ProgMemShift > 0)
						PICkitFunctions.MetaCmd_CHANGE_CHKSM_FRMT(2);
					Label label3 = displayStatusWindow;
					label3.Text += "EE... ";
					Update();
					PICkitFunctions.RunScript(0, 1);
					if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].EERdPrepScript > 0)
					{
						if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].EEMemHexBytes == 4)
							PICkitFunctions.DownloadAddress3((int)(PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].EEAddr / 2));
						else
							PICkitFunctions.DownloadAddress3(0);
						PICkitFunctions.RunScript(8, 1);
					}
					int eememBytesPerWord = PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].EEMemBytesPerWord;
					int num9 = 128 / (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].EERdLocations * eememBytesPerWord);
					int num10 = num9 * PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].EERdLocations;
					int num11 = 0;
					uint eeblank = GetEEBlank();
					progressBar1.Value = 0;
					progressBar1.Maximum = PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].EEMem / num10;
					while(true)
					{
						PICkitFunctions.RunScriptUploadNoLen(9, num9);
						Array.Copy(PICkitFunctions.Usb_read_array, 1, array, 0, 64);
						PICkitFunctions.UploadDataNoLen();
						Array.Copy(PICkitFunctions.Usb_read_array, 1, array, 64, 64);
						int num12 = 0;
						for (int j = 0; j < num10; j++)
						{
							int num13 = 0;
							uint num14 = array[num12 + num13++];
							if (num13 < eememBytesPerWord)
								num14 |= (uint)array[num12 + num13++] << 8;
							num12 += num13;
							if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].ProgMemShift > 0)
								num14 = num14 >> 1 & eeblank;
							if (num14 != PICkitFunctions.DeviceBuffers.EEPromMemory[num11++] && !PICkitFunctions.LearnMode)
								goto Block_51;
							if (num11 >= PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].EEMem)
								break;
						}
						progressBar1.PerformStep();
						if (num11 >= PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].EEMem)
							goto Block_55;
					}
					Block_51:
					PICkitFunctions.RunScript(1, 1);
					ConditionalVddOff();
					if (!writeVerify)
						displayStatusWindow.Text = "Verification of EE Data Memory failed at address\n";
					else
						displayStatusWindow.Text = "Programming failed at EE Data address\n";
					if (eeblank == 65535U)
					{
						Label label4 = displayStatusWindow;
						label4.Text += string.Format("0x{0:X4}", (num11 - 1) * 2);
					}
					else
					{
						Label label5 = displayStatusWindow;
						label5.Text += string.Format("0x{0:X4}", num11 - 1);
					}
					statusWindowColor = Constants.StatusColor.red;
					UpdateGUI(true);
					return false;
					Block_55:
					PICkitFunctions.RunScript(1, 1);
					if (PICkitFunctions.LearnMode && PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].ProgMemShift > 0)
						PICkitFunctions.MetaCmd_CHANGE_CHKSM_FRMT(1);
				}
				if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].UserIDWords > 0 && checkBoxProgMemEnabled.Checked)
				{
					Label label6 = displayStatusWindow;
					label6.Text += "UserIDs... ";
					Update();
					PICkitFunctions.RunScript(0, 1);
					if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].UserIDRdPrepScript > 0)
						PICkitFunctions.RunScript(16, 1);
					int userIDBytes = (int)PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].UserIDBytes;
					int num15 = 0;
					int num16 = 0;
					PICkitFunctions.RunScriptUploadNoLen(17, 1);
					Array.Copy(PICkitFunctions.Usb_read_array, 1, array, 0, 64);
					if ((long)(PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].UserIDWords * userIDBytes) > 64)
					{
						PICkitFunctions.UploadDataNoLen();
						Array.Copy(PICkitFunctions.Usb_read_array, 1, array, 64, 64);
					}
					PICkitFunctions.RunScript(1, 1);
					while(true)
					{
						int num17 = 0;
						uint num18 = array[num16 + num17++];
						if (num17 < userIDBytes)
							num18 |= (uint)array[num16 + num17++] << 8;
						if (num17 < userIDBytes)
							num18 |= (uint)array[num16 + num17++] << 16;
						if (num17 < userIDBytes)
							num18 |= (uint)array[num16 + num17++] << 24;
						num16 += num17;
						if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].ProgMemShift > 0)
							num18 = (num18 >> 1 & PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue);
						if (num18 != PICkitFunctions.DeviceBuffers.UserIDs[num15++] && !PICkitFunctions.LearnMode)
							break;
						if (num15 >= (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].UserIDWords)
							goto IL_BAC;
					}
					ConditionalVddOff();
					if (!writeVerify)
						displayStatusWindow.Text = "Verification of User IDs failed.";
					else
						displayStatusWindow.Text = "Programming failed at User IDs.";
					statusWindowColor = Constants.StatusColor.red;
					UpdateGUI(true);
					return false;
				}
				IL_BAC:
				if (!writeVerify && !VerifyConfig(configWords, configLocation))
				{
					return false;
				}
				PICkitFunctions.RunScript(1, 1);
				if (!writeVerify)
				{
					statusWindowColor = Constants.StatusColor.green;
					displayStatusWindow.Text = "Verification Successful.\n";
					ConditionalVddOff();
				}
				UpdateGUI(true);
				return true;
			}
		}

		private bool VerifyConfig(int configWords, int configLocation)
		{
			if (configWords > 0 && configLocation > PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgramMem && checkBoxProgMemEnabled.Checked)
			{
				Label label = displayStatusWindow;
				label.Text += "Config... ";
				Update();
				PICkitFunctions.RunScript(0, 1);
				PICkitFunctions.RunScript(13, 1);
				PICkitFunctions.UploadData();
				PICkitFunctions.RunScript(1, 1);
				int num = 2;
				for (int i = 0; i < configWords; i++)
				{
					uint num2 = PICkitFunctions.Usb_read_array[num++];
					num2 |= (uint)PICkitFunctions.Usb_read_array[num++] << 8;
					if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].ProgMemShift > 0)
						num2 = num2 >> 1 & PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue;
					num2 &= PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[i];
					uint num3 = PICkitFunctions.DeviceBuffers.ConfigWords[i] & PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[i];
					if (i == (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].CPConfig - 1))
					{
						if (enableCodeProtectToolStripMenuItem.Enabled && enableCodeProtectToolStripMenuItem.Checked)
							num3 &= ~(uint)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].CPMask;
						if (enableDataProtectStripMenuItem.Enabled && enableDataProtectStripMenuItem.Checked)
							num3 &= ~(uint)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].DPMask;
					}
					if (num3 != num2 && !PICkitFunctions.LearnMode)
					{
						ConditionalVddOff();
						displayStatusWindow.Text = "Verification of configuration failed.";
						statusWindowColor = Constants.StatusColor.red;
						UpdateGUI(true);
						return false;
					}
				}
			}
			return true;
		}

		private void DownloadPk2Firmware(object sender, EventArgs e)
		{
			if (openFWFile.ShowDialog() == DialogResult.OK)
				DownloadNewFirmware();
		}

		private void DownloadNewFirmware()
		{
			progressBar1.Value = 0;
			progressBar1.Maximum = 2;
			displayStatusWindow.Text = "Downloading New PICkit 2 Operating System.";
			displayStatusWindow.BackColor = Color.SteelBlue;
			Update();
			if (!Pk2BootLoader.ReadHexAndDownload(openFWFile.FileName, ref pk2number))
			{
				displayStatusWindow.Text = "Downloading failed.\nUse Tools > Check Communications to reconnect.";
				displayStatusWindow.BackColor = Color.Salmon;
				downloadPICkit2FirmwareToolStripMenuItem.Enabled = false;
				chkBoxVddOn.Enabled = false;
				numUpDnVdd.Enabled = false;
				deviceToolStripMenuItem.Enabled = false;
				DisableGUIControls();
				return;
			}
			progressBar1.PerformStep();
			displayStatusWindow.Text = "Verifying PICkit 2 Operating System.";
			Update();
			if (!Pk2BootLoader.ReadHexAndVerify(openFWFile.FileName))
			{
				displayStatusWindow.Text = "Operating System verification failed.";
				displayStatusWindow.BackColor = Color.Salmon;
				return;
			}
			if (!PICkitFunctions.BL_WriteFWLoadedKey())
			{
				displayStatusWindow.Text = "Error loading Operating System.";
				displayStatusWindow.BackColor = Color.Salmon;
				return;
			}
			progressBar1.PerformStep();
			displayStatusWindow.Text = "Verification Successful!\nWaiting for PICkit 2 to reset....";
			displayStatusWindow.BackColor = Color.LimeGreen;
			Update();
			PICkitFunctions.BL_Reset();
			Thread.Sleep(5000);
			PICkitFunctions.ResetPk2Number();
			if (!DetectPICkit2(true, true))
			{
				return;
			}
			PICkitFunctions.VddOff();
			LookForPoweredTarget(false);
			if (PICkitFunctions.DetectDevice(16777215, true, chkBoxVddOn.Checked))
			{
				SetGUIVoltageLimits(true);
				displayStatusWindow.Text += "\nPIC Device Found.";
				FullEnableGUIControls();
			}
			else
				PartialEnableGUIControls();
			CheckForPowerErrors();
			UpdateGUI(true);
		}

		private void ProgrammingSpeed(object sender, EventArgs e)
		{
			if (fastProgrammingToolStripMenuItem.Checked)
			{
				PICkitFunctions.SetFastProgramming(true);
				displayStatusWindow.BackColor = SystemColors.Info;
				if (!PICkitFunctions.FamilyIsEEPROM())
				{
					displayStatusWindow.Text = "Fast programming On- Programming operations\n";
					Label label = displayStatusWindow;
					label.Text += "are faster, but less tolerant of loaded ICSP lines.";
					return;
				}
				if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[0] == 1)
				{
					displayStatusWindow.Text = "Fast programming On- 400kHz I2C\n";
					return;
				}
				if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[0] == 4)
				{
					displayStatusWindow.Text = "Fast programming On- FBUS = 100kHz\n";
					return;
				}
				displayStatusWindow.Text = "Fast programming On- 925kHz SCK\n";
				return;
			}
			else
			{
				PICkitFunctions.SetFastProgramming(false);
				displayStatusWindow.BackColor = SystemColors.Info;
				if (!PICkitFunctions.FamilyIsEEPROM())
				{
					displayStatusWindow.Text = "Fast programming Off - Programming operations\n";
					Label label2 = displayStatusWindow;
					label2.Text += "are slower, but more tolerant of loaded ICSP lines.";
					return;
				}
				if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[0] == 1)
				{
					displayStatusWindow.Text = "Fast programming Off - 100kHz I2C\n";
					return;
				}
				if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[0] == 4)
				{
					displayStatusWindow.Text = "Fast programming Off- FBUS = 25kHz\n";
					return;
				}
				displayStatusWindow.Text = "Fast programming Off - 245kHz SCK\n";
				return;
			}
		}

		private void ClickAbout(object sender, EventArgs e)
		{
			DialogAbout dialogAbout = new DialogAbout();
			dialogAbout.ShowDialog();
		}

		private void LaunchUsersGuide(object sender, EventArgs e)
		{
			try
			{
				Process.Start(homeDirectory + "\\PICkit2 User Guide 51553E.pdf");
			}
			catch
			{
				MessageBox.Show("Unable to open User's Guide.");
			}
		}

		private void LaunchReadMe(object sender, EventArgs e)
		{
			try
			{
				Process.Start(homeDirectory + "\\PICkit 2 Readme.txt");
			}
			catch
			{
				MessageBox.Show("Unable to open ReadMe file.");
			}
		}

		private void CodeProtect(object sender, EventArgs e)
		{
			if (enableDataProtectStripMenuItem.Enabled && PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].DPMask == 0)
				enableDataProtectStripMenuItem.Checked = enableCodeProtectToolStripMenuItem.Checked;
			UpdateGUI(true);
		}

		private void DataProtect(object sender, EventArgs e)
		{
			if (enableDataProtectStripMenuItem.Enabled && PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].DPMask == 0)
				enableCodeProtectToolStripMenuItem.Checked = enableDataProtectStripMenuItem.Checked;
			UpdateGUI(true);
		}

		private void WriteOnButton(object sender, EventArgs e)
		{
			if (writeOnPICkitButtonToolStripMenuItem.Checked)
			{
				timerButton.Enabled = true;
				buttonLast = true;
				displayStatusWindow.Text = "Waiting for PICkit 2 button to be pressed....";
				return;
			}
			timerButton.Enabled = false;
		}

		private void TimerGoesOff(object sender, EventArgs e)
		{
			if (!PICkitFunctions.ButtonPressed())
			{
				buttonLast = false;
				return;
			}
			if (buttonLast)
				return;
			buttonLast = true;
			DeviceWrite();
			CheckForPowerErrors();
		}

		private void ConditionalVddOff()
		{
			if (!chkBoxVddOn.Checked)
				PICkitFunctions.VddOff();
		}

		private void ButtonReadExport(object sender, EventArgs e)
		{
			if (PICkitFunctions.FamilyIsKeeloq())
			{
				displayStatusWindow.Text = "Read not supported for this device type.";
				statusWindowColor = Constants.StatusColor.yellow;
				UpdateGUI(false);
				return;
			}
			DeviceRead();
			UpdateGUI(true);
			Refresh();
			saveHexFileDialog.ShowDialog();
		}

		private void MenuVddAuto(object sender, EventArgs e)
		{
			VddAuto();
		}

		private void VddAuto()
		{
			autoDetectToolStripMenuItem.Checked = true;
			forcePICkit2ToolStripMenuItem.Checked = false;
			forceTargetToolStripMenuItem.Checked = false;
			LookForPoweredTarget(false);
		}

		private void MenuVddPk2(object sender, EventArgs e)
		{
			VddPk2();
		}

		private void VddPk2()
		{
			autoDetectToolStripMenuItem.Checked = false;
			forcePICkit2ToolStripMenuItem.Checked = true;
			forceTargetToolStripMenuItem.Checked = false;
			LookForPoweredTarget(false);
		}

		private void MenuVddTarget(object sender, EventArgs e)
		{
			VddTarget();
		}

		private void VddTarget()
		{
			autoDetectToolStripMenuItem.Checked = false;
			forcePICkit2ToolStripMenuItem.Checked = false;
			forceTargetToolStripMenuItem.Checked = true;
			LookForPoweredTarget(false);
		}

		private void DeviceFamilyClick(object sender, ToolStripItemClickedEventArgs e)
		{
			ToolStripMenuItem toolStripMenuItem = (ToolStripMenuItem)e.ClickedItem;
			if (toolStripMenuItem.HasDropDownItems)
				return;
			labelConfig.Enabled = false;
			string a;
			if (toolStripMenuItem.OwnerItem.Equals(deviceToolStripMenuItem))
				a = toolStripMenuItem.Text;
			else
				a = toolStripMenuItem.OwnerItem.Text + "/" + toolStripMenuItem.Text;
			int num = 0;
			while (num < PICkitFunctions.DevFile.Families.Length && !(a == PICkitFunctions.DevFile.Families[num].FamilyName))
				num++;
			for (int i = 1; i < PICkitFunctions.DevFile.Info.NumberParts; i++)
			{
				if (PICkitFunctions.DevFile.PartsList[i].Family == num)
				{
					PICkitFunctions.DevFile.PartsList[0].VddMax = PICkitFunctions.DevFile.PartsList[i].VddMax;
					PICkitFunctions.DevFile.PartsList[0].VddMin = PICkitFunctions.DevFile.PartsList[i].VddMin;
					break;
				}
			}
			FamilySelectLogic(num, true);
		}

		private void FamilySelectLogic(int family, bool updateGUI_OK)
		{
			if (family != PICkitFunctions.GetActiveFamily())
			{
				PICkitFunctions.ActivePart = 0;
				SetGUIVoltageLimits(true);
			}
			else
				SetGUIVoltageLimits(false);
			displayStatusWindow.Text = "";
			if (PICkitFunctions.DevFile.Families[family].PartDetect)
			{
				PICkitFunctions.SetActiveFamily(family);
				if (PreProgrammingCheck(family))
				{
					displayStatusWindow.Text = PICkitFunctions.DevFile.Families[family].FamilyName + " device found.";
					SetGUIVoltageLimits(false);
				}
				comboBoxSelectPart.Visible = false;
				displayDevice.Visible = true;
				if (updateGUI_OK)
					UpdateGUI(true);
			}
			else
			{
				BuildDeviceSelectDropDown(family);
				comboBoxSelectPart.Visible = true;
				comboBoxSelectPart.SelectedIndex = 0;
				displayDevice.Visible = true;
				PICkitFunctions.SetActiveFamily(family);
				if (updateGUI_OK)
					UpdateGUI(true);
				DisableGUIControls();
			}
			displayDataSource.Text = "None (Empty/Erased)";
		}

		private void BuildDeviceSelectDropDown(int family)
		{
			comboBoxSelectPart.Items.Clear();
			comboBoxSelectPart.Items.Add("-Select Part-");
			for (int i = 1; i < PICkitFunctions.DevFile.Info.NumberParts; i++)
			{
				if (PICkitFunctions.DevFile.PartsList[i].Family == family)
					comboBoxSelectPart.Items.Add(PICkitFunctions.DevFile.PartsList[i].PartName);
			}
		}

		private void SelectPart(object sender, EventArgs e)
		{
			if (comboBoxSelectPart.SelectedIndex == 0)
				DisableGUIControls();
			else
			{
				string b = comboBoxSelectPart.SelectedItem.ToString();
				FullEnableGUIControls();
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
			SetGUIVoltageLimits(true);
			displayDataSource.Text = "None (Empty/Erased)";
			if (useLVP && PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].LVPScript > 0)
				toolStripMenuItemLVPEnabled.Checked = true;
			useLVP = false;
			UpdateGUI(true);
		}

		private void AutoDownloadFW(object sender, EventArgs e)
		{
			timerDLFW.Enabled = false;
			displayStatusWindow.Text = "The PICkit 2 Operating System v" + PICkitFunctions.FirmwareVersion + " must be updated.";
			DialogResult dialogResult = MessageBox.Show("PICkit 2 Operating System must be updated\nbefore it can be used with this software\nversion.\n\nClick OK to download a new Operating System.", "Update Operating System", MessageBoxButtons.OKCancel);
			if (dialogResult == DialogResult.OK)
			{
				openFWFile.FileName = "PK2V023200.hex";
				DownloadNewFirmware();
				oldFirmware = false;
				return;
			}
			displayStatusWindow.Text = "The PICkit 2 OS v" + PICkitFunctions.FirmwareVersion + " must be updated.\nUse the Tools menu to download a new OS.";
		}

		private void SaveINI()
		{
			StreamWriter streamWriter = File.CreateText(homeDirectory + "\\PICkit2.ini");
			string text = ";PICkit 2 version 2.61.00 INI file.";
			streamWriter.WriteLine(text);
            DateTime dateTime = DateTime.Now;
            text = ";" + dateTime.Date.ToShortDateString() + " " + dateTime.ToShortTimeString();
			streamWriter.WriteLine(text);
			streamWriter.WriteLine("");
			text = "Y";
			if (toolStripMenuItemManualSelect.Checked)
			{
				text = "N";
				searchOnStartup = false;
			}
			else if (!autoDetectInINI)
				searchOnStartup = true;
			streamWriter.WriteLine("ADET: " + text);
			text = "N";
			if (searchOnStartup)
				text = "Y";
			streamWriter.WriteLine("PDET: " + text);
			if (PICkitFunctions.DevFile.Families == null)
				text = lastFamily;
			else
				text = PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].FamilyName;
			streamWriter.WriteLine("LFAM: " + text);
			text = "N";
			if (verifyOnWriteToolStripMenuItem.Checked)
				text = "Y";
			streamWriter.WriteLine("VRFW: " + text);
			text = "N";
			if (writeOnPICkitButtonToolStripMenuItem.Checked)
				text = "Y";
			streamWriter.WriteLine("WRBT: " + text);
			text = "N";
			if (MCLRtoolStripMenuItem.Checked)
				text = "Y";
			streamWriter.WriteLine("MCLR: " + text);
			if (VppFirstToolStripMenuItem.Checked)
				RestoreVddTarget();
			text = "Auto";
			if (forcePICkit2ToolStripMenuItem.Checked)
				text = "PICkit";
			else if (forceTargetToolStripMenuItem.Checked)
				text = "Target";
			streamWriter.WriteLine("TVDD: " + text);
			text = "N";
			if (fastProgrammingToolStripMenuItem.Checked)
				text = "Y";
			streamWriter.WriteLine("FPRG: " + text);
			text = string.Format("PCLK: {0:G}", slowSpeedICSP);
			streamWriter.WriteLine(text);
			text = "N";
			if (multiWindow)
				comboBoxProgMemView.SelectedIndex = programMemMultiWin.GetViewMode();
			if (comboBoxProgMemView.SelectedIndex == 1)
				text = "Y";
			else if (comboBoxProgMemView.SelectedIndex == 2)
				text = "B";
			streamWriter.WriteLine("PASC: " + text);
			text = "N";
			if (multiWindow)
				comboBoxProgMemView.SelectedIndex = eepromDataMultiWin.GetViewMode();
			if (comboBoxEE.SelectedIndex == 1)
				text = "Y";
			else if (comboBoxEE.SelectedIndex == 2)
				text = "B";
			streamWriter.WriteLine("EASC: " + text);
			text = "N";
			if (allowDataEdits)
				text = "Y";
			streamWriter.WriteLine("EDIT: " + text);
			if (displayRev.Visible)
				streamWriter.WriteLine("REVS: Y");
			text = string.Format("SETV: {0:0.0}", numUpDnVdd.Value);
			streamWriter.WriteLine(text);
			text = "N";
			if (toolStripMenuItemClearBuffersErase.Checked)
				text = "Y";
			streamWriter.WriteLine("CLBF: " + text);
			text = "N";
			if (usePE33)
				text = "Y";
			streamWriter.WriteLine("PE33: " + text);
			text = "N";
			if (usePE24)
				text = "Y";
			streamWriter.WriteLine("PE24: " + text);
			text = "0";
			if (as1BitValueToolStripMenuItem.Checked)
				text = "1";
			else if (asReadOrImportedToolStripMenuItem.Checked)
				text = "R";
			streamWriter.WriteLine("CFGU: " + text);
			text = "N";
			if (toolStripMenuItemLVPEnabled.Checked)
				text = "Y";
			streamWriter.WriteLine("LVPE: " + text);
			text = "N";
			if (deviceVerification)
				text = "Y";
			streamWriter.WriteLine("DVER: " + text);
			streamWriter.WriteLine("HEX1: " + hex1);
			streamWriter.WriteLine("HEX2: " + hex2);
			streamWriter.WriteLine("HEX3: " + hex3);
			streamWriter.WriteLine("HEX4: " + hex4);
			if (selectDeviceFile)
				streamWriter.WriteLine("SDAT: Y");
			if (testMemoryEnabled)
			{
				text = "N";
				if (testMemoryOpen)
					text = "Y";
				streamWriter.WriteLine("TMEN: " + text);
				text = string.Format("TMWD: {0:G}", testMemoryWords);
				streamWriter.WriteLine(text);
				text = "N";
				if (testMemoryImportExport)
					text = "Y";
				streamWriter.WriteLine("TMIE: " + text);
			}
			text = "N";
			if (multiWindow)
				text = "Y";
			streamWriter.WriteLine("MWEN: " + text);
			text = string.Format("MWLX: {0:G}", Location.X);
			streamWriter.WriteLine(text);
			text = string.Format("MWLY: {0:G}", Location.Y);
			streamWriter.WriteLine(text);
			text = "N";
			if (mainWinOwnsMem)
				text = "Y";
			streamWriter.WriteLine("MWFR: " + text);
			text = "N";
			if (multiWinPMemOpen)
				text = "Y";
			streamWriter.WriteLine("PMEN: " + text);
			text = string.Format("PMLX: {0:G}", programMemMultiWin.Location.X);
			streamWriter.WriteLine(text);
			text = string.Format("PMLY: {0:G}", programMemMultiWin.Location.Y);
			streamWriter.WriteLine(text);
			text = string.Format("PMSX: {0:G}", programMemMultiWin.Size.Width);
			streamWriter.WriteLine(text);
			text = string.Format("PMSY: {0:G}", programMemMultiWin.Size.Height);
			streamWriter.WriteLine(text);
			text = "N";
			if (multiWinEEMemOpen)
				text = "Y";
			streamWriter.WriteLine("EEEN: " + text);
			text = string.Format("EELX: {0:G}", eepromDataMultiWin.Location.X);
			streamWriter.WriteLine(text);
			text = string.Format("EELY: {0:G}", eepromDataMultiWin.Location.Y);
			streamWriter.WriteLine(text);
			text = string.Format("EESX: {0:G}", eepromDataMultiWin.Size.Width);
			streamWriter.WriteLine(text);
			text = string.Format("EESY: {0:G}", eepromDataMultiWin.Size.Height);
			streamWriter.WriteLine(text);
			streamWriter.WriteLine("UABD: " + uartWindow.GetBaudRate());
			text = "N";
			if (uartWindow.IsHexMode())
				text = "Y";
			streamWriter.WriteLine("UAHX: " + text);
			streamWriter.WriteLine("UAS1: " + uartWindow.GetStringMacro(1));
			streamWriter.WriteLine("UAS2: " + uartWindow.GetStringMacro(2));
			streamWriter.WriteLine("UAS3: " + uartWindow.GetStringMacro(3));
			streamWriter.WriteLine("UAS4: " + uartWindow.GetStringMacro(4));
			text = "N";
			if (uartWindow.GetAppendCRLF())
				text = "Y";
			streamWriter.WriteLine("UACL: " + text);
			text = "N";
			if (uartWindow.GetWrap())
				text = "Y";
			streamWriter.WriteLine("UAWR: " + text);
			text = "N";
			if (uartWindow.GetEcho())
				text = "Y";
			streamWriter.WriteLine("UAEC: " + text);
			text = "N";
			if (logicWindow.GetModeAnalyzer())
				text = "Y";
			streamWriter.WriteLine("LTAM: " + text);
			text = string.Format("LTZM: {0:G}", logicWindow.GetZoom());
			streamWriter.WriteLine(text);
			text = string.Format("LTT1: {0:G}", logicWindow.GetCh1Trigger());
			streamWriter.WriteLine(text);
			text = string.Format("LTT2: {0:G}", logicWindow.GetCh2Trigger());
			streamWriter.WriteLine(text);
			text = string.Format("LTT3: {0:G}", logicWindow.GetCh3Trigger());
			streamWriter.WriteLine(text);
			text = string.Format("LTTC: {0:G}", logicWindow.GetTrigCount());
			streamWriter.WriteLine(text);
			text = string.Format("LTSR: {0:G}", logicWindow.GetSampleRate());
			streamWriter.WriteLine(text);
			text = string.Format("LTTP: {0:G}", logicWindow.GetTriggerPosition());
			streamWriter.WriteLine(text);
			text = "N";
			if (logicWindow.GetCursorsEnabled())
				text = "Y";
			streamWriter.WriteLine("LTCE: " + text);
			text = string.Format("LTCX: {0:G}", logicWindow.GetXCursorPos());
			streamWriter.WriteLine(text);
			text = string.Format("LTCY: {0:G}", logicWindow.GetYCursorPos());
			streamWriter.WriteLine(text);
			text = "0";
			if (ptgMemory > 0 && ptgMemory <= 5)
			{
				if (ptgMemory == 1)
					text = "1";
				else if (ptgMemory == 2)
					text = "2";
				else if (ptgMemory == 3)
					text = "3";
				else if (ptgMemory == 4)
					text = "4";
				else if (ptgMemory == 5)
					text = "5";
			}
			streamWriter.WriteLine("PTGM: " + text);
			text = "N";
			if (playSuccessWav)
				text = "Y";
			streamWriter.WriteLine("SDSP: " + text);
			text = "N";
			if (playWarningWav)
				text = "Y";
			streamWriter.WriteLine("SDWP: " + text);
			text = "N";
			if (playErrorWav)
				text = "Y";
			streamWriter.WriteLine("SDEP: " + text);
			streamWriter.WriteLine("SDSF: " + successWavFile);
			streamWriter.WriteLine("SDWF: " + warningWavFile);
			streamWriter.WriteLine("SDEF: " + errorWavFile);
			streamWriter.Flush();
			streamWriter.Close();
		}

		private float LoadINI()
		{
			float num = 0f;
			try
			{
				int height = SystemInformation.VirtualScreen.Height;
				int width = SystemInformation.VirtualScreen.Width;
				FileInfo fileInfo = new FileInfo("PICkit2.ini");
				homeDirectory = fileInfo.DirectoryName;
				successWavFile = homeDirectory + successWavFile;
				warningWavFile = homeDirectory + warningWavFile;
				errorWavFile = homeDirectory + errorWavFile;
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
								toolStripMenuItemManualSelect.Checked = true;
								autoDetectInINI = false;
								searchOnStartup = false;
							}
							break;
						case "PDET:":
							if (string.Compare(text.Substring(6, 1), "N") == 0)
								searchOnStartup = false;
							break;
						case "LFAM:":
							lastFamily = text.Substring(6);
							break;
						case "VRFW:":
							if (string.Compare(text.Substring(6, 1), "N") == 0)
								verifyOnWriteToolStripMenuItem.Checked = false;
							break;
						case "WRBT:":
							if (string.Compare(text.Substring(6, 1), "Y") == 0)
							{
								writeOnPICkitButtonToolStripMenuItem.Checked = true;
								timerButton.Enabled = true;
								buttonLast = true;
							}
							break;
						case "MCLR:":
							if (string.Compare(text.Substring(6, 1), "Y") == 0)
							{
								MCLRtoolStripMenuItem.Checked = true;
								checkBoxMCLR.Checked = true;
								PICkitFunctions.HoldMCLR(true);
							}
							break;
						case "TVDD:":
							if (string.Compare(text.Substring(6, 1), "P") == 0)
								VddPk2();
							else if (string.Compare(text.Substring(6, 1), "T") == 0)
								VddTarget();
							break;
						case "FPRG:":
							if (string.Compare(text.Substring(6, 1), "N") == 0)
							{
								fastProgrammingToolStripMenuItem.Checked = false;
								PICkitFunctions.SetFastProgramming(false);
							}
							break;
						case "PCLK:":
							if (text.Length == 7)
								slowSpeedICSP = byte.Parse(text.Substring(6, 1));
							else
								slowSpeedICSP = byte.Parse(text.Substring(6, 2));
							if (slowSpeedICSP < 2)
								slowSpeedICSP = 2;
							if (slowSpeedICSP > 16)
								slowSpeedICSP = 16;
							break;
						case "PASC:":
							if (string.Compare(text.Substring(6, 1), "Y") == 0)
								comboBoxProgMemView.SelectedIndex = 1;
							else if (string.Compare(text.Substring(6, 1), "B") == 0)
								comboBoxProgMemView.SelectedIndex = 2;
							break;
						case "EASC:":
							if (string.Compare(text.Substring(6, 1), "Y") == 0)
								comboBoxEE.SelectedIndex = 1;
							else if (string.Compare(text.Substring(6, 1), "B") == 0)
								comboBoxEE.SelectedIndex = 2;
							break;
						case "EDIT:":
							if (string.Compare(text.Substring(6, 1), "N") == 0)
							{
								allowDataEdits = false;
								calibrateToolStripMenuItem.Visible = false;
							}
							break;
						case "REVS:":
							displayRev.Visible = true;
							break;
						case "SETV:":
							if (text.Length == 9)
							{
								num = float.Parse(text.Substring(6, 3));
								if (num > 5)
									num = 5;
								if ((double)num < 2.5)
									num = 2.5f;
							}
							else
								num = 0;
							break;
						case "CLBF:":
							if (string.Compare(text.Substring(6, 1), "N") == 0)
								toolStripMenuItemClearBuffersErase.Checked = false;
							break;
						case "PE33:":
							if (string.Compare(text.Substring(6, 1), "N") == 0)
								usePE33 = false;
							break;
						case "PE24:":
							if (string.Compare(text.Substring(6, 1), "N") == 0)
								usePE24 = false;
							break;
						case "CFGU:":
							if (string.Compare(text.Substring(6, 1), "1") == 0)
							{
								as0BitValueToolStripMenuItem.Checked = false;
								as1BitValueToolStripMenuItem.Checked = true;
							}
							else if (string.Compare(text.Substring(6, 1), "R") == 0)
							{
								as0BitValueToolStripMenuItem.Checked = false;
								asReadOrImportedToolStripMenuItem.Checked = true;
							}
							break;
						case "LVPE:":
							if (string.Compare(text.Substring(6, 1), "Y") == 0)
								useLVP = true;
							break;
						case "DVER:":
							if (string.Compare(text.Substring(6, 1), "N") == 0)
								deviceVerification = false;
							break;
						case "HEX1:":
							hex1 = text.Substring(6);
							if (hex1.Length > 3)
							{
								hex1ToolStripMenuItem.Visible = true;
								toolStripMenuItem5.Visible = true;
							}
							break;
						case "HEX2:":
							hex2 = text.Substring(6);
							if (hex2.Length > 3)
								hex2ToolStripMenuItem.Visible = true;
							break;
						case "HEX3:":
							hex3 = text.Substring(6);
							if (hex3.Length > 3)
								hex3ToolStripMenuItem.Visible = true;
							break;
						case "HEX4:":
							hex4 = text.Substring(6);
							if (hex4.Length > 3)
								hex4ToolStripMenuItem.Visible = true;
							break;
						case "SDAT:":
							if (string.Compare(text.Substring(6, 1), "Y") == 0)
								selectDeviceFile = true;
							break;
						case "TMEN:":
							testMemoryEnabled = true;
							if (text.Length > 5 && string.Compare(text.Substring(6, 1), "Y") == 0)
								testMemoryOpen = true;
							break;
						case "TMWD:":
							testMemoryWords = int.Parse(text.Substring(6, text.Length - 6));
							if (testMemoryWords < 16)
								testMemoryWords = 16;
							if (testMemoryWords > 1024)
								testMemoryWords = 1024;
							break;
						case "TMIE:":
							if (string.Compare(text.Substring(6, 1), "Y") == 0)
								testMemoryImportExport = true;
							break;
						case "MWEN:":
							if (string.Compare(text.Substring(6, 1), "Y") == 0)
							{
								multiWindow = true;
								viewChanged = true;
							}
							break;
						case "MWLX:":
						{
							int num3 = int.Parse(text.Substring(6, text.Length - 6));
							if (num3 < 0)
								num3 = 0;
							if (num3 > width)
								num3 = width - 75;
							Location = new Point(num3, Location.Y);
							break;
						}
						case "MWLY:":
						{
							int num3 = int.Parse(text.Substring(6, text.Length - 6));
							if (num3 < 0)
								num3 = 0;
							if (num3 > height)
								num3 = height - 75;
							Location = new Point(Location.X, num3);
							break;
						}
						case "MWFR:":
							if (string.Compare(text.Substring(6, 1), "N") == 0)
								mainWinOwnsMem = false;
							break;
						case "PMEN:":
							if (string.Compare(text.Substring(6, 1), "N") == 0)
								multiWinPMemOpen = false;
							break;
						case "PMLX:":
						{
							int num3 = int.Parse(text.Substring(6, text.Length - 6));
							if (num3 < 0)
								num3 = 0;
							if (num3 > width)
								num3 = width - 75;
							programMemMultiWin.Location = new Point(num3, programMemMultiWin.Location.Y);
							break;
						}
						case "PMLY:":
						{
							int num3 = int.Parse(text.Substring(6, text.Length - 6));
							if (num3 < 0)
								num3 = 0;
							if (num3 > height)
								num3 = height - 75;
							programMemMultiWin.Location = new Point(programMemMultiWin.Location.X, num3);
							break;
						}
						case "PMSX:":
						{
							int num3 = int.Parse(text.Substring(6, text.Length - 6));
							if (num3 < 50)
								num3 = 50;
							if (num3 > width)
								num3 = width;
							programMemMultiWin.Size = new Size(num3, programMemMultiWin.Size.Height);
							break;
						}
						case "PMSY:":
						{
							int num3 = int.Parse(text.Substring(6, text.Length - 6));
							if (num3 < 50)
								num3 = 50;
							if (num3 > height)
								num3 = height;
							programMemMultiWin.Size = new Size(programMemMultiWin.Size.Width, num3);
							break;
						}
						case "EEEN:":
							if (string.Compare(text.Substring(6, 1), "N") == 0)
								multiWinEEMemOpen = false;
							break;
						case "EELX:":
						{
							int num3 = int.Parse(text.Substring(6, text.Length - 6));
							if (num3 < 0)
								num3 = 0;
							if (num3 > width)
								num3 = width - 75;
							eepromDataMultiWin.Location = new Point(num3, eepromDataMultiWin.Location.Y);
							break;
						}
						case "EELY:":
						{
							int num3 = int.Parse(text.Substring(6, text.Length - 6));
							if (num3 < 0)
								num3 = 0;
							if (num3 > height)
								num3 = height - 75;
							eepromDataMultiWin.Location = new Point(eepromDataMultiWin.Location.X, num3);
							break;
						}
						case "EESX:":
						{
							int num3 = int.Parse(text.Substring(6, text.Length - 6));
							if (num3 < 50)
								num3 = 50;
							if (num3 > width)
								num3 = width;
							eepromDataMultiWin.Size = new Size(num3, eepromDataMultiWin.Size.Height);
							break;
						}
						case "EESY:":
						{
							int num3 = int.Parse(text.Substring(6, text.Length - 6));
							if (num3 < 50)
								num3 = 50;
							if (num3 > height)
								num3 = height;
							eepromDataMultiWin.Size = new Size(eepromDataMultiWin.Size.Width, num3);
							break;
						}
						case "UABD:":
							uartWindow.SetBaudRate(text.Substring(6));
							break;
						case "UAHX:":
							if (string.Compare(text.Substring(6, 1), "Y") == 0)
								uartWindow.SetModeHex();
							break;
						case "UAS1:":
							uartWindow.SetStringMacro(text.Substring(6), 1);
							break;
						case "UAS2:":
							uartWindow.SetStringMacro(text.Substring(6), 2);
							break;
						case "UAS3:":
							uartWindow.SetStringMacro(text.Substring(6), 3);
							break;
						case "UAS4:":
							uartWindow.SetStringMacro(text.Substring(6), 4);
							break;
						case "UACL:":
							if (string.Compare(text.Substring(6, 1), "N") == 0)
								uartWindow.ClearAppendCRLF();
							break;
						case "UAWR:":
							if (string.Compare(text.Substring(6, 1), "N") == 0)
								uartWindow.ClearWrap();
							break;
						case "UAEC:":
							if (string.Compare(text.Substring(6, 1), "N") == 0)
								uartWindow.ClearEcho();
							break;
						case "LTAM:":
							if (string.Compare(text.Substring(6, 1), "Y") == 0)
								logicWindow.SetModeAnalyzer();
							break;
						case "LTZM:":
						{
							int num3 = int.Parse(text.Substring(6, text.Length - 6));
							if (num3 > 3)
								num3 = 3;
							logicWindow.SetZoom(num3);
							break;
						}
						case "LTT1:":
						{
							int num3 = int.Parse(text.Substring(6, text.Length - 6));
							if (num3 > 5)
								num3 = 5;
							logicWindow.SetCh1Trigger(num3);
							break;
						}
						case "LTT2:":
						{
							int num3 = int.Parse(text.Substring(6, text.Length - 6));
							if (num3 > 5)
								num3 = 5;
							logicWindow.SetCh2Trigger(num3);
							break;
						}
						case "LTT3:":
						{
							int num3 = int.Parse(text.Substring(6, text.Length - 6));
							if (num3 > 5)
								num3 = 5;
							logicWindow.SetCh3Trigger(num3);
							break;
						}
						case "LTTC:":
						{
							int num3 = int.Parse(text.Substring(6, text.Length - 6));
							if (num3 > 256)
								num3 = 256;
							if (num3 < 1)
								num3 = 1;
							logicWindow.SetTrigCount(num3);
							break;
						}
						case "LTSR:":
						{
							int num3 = int.Parse(text.Substring(6, text.Length - 6));
							if (num3 > 7)
								num3 = 7;
							logicWindow.SetSampleRate(num3);
							break;
						}
						case "LTTP:":
						{
							int num3 = int.Parse(text.Substring(6, text.Length - 6));
							if (num3 > 5)
								num3 = 5;
							logicWindow.SetTriggerPosition(num3);
							break;
						}
						case "LTCE:":
							if (string.Compare(text.Substring(6, 1), "Y") == 0)
								logicWindow.SetCursorsEnabled(true);
							break;
						case "LTCX:":
						{
							int num3 = int.Parse(text.Substring(6, text.Length - 6));
							if (num3 > 4095)
								num3 = 4095;
							logicWindow.SetXCursorPos(num3);
							break;
						}
						case "LTCY:":
						{
							int num3 = int.Parse(text.Substring(6, text.Length - 6));
							if (num3 > 4095)
								num3 = 4095;
							logicWindow.SetYCursorPos(num3);
							break;
						}
						case "PTGM:":
							if (string.Compare(text.Substring(6, 1), "1") == 0)
								ptgMemory = 1;
							else if (string.Compare(text.Substring(6, 1), "2") == 0)
								ptgMemory = 2;
							else if (string.Compare(text.Substring(6, 1), "3") == 0)
								ptgMemory = 3;
							else if (string.Compare(text.Substring(6, 1), "4") == 0)
								ptgMemory = 4;
							else if (string.Compare(text.Substring(6, 1), "5") == 0)
								ptgMemory = 5;
							break;
						case "SDSP:":
							if (string.Compare(text.Substring(6, 1), "Y") == 0)
								playSuccessWav = true;
							break;
						case "SDWP:":
							if (string.Compare(text.Substring(6, 1), "Y") == 0)
								playWarningWav = true;
							break;
						case "SDEP:":
							if (string.Compare(text.Substring(6, 1), "Y") == 0)
								playErrorWav = true;
							break;
						case "SDSF:":
							successWavFile = text.Substring(6);
							break;
						case "SDWF:":
							warningWavFile = text.Substring(6);
							break;
						case "SDEF:":
							errorWavFile = text.Substring(6);
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
			hex1ToolStripMenuItem.Text = "&1 " + ShortenHex(hex1);
			hex2ToolStripMenuItem.Text = "&2 " + ShortenHex(hex2);
			hex3ToolStripMenuItem.Text = "&3 " + ShortenHex(hex3);
			hex4ToolStripMenuItem.Text = "&4 " + ShortenHex(hex4);
			return num;
		}

		private string ShortenHex(string fullPath)
		{
			if (fullPath.Length > 42)
				return fullPath.Substring(0, 3) + "..." + fullPath.Substring(fullPath.Length - 36);
			return fullPath;
		}

		private void Hex1Click(object sender, EventArgs e)
		{
			HexImportFromHistory(hex1);
		}

		private void Hex2Click(object sender, EventArgs e)
		{
			HexImportFromHistory(hex2);
		}

		private void Hex3Click(object sender, EventArgs e)
		{
			HexImportFromHistory(hex3);
		}

		private void Hex4Click(object sender, EventArgs e)
		{
			HexImportFromHistory(hex4);
		}

		private void HexImportFromHistory(string filename)
		{
			if (importFileToolStripMenuItem.Enabled && filename.Length > 3)
			{
				openHexFileDialog.FileName = filename;
				ImportHexFileGo();
				UpdateGUI(true);
			}
		}

		private void LaunchLPCDemoGuide(object sender, EventArgs e)
		{
			try
			{
				Process.Start(homeDirectory + "\\Low Pin Count User Guide 51556a.pdf");
			}
			catch
			{
				MessageBox.Show("Unable to open\nLPC Demo Board User's Guide.");
			}
		}

		private void UG44pinToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				Process.Start(homeDirectory + "\\44-Pin Demo Board User Guide 41296b.pdf");
			}
			catch
			{
				MessageBox.Show("Unable to open\n44-Pin Demo Board User's Guide.");
			}
		}

		private void MemorySelectVerify(object sender, EventArgs e)
		{
			if (sender.Equals(checkBoxProgMemEnabled))
				checkBoxProgMemEnabledAlt.Checked = checkBoxProgMemEnabled.Checked;
			if (sender.Equals(checkBoxProgMemEnabledAlt))
				checkBoxProgMemEnabled.Checked = checkBoxProgMemEnabledAlt.Checked;
			if (sender.Equals(checkBoxEEMem))
				checkBoxEEDATAMemoryEnabledAlt.Checked = checkBoxEEMem.Checked;
			if (sender.Equals(checkBoxEEDATAMemoryEnabledAlt))
				checkBoxEEMem.Checked = checkBoxEEDATAMemoryEnabledAlt.Checked;
			if (!checkBoxProgMemEnabled.Checked && !checkBoxEEMem.Checked)
			{
				MessageBox.Show("At least one memory region\nmust be selected.");
				if (sender.Equals(checkBoxProgMemEnabled) || sender.Equals(checkBoxProgMemEnabledAlt))
				{
					checkBoxProgMemEnabled.Checked = true;
					checkBoxProgMemEnabledAlt.Checked = true;
				}
				else
				{
					checkBoxEEMem.Checked = true;
					checkBoxEEDATAMemoryEnabledAlt.Checked = true;
				}
			}
			UpdateGUI(false);
		}

		private void SetOSCCAL(object sender, EventArgs e)
		{
			if (setOSCCALToolStripMenuItem.Enabled)
			{
				SetOSCCAL setOSCCAL = new SetOSCCAL();
				setOSCCAL.ShowDialog();
				if (setOSCCALValue)
				{
					EraseDeviceAll(true, new uint[0]);
					Label label = this.displayStatusWindow;
					label.Text += "\nOSCCAL Set.";
				}
				setOSCCALValue = false;
				UpdateGUI(true);
			}
		}

		private void PICkit2OnTheWeb(object sender, EventArgs e)
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

		private void TroubleshhotToolStripMenuItem_Click(object sender, EventArgs e)
		{
			DialogTroubleshoot dialogTroubleshoot = new DialogTroubleshoot();
			dialogTroubleshoot.ShowDialog();
			chkBoxVddOn.Checked = false;
			if (selfPoweredTarget)
				PICkitFunctions.ForceTargetPowered();
		}

		private void MCLRtoolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (MCLRtoolStripMenuItem.Checked)
			{
				checkBoxMCLR.Checked = false;
				MCLRtoolStripMenuItem.Checked = false;
				PICkitFunctions.HoldMCLR(false);
				return;
			}
			checkBoxMCLR.Checked = true;
			MCLRtoolStripMenuItem.Checked = true;
			PICkitFunctions.HoldMCLR(true);
		}

		private void ToolStripMenuItemTestMemory_Click(object sender, EventArgs e)
		{
			if (testMemoryEnabled && !testMemoryOpen)
			{
				OpenTestMemory();
			}
		}

		private void OpenTestMemory()
		{
			formTestMem = new FormTestMemory();
			formTestMem.UpdateMainFormGUI = new DelegateUpdateGUI(this.ExtCallUpdateGUI);
			formTestMem.CallMainFormEraseWrCal = new DelegateWriteCal(this.ExtCallCalEraseWrite);
			formTestMem.Show();
		}

		private void CheckBoxAutoImportWrite_Click(object sender, EventArgs e)
		{
			if (!checkBoxAutoImportWrite.Checked)
				displayStatusWindow.Text = "Exited Auto-Import-Write mode.";
			if (checkBoxAutoImportWrite.Checked)
			{
				importGo = false;
				if (hex1.Length > 3)
					openHexFileDialog.FileName = hex1;
				openHexFileDialog.ShowDialog();
				if (importGo)
				{
					UpdateGUI(true);
					Refresh();
					if (DeviceWrite())
					{
						importFileToolStripMenuItem.Enabled = false;
						exportFileToolStripMenuItem.Enabled = false;
						readDeviceToolStripMenuItem.Enabled = false;
						writeDeviceToolStripMenuItem.Enabled = false;
						verifyToolStripMenuItem.Enabled = false;
						eraseToolStripMenuItem.Enabled = false;
						blankCheckToolStripMenuItem.Enabled = false;
						writeOnPICkitButtonToolStripMenuItem.Enabled = false;
						picKit2GoToolStripMenuItem.Enabled = false;
						setOSCCALToolStripMenuItem.Enabled = false;
						buttonRead.Enabled = false;
						buttonWrite.Enabled = false;
						buttonVerify.Enabled = false;
						buttonErase.Enabled = false;
						buttonBlankCheck.Enabled = false;
						dataGridProgramMemory.Enabled = false;
						dataGridViewEEPROM.Enabled = false;
						buttonExportHex.Enabled = false;
						deviceToolStripMenuItem.Enabled = false;
						checkCommunicationToolStripMenuItem.Enabled = false;
						troubleshhotToolStripMenuItem.Enabled = false;
						downloadPICkit2FirmwareToolStripMenuItem.Enabled = false;
						Label label = displayStatusWindow;
						label.Text += "Waiting for file update...  (Click button again to exit)";
						timerAutoImportWrite.Enabled = true;
					}
					else
						importGo = false;
				}
				else
					UpdateGUI(true);
				if (!importGo)
					checkBoxAutoImportWrite.Checked = false;
			}
		}

		private void CheckBoxAutoImportWrite_Changed(object sender, EventArgs e)
		{
			if (!checkBoxAutoImportWrite.Checked || !importGo)
			{
				importFileToolStripMenuItem.Enabled = true;
				exportFileToolStripMenuItem.Enabled = true;
				readDeviceToolStripMenuItem.Enabled = true;
				writeDeviceToolStripMenuItem.Enabled = true;
				verifyToolStripMenuItem.Enabled = true;
				eraseToolStripMenuItem.Enabled = true;
				blankCheckToolStripMenuItem.Enabled = true;
				writeOnPICkitButtonToolStripMenuItem.Enabled = true;
				picKit2GoToolStripMenuItem.Enabled = true;
				setOSCCALToolStripMenuItem.Enabled = true;
				buttonRead.Enabled = true;
				buttonWrite.Enabled = true;
				buttonVerify.Enabled = true;
				buttonErase.Enabled = true;
				buttonBlankCheck.Enabled = true;
				dataGridProgramMemory.Enabled = true;
				dataGridViewEEPROM.Enabled = true;
				buttonExportHex.Enabled = true;
				deviceToolStripMenuItem.Enabled = true;
				checkCommunicationToolStripMenuItem.Enabled = true;
				troubleshhotToolStripMenuItem.Enabled = true;
				downloadPICkit2FirmwareToolStripMenuItem.Enabled = true;
				timerAutoImportWrite.Enabled = false;
				FLASHWINFO flashwinfo = default;
				flashwinfo.cbSize = (ushort)Marshal.SizeOf(flashwinfo);
				flashwinfo.hwnd = Handle;
				flashwinfo.dwFlags = 14U;
				flashwinfo.uCount = ushort.MaxValue;
				flashwinfo.dwTimeout = 0U;
				FlashWindowEx(ref flashwinfo);
				if (WindowState == FormWindowState.Minimized)
					WindowState = FormWindowState.Normal;
			}
		}

		private void TimerAutoImportWrite_Tick(object sender, EventArgs e)
		{
			FileInfo fileInfo = new FileInfo(openHexFileDialog.FileName);
			if (ImportExportHex.LastWriteTime != fileInfo.LastWriteTime)
			{
				if (DeviceWrite())
				{
					importFileToolStripMenuItem.Enabled = false;
					exportFileToolStripMenuItem.Enabled = false;
					readDeviceToolStripMenuItem.Enabled = false;
					writeDeviceToolStripMenuItem.Enabled = false;
					verifyToolStripMenuItem.Enabled = false;
					eraseToolStripMenuItem.Enabled = false;
					blankCheckToolStripMenuItem.Enabled = false;
					writeOnPICkitButtonToolStripMenuItem.Enabled = false;
					picKit2GoToolStripMenuItem.Enabled = false;
					setOSCCALToolStripMenuItem.Enabled = false;
					buttonRead.Enabled = false;
					buttonWrite.Enabled = false;
					buttonVerify.Enabled = false;
					buttonErase.Enabled = false;
					buttonBlankCheck.Enabled = false;
					dataGridProgramMemory.Enabled = false;
					dataGridViewEEPROM.Enabled = false;
					buttonExportHex.Enabled = false;
					deviceToolStripMenuItem.Enabled = false;
					checkCommunicationToolStripMenuItem.Enabled = false;
					troubleshhotToolStripMenuItem.Enabled = false;
					downloadPICkit2FirmwareToolStripMenuItem.Enabled = false;
					Label label = displayStatusWindow;
					label.Text += "Waiting for file update...  (Click button again to exit)";
					return;
				}
				timerAutoImportWrite.Enabled = false;
				checkBoxAutoImportWrite.Checked = false;
			}
		}

		private void ButtonShowIDMem_Click(object sender, EventArgs e)
		{
			if (!DialogUserIDs.IDMemOpen)
			{
				dialogIDMemory = new DialogUserIDs();
				dialogIDMemory.Show();
			}
		}

		private uint GetEEBlank()
		{
			uint result = 255;
			if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].EEMemAddressIncrement > 1)
				result = 65535;
			if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue == 4095)
				result = 4095;
			return result;
		}

		private void RestoreVddTarget()
		{
			if (vddTargetSave == Constants.VddTargetSelect.auto)
			{
				VddAuto();
				return;
			}
			if (vddTargetSave == Constants.VddTargetSelect.pickit2)
			{
				VddPk2();
				return;
			}
			VddTarget();
		}

		private void VppFirstToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (!VppFirstToolStripMenuItem.Checked)
			{
				PICkitFunctions.ClearVppFirstProgramEntry();
				if (toolStripMenuItemManualSelect.Checked)
				{
					PICkitFunctions.PrepNewPart(false);
				}
				if (!toolStripMenuItemLVPEnabled.Checked)
				{
					displayStatusWindow.Text = "Normal programming mode entry.";
				}
				targetPowerToolStripMenuItem.Enabled = true;
				RestoreVddTarget();
				return;
			}
			if (toolStripMenuItemLVPEnabled.Checked)
			{
				string text = PICkitFunctions.DevFile.Scripts[PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].LVPScript - 1].ScriptName;
				text = text.Substring(text.Length - 2);
				if (text == "HV")
					MessageBox.Show("'Use High Voltage Program Entry' is enabled.\n\nVPP First Program Entry may not be used\nwhile that option is enabled.", "Use VPP First Program Entry");
				else
					MessageBox.Show("'Use LVP Program Entry' is enabled.\n\nVPP First Program Entry may not be used\nwhile that option is enabled.", "Use VPP First Program Entry");
				VppFirstToolStripMenuItem.Checked = false;
				return;
			}
			PICkitFunctions.SetVPPFirstProgramEntry();
			displayStatusWindow.Text = "VPP First programming mode entry set.\nTo use, PICkit 2 MUST supply VDD to target.";
			if (toolStripMenuItemManualSelect.Checked)
				PICkitFunctions.PrepNewPart(false);
			if (autoDetectToolStripMenuItem.Checked)
				vddTargetSave = Constants.VddTargetSelect.auto;
			else if (forcePICkit2ToolStripMenuItem.Checked)
				vddTargetSave = Constants.VddTargetSelect.pickit2;
			else
				vddTargetSave = Constants.VddTargetSelect.target;
			VddPk2();
			targetPowerToolStripMenuItem.Enabled = false;
		}

		private bool EEPROMWrite(bool eraseWrite)
		{
			if (!PreProgrammingCheck(PICkitFunctions.GetActiveFamily()))
				return false;
			UpdateGUI(false);
			Update();
			if (checkImportFile && !eraseWrite)
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
						UpdateGUI(true);
						return false;
					}
				}
			}
			PICkitFunctions.VddOn();
			if (eraseWrite)
				displayStatusWindow.Text = "Erasing device:\n";
			else
				displayStatusWindow.Text = "Writing device:\n";
			Update();
			int programMem = (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgramMem;
			if (checkBoxProgMemEnabled.Checked)
			{
				PICkitFunctions.RunScript(0, 1);
				Label label = displayStatusWindow;
				label.Text += "EEPROM... ";
				Update();
				progressBar1.Value = 0;
				int num = 3;
				if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[0] == 4)
					num = 4;
				int progMemWrWords = (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgMemWrWords;
				int bytesPerLocation = (int)PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BytesPerLocation;
				int num2 = 256;
				if (programMem < num2)
					num2 = programMem + programMem / (progMemWrWords * bytesPerLocation) * num;
				if (num2 > 256)
					num2 = 256;
				int num3 = num2 / (progMemWrWords * bytesPerLocation + num);
				int num4 = num3 * progMemWrWords;
				int num5 = 0;
				progressBar1.Maximum = programMem / num4;
				byte[] array = new byte[256];
				if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgMemWrPrepScript != 0)
					PICkitFunctions.RunScript(6, 1);
				while(true)
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
							int num7 = EEPROM24BitAddress(num5, false);
							if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[0] == 4)
								array[num6++] = 150;
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
					if ((PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[0] == 1 || PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[0] == 4) && EEPROM_CheckBusErrors())
					{
						break;
					}
					progressBar1.PerformStep();
					if (num5 >= programMem)
						goto IL_3B2;
				}
				return false;
			}
			IL_3B2:
			PICkitFunctions.RunScript(1, 1);
			bool flag = true;
			if (verifyOnWriteToolStripMenuItem.Checked && !eraseWrite)
			{
				if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[0] == 4)
					ConditionalVddOff();
				flag = DeviceVerify(true, programMem - 1, false);
			}
			ConditionalVddOff();
			if (flag && !eraseWrite)
			{
				statusWindowColor = Constants.StatusColor.green;
				displayStatusWindow.Text = "Programming Successful.\n";
				UpdateGUI(true);
				return true;
			}
			return flag;
		}

		private int EEPROM24BitAddress(int wordsWritten, bool setReadBit)
		{
			if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[0] == 1)
			{
				int num = PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[3];
				int num2 = wordsWritten & PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[1] & 65535;
				int num3 = wordsWritten >> PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[2];
				num3 <<= 17 + num;
				if (num > 0)
				{
					if (checkBoxA0CS.Checked)
						num3 |= 131072;
					if (checkBoxA1CS.Checked)
						num3 |= 262144;
					if (checkBoxA2CS.Checked)
						num3 |= 524288;
				}
				num2 += (num3 & 917504) + 10485760;
				if (setReadBit)
					num2 |= 65536;
				return num2;
			}
			if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[0] == 2)
			{
				int num4;
				if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgramMem <= 65536)
				{
					num4 = wordsWritten & PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[1] & 65535;
					int num5 = wordsWritten >> PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[2];
					num5 <<= 19;
					num4 += (num5 & 524288) + 131072;
					if (setReadBit)
						num4 |= 65536;
				}
				else
					num4 = wordsWritten;
				return num4;
			}
			if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[0] == 3)
			{
				int num6 = 5;
				int num7 = wordsWritten & PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[1] & 65535;
				if (setReadBit)
					num6 = 6;
				num6 <<= PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[2];
				return num7 | num6;
			}
			if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[0] == 4)
			{
				int num8 = wordsWritten & PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[1] & 65535;
				if (setReadBit)
					num8 |= 196608;
				else
					num8 |= 7077888;
				return num8;
			}
			return 0;
		}

		private bool EEPROM_CheckBusErrors()
		{
			if (PICkitFunctions.BusErrorCheck())
			{
				PICkitFunctions.RunScript(1, 1);
				ConditionalVddOff();
				if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[0] == 4)
					displayStatusWindow.Text = "UNI/O Bus Error (NoSAK) - Aborted.\n";
				else
					displayStatusWindow.Text = "I2C Bus Error (No Acknowledge) - Aborted.\n";
				statusWindowColor = Constants.StatusColor.yellow;
				this.UpdateGUI(true);
				return true;
			}
			return false;
		}

		private void CalibrateToolStripMenuItem_Click(object sender, EventArgs e)
		{
			DialogCalibrate dialogCalibrate = new DialogCalibrate();
			dialogCalibrate.ShowDialog();
			chkBoxVddOn.Checked = false;
			if (selfPoweredTarget)
				PICkitFunctions.ForceTargetPowered();
			DetectPICkit2(true, true);
		}

		private void UARTtoolStripMenuItem_Click(object sender, EventArgs e)
		{
			timerButton.Enabled = false;
			MCLRtoolStripMenuItem.Checked = false;
			checkBoxMCLR.Checked = false;
			uartWindow.SetVddBox(numUpDnVdd.Enabled, chkBoxVddOn.Checked);
			if (multiWindow)
			{
				programMemMultiWin.Hide();
				eepromDataMultiWin.Hide();
			}
			Hide();
			uartWindow.ShowDialog();
			Show();
			if (multiWindow)
			{
				if (multiWinPMemOpen)
					programMemMultiWin.Show();
				if (multiWinEEMemOpen && PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].EEMem > 0)
					eepromDataMultiWin.Show();
				Focus();
			}
			if (!selfPoweredTarget)
				PICkitFunctions.ForcePICkitPowered();
			if (writeOnPICkitButtonToolStripMenuItem.Checked)
			{
				buttonLast = true;
				timerButton.Enabled = true;
			}
		}

		private void ToolStripMenuItemSingleWindow_Click(object sender, EventArgs e)
		{
			if (multiWindow)
				viewChanged = true;
			multiWindow = false;
			UpdateGUI(true);
		}

		private void ToolStripMenuItemMultiWindow_Click(object sender, EventArgs e)
		{
			if (!multiWindow)
				viewChanged = true;
			multiWindow = true;
			UpdateGUI(true);
		}

		private void ToolStripMenuItemShowProgramMemory_Click(object sender, EventArgs e)
		{
			if (multiWinPMemOpen)
			{
				multiWinPMemOpen = false;
				toolStripMenuItemShowProgramMemory.Checked = false;
				programMemMultiWin.Hide();
			}
			else
			{
				multiWinPMemOpen = true;
				toolStripMenuItemShowProgramMemory.Checked = true;
				programMemMultiWin.Show();
			}
			Focus();
		}

		private void ToolStripMenuItemShowEEPROMData_Click(object sender, EventArgs e)
		{
			if (multiWinEEMemOpen)
			{
				multiWinEEMemOpen = false;
				toolStripMenuItemShowEEPROMData.Checked = false;
				eepromDataMultiWin.Hide();
			}
			else
			{
				multiWinEEMemOpen = true;
				toolStripMenuItemShowEEPROMData.Checked = true;
				eepromDataMultiWin.Show();
			}
			Focus();
		}

		private void FormPICkit2_Move(object sender, EventArgs e)
		{
			if (WindowState != FormWindowState.Minimized)
			{
				if (multiWindow && mainWinOwnsMem)
				{
					int num = Location.X - lastLoc.X;
					int num2 = Location.Y - lastLoc.Y;
					int height = SystemInformation.VirtualScreen.Height;
					int width = SystemInformation.VirtualScreen.Width;
					int num3 = programMemMultiWin.Location.X + num;
					int num4 = programMemMultiWin.Location.Y + num2;
					if (num3 + 75 > width)
						num3 = width - 75;
					if (num3 < 0)
						num3 = 0;
					if (num4 + 75 > height)
						num4 = height - 75;
					if (num4 < 0)
						num4 = 0;
					if (programMemMultiWin.WindowState != FormWindowState.Maximized && programMemMultiWin.WindowState != FormWindowState.Minimized)
						programMemMultiWin.Location = new Point(num3, num4);
					num3 = eepromDataMultiWin.Location.X + num;
					num4 = eepromDataMultiWin.Location.Y + num2;
					if (num3 + 75 > width)
						num3 = width - 75;
					if (num3 < 0)
						num3 = 0;
					if (num4 + 75 > height)
						num4 = height - 75;
					if (num4 < 0)
						num4 = 0;
					if (eepromDataMultiWin.WindowState != FormWindowState.Maximized && eepromDataMultiWin.WindowState != FormWindowState.Minimized)
						eepromDataMultiWin.Location = new Point(num3, num4);
				}
				lastLoc = Location;
			}
		}

		private void PICkit2GoToolStripMenuItem_Click(object sender, EventArgs e)
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
			if (!checkBoxEEMem.Checked && checkBoxEEMem.Enabled && PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgMemEraseScript == 0)
			{
				MessageBox.Show("PICkit 2 Programmer-To-Go does not support\npreserving EEPROM on devices requiring a\nRead/Restore operation.\n\nThe entire device must be programmed.", "Programmer-To-Go");
				return;
			}
			if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue > 16383)
			{
				int bytesPerLocation = PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BytesPerLocation;
				int num = (int)(PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigAddr / PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].ProgMemHexBytes);
				int configWords = PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigWords;
				int num2 = PICkitFunctions.DeviceBuffers.ProgramMemory.Length;
				if (num < PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgramMem && configWords > 0)
					num2 -= configWords + 1;
				int progMemWrWords = PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgMemWrWords;
				int num3 = 256 / (progMemWrWords * bytesPerLocation);
				int num4 = num3 * progMemWrWords;
				num2 = PICkitFunctions.FindLastUsedInBuffer(PICkitFunctions.DeviceBuffers.ProgramMemory, PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue, num2 - 1);
				int num5 = (num2 + 1) / num4;
				if ((num2 + 1) % num4 > 0)
					num5++;
				num2 = num5 * num4;
				if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue > 65535)
				{
					float num6 = 1.23f;
					if (PICkitFunctions.FamilyIsdsPIC30())
						num6 = 1.26f;
					num2 = (int)(num2 * num6);
				}
				else
				{
					float num7 = 1.22f;
					if (PICkitFunctions.FamilyIsPIC18J())
						num7 = 1.17f;
					num2 = (int)(num2 * num7);
				}
				num2 *= bytesPerLocation;
				int num8 = 131072;
				if (ptgMemory >= 1)
					num8 = 131072 * (2 << (ptgMemory - 1));
				if (num2 > num8)
				{
					if (ptgMemory > 0)
					{
						MessageBox.Show("The data in the buffer is too large\nto be downloaded to PICkit 2.\n\nIt cannot be used with Programmer-To-Go.", "Programmer-To-Go");
						return;
					}
					MessageBox.Show("The data in the buffer is too large\nto be downloaded to PICkit 2.\n\nSee section 3.1 of the Programmer-To-Go\nUser Guide for information on increasing\nthe PICkit 2 memory.", "Programmer-To-Go");
					return;
				}
			}
			if (VppFirstToolStripMenuItem.Checked && VppFirstToolStripMenuItem.Enabled && (float)numUpDnVdd.Value < PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].VddErase && PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].DebugRowEraseScript == 0)
			{
				MessageBox.Show("VPP First Program Entry selected:\nPICkit 2 must power target.\n\nHowever, VDD box setpoint is below the\nminimum Erase voltage for this part.", "Programmer-To-Go");
				return;
			}
			timerButton.Enabled = false;
            DialogPK2Go dialogPK2Go = new DialogPK2Go
            {
                vddVolts = (float)numUpDnVdd.Value
            };
            if (multiWindow)
				dialogPK2Go.dataSource = ShortenHex(displayDataSource.Text);
			else
				dialogPK2Go.dataSource = displayDataSource.Text;
			if (enableCodeProtectToolStripMenuItem.Checked || enableDataProtectStripMenuItem.Checked)
			{
				if (enableDataProtectStripMenuItem.Checked)
					dialogPK2Go.dataProtect = true;
				if (enableCodeProtectToolStripMenuItem.Checked)
					dialogPK2Go.codeProtect = true;
			}
			dialogPK2Go.writeProgMem = checkBoxProgMemEnabled.Checked;
			dialogPK2Go.writeEEPROM = checkBoxEEMem.Checked;
			if (verifyOnWriteToolStripMenuItem.Checked)
				dialogPK2Go.verifyDevice = true;
			if (VppFirstToolStripMenuItem.Enabled && VppFirstToolStripMenuItem.Checked)
				dialogPK2Go.vppFirst = true;
			if (fastProgrammingToolStripMenuItem.Enabled && !fastProgrammingToolStripMenuItem.Checked)
				dialogPK2Go.fastProgramming = false;
			dialogPK2Go.icspSpeedSlow = slowSpeedICSP;
			if (MCLRtoolStripMenuItem.Enabled && MCLRtoolStripMenuItem.Checked)
				dialogPK2Go.holdMCLR = true;
			dialogPK2Go.SetPTGMemory(ptgMemory);
			dialogPK2Go.PICkit2WriteGo = new DelegateWrite(ExtCallWrite);
			dialogPK2Go.OpenProgToGoGuide = new DelegateOpenProgToGoGuide(OpenProgToGoUserGuide);
			bool flag = usePE33;
			usePE33 = false;
			bool flag2 = usePE24;
			usePE24 = false;
			dialogPK2Go.ShowDialog();
			usePE33 = flag;
			usePE24 = flag2;
			if (!selfPoweredTarget)
				PICkitFunctions.ForcePICkitPowered();
			else
				PICkitFunctions.ForcePICkitPowered();
			if (writeOnPICkitButtonToolStripMenuItem.Checked)
			{
				buttonLast = true;
				timerButton.Enabled = true;
			}
		}

		private void ToolStripMenuItemManualSelect_Click(object sender, EventArgs e)
		{
			ManualAutoSelectToggle(true);
		}

		private void ManualAutoSelectToggle(bool updateGUI_OK)
		{
			if (toolStripMenuItemManualSelect.Checked)
			{
				for (int i = 0; i < PICkitFunctions.DevFile.Families.Length; i++)
					PICkitFunctions.DevFile.Families[i].PartDetect = false;
			}
			else
			{
				for (int j = 0; j < PICkitFunctions.DevFile.Families.Length; j++)
				{
					if (PICkitFunctions.DevFile.Families[j].DeviceIDMask > 0U)
						PICkitFunctions.DevFile.Families[j].PartDetect = true;
				}
				toolStripMenuItemLVPEnabled.Checked = false;
			}
			FamilySelectLogic(PICkitFunctions.GetActiveFamily(), updateGUI_OK);
		}

		private void ToolStripMenuItemProgToGo_Click(object sender, EventArgs e)
		{
			OpenProgToGoUserGuide();
		}

		public void OpenProgToGoUserGuide()
		{
			try
			{
				Process.Start(homeDirectory + "\\Programmer-To-Go User Guide.pdf");
			}
			catch
			{
				MessageBox.Show("Unable to open Programmer-To-Go Guide.");
			}
		}

		private void ToolStripMenuItemLogicTool_Click(object sender, EventArgs e)
		{
			timerButton.Enabled = false;
			MCLRtoolStripMenuItem.Checked = false;
			checkBoxMCLR.Checked = false;
			logicWindow.SetVddBox(numUpDnVdd.Enabled, chkBoxVddOn.Checked);
			if (multiWindow)
			{
				programMemMultiWin.Hide();
				eepromDataMultiWin.Hide();
			}
			Hide();
			logicWindow.ShowDialog();
			Show();
			if (multiWindow)
			{
				if (multiWinPMemOpen)
					programMemMultiWin.Show();
				if (multiWinEEMemOpen && PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].EEMem > 0)
					eepromDataMultiWin.Show();
				Focus();
			}
			if (writeOnPICkitButtonToolStripMenuItem.Checked)
			{
				buttonLast = true;
				timerButton.Enabled = true;
			}
		}

		private void ContextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
		{
			if (e.ClickedItem.Text == "Select All")
			{
				if (dataGridProgramMemory.ContainsFocus)
				{
					dataGridProgramMemory.SelectAll();
					return;
				}
				if (dataGridViewEEPROM.ContainsFocus)
				{
					dataGridViewEEPROM.SelectAll();
					return;
				}
			}
			else if (e.ClickedItem.Text == "Copy")
			{
				if (dataGridProgramMemory.ContainsFocus)
				{
					Clipboard.SetDataObject(dataGridProgramMemory.GetClipboardContent());
					return;
				}
				if (dataGridViewEEPROM.ContainsFocus)
					Clipboard.SetDataObject(dataGridViewEEPROM.GetClipboardContent());
			}
		}

		private void DataGridProgramMemory_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
				dataGridProgramMemory.Focus();
		}

		private void DataGridViewEEPROM_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
				dataGridViewEEPROM.Focus();
		}

		private void ToolStripMenuItemLogicToolUG_Click(object sender, EventArgs e)
		{
			try
			{
				Process.Start(homeDirectory + "\\Logic Tool User Guide.pdf");
			}
			catch
			{
				MessageBox.Show("Unable to open Logic Tool User Guide.");
			}
		}

		private void CalAutoRegenerateToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (setOSCCALToolStripMenuItem.Enabled)
			{
				if (MessageBox.Show("Regenerating the OSCCAL value\nwill completely erase this\npart.\n\nAre you sure you wish to\ncontinue?", "Regenerate OSCCAL", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
					return;
				if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue == 4095)
				{
					short num = 0;
					float num2 = 0;
					verifyOSCCALValue = false;
					for (int i = 0; i < 5; i++)
					{
						float num3 = (1 - 400 / num2) / 0.0057f + 0.5f;
						num += (short)num3;
						if (num < -64 || num > 63)
						{
							ConditionalVddOff();
							EraseDeviceAll(false, new uint[0]);
							verifyOSCCALValue = true;
							UpdateGUI(true);
							MessageBox.Show("Regenerating OSCCAL Failed!\n\nCalibration out of range.", "Regenerate OSCCAL");
							return;
						}
						PICkitFunctions.ResetBuffers();
						PICkitFunctions.DeviceBuffers.ProgramMemory[0] = Constants.BASELINE_CAL[0] | (uint)(num << 1 & 255);
						PICkitFunctions.DeviceBuffers.ConfigWords[0] = PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[6];
						for (int j = 1; j < Constants.BASELINE_CAL.Length; j++)
							PICkitFunctions.DeviceBuffers.ProgramMemory[j] = Constants.BASELINE_CAL[j];
						if (!DeviceWrite())
						{
							PICkitFunctions.ResetBuffers();
							verifyOSCCALValue = true;
							UpdateGUI(true);
							MessageBox.Show("Regenerating OSCCAL Failed!\n\nUnable to program part.", "Regenerate OSCCAL");
							return;
						}
						PICkitFunctions.VddOn();
						for (int k = 0; k < 3; k++)
						{
							num2 = PICkitFunctions.MeasurePGDPulse();
							if (num2 < 695 && num2 > 10)
								break;
							if (k == 2)
							{
								ConditionalVddOff();
								EraseDeviceAll(false, new uint[0]);
								verifyOSCCALValue = true;
								MessageBox.Show("Regenerating OSCCAL Failed!\n\nUnable to connect to\ncalibration executive.", "Regenerate OSCCAL");
								UpdateGUI(true);
								return;
							}
						}
						ConditionalVddOff();
						float num4 = 404;
						if (i == 4)
							num4 = 406;
						float num5 = 396;
						if (i == 4)
							num4 = 394;
						if (num2 > num5 && num2 < num4)
						{
							PICkitFunctions.DeviceBuffers.OSCCAL = PICkitFunctions.DeviceBuffers.ProgramMemory[0];
							EraseDeviceAll(true, new uint[0]);
							verifyOSCCALValue = true;
							MessageBox.Show("Success!\n\nOSSCAL Regenerated and\nwritten to device.", "Regenerate OSCCAL");
							UpdateGUI(true);
							return;
						}
					}
				}
				else
				{
					short num6 = 32;
					float num7 = 0;
					verifyOSCCALValue = false;
					for (int l = 0; l < 5; l++)
					{
						float num8 = (1 - 400 / num7) / 0.007f + 0.5f;
						num6 += (short)num8;
						if (num6 < 0 || num6 > 63)
						{
							ConditionalVddOff();
							EraseDeviceAll(false, new uint[0]);
							verifyOSCCALValue = true;
							UpdateGUI(true);
							MessageBox.Show("Regenerating OSCCAL Failed!\n\nCalibration out of range.", "Regenerate OSCCAL");
							return;
						}
						PICkitFunctions.ResetBuffers();
						PICkitFunctions.DeviceBuffers.ProgramMemory[0] = Constants.MR16F676FAM_CAL[0] | (uint)(num6 << 2 & 255);
						PICkitFunctions.DeviceBuffers.ConfigWords[0] = PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[6];
						for (int m = 1; m < Constants.MR16F676FAM_CAL.Length; m++)
							PICkitFunctions.DeviceBuffers.ProgramMemory[m] = Constants.MR16F676FAM_CAL[m];
						if (!DeviceWrite())
						{
							PICkitFunctions.ResetBuffers();
							verifyOSCCALValue = true;
							UpdateGUI(true);
							MessageBox.Show("Regenerating OSCCAL Failed!\n\nUnable to program part.", "Regenerate OSCCAL");
							return;
						}
						PICkitFunctions.VddOn();
						for (int n = 0; n < 3; n++)
						{
							num7 = PICkitFunctions.MeasurePGDPulse();
							if (num7 < 695f && num7 > 10)
								break;
							if (n == 2)
							{
								ConditionalVddOff();
								EraseDeviceAll(false, new uint[0]);
								verifyOSCCALValue = true;
								MessageBox.Show("Regenerating OSCCAL Failed!\n\nUnable to connect to\ncalibration executive.", "Regenerate OSCCAL");
								UpdateGUI(true);
								return;
							}
						}
						ConditionalVddOff();
						float num9 = 404;
						if (l == 4)
							num9 = 406;
						float num10 = 396;
						if (l == 4)
							num9 = 394;
						if (num7 > num10 && num7 < num9)
						{
							PICkitFunctions.DeviceBuffers.OSCCAL = PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[7] | (PICkitFunctions.DeviceBuffers.ProgramMemory[0] & 255U);
							EraseDeviceAll(true, new uint[0]);
							verifyOSCCALValue = true;
							MessageBox.Show("Success!\n\nOSSCAL Regenerated and\nwritten to device.", "Regenerate OSCCAL");
							UpdateGUI(true);
							return;
						}
					}
				}
				EraseDeviceAll(false, new uint[0]);
				verifyOSCCALValue = true;
				UpdateGUI(true);
				MessageBox.Show("Regenerating OSCCAL Failed!\n\nUnable to calibrate.", "Regenerate OSCCAL");
			}
		}

		private void TimerInitalUpdate_Tick(object sender, EventArgs e)
		{
			timerInitalUpdate.Enabled = false;
			toolStripMenuItemShowProgramMemory.Checked = saveMultWinPMemOpen;
			multiWinPMemOpen = saveMultWinPMemOpen;
			if (multiWinPMemOpen)
				programMemMultiWin.Show();
			toolStripMenuItemShowEEPROMData.Checked = saveMultiWinEEMemOpen;
			multiWinEEMemOpen = saveMultiWinEEMemOpen;
			if (multiWinEEMemOpen && PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].EEMem > 0)
				eepromDataMultiWin.Show();
			Focus();
		}

		private void MainWindowAlwaysInFrontToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (mainWindowAlwaysInFrontToolStripMenuItem.Checked)
			{
				mainWinOwnsMem = true;
				AddOwnedForm(programMemMultiWin);
				AddOwnedForm(eepromDataMultiWin);
				return;
			}
			mainWinOwnsMem = false;
			RemoveOwnedForm(programMemMultiWin);
			RemoveOwnedForm(eepromDataMultiWin);
		}

		private bool UseProgExec33()
		{
			return (PICkitFunctions.FamilyIsdsPIC33F() || PICkitFunctions.FamilyIsPIC24H()) && PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgramMem >= 4096U && usePE33;
		}

		private bool UseProgExec24F()
		{
			return PICkitFunctions.FamilyIsPIC24FJ() && usePE24;
		}

		private void UpdateAlertSoundCheck()
		{
			toolStripMenuItemSounds.Checked = (playErrorWav || playSuccessWav || playWarningWav);
		}

		private void ToolStripMenuItemSounds_Click(object sender, EventArgs e)
		{
			DialogSounds dialogSounds = new DialogSounds();
			dialogSounds.ShowDialog();
			UpdateAlertSoundCheck();
		}

		private void As0BitValueToolStripMenuItem_Click(object sender, EventArgs e)
		{
			as0BitValueToolStripMenuItem.Checked = true;
			as1BitValueToolStripMenuItem.Checked = false;
			asReadOrImportedToolStripMenuItem.Checked = false;
			UpdateGUI(true);
		}

		private void As1BitValueToolStripMenuItem_Click(object sender, EventArgs e)
		{
			as0BitValueToolStripMenuItem.Checked = false;
			as1BitValueToolStripMenuItem.Checked = true;
			asReadOrImportedToolStripMenuItem.Checked = false;
			UpdateGUI(true);
		}

		private void AsReadOrImportedToolStripMenuItem_Click(object sender, EventArgs e)
		{
			as0BitValueToolStripMenuItem.Checked = false;
			as1BitValueToolStripMenuItem.Checked = false;
			asReadOrImportedToolStripMenuItem.Checked = true;
			UpdateGUI(true);
		}

		private void LabelConfig_Click(object sender, EventArgs e)
		{
			DialogConfigEdit dialogConfigEdit = new DialogConfigEdit();
			dialogConfigEdit.scalefactW = scalefactW;
			dialogConfigEdit.scalefactH = scalefactH;
			if (as0BitValueToolStripMenuItem.Checked)
				dialogConfigEdit.SetDisplayMask(0);
			else if (as1BitValueToolStripMenuItem.Checked)
				dialogConfigEdit.SetDisplayMask(1);
			else
				dialogConfigEdit.SetDisplayMask(2);
			dialogConfigEdit.ShowDialog();
			if (configsEdited)
			{
				displayDataSource.Text = "Edited.";
				checkImportFile = false;
				configsEdited = false;
			}
			UpdateGUI(true);
		}

		private void ToolStripMenuItemLVPEnabled_CheckedChanged(object sender, EventArgs e)
		{
			if (toolStripMenuItemLVPEnabled.Checked)
			{
				if (!toolStripMenuItemManualSelect.Checked)
				{
					string text = PICkitFunctions.DevFile.Scripts[PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].LVPScript - 1].ScriptName;
					text = text.Substring(text.Length - 2);
					if (text == "HV")
						MessageBox.Show("High Voltage Program entry may not be used\nwhen auto-detecting parts.\n\nSelect 'Programmer > Manual Device Select'\nto allow HVP to be used.", "Use HVP Program Entry");
					else
						MessageBox.Show("Low Voltage Program entry may not be used\nwhen auto-detecting parts.\n\nSelect 'Programmer > Manual Device Select'\nto allow LVP to be used.", "Use LVP Program Entry");
					toolStripMenuItemLVPEnabled.Checked = false;
				}
				else if (VppFirstToolStripMenuItem.Checked)
				{
					MessageBox.Show("'Use VPP First Program Entry' is enabled.\n\nLVP Program Entry may not be used while\nthat option is enabled.", "Use LVP Program Entry");
					toolStripMenuItemLVPEnabled.Checked = false;
				}
				else
				{
					PICkitFunctions.SetLVPProgramEntry();
					string text2 = PICkitFunctions.DevFile.Scripts[PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].LVPScript - 1].ScriptName;
					if (text2.Substring(text2.Length - 2) == "HV")
						displayStatusWindow.Text = "High Voltage Programming (HVP) entry set.";
					else
						displayStatusWindow.Text = "Low Voltage Programming (LVP) entry set.";
					text2 = text2.Substring(text2.Length - 3);
					if (text2 == "PGM")
					{
						Label label = displayStatusWindow;
						label.Text += "\nConnect PICkit 2 AUX to device PGM pin.";
					}
					PICkitFunctions.PrepNewPart(false);
				}
			}
			else
			{
				PICkitFunctions.ClearLVPProgramEntry();
				if (!VppFirstToolStripMenuItem.Checked)
					displayStatusWindow.Text = "Normal programming mode entry.";
				if (toolStripMenuItemManualSelect.Checked)
					PICkitFunctions.PrepNewPart(false);
			}
			UpdateGUI(false);
		}

        public static bool showWriteEraseVddDialog = true;
        public static bool continueWriteErase = false;
        public static bool setOSCCALValue = false;
        public static bool configsEdited = false;
        public static bool testMemoryOpen = false;
        public static bool testMemoryEnabled = false;
        public static int testMemoryWords = 64;
        public static ushort pk2number = 0;
        public static bool testMemoryImportExport = false;
        public static FormTestMemory formTestMem;
        public static string deviceFileName = "PK2DeviceFile.dat";
        public static float scalefactW = 1;
        public static float scalefactH = 1;
        public static string homeDirectory;
        public static byte slowSpeedICSP = 4;
        public static bool playSuccessWav = false;
        public static string successWavFile = "\\Sounds\\success.wav";
        public static bool playWarningWav = false;
        public static string warningWavFile = "\\Sounds\\warning.wav";
        public static bool playErrorWav = false;
        public static string errorWavFile = "\\Sounds\\error.wav";
        private static bool selfPoweredTarget;
        private static Constants.StatusColor statusWindowColor = Constants.StatusColor.normal;
        private readonly DialogVDDErase dialogVddErase = new DialogVDDErase();
        private DialogUserIDs dialogIDMemory;
        private Constants.VddTargetSelect vddTargetSave;
        private readonly DialogUART uartWindow = new DialogUART();
        private readonly DialogLogic logicWindow = new DialogLogic();
        private readonly FormMultiWinProgMem programMemMultiWin = new FormMultiWinProgMem();
        private readonly FormMultiWinEEData eepromDataMultiWin = new FormMultiWinEEData();
        private Point lastLoc = new Point(0, 0);
        private bool buttonLast = true;
        private bool checkImportFile;
        private bool oldFirmware;
        private bool bootLoad;
        private bool importGo;
        private bool allowDataEdits = true;
        private bool progMemJustEdited;
        private bool eeMemJustEdited;
        private bool searchOnStartup = true;
        private bool autoDetectInINI = true;
        private bool selectDeviceFile;
        private bool viewChanged;
        private bool multiWindow;
        private bool multiWinPMemOpen = true;
        private bool multiWinEEMemOpen = true;
        private readonly bool saveMultWinPMemOpen = true;
        private readonly bool saveMultiWinEEMemOpen = true;
        private bool verifyOSCCALValue = true;
        private bool mainWinOwnsMem = true;
        private bool usePE33 = true;
        private bool usePE24 = true;
        private bool useLVP;
        private bool deviceVerification = true;
        private byte ptgMemory;
        private string lastFamily = "Midrange";
        private string hex1 = "";
        private string hex2 = "";
        private string hex3 = "";
        private string hex4 = "";
        private readonly SoundPlayer wavPlayer = new SoundPlayer();
        public struct FLASHWINFO
        {
            public ushort cbSize;
            public IntPtr hwnd;
            public uint dwFlags;
            public ushort uCount;
            public uint dwTimeout;
        }
    }
}
