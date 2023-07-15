using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace PICkit2V3
{
	// Token: 0x02000006 RID: 6
	public partial class DialogUART : Form
	{
		// Token: 0x06000020 RID: 32 RVA: 0x00003B40 File Offset: 0x00002B40
		public DialogUART()
		{
			this.InitializeComponent();
			base.KeyPress += this.OnKeyPress;
			this.baudList = new DialogUART.baudTable[7];
			this.baudList[0].baudRate = "300";
			this.baudList[0].baudValue = 45554U;
			this.baudList[1].baudRate = "1200";
			this.baudList[1].baudValue = 60554U;
			this.baudList[2].baudRate = "2400";
			this.baudList[2].baudValue = 63054U;
			this.baudList[3].baudRate = "4800";
			this.baudList[3].baudValue = 64304U;
			this.baudList[4].baudRate = "9600";
			this.baudList[4].baudValue = 64929U;
			this.baudList[5].baudRate = "19200";
			this.baudList[5].baudValue = 65242U;
			this.baudList[6].baudRate = "38400";
			this.baudList[6].baudValue = 65398U;
			this.buildBaudList();
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00003CB8 File Offset: 0x00002CB8
		public string GetBaudRate()
		{
			return this.comboBoxBaud.SelectedItem.ToString();
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00003CCA File Offset: 0x00002CCA
		public bool IsHexMode()
		{
			return this.radioButtonHex.Checked;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00003CD7 File Offset: 0x00002CD7
		public string GetStringMacro(int macroNum)
		{
			if (macroNum == 2)
			{
				return this.textBoxString2.Text;
			}
			if (macroNum == 3)
			{
				return this.textBoxString3.Text;
			}
			if (macroNum == 4)
			{
				return this.textBoxString4.Text;
			}
			return this.textBoxString1.Text;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00003D14 File Offset: 0x00002D14
		public bool GetAppendCRLF()
		{
			return this.checkBoxCRLF.Checked;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00003D21 File Offset: 0x00002D21
		public bool GetWrap()
		{
			return this.checkBoxWrap.Checked;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00003D2E File Offset: 0x00002D2E
		public bool GetEcho()
		{
			return this.checkBoxEcho.Checked;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00003D3C File Offset: 0x00002D3C
		public void SetBaudRate(string baudRate)
		{
			for (int i = 0; i < this.baudList.Length; i++)
			{
				if (baudRate == this.comboBoxBaud.Items[i].ToString())
				{
					this.comboBoxBaud.SelectedIndex = i;
					return;
				}
				if (i + 1 == this.baudList.Length)
				{
					this.comboBoxBaud.Items.Add(baudRate);
					this.comboBoxBaud.SelectedIndex = i + 3;
				}
			}
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00003DB4 File Offset: 0x00002DB4
		public void SetStringMacro(string macro, int macroNum)
		{
			if (macroNum == 2)
			{
				this.textBoxString2.Text = macro;
				this.hex1Length = macro.Length;
				return;
			}
			if (macroNum == 3)
			{
				this.textBoxString3.Text = macro;
				this.hex2Length = macro.Length;
				return;
			}
			if (macroNum == 4)
			{
				this.textBoxString4.Text = macro;
				this.hex3Length = macro.Length;
				return;
			}
			this.textBoxString1.Text = macro;
			this.hex4Length = macro.Length;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00003E30 File Offset: 0x00002E30
		public void SetModeHex()
		{
			this.radioButtonHex.Checked = true;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00003E3E File Offset: 0x00002E3E
		public void ClearAppendCRLF()
		{
			this.checkBoxCRLF.Checked = false;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00003E4C File Offset: 0x00002E4C
		public void ClearWrap()
		{
			this.checkBoxWrap.Checked = false;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00003E5A File Offset: 0x00002E5A
		public void ClearEcho()
		{
			this.checkBoxEcho.Checked = false;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00003E68 File Offset: 0x00002E68
		public void SetVddBox(bool enable, bool check)
		{
			this.checkBoxVDD.Enabled = enable;
			this.checkBoxVDD.Checked = check;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00003E84 File Offset: 0x00002E84
		private void buildBaudList()
		{
			for (int i = 0; i < this.baudList.Length; i++)
			{
				this.comboBoxBaud.Items.Add(this.baudList[i].baudRate);
			}
			this.comboBoxBaud.Items.Add("Custom...");
			this.comboBoxBaud.SelectedIndex = 0;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00003EE8 File Offset: 0x00002EE8
		private void buttonExit_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00003EF0 File Offset: 0x00002EF0
		private void DialogUART_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (this.logFile != null)
			{
				this.closeLogFile();
			}
			this.timerPollForData.Enabled = false;
			PICkitFunctions.ExitUARTMode();
			this.radioButtonConnect.Checked = false;
			this.radioButtonDisconnect.Checked = true;
			this.comboBoxBaud.Enabled = true;
			this.buttonString1.Enabled = false;
			this.buttonString2.Enabled = false;
			this.buttonString3.Enabled = false;
			this.buttonString4.Enabled = false;
			this.panelVdd.Enabled = true;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00003F80 File Offset: 0x00002F80
		public void OnKeyPress(object sender, KeyPressEventArgs e)
		{
			string text = "0123456789ABCDEF";
			if (this.textBoxString1.ContainsFocus | this.textBoxString2.ContainsFocus | this.textBoxString3.ContainsFocus | this.textBoxString4.ContainsFocus)
			{
				return;
			}
			if (e.KeyChar == '\u0003' || e.KeyChar == '\u0018')
			{
				this.textBoxDisplay.Copy();
				return;
			}
			if (this.radioButtonDisconnect.Checked)
			{
				return;
			}
			this.textBoxDisplay.Focus();
			if (this.radioButtonHex.Checked)
			{
				string text2 = e.KeyChar.ToString();
				text2 = text2.ToUpper();
				if (text2.IndexOfAny(text.ToCharArray()) != 0)
				{
					this.labelTypeHex.Text = "Type Hex : ";
					this.labelTypeHex.Visible = false;
					return;
				}
				if (this.labelTypeHex.Visible)
				{
					string text3 = this.labelTypeHex.Text.Substring(11, 1) + text2;
					this.labelTypeHex.Text = "Type Hex : ";
					this.labelTypeHex.Visible = false;
					byte[] array = new byte[]
					{
						(byte)Utilities.Convert_Value_To_Int("0x" + text3)
					};
					text3 = "TX:  " + text3 + "\r\n";
					this.textBoxDisplay.AppendText(text3);
					this.textBoxDisplay.SelectionStart = this.textBoxDisplay.Text.Length;
					this.textBoxDisplay.ScrollToCaret();
					if (this.logFile != null)
					{
						this.logFile.Write(text3);
					}
					PICkitFunctions.DataDownload(array, 0, array.Length);
					return;
				}
				this.labelTypeHex.Text = "Type Hex : " + text2 + "_";
				this.labelTypeHex.Visible = true;
				return;
			}
			else
			{
				if (e.KeyChar == '\u0016')
				{
					this.textBoxDisplay.SelectionStart = this.textBoxDisplay.Text.Length;
					TextBox textBox = new TextBox();
					textBox.Multiline = true;
					textBox.Paste();
					do
					{
						int num = textBox.Text.Length;
						if (num > 60)
						{
							num = 60;
						}
						this.sendString(textBox.Text.Substring(0, num), false);
						textBox.Text = textBox.Text.Substring(num);
						float num2 = float.Parse(this.comboBoxBaud.SelectedItem.ToString());
						num2 = 1f / num2 * 12f * (float)num;
						num2 *= 1000f;
						Thread.Sleep((int)num2);
					}
					while (textBox.Text.Length > 0);
					textBox.Dispose();
					return;
				}
				string text4 = e.KeyChar.ToString();
				if (text4 == "\r")
				{
					text4 = "\r\n";
				}
				this.sendString(text4, false);
				return;
			}
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00004248 File Offset: 0x00003248
		private void radioButtonConnect_Click_1(object sender, EventArgs e)
		{
			if (!this.radioButtonConnect.Checked)
			{
				if (this.comboBoxBaud.SelectedIndex == 0)
				{
					MessageBox.Show("Please Select a Baud Rate.");
					return;
				}
				uint num = 0U;
				for (int i = 0; i < this.baudList.Length; i++)
				{
					if (this.comboBoxBaud.SelectedItem.ToString() == this.baudList[i].baudRate)
					{
						num = this.baudList[i].baudValue;
						break;
					}
					if (i + 1 == this.baudList.Length)
					{
						try
						{
							float num2 = float.Parse(this.comboBoxBaud.SelectedItem.ToString());
							num2 = (1f / num2 - 3E-06f) / 1.6667E-07f;
							num = 65536U - (uint)num2;
						}
						catch
						{
							MessageBox.Show("Error with Baud setting.");
							return;
						}
					}
				}
				this.panelVdd.Enabled = false;
				PICkitFunctions.EnterUARTMode(num);
				this.radioButtonConnect.Checked = true;
				this.radioButtonDisconnect.Checked = false;
				this.buttonString1.Enabled = true;
				this.buttonString2.Enabled = true;
				this.buttonString3.Enabled = true;
				this.buttonString4.Enabled = true;
				this.comboBoxBaud.Enabled = false;
				if (num < 60554U)
				{
					this.timerPollForData.Interval = 75;
				}
				else
				{
					this.timerPollForData.Interval = 15;
				}
				this.timerPollForData.Enabled = true;
			}
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000043D0 File Offset: 0x000033D0
		private void radioButtonDisconnect_Click(object sender, EventArgs e)
		{
			if (!this.radioButtonDisconnect.Checked)
			{
				this.radioButtonConnect.Checked = false;
				this.radioButtonDisconnect.Checked = true;
				PICkitFunctions.ExitUARTMode();
				this.comboBoxBaud.Enabled = true;
				this.timerPollForData.Enabled = false;
				this.buttonString1.Enabled = false;
				this.buttonString2.Enabled = false;
				this.buttonString3.Enabled = false;
				this.buttonString4.Enabled = false;
				this.panelVdd.Enabled = true;
				this.labelTypeHex.Text = "Type Hex : ";
				this.labelTypeHex.Visible = false;
			}
		}

		// Token: 0x06000034 RID: 52 RVA: 0x0000447B File Offset: 0x0000347B
		private void buttonClearScreen_Click(object sender, EventArgs e)
		{
			this.textBoxDisplay.Text = "";
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00004490 File Offset: 0x00003490
		private void timerPollForData_Tick(object sender, EventArgs e)
		{
			PICkitFunctions.UploadData();
			if (PICkitFunctions.Usb_read_array[1] > 0)
			{
				string text = "";
				if (this.radioButtonASCII.Checked)
				{
					text = Encoding.ASCII.GetString(PICkitFunctions.Usb_read_array, 2, (int)PICkitFunctions.Usb_read_array[1]);
				}
				else
				{
					if (this.newRX)
					{
						text = "RX:  ";
						this.newRX = false;
					}
					for (int i = 0; i < (int)PICkitFunctions.Usb_read_array[1]; i++)
					{
						text += string.Format("{0:X2} ", PICkitFunctions.Usb_read_array[i + 2]);
					}
				}
				if (this.logFile != null)
				{
					this.logFile.Write(text);
				}
				this.textBoxDisplay.AppendText(text);
				while (this.textBoxDisplay.Text.Length > 16400)
				{
					int num = this.textBoxDisplay.Text.IndexOf("\r\n") + 2;
					if (num == 1)
					{
						num = this.textBoxDisplay.Text.Length - 16000;
					}
					this.textBoxDisplay.Text = this.textBoxDisplay.Text.Substring(num);
				}
				this.textBoxDisplay.SelectionStart = this.textBoxDisplay.Text.Length;
				this.textBoxDisplay.ScrollToCaret();
				return;
			}
			if (!this.newRX && this.radioButtonHex.Checked)
			{
				this.textBoxDisplay.AppendText("\r\n");
				if (this.logFile != null)
				{
					this.logFile.Write("\r\n");
				}
				this.textBoxDisplay.SelectionStart = this.textBoxDisplay.Text.Length;
				this.textBoxDisplay.ScrollToCaret();
			}
			this.newRX = true;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x0000463C File Offset: 0x0000363C
		private int getLastLineLength(string text)
		{
			int num = text.LastIndexOf("\r\n") + 2;
			if (num < 2)
			{
				num = 0;
			}
			return text.Length - num;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00004668 File Offset: 0x00003668
		private void textBoxString1_TextChanged(object sender, EventArgs e)
		{
			if (this.textBoxString1.Text.Length > 60 && this.radioButtonASCII.Checked)
			{
				this.textBoxString1.Text = this.textBoxString1.Text.Substring(0, 60);
				this.textBoxString1.SelectionStart = 60;
			}
			if (this.radioButtonHex.Checked)
			{
				this.formatHexString(this.textBoxString1, ref this.hex1Length);
			}
		}

		// Token: 0x06000038 RID: 56 RVA: 0x000046E0 File Offset: 0x000036E0
		private void textBoxString2_TextChanged(object sender, EventArgs e)
		{
			if (this.textBoxString2.Text.Length > 60 && this.radioButtonASCII.Checked)
			{
				this.textBoxString2.Text = this.textBoxString2.Text.Substring(0, 60);
				this.textBoxString2.SelectionStart = 60;
			}
			if (this.radioButtonHex.Checked)
			{
				this.formatHexString(this.textBoxString2, ref this.hex2Length);
			}
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00004758 File Offset: 0x00003758
		private void textBoxString3_TextChanged(object sender, EventArgs e)
		{
			if (this.textBoxString3.Text.Length > 60 && this.radioButtonASCII.Checked)
			{
				this.textBoxString3.Text = this.textBoxString3.Text.Substring(0, 60);
				this.textBoxString3.SelectionStart = 60;
			}
			if (this.radioButtonHex.Checked)
			{
				this.formatHexString(this.textBoxString3, ref this.hex3Length);
			}
		}

		// Token: 0x0600003A RID: 58 RVA: 0x000047D0 File Offset: 0x000037D0
		private void textBoxString4_TextChanged(object sender, EventArgs e)
		{
			if (this.textBoxString4.Text.Length > 60 && this.radioButtonASCII.Checked)
			{
				this.textBoxString4.Text = this.textBoxString4.Text.Substring(0, 60);
				this.textBoxString4.SelectionStart = 60;
			}
			if (this.radioButtonHex.Checked)
			{
				this.formatHexString(this.textBoxString4, ref this.hex4Length);
			}
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00004848 File Offset: 0x00003848
		private void formatHexString(TextBox textBoxToFormat, ref int priorLength)
		{
			string text = textBoxToFormat.Text.ToUpper();
			text = text.Replace(" ", "");
			string text2 = "";
			for (int i = 0; i < text.Length; i++)
			{
				if (!char.IsNumber(text, i) && text[i] != 'A' && text[i] != 'B' && text[i] != 'C' && text[i] != 'D' && text[i] != 'E' && text[i] != 'F')
				{
					text2 += '0';
				}
				else
				{
					text2 += text[i];
				}
				if ((i + 1) % 2 == 0)
				{
					text2 += " ";
				}
			}
			if (text2.Length > 143)
			{
				text2 = text2.Substring(0, 143);
			}
			int num = textBoxToFormat.SelectionStart;
			if (num > 0 && num <= text2.Length && num < textBoxToFormat.Text.Length && textBoxToFormat.Text[num] == ' ' && text2[num - 1] == ' ')
			{
				num++;
			}
			else if (num >= textBoxToFormat.Text.Length && priorLength < textBoxToFormat.Text.Length)
			{
				num = text2.Length;
			}
			textBoxToFormat.Text = text2;
			textBoxToFormat.SelectionStart = num;
			priorLength = textBoxToFormat.Text.Length;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x000049AE File Offset: 0x000039AE
		private void buttonString1_Click(object sender, EventArgs e)
		{
			this.sendString(this.textBoxString1.Text, this.checkBoxCRLF.Checked);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x000049CC File Offset: 0x000039CC
		private void buttonString2_Click(object sender, EventArgs e)
		{
			this.sendString(this.textBoxString2.Text, this.checkBoxCRLF.Checked);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x000049EA File Offset: 0x000039EA
		private void buttonString3_Click(object sender, EventArgs e)
		{
			this.sendString(this.textBoxString3.Text, this.checkBoxCRLF.Checked);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00004A08 File Offset: 0x00003A08
		private void buttonString4_Click(object sender, EventArgs e)
		{
			this.sendString(this.textBoxString4.Text, this.checkBoxCRLF.Checked);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00004A28 File Offset: 0x00003A28
		private void sendString(string dataString, bool appendCRLF)
		{
			if (dataString.Length == 0)
			{
				return;
			}
			if (this.radioButtonASCII.Checked)
			{
				if (appendCRLF)
				{
					dataString += "\r\n";
				}
				if (this.checkBoxEcho.Checked)
				{
					this.textBoxDisplay.AppendText(dataString);
					this.textBoxDisplay.SelectionStart = this.textBoxDisplay.Text.Length;
					this.textBoxDisplay.ScrollToCaret();
				}
				if (this.logFile != null)
				{
					this.logFile.Write(dataString);
				}
				byte[] bytes = Encoding.Unicode.GetBytes(dataString);
				byte[] array = Encoding.Convert(Encoding.Unicode, Encoding.ASCII, bytes);
				PICkitFunctions.DataDownload(array, 0, array.Length);
				return;
			}
			int num;
			if (dataString.Length > 142)
			{
				num = 48;
			}
			else
			{
				num = dataString.Length / 3;
				dataString = dataString.Substring(0, num * 3);
			}
			byte[] array2 = new byte[num];
			for (int i = 0; i < num; i++)
			{
				array2[i] = (byte)Utilities.Convert_Value_To_Int("0x" + dataString.Substring(3 * i, 2));
			}
			dataString = "TX:  " + dataString + "\r\n";
			this.textBoxDisplay.AppendText(dataString);
			this.textBoxDisplay.SelectionStart = this.textBoxDisplay.Text.Length;
			this.textBoxDisplay.ScrollToCaret();
			if (this.logFile != null)
			{
				this.logFile.Write(dataString);
			}
			PICkitFunctions.DataDownload(array2, 0, array2.Length);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00004B9C File Offset: 0x00003B9C
		private void buttonLog_Click(object sender, EventArgs e)
		{
			if (this.logFile == null)
			{
				this.saveFileDialogLogFile.ShowDialog();
				return;
			}
			this.closeLogFile();
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00004BB9 File Offset: 0x00003BB9
		private void closeLogFile()
		{
			this.logFile.Close();
			this.logFile = null;
			this.buttonLog.Text = "Log to File";
			this.buttonLog.BackColor = SystemColors.ControlLight;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00004BED File Offset: 0x00003BED
		private void saveFileDialogLogFile_FileOk(object sender, CancelEventArgs e)
		{
			this.logFile = new StreamWriter(this.saveFileDialogLogFile.FileName);
			this.buttonLog.Text = "Logging Data...";
			this.buttonLog.BackColor = Color.Green;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00004C28 File Offset: 0x00003C28
		private void radioButtonASCII_CheckedChanged(object sender, EventArgs e)
		{
			if (this.radioButtonASCII.Checked)
			{
				this.checkBoxCRLF.Visible = true;
				this.checkBoxEcho.Enabled = true;
				this.labelTypeHex.Visible = false;
				this.labelTypeHex.Text = "Type Hex : ";
				this.labelMacros.Text = "String Macros:";
				this.textBoxString1.Text = this.convertHexSequenceToStringMacro(this.textBoxString1.Text);
				this.textBoxString2.Text = this.convertHexSequenceToStringMacro(this.textBoxString2.Text);
				this.textBoxString3.Text = this.convertHexSequenceToStringMacro(this.textBoxString3.Text);
				this.textBoxString4.Text = this.convertHexSequenceToStringMacro(this.textBoxString4.Text);
				if (this.textBoxDisplay.Text.Length > 0 && this.textBoxDisplay.Text[this.textBoxDisplay.Text.Length - 1] != '\n')
				{
					this.textBoxDisplay.AppendText("\r\n");
				}
			}
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00004D44 File Offset: 0x00003D44
		private void radioButtonHex_CheckedChanged(object sender, EventArgs e)
		{
			if (this.radioButtonHex.Checked)
			{
				this.checkBoxCRLF.Visible = false;
				this.checkBoxEcho.Enabled = false;
				this.labelTypeHex.Text = "Type Hex : ";
				this.labelTypeHex.Visible = false;
				this.labelMacros.Text = "Send Hex Sequences:";
				this.textBoxString1.Text = this.convertStringMacroToHexSequence(this.textBoxString1.Text);
				this.textBoxString2.Text = this.convertStringMacroToHexSequence(this.textBoxString2.Text);
				this.textBoxString3.Text = this.convertStringMacroToHexSequence(this.textBoxString3.Text);
				this.textBoxString4.Text = this.convertStringMacroToHexSequence(this.textBoxString4.Text);
				if (this.textBoxDisplay.Text.Length > 0 && this.textBoxDisplay.Text[this.textBoxDisplay.Text.Length - 1] != '\n')
				{
					this.textBoxDisplay.AppendText("\r\n");
				}
			}
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00004E60 File Offset: 0x00003E60
		private string convertHexSequenceToStringMacro(string hexSeq)
		{
			int num;
			if (hexSeq.Length > 142)
			{
				num = 48;
			}
			else
			{
				num = hexSeq.Length / 3;
			}
			byte[] array = new byte[num];
			for (int i = 0; i < num; i++)
			{
				array[i] = (byte)Utilities.Convert_Value_To_Int("0x" + hexSeq.Substring(3 * i, 2));
			}
			return Encoding.ASCII.GetString(array, 0, array.Length);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00004ECC File Offset: 0x00003ECC
		private string convertStringMacroToHexSequence(string stringMacro)
		{
			if (stringMacro.Length > 48)
			{
				stringMacro = stringMacro.Substring(0, 48);
			}
			byte[] bytes = Encoding.Unicode.GetBytes(stringMacro);
			byte[] array = Encoding.Convert(Encoding.Unicode, Encoding.ASCII, bytes);
			string text = "";
			for (int i = 0; i < array.Length; i++)
			{
				text += string.Format("{0:X2} ", array[i]);
			}
			return text;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00004F39 File Offset: 0x00003F39
		private void checkBoxWrap_CheckedChanged(object sender, EventArgs e)
		{
			this.textBoxDisplay.WordWrap = this.checkBoxWrap.Checked;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00004F54 File Offset: 0x00003F54
		private void comboBoxBaud_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.comboBoxBaud.SelectedItem.ToString() == "Custom...")
			{
				DialogCustomBaud dialogCustomBaud = new DialogCustomBaud();
				dialogCustomBaud.ShowDialog();
				if (DialogUART.CustomBaud == "")
				{
					this.comboBoxBaud.SelectedIndex = 0;
					return;
				}
				if (this.comboBoxBaud.Items.Count != this.comboBoxBaud.SelectedIndex + 1)
				{
					this.comboBoxBaud.Items.RemoveAt(this.comboBoxBaud.SelectedIndex + 1);
				}
				this.comboBoxBaud.Items.Add(DialogUART.CustomBaud);
				this.comboBoxBaud.SelectedIndex++;
			}
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00005010 File Offset: 0x00004010
		private void pictureBoxHelp_Click(object sender, EventArgs e)
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

		// Token: 0x0600004B RID: 75 RVA: 0x00005054 File Offset: 0x00004054
		private void checkBoxVDD_Click(object sender, EventArgs e)
		{
			this.VddCallback(true, this.checkBoxVDD.Checked);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x0000506D File Offset: 0x0000406D
		private void textBoxDisplay_Leave(object sender, EventArgs e)
		{
			this.labelTypeHex.Visible = false;
			this.labelTypeHex.Text = "Type Hex : ";
		}

		// Token: 0x04000018 RID: 24
		private const string CUSTOM_BAUD = "Custom...";

		// Token: 0x04000019 RID: 25
		private const int MaxLengthASCII = 60;

		// Token: 0x0400001A RID: 26
		private const int MaxHexLength = 143;

		// Token: 0x0400001B RID: 27
		public DelegateVddCallback VddCallback;

		// Token: 0x0400001C RID: 28
		public static string CustomBaud = "";

		// Token: 0x0400001D RID: 29
		private DialogUART.baudTable[] baudList;

		// Token: 0x0400001E RID: 30
		private StreamWriter logFile;

		// Token: 0x0400001F RID: 31
		private bool newRX = true;

		// Token: 0x04000020 RID: 32
		private int hex1Length;

		// Token: 0x04000021 RID: 33
		private int hex2Length;

		// Token: 0x04000022 RID: 34
		private int hex3Length;

		// Token: 0x04000023 RID: 35
		private int hex4Length;

		// Token: 0x02000007 RID: 7
		private struct baudTable
		{
			// Token: 0x04000047 RID: 71
			public string baudRate;

			// Token: 0x04000048 RID: 72
			public uint baudValue;
		}
	}
}
